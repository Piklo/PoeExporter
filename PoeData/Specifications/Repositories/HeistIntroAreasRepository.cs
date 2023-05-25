using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistIntroAreasDat"/> related data and helper methods.
/// </summary>
public sealed class HeistIntroAreasRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistIntroAreasDat> Items { get; }

    private Dictionary<string, List<HeistIntroAreasDat>>? byId;
    private Dictionary<int, List<HeistIntroAreasDat>>? byHeistAreasKey;
    private Dictionary<int, List<HeistIntroAreasDat>>? byUnknown24;
    private Dictionary<int, List<HeistIntroAreasDat>>? byUnknown28;
    private Dictionary<string, List<HeistIntroAreasDat>>? byDGRFile;
    private Dictionary<int, List<HeistIntroAreasDat>>? byUnknown40;
    private Dictionary<int, List<HeistIntroAreasDat>>? byUnknown44;
    private Dictionary<bool, List<HeistIntroAreasDat>>? byUnknown48;
    private Dictionary<int, List<HeistIntroAreasDat>>? byUnknown49;
    private Dictionary<int, List<HeistIntroAreasDat>>? byUnknown53;
    private Dictionary<int, List<HeistIntroAreasDat>>? byUnknown57;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistIntroAreasRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistIntroAreasRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HeistIntroAreasDat? item)
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
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
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistIntroAreasDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<string, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.HeistAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistAreasKey(int? key, out HeistIntroAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistAreasKey(key, out var items))
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.HeistAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistAreasKey(int? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        if (byHeistAreasKey is null)
        {
            byHeistAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byHeistAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistIntroAreasDat>> GetManyToManyByHeistAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out HeistIntroAreasDat? item)
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
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
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistIntroAreasDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out HeistIntroAreasDat? item)
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
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
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistIntroAreasDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.DGRFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDGRFile(string? key, out HeistIntroAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDGRFile(key, out var items))
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.DGRFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDGRFile(string? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        if (byDGRFile is null)
        {
            byDGRFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.DGRFile;

                if (!byDGRFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDGRFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDGRFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byDGRFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistIntroAreasDat>> GetManyToManyByDGRFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<string, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDGRFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out HeistIntroAreasDat? item)
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
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
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistIntroAreasDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out HeistIntroAreasDat? item)
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
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
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistIntroAreasDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(bool? key, out HeistIntroAreasDat? item)
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(bool? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
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
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistIntroAreasDat>> GetManyToManyByUnknown48(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<bool, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown49(int? key, out HeistIntroAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown49(key, out var items))
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown49(int? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        if (byUnknown49 is null)
        {
            byUnknown49 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown49;

                if (!byUnknown49.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown49.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown49.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byUnknown49"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistIntroAreasDat>> GetManyToManyByUnknown49(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown49(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown53"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown53(int? key, out HeistIntroAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown53(key, out var items))
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown53"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown53(int? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        if (byUnknown53 is null)
        {
            byUnknown53 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown53;

                if (!byUnknown53.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown53.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown53.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byUnknown53"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistIntroAreasDat>> GetManyToManyByUnknown53(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown53(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown57(int? key, out HeistIntroAreasDat? item)
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
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown57(int? key, out IReadOnlyList<HeistIntroAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistIntroAreasDat>();
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
            items = Array.Empty<HeistIntroAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistIntroAreasDat"/> with <see cref="HeistIntroAreasDat.byUnknown57"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistIntroAreasDat>> GetManyToManyByUnknown57(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistIntroAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistIntroAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown57(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistIntroAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistIntroAreasDat[] Load()
    {
        const string filePath = "Data/HeistIntroAreas.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistIntroAreasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeistAreasKey
            (var heistareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DGRFile
            (var dgrfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown53
            (var unknown53Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown57
            (var unknown57Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistIntroAreasDat()
            {
                Id = idLoading,
                HeistAreasKey = heistareaskeyLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                DGRFile = dgrfileLoading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown49 = unknown49Loading,
                Unknown53 = unknown53Loading,
                Unknown57 = unknown57Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
