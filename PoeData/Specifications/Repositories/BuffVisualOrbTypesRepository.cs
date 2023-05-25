using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BuffVisualOrbTypesDat"/> related data and helper methods.
/// </summary>
public sealed class BuffVisualOrbTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BuffVisualOrbTypesDat> Items { get; }

    private Dictionary<string, List<BuffVisualOrbTypesDat>>? byId;
    private Dictionary<float, List<BuffVisualOrbTypesDat>>? byUnknown8;
    private Dictionary<float, List<BuffVisualOrbTypesDat>>? byUnknown12;
    private Dictionary<float, List<BuffVisualOrbTypesDat>>? byUnknown16;
    private Dictionary<float, List<BuffVisualOrbTypesDat>>? byUnknown20;
    private Dictionary<int, List<BuffVisualOrbTypesDat>>? byUnknown24;
    private Dictionary<bool, List<BuffVisualOrbTypesDat>>? byUnknown28;
    private Dictionary<int, List<BuffVisualOrbTypesDat>>? byUnknown29;
    private Dictionary<int, List<BuffVisualOrbTypesDat>>? byUnknown45;
    private Dictionary<int, List<BuffVisualOrbTypesDat>>? byUnknown61;
    private Dictionary<bool, List<BuffVisualOrbTypesDat>>? byUnknown65;
    private Dictionary<float, List<BuffVisualOrbTypesDat>>? byUnknown66;
    private Dictionary<int, List<BuffVisualOrbTypesDat>>? byUnknown70;
    private Dictionary<float, List<BuffVisualOrbTypesDat>>? byUnknown74;
    private Dictionary<bool, List<BuffVisualOrbTypesDat>>? byUnknown78;
    private Dictionary<float, List<BuffVisualOrbTypesDat>>? byUnknown79;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuffVisualOrbTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BuffVisualOrbTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BuffVisualOrbTypesDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
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
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualOrbTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(float? key, out BuffVisualOrbTypesDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(float? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
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
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, BuffVisualOrbTypesDat>> GetManyToManyByUnknown8(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<float, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(float? key, out BuffVisualOrbTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown12(key, out var items))
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(float? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;

                if (!byUnknown12.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown12.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, BuffVisualOrbTypesDat>> GetManyToManyByUnknown12(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<float, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(float? key, out BuffVisualOrbTypesDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(float? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
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
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, BuffVisualOrbTypesDat>> GetManyToManyByUnknown16(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<float, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(float? key, out BuffVisualOrbTypesDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(float? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
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
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, BuffVisualOrbTypesDat>> GetManyToManyByUnknown20(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<float, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out BuffVisualOrbTypesDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;

                if (!byUnknown24.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualOrbTypesDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(bool? key, out BuffVisualOrbTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(bool? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffVisualOrbTypesDat>> GetManyToManyByUnknown28(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<bool, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown29(int? key, out BuffVisualOrbTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown29(key, out var items))
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown29(int? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        if (byUnknown29 is null)
        {
            byUnknown29 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown29;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown29.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown29.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown29.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown29"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualOrbTypesDat>> GetManyToManyByUnknown29(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown29(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown45(int? key, out BuffVisualOrbTypesDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown45(int? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        if (byUnknown45 is null)
        {
            byUnknown45 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown45;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown45.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown45.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown45.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown45"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualOrbTypesDat>> GetManyToManyByUnknown45(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown45(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(int? key, out BuffVisualOrbTypesDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(int? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
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
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualOrbTypesDat>> GetManyToManyByUnknown61(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown65(bool? key, out BuffVisualOrbTypesDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown65(bool? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
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
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown65"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffVisualOrbTypesDat>> GetManyToManyByUnknown65(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<bool, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown65(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown66(float? key, out BuffVisualOrbTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown66(key, out var items))
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown66(float? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        if (byUnknown66 is null)
        {
            byUnknown66 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown66;

                if (!byUnknown66.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown66.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown66.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown66"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, BuffVisualOrbTypesDat>> GetManyToManyByUnknown66(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<float, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown66(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown70(int? key, out BuffVisualOrbTypesDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown70(int? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
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
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown70"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualOrbTypesDat>> GetManyToManyByUnknown70(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown70(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown74(float? key, out BuffVisualOrbTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown74(key, out var items))
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown74(float? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        if (byUnknown74 is null)
        {
            byUnknown74 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown74;

                if (!byUnknown74.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown74.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown74.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown74"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, BuffVisualOrbTypesDat>> GetManyToManyByUnknown74(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<float, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown74(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown78(bool? key, out BuffVisualOrbTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown78(key, out var items))
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown78(bool? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        if (byUnknown78 is null)
        {
            byUnknown78 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown78;

                if (!byUnknown78.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown78.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown78.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown78"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffVisualOrbTypesDat>> GetManyToManyByUnknown78(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<bool, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown78(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown79(float? key, out BuffVisualOrbTypesDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown79(float? key, out IReadOnlyList<BuffVisualOrbTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbTypesDat>();
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
            items = Array.Empty<BuffVisualOrbTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbTypesDat"/> with <see cref="BuffVisualOrbTypesDat.byUnknown79"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, BuffVisualOrbTypesDat>> GetManyToManyByUnknown79(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, BuffVisualOrbTypesDat>>();
        }

        var items = new List<ResultItem<float, BuffVisualOrbTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown79(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, BuffVisualOrbTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BuffVisualOrbTypesDat[] Load()
    {
        const string filePath = "Data/BuffVisualOrbTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BuffVisualOrbTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BuffVisualOrbTypesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown29 = unknown29Loading,
                Unknown45 = unknown45Loading,
                Unknown61 = unknown61Loading,
                Unknown65 = unknown65Loading,
                Unknown66 = unknown66Loading,
                Unknown70 = unknown70Loading,
                Unknown74 = unknown74Loading,
                Unknown78 = unknown78Loading,
                Unknown79 = unknown79Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
