using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PropheciesDat"/> related data and helper methods.
/// </summary>
public sealed class PropheciesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PropheciesDat> Items { get; }

    private Dictionary<string, List<PropheciesDat>>? byId;
    private Dictionary<string, List<PropheciesDat>>? byPredictionText;
    private Dictionary<int, List<PropheciesDat>>? byUnknown16;
    private Dictionary<string, List<PropheciesDat>>? byName;
    private Dictionary<string, List<PropheciesDat>>? byFlavourText;
    private Dictionary<int, List<PropheciesDat>>? byQuestTracker_ClientStringsKeys;
    private Dictionary<string, List<PropheciesDat>>? byOGGFile;
    private Dictionary<int, List<PropheciesDat>>? byProphecyChainKey;
    private Dictionary<int, List<PropheciesDat>>? byProphecyChainPosition;
    private Dictionary<bool, List<PropheciesDat>>? byIsEnabled;
    private Dictionary<int, List<PropheciesDat>>? bySealCost;
    private Dictionary<string, List<PropheciesDat>>? byPredictionText2;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropheciesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PropheciesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out PropheciesDat? item)
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
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
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PropheciesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PropheciesDat>>();
        }

        var items = new List<ResultItem<string, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.PredictionText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPredictionText(string? key, out PropheciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPredictionText(key, out var items))
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.PredictionText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPredictionText(string? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        if (byPredictionText is null)
        {
            byPredictionText = new();
            foreach (var item in Items)
            {
                var itemKey = item.PredictionText;

                if (!byPredictionText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPredictionText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPredictionText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byPredictionText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PropheciesDat>> GetManyToManyByPredictionText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PropheciesDat>>();
        }

        var items = new List<ResultItem<string, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPredictionText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out PropheciesDat? item)
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
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
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PropheciesDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PropheciesDat>>();
        }

        var items = new List<ResultItem<int, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out PropheciesDat? item)
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
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
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PropheciesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PropheciesDat>>();
        }

        var items = new List<ResultItem<string, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourText(string? key, out PropheciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlavourText(key, out var items))
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourText(string? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        if (byFlavourText is null)
        {
            byFlavourText = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlavourText;

                if (!byFlavourText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFlavourText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFlavourText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byFlavourText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PropheciesDat>> GetManyToManyByFlavourText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PropheciesDat>>();
        }

        var items = new List<ResultItem<string, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.QuestTracker_ClientStringsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestTracker_ClientStringsKeys(int? key, out PropheciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestTracker_ClientStringsKeys(key, out var items))
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.QuestTracker_ClientStringsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestTracker_ClientStringsKeys(int? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        if (byQuestTracker_ClientStringsKeys is null)
        {
            byQuestTracker_ClientStringsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestTracker_ClientStringsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byQuestTracker_ClientStringsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byQuestTracker_ClientStringsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byQuestTracker_ClientStringsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byQuestTracker_ClientStringsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PropheciesDat>> GetManyToManyByQuestTracker_ClientStringsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PropheciesDat>>();
        }

        var items = new List<ResultItem<int, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestTracker_ClientStringsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.OGGFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOGGFile(string? key, out PropheciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOGGFile(key, out var items))
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.OGGFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOGGFile(string? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        if (byOGGFile is null)
        {
            byOGGFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.OGGFile;

                if (!byOGGFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOGGFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOGGFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byOGGFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PropheciesDat>> GetManyToManyByOGGFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PropheciesDat>>();
        }

        var items = new List<ResultItem<string, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOGGFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.ProphecyChainKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProphecyChainKey(int? key, out PropheciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProphecyChainKey(key, out var items))
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.ProphecyChainKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProphecyChainKey(int? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        if (byProphecyChainKey is null)
        {
            byProphecyChainKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ProphecyChainKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byProphecyChainKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byProphecyChainKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byProphecyChainKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byProphecyChainKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PropheciesDat>> GetManyToManyByProphecyChainKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PropheciesDat>>();
        }

        var items = new List<ResultItem<int, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProphecyChainKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.ProphecyChainPosition"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProphecyChainPosition(int? key, out PropheciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProphecyChainPosition(key, out var items))
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.ProphecyChainPosition"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProphecyChainPosition(int? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        if (byProphecyChainPosition is null)
        {
            byProphecyChainPosition = new();
            foreach (var item in Items)
            {
                var itemKey = item.ProphecyChainPosition;

                if (!byProphecyChainPosition.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byProphecyChainPosition.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byProphecyChainPosition.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byProphecyChainPosition"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PropheciesDat>> GetManyToManyByProphecyChainPosition(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PropheciesDat>>();
        }

        var items = new List<ResultItem<int, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProphecyChainPosition(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.IsEnabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsEnabled(bool? key, out PropheciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsEnabled(key, out var items))
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.IsEnabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsEnabled(bool? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        if (byIsEnabled is null)
        {
            byIsEnabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsEnabled;

                if (!byIsEnabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsEnabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsEnabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byIsEnabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PropheciesDat>> GetManyToManyByIsEnabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PropheciesDat>>();
        }

        var items = new List<ResultItem<bool, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsEnabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.SealCost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySealCost(int? key, out PropheciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySealCost(key, out var items))
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.SealCost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySealCost(int? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        if (bySealCost is null)
        {
            bySealCost = new();
            foreach (var item in Items)
            {
                var itemKey = item.SealCost;

                if (!bySealCost.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySealCost.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySealCost.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.bySealCost"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PropheciesDat>> GetManyToManyBySealCost(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PropheciesDat>>();
        }

        var items = new List<ResultItem<int, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySealCost(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.PredictionText2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPredictionText2(string? key, out PropheciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPredictionText2(key, out var items))
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
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.PredictionText2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPredictionText2(string? key, out IReadOnlyList<PropheciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        if (byPredictionText2 is null)
        {
            byPredictionText2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.PredictionText2;

                if (!byPredictionText2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPredictionText2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPredictionText2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PropheciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PropheciesDat"/> with <see cref="PropheciesDat.byPredictionText2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PropheciesDat>> GetManyToManyByPredictionText2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PropheciesDat>>();
        }

        var items = new List<ResultItem<string, PropheciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPredictionText2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PropheciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PropheciesDat[] Load()
    {
        const string filePath = "Data/Prophecies.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PropheciesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PredictionText
            (var predictiontextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading QuestTracker_ClientStringsKeys
            (var tempquesttracker_clientstringskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var questtracker_clientstringskeysLoading = tempquesttracker_clientstringskeysLoading.AsReadOnly();

            // loading OGGFile
            (var oggfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ProphecyChainKey
            (var prophecychainkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ProphecyChainPosition
            (var prophecychainpositionLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SealCost
            (var sealcostLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PredictionText2
            (var predictiontext2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PropheciesDat()
            {
                Id = idLoading,
                PredictionText = predictiontextLoading,
                Unknown16 = unknown16Loading,
                Name = nameLoading,
                FlavourText = flavourtextLoading,
                QuestTracker_ClientStringsKeys = questtracker_clientstringskeysLoading,
                OGGFile = oggfileLoading,
                ProphecyChainKey = prophecychainkeyLoading,
                ProphecyChainPosition = prophecychainpositionLoading,
                IsEnabled = isenabledLoading,
                SealCost = sealcostLoading,
                PredictionText2 = predictiontext2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
