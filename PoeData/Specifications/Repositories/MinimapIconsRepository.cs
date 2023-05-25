using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MinimapIconsDat"/> related data and helper methods.
/// </summary>
public sealed class MinimapIconsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MinimapIconsDat> Items { get; }

    private Dictionary<string, List<MinimapIconsDat>>? byId;
    private Dictionary<int, List<MinimapIconsDat>>? byMinimapIconRadius;
    private Dictionary<int, List<MinimapIconsDat>>? byLargemapIconRadius;
    private Dictionary<bool, List<MinimapIconsDat>>? byUnknown16;
    private Dictionary<bool, List<MinimapIconsDat>>? byUnknown17;
    private Dictionary<bool, List<MinimapIconsDat>>? byUnknown18;
    private Dictionary<int, List<MinimapIconsDat>>? byMinimapIconPointerMaxDistance;
    private Dictionary<int, List<MinimapIconsDat>>? byUnknown23;

    /// <summary>
    /// Initializes a new instance of the <see cref="MinimapIconsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MinimapIconsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MinimapIconsDat? item)
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
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MinimapIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MinimapIconsDat>();
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
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MinimapIconsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MinimapIconsDat>>();
        }

        var items = new List<ResultItem<string, MinimapIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MinimapIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.MinimapIconRadius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinimapIconRadius(int? key, out MinimapIconsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinimapIconRadius(key, out var items))
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
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.MinimapIconRadius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinimapIconRadius(int? key, out IReadOnlyList<MinimapIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        if (byMinimapIconRadius is null)
        {
            byMinimapIconRadius = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinimapIconRadius;

                if (!byMinimapIconRadius.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinimapIconRadius.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinimapIconRadius.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.byMinimapIconRadius"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MinimapIconsDat>> GetManyToManyByMinimapIconRadius(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MinimapIconsDat>>();
        }

        var items = new List<ResultItem<int, MinimapIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinimapIconRadius(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MinimapIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.LargemapIconRadius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLargemapIconRadius(int? key, out MinimapIconsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLargemapIconRadius(key, out var items))
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
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.LargemapIconRadius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLargemapIconRadius(int? key, out IReadOnlyList<MinimapIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        if (byLargemapIconRadius is null)
        {
            byLargemapIconRadius = new();
            foreach (var item in Items)
            {
                var itemKey = item.LargemapIconRadius;

                if (!byLargemapIconRadius.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLargemapIconRadius.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLargemapIconRadius.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.byLargemapIconRadius"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MinimapIconsDat>> GetManyToManyByLargemapIconRadius(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MinimapIconsDat>>();
        }

        var items = new List<ResultItem<int, MinimapIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLargemapIconRadius(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MinimapIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(bool? key, out MinimapIconsDat? item)
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
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(bool? key, out IReadOnlyList<MinimapIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MinimapIconsDat>();
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
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MinimapIconsDat>> GetManyToManyByUnknown16(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MinimapIconsDat>>();
        }

        var items = new List<ResultItem<bool, MinimapIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MinimapIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.Unknown17"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown17(bool? key, out MinimapIconsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown17(key, out var items))
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
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.Unknown17"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown17(bool? key, out IReadOnlyList<MinimapIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        if (byUnknown17 is null)
        {
            byUnknown17 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown17;

                if (!byUnknown17.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown17.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown17.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.byUnknown17"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MinimapIconsDat>> GetManyToManyByUnknown17(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MinimapIconsDat>>();
        }

        var items = new List<ResultItem<bool, MinimapIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown17(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MinimapIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.Unknown18"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown18(bool? key, out MinimapIconsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown18(key, out var items))
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
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.Unknown18"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown18(bool? key, out IReadOnlyList<MinimapIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        if (byUnknown18 is null)
        {
            byUnknown18 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown18;

                if (!byUnknown18.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown18.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown18.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.byUnknown18"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MinimapIconsDat>> GetManyToManyByUnknown18(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MinimapIconsDat>>();
        }

        var items = new List<ResultItem<bool, MinimapIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown18(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MinimapIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.MinimapIconPointerMaxDistance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinimapIconPointerMaxDistance(int? key, out MinimapIconsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinimapIconPointerMaxDistance(key, out var items))
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
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.MinimapIconPointerMaxDistance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinimapIconPointerMaxDistance(int? key, out IReadOnlyList<MinimapIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        if (byMinimapIconPointerMaxDistance is null)
        {
            byMinimapIconPointerMaxDistance = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinimapIconPointerMaxDistance;

                if (!byMinimapIconPointerMaxDistance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinimapIconPointerMaxDistance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinimapIconPointerMaxDistance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.byMinimapIconPointerMaxDistance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MinimapIconsDat>> GetManyToManyByMinimapIconPointerMaxDistance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MinimapIconsDat>>();
        }

        var items = new List<ResultItem<int, MinimapIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinimapIconPointerMaxDistance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MinimapIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.Unknown23"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown23(int? key, out MinimapIconsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown23(key, out var items))
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
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.Unknown23"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown23(int? key, out IReadOnlyList<MinimapIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        if (byUnknown23 is null)
        {
            byUnknown23 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown23;

                if (!byUnknown23.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown23.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown23.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MinimapIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MinimapIconsDat"/> with <see cref="MinimapIconsDat.byUnknown23"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MinimapIconsDat>> GetManyToManyByUnknown23(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MinimapIconsDat>>();
        }

        var items = new List<ResultItem<int, MinimapIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown23(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MinimapIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MinimapIconsDat[] Load()
    {
        const string filePath = "Data/MinimapIcons.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MinimapIconsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinimapIconRadius
            (var minimapiconradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LargemapIconRadius
            (var largemapiconradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown17
            (var unknown17Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown18
            (var unknown18Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MinimapIconPointerMaxDistance
            (var minimapiconpointermaxdistanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown23
            (var unknown23Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MinimapIconsDat()
            {
                Id = idLoading,
                MinimapIconRadius = minimapiconradiusLoading,
                LargemapIconRadius = largemapiconradiusLoading,
                Unknown16 = unknown16Loading,
                Unknown17 = unknown17Loading,
                Unknown18 = unknown18Loading,
                MinimapIconPointerMaxDistance = minimapiconpointermaxdistanceLoading,
                Unknown23 = unknown23Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
