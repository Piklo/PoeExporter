using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UniqueStashTypesDat"/> related data and helper methods.
/// </summary>
public sealed class UniqueStashTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UniqueStashTypesDat> Items { get; }

    private Dictionary<string, List<UniqueStashTypesDat>>? byId;
    private Dictionary<int, List<UniqueStashTypesDat>>? byOrder;
    private Dictionary<int, List<UniqueStashTypesDat>>? byWidth;
    private Dictionary<int, List<UniqueStashTypesDat>>? byHeight;
    private Dictionary<int, List<UniqueStashTypesDat>>? byUnknown20;
    private Dictionary<int, List<UniqueStashTypesDat>>? byUnknown24;
    private Dictionary<string, List<UniqueStashTypesDat>>? byName;
    private Dictionary<int, List<UniqueStashTypesDat>>? byStandardCount;
    private Dictionary<string, List<UniqueStashTypesDat>>? byImage;
    private Dictionary<int, List<UniqueStashTypesDat>>? byChallengeLeagueCount;
    private Dictionary<bool, List<UniqueStashTypesDat>>? byIsDisabled;

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueStashTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UniqueStashTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out UniqueStashTypesDat? item)
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
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
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UniqueStashTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<string, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Order"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOrder(int? key, out UniqueStashTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOrder(key, out var items))
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Order"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOrder(int? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        if (byOrder is null)
        {
            byOrder = new();
            foreach (var item in Items)
            {
                var itemKey = item.Order;

                if (!byOrder.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOrder.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOrder.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byOrder"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashTypesDat>> GetManyToManyByOrder(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOrder(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Width"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWidth(int? key, out UniqueStashTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWidth(key, out var items))
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Width"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWidth(int? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        if (byWidth is null)
        {
            byWidth = new();
            foreach (var item in Items)
            {
                var itemKey = item.Width;

                if (!byWidth.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWidth.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWidth.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byWidth"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashTypesDat>> GetManyToManyByWidth(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWidth(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Height"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeight(int? key, out UniqueStashTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeight(key, out var items))
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Height"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeight(int? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        if (byHeight is null)
        {
            byHeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.Height;

                if (!byHeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byHeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashTypesDat>> GetManyToManyByHeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out UniqueStashTypesDat? item)
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
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
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashTypesDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out UniqueStashTypesDat? item)
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
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
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashTypesDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out UniqueStashTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByName(key, out var items))
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;

                if (!byName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UniqueStashTypesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<string, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.StandardCount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStandardCount(int? key, out UniqueStashTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStandardCount(key, out var items))
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.StandardCount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStandardCount(int? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        if (byStandardCount is null)
        {
            byStandardCount = new();
            foreach (var item in Items)
            {
                var itemKey = item.StandardCount;

                if (!byStandardCount.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStandardCount.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStandardCount.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byStandardCount"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashTypesDat>> GetManyToManyByStandardCount(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStandardCount(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Image"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImage(string? key, out UniqueStashTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByImage(key, out var items))
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.Image"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImage(string? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        if (byImage is null)
        {
            byImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.Image;

                if (!byImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UniqueStashTypesDat>> GetManyToManyByImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<string, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.ChallengeLeagueCount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChallengeLeagueCount(int? key, out UniqueStashTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChallengeLeagueCount(key, out var items))
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.ChallengeLeagueCount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChallengeLeagueCount(int? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        if (byChallengeLeagueCount is null)
        {
            byChallengeLeagueCount = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChallengeLeagueCount;

                if (!byChallengeLeagueCount.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChallengeLeagueCount.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChallengeLeagueCount.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byChallengeLeagueCount"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashTypesDat>> GetManyToManyByChallengeLeagueCount(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChallengeLeagueCount(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.IsDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsDisabled(bool? key, out UniqueStashTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsDisabled(key, out var items))
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
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.IsDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsDisabled(bool? key, out IReadOnlyList<UniqueStashTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        if (byIsDisabled is null)
        {
            byIsDisabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsDisabled;

                if (!byIsDisabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsDisabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsDisabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashTypesDat"/> with <see cref="UniqueStashTypesDat.byIsDisabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UniqueStashTypesDat>> GetManyToManyByIsDisabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UniqueStashTypesDat>>();
        }

        var items = new List<ResultItem<bool, UniqueStashTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsDisabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UniqueStashTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UniqueStashTypesDat[] Load()
    {
        const string filePath = "Data/UniqueStashTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueStashTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Order
            (var orderLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Width
            (var widthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Height
            (var heightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StandardCount
            (var standardcountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Image
            (var imageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ChallengeLeagueCount
            (var challengeleaguecountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsDisabled
            (var isdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueStashTypesDat()
            {
                Id = idLoading,
                Order = orderLoading,
                Width = widthLoading,
                Height = heightLoading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Name = nameLoading,
                StandardCount = standardcountLoading,
                Image = imageLoading,
                ChallengeLeagueCount = challengeleaguecountLoading,
                IsDisabled = isdisabledLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
