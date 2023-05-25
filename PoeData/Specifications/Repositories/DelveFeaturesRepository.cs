using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DelveFeaturesDat"/> related data and helper methods.
/// </summary>
public sealed class DelveFeaturesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DelveFeaturesDat> Items { get; }

    private Dictionary<string, List<DelveFeaturesDat>>? byId;
    private Dictionary<string, List<DelveFeaturesDat>>? byName;
    private Dictionary<int, List<DelveFeaturesDat>>? bySpawnWeight;
    private Dictionary<int, List<DelveFeaturesDat>>? byWorldAreasKey;
    private Dictionary<string, List<DelveFeaturesDat>>? byImage;
    private Dictionary<int, List<DelveFeaturesDat>>? byAchievementItemsKeys;
    private Dictionary<int, List<DelveFeaturesDat>>? byUnknown72;
    private Dictionary<int, List<DelveFeaturesDat>>? byMinDepth;
    private Dictionary<string, List<DelveFeaturesDat>>? byDescription;
    private Dictionary<int, List<DelveFeaturesDat>>? byUnknown112;
    private Dictionary<int, List<DelveFeaturesDat>>? byUnknown116;
    private Dictionary<int, List<DelveFeaturesDat>>? byUnknown132;
    private Dictionary<int, List<DelveFeaturesDat>>? byUnknown148;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveFeaturesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DelveFeaturesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out DelveFeaturesDat? item)
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
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
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DelveFeaturesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<string, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out DelveFeaturesDat? item)
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
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
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DelveFeaturesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<string, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out DelveFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight(key, out var items))
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        if (bySpawnWeight is null)
        {
            bySpawnWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight;
                foreach (var listKey in itemKey)
                {
                    if (!bySpawnWeight.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpawnWeight.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpawnWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveFeaturesDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<int, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out DelveFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        if (byWorldAreasKey is null)
        {
            byWorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveFeaturesDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<int, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Image"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImage(string? key, out DelveFeaturesDat? item)
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Image"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImage(string? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
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
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DelveFeaturesDat>> GetManyToManyByImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<string, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys(int? key, out DelveFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys(int? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        if (byAchievementItemsKeys is null)
        {
            byAchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byAchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveFeaturesDat>> GetManyToManyByAchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<int, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out DelveFeaturesDat? item)
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown72.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown72.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveFeaturesDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<int, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.MinDepth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinDepth(int? key, out DelveFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinDepth(key, out var items))
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.MinDepth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinDepth(int? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        if (byMinDepth is null)
        {
            byMinDepth = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinDepth;
                foreach (var listKey in itemKey)
                {
                    if (!byMinDepth.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMinDepth.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMinDepth.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byMinDepth"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveFeaturesDat>> GetManyToManyByMinDepth(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<int, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinDepth(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out DelveFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DelveFeaturesDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<string, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown112(int? key, out DelveFeaturesDat? item)
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown112(int? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        if (byUnknown112 is null)
        {
            byUnknown112 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown112;

                if (!byUnknown112.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown112.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown112.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byUnknown112"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveFeaturesDat>> GetManyToManyByUnknown112(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<int, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown112(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown116(int? key, out DelveFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown116(key, out var items))
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown116(int? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        if (byUnknown116 is null)
        {
            byUnknown116 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown116;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown116.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown116.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown116.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byUnknown116"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveFeaturesDat>> GetManyToManyByUnknown116(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<int, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown116(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Unknown132"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown132(int? key, out DelveFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown132(key, out var items))
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Unknown132"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown132(int? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        if (byUnknown132 is null)
        {
            byUnknown132 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown132;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown132.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown132.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown132.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byUnknown132"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveFeaturesDat>> GetManyToManyByUnknown132(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<int, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown132(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown148(int? key, out DelveFeaturesDat? item)
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
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown148(int? key, out IReadOnlyList<DelveFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        if (byUnknown148 is null)
        {
            byUnknown148 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown148;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown148.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown148.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown148.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveFeaturesDat"/> with <see cref="DelveFeaturesDat.byUnknown148"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveFeaturesDat>> GetManyToManyByUnknown148(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveFeaturesDat>>();
        }

        var items = new List<ResultItem<int, DelveFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown148(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DelveFeaturesDat[] Load()
    {
        const string filePath = "Data/DelveFeatures.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveFeaturesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnWeight
            (var tempspawnweightLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweightLoading = tempspawnweightLoading.AsReadOnly();

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Image
            (var imageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading Unknown72
            (var tempunknown72Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown72Loading = tempunknown72Loading.AsReadOnly();

            // loading MinDepth
            (var tempmindepthLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var mindepthLoading = tempmindepthLoading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown116
            (var tempunknown116Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown116Loading = tempunknown116Loading.AsReadOnly();

            // loading Unknown132
            (var tempunknown132Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown132Loading = tempunknown132Loading.AsReadOnly();

            // loading Unknown148
            (var tempunknown148Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown148Loading = tempunknown148Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveFeaturesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                SpawnWeight = spawnweightLoading,
                WorldAreasKey = worldareaskeyLoading,
                Image = imageLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                Unknown72 = unknown72Loading,
                MinDepth = mindepthLoading,
                Description = descriptionLoading,
                Unknown112 = unknown112Loading,
                Unknown116 = unknown116Loading,
                Unknown132 = unknown132Loading,
                Unknown148 = unknown148Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
