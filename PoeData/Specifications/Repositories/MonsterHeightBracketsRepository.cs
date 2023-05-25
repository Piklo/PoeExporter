using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterHeightBracketsDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterHeightBracketsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterHeightBracketsDat> Items { get; }

    private Dictionary<string, List<MonsterHeightBracketsDat>>? byId;
    private Dictionary<int, List<MonsterHeightBracketsDat>>? byUnknown8;
    private Dictionary<int, List<MonsterHeightBracketsDat>>? byBuffVisuals1;
    private Dictionary<int, List<MonsterHeightBracketsDat>>? byBuffVisuals2;
    private Dictionary<int, List<MonsterHeightBracketsDat>>? byUnknown44;
    private Dictionary<float, List<MonsterHeightBracketsDat>>? byUnknown48;
    private Dictionary<float, List<MonsterHeightBracketsDat>>? byUnknown52;
    private Dictionary<int, List<MonsterHeightBracketsDat>>? byTag;
    private Dictionary<int, List<MonsterHeightBracketsDat>>? byUnknown72;
    private Dictionary<float, List<MonsterHeightBracketsDat>>? byUnknown76;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterHeightBracketsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterHeightBracketsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MonsterHeightBracketsDat? item)
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
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MonsterHeightBracketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
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
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterHeightBracketsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterHeightBracketsDat>>();
        }

        var items = new List<ResultItem<string, MonsterHeightBracketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterHeightBracketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out MonsterHeightBracketsDat? item)
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
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<MonsterHeightBracketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
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
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterHeightBracketsDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterHeightBracketsDat>>();
        }

        var items = new List<ResultItem<int, MonsterHeightBracketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterHeightBracketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.BuffVisuals1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisuals1(int? key, out MonsterHeightBracketsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffVisuals1(key, out var items))
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
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.BuffVisuals1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisuals1(int? key, out IReadOnlyList<MonsterHeightBracketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        if (byBuffVisuals1 is null)
        {
            byBuffVisuals1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffVisuals1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffVisuals1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffVisuals1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffVisuals1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.byBuffVisuals1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterHeightBracketsDat>> GetManyToManyByBuffVisuals1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterHeightBracketsDat>>();
        }

        var items = new List<ResultItem<int, MonsterHeightBracketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisuals1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterHeightBracketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.BuffVisuals2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisuals2(int? key, out MonsterHeightBracketsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffVisuals2(key, out var items))
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
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.BuffVisuals2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisuals2(int? key, out IReadOnlyList<MonsterHeightBracketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        if (byBuffVisuals2 is null)
        {
            byBuffVisuals2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffVisuals2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffVisuals2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffVisuals2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffVisuals2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.byBuffVisuals2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterHeightBracketsDat>> GetManyToManyByBuffVisuals2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterHeightBracketsDat>>();
        }

        var items = new List<ResultItem<int, MonsterHeightBracketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisuals2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterHeightBracketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out MonsterHeightBracketsDat? item)
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
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<MonsterHeightBracketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
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
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterHeightBracketsDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterHeightBracketsDat>>();
        }

        var items = new List<ResultItem<int, MonsterHeightBracketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterHeightBracketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(float? key, out MonsterHeightBracketsDat? item)
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
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(float? key, out IReadOnlyList<MonsterHeightBracketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
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
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, MonsterHeightBracketsDat>> GetManyToManyByUnknown48(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, MonsterHeightBracketsDat>>();
        }

        var items = new List<ResultItem<float, MonsterHeightBracketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, MonsterHeightBracketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(float? key, out MonsterHeightBracketsDat? item)
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
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(float? key, out IReadOnlyList<MonsterHeightBracketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
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
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, MonsterHeightBracketsDat>> GetManyToManyByUnknown52(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, MonsterHeightBracketsDat>>();
        }

        var items = new List<ResultItem<float, MonsterHeightBracketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, MonsterHeightBracketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Tag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTag(int? key, out MonsterHeightBracketsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTag(key, out var items))
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
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Tag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTag(int? key, out IReadOnlyList<MonsterHeightBracketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        if (byTag is null)
        {
            byTag = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tag;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTag.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTag.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTag.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.byTag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterHeightBracketsDat>> GetManyToManyByTag(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterHeightBracketsDat>>();
        }

        var items = new List<ResultItem<int, MonsterHeightBracketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterHeightBracketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out MonsterHeightBracketsDat? item)
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
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<MonsterHeightBracketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
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
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterHeightBracketsDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterHeightBracketsDat>>();
        }

        var items = new List<ResultItem<int, MonsterHeightBracketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterHeightBracketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(float? key, out MonsterHeightBracketsDat? item)
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
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(float? key, out IReadOnlyList<MonsterHeightBracketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightBracketsDat>();
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
            items = Array.Empty<MonsterHeightBracketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightBracketsDat"/> with <see cref="MonsterHeightBracketsDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, MonsterHeightBracketsDat>> GetManyToManyByUnknown76(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, MonsterHeightBracketsDat>>();
        }

        var items = new List<ResultItem<float, MonsterHeightBracketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, MonsterHeightBracketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterHeightBracketsDat[] Load()
    {
        const string filePath = "Data/MonsterHeightBrackets.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterHeightBracketsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffVisuals1
            (var buffvisuals1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BuffVisuals2
            (var buffvisuals2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Tag
            (var tagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterHeightBracketsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                BuffVisuals1 = buffvisuals1Loading,
                BuffVisuals2 = buffvisuals2Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Tag = tagLoading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
