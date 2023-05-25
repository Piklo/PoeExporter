using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="WorldPopupIconTypesDat"/> related data and helper methods.
/// </summary>
public sealed class WorldPopupIconTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<WorldPopupIconTypesDat> Items { get; }

    private Dictionary<string, List<WorldPopupIconTypesDat>>? byId;
    private Dictionary<string, List<WorldPopupIconTypesDat>>? byUnknown8;
    private Dictionary<int, List<WorldPopupIconTypesDat>>? byUnknown16;
    private Dictionary<int, List<WorldPopupIconTypesDat>>? byUnknown20;
    private Dictionary<bool, List<WorldPopupIconTypesDat>>? byUnknown24;
    private Dictionary<float, List<WorldPopupIconTypesDat>>? byUnknown25;
    private Dictionary<float, List<WorldPopupIconTypesDat>>? byUnknown29;
    private Dictionary<float, List<WorldPopupIconTypesDat>>? byUnknown33;
    private Dictionary<string, List<WorldPopupIconTypesDat>>? byUnknown37;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorldPopupIconTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal WorldPopupIconTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out WorldPopupIconTypesDat? item)
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
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<WorldPopupIconTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
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
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WorldPopupIconTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WorldPopupIconTypesDat>>();
        }

        var items = new List<ResultItem<string, WorldPopupIconTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WorldPopupIconTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(string? key, out WorldPopupIconTypesDat? item)
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
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(string? key, out IReadOnlyList<WorldPopupIconTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
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

        if (!byUnknown8.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WorldPopupIconTypesDat>> GetManyToManyByUnknown8(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WorldPopupIconTypesDat>>();
        }

        var items = new List<ResultItem<string, WorldPopupIconTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WorldPopupIconTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out WorldPopupIconTypesDat? item)
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
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<WorldPopupIconTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
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
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldPopupIconTypesDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldPopupIconTypesDat>>();
        }

        var items = new List<ResultItem<int, WorldPopupIconTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldPopupIconTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out WorldPopupIconTypesDat? item)
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
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<WorldPopupIconTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
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
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldPopupIconTypesDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldPopupIconTypesDat>>();
        }

        var items = new List<ResultItem<int, WorldPopupIconTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldPopupIconTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(bool? key, out WorldPopupIconTypesDat? item)
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
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(bool? key, out IReadOnlyList<WorldPopupIconTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
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
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldPopupIconTypesDat>> GetManyToManyByUnknown24(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldPopupIconTypesDat>>();
        }

        var items = new List<ResultItem<bool, WorldPopupIconTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldPopupIconTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown25(float? key, out WorldPopupIconTypesDat? item)
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
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown25(float? key, out IReadOnlyList<WorldPopupIconTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
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
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.byUnknown25"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, WorldPopupIconTypesDat>> GetManyToManyByUnknown25(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, WorldPopupIconTypesDat>>();
        }

        var items = new List<ResultItem<float, WorldPopupIconTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown25(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, WorldPopupIconTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown29(float? key, out WorldPopupIconTypesDat? item)
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
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown29(float? key, out IReadOnlyList<WorldPopupIconTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        if (byUnknown29 is null)
        {
            byUnknown29 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown29;

                if (!byUnknown29.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown29.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown29.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.byUnknown29"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, WorldPopupIconTypesDat>> GetManyToManyByUnknown29(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, WorldPopupIconTypesDat>>();
        }

        var items = new List<ResultItem<float, WorldPopupIconTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown29(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, WorldPopupIconTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown33(float? key, out WorldPopupIconTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown33(key, out var items))
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
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown33(float? key, out IReadOnlyList<WorldPopupIconTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        if (byUnknown33 is null)
        {
            byUnknown33 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown33;

                if (!byUnknown33.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown33.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown33.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.byUnknown33"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, WorldPopupIconTypesDat>> GetManyToManyByUnknown33(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, WorldPopupIconTypesDat>>();
        }

        var items = new List<ResultItem<float, WorldPopupIconTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown33(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, WorldPopupIconTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown37(string? key, out WorldPopupIconTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown37(key, out var items))
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
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown37(string? key, out IReadOnlyList<WorldPopupIconTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        if (byUnknown37 is null)
        {
            byUnknown37 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown37;

                if (!byUnknown37.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown37.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown37.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WorldPopupIconTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldPopupIconTypesDat"/> with <see cref="WorldPopupIconTypesDat.byUnknown37"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WorldPopupIconTypesDat>> GetManyToManyByUnknown37(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WorldPopupIconTypesDat>>();
        }

        var items = new List<ResultItem<string, WorldPopupIconTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown37(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WorldPopupIconTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private WorldPopupIconTypesDat[] Load()
    {
        const string filePath = "Data/WorldPopupIconTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WorldPopupIconTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WorldPopupIconTypesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown25 = unknown25Loading,
                Unknown29 = unknown29Loading,
                Unknown33 = unknown33Loading,
                Unknown37 = unknown37Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
