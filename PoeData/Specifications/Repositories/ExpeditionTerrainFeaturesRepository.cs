using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ExpeditionTerrainFeaturesDat"/> related data and helper methods.
/// </summary>
public sealed class ExpeditionTerrainFeaturesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ExpeditionTerrainFeaturesDat> Items { get; }

    private Dictionary<string, List<ExpeditionTerrainFeaturesDat>>? byId;
    private Dictionary<int, List<ExpeditionTerrainFeaturesDat>>? byExtraFeature;
    private Dictionary<int, List<ExpeditionTerrainFeaturesDat>>? byExpeditionFaction;
    private Dictionary<int, List<ExpeditionTerrainFeaturesDat>>? byMinLevel;
    private Dictionary<int, List<ExpeditionTerrainFeaturesDat>>? byMaxLevel;
    private Dictionary<int, List<ExpeditionTerrainFeaturesDat>>? byUnknown48;
    private Dictionary<int, List<ExpeditionTerrainFeaturesDat>>? byArea;
    private Dictionary<int, List<ExpeditionTerrainFeaturesDat>>? byExpeditionAreas;
    private Dictionary<int, List<ExpeditionTerrainFeaturesDat>>? byUnknown84;
    private Dictionary<bool, List<ExpeditionTerrainFeaturesDat>>? byUnknown88;
    private Dictionary<int, List<ExpeditionTerrainFeaturesDat>>? byUnearthAchievements;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpeditionTerrainFeaturesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ExpeditionTerrainFeaturesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ExpeditionTerrainFeaturesDat? item)
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
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
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExpeditionTerrainFeaturesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<string, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.ExtraFeature"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExtraFeature(int? key, out ExpeditionTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExtraFeature(key, out var items))
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.ExtraFeature"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExtraFeature(int? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        if (byExtraFeature is null)
        {
            byExtraFeature = new();
            foreach (var item in Items)
            {
                var itemKey = item.ExtraFeature;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byExtraFeature.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byExtraFeature.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byExtraFeature.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byExtraFeature"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionTerrainFeaturesDat>> GetManyToManyByExtraFeature(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExtraFeature(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.ExpeditionFaction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExpeditionFaction(int? key, out ExpeditionTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExpeditionFaction(key, out var items))
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.ExpeditionFaction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExpeditionFaction(int? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        if (byExpeditionFaction is null)
        {
            byExpeditionFaction = new();
            foreach (var item in Items)
            {
                var itemKey = item.ExpeditionFaction;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byExpeditionFaction.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byExpeditionFaction.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byExpeditionFaction.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byExpeditionFaction"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionTerrainFeaturesDat>> GetManyToManyByExpeditionFaction(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExpeditionFaction(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out ExpeditionTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLevel(key, out var items))
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        if (byMinLevel is null)
        {
            byMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLevel;

                if (!byMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionTerrainFeaturesDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out ExpeditionTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxLevel(key, out var items))
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        if (byMaxLevel is null)
        {
            byMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxLevel;

                if (!byMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionTerrainFeaturesDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out ExpeditionTerrainFeaturesDat? item)
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
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
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionTerrainFeaturesDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.Area"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArea(int? key, out ExpeditionTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArea(key, out var items))
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.Area"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArea(int? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        if (byArea is null)
        {
            byArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.Area;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byArea.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byArea.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionTerrainFeaturesDat>> GetManyToManyByArea(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.ExpeditionAreas"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExpeditionAreas(int? key, out ExpeditionTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExpeditionAreas(key, out var items))
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.ExpeditionAreas"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExpeditionAreas(int? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        if (byExpeditionAreas is null)
        {
            byExpeditionAreas = new();
            foreach (var item in Items)
            {
                var itemKey = item.ExpeditionAreas;
                foreach (var listKey in itemKey)
                {
                    if (!byExpeditionAreas.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byExpeditionAreas.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byExpeditionAreas.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byExpeditionAreas"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionTerrainFeaturesDat>> GetManyToManyByExpeditionAreas(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExpeditionAreas(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out ExpeditionTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown84(key, out var items))
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        if (byUnknown84 is null)
        {
            byUnknown84 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown84;

                if (!byUnknown84.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown84.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown84.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionTerrainFeaturesDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(bool? key, out ExpeditionTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown88(key, out var items))
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(bool? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;

                if (!byUnknown88.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ExpeditionTerrainFeaturesDat>> GetManyToManyByUnknown88(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<bool, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.UnearthAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnearthAchievements(int? key, out ExpeditionTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnearthAchievements(key, out var items))
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
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.UnearthAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnearthAchievements(int? key, out IReadOnlyList<ExpeditionTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        if (byUnearthAchievements is null)
        {
            byUnearthAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.UnearthAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byUnearthAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnearthAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnearthAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionTerrainFeaturesDat"/> with <see cref="ExpeditionTerrainFeaturesDat.byUnearthAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionTerrainFeaturesDat>> GetManyToManyByUnearthAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnearthAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ExpeditionTerrainFeaturesDat[] Load()
    {
        const string filePath = "Data/ExpeditionTerrainFeatures.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionTerrainFeaturesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ExtraFeature
            (var extrafeatureLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ExpeditionFaction
            (var expeditionfactionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Area
            (var areaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ExpeditionAreas
            (var tempexpeditionareasLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var expeditionareasLoading = tempexpeditionareasLoading.AsReadOnly();

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading UnearthAchievements
            (var tempunearthachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unearthachievementsLoading = tempunearthachievementsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionTerrainFeaturesDat()
            {
                Id = idLoading,
                ExtraFeature = extrafeatureLoading,
                ExpeditionFaction = expeditionfactionLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                Unknown48 = unknown48Loading,
                Area = areaLoading,
                ExpeditionAreas = expeditionareasLoading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                UnearthAchievements = unearthachievementsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
