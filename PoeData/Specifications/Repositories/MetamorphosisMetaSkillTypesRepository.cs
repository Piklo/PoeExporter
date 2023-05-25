using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MetamorphosisMetaSkillTypesDat"/> related data and helper methods.
/// </summary>
public sealed class MetamorphosisMetaSkillTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MetamorphosisMetaSkillTypesDat> Items { get; }

    private Dictionary<string, List<MetamorphosisMetaSkillTypesDat>>? byId;
    private Dictionary<string, List<MetamorphosisMetaSkillTypesDat>>? byName;
    private Dictionary<string, List<MetamorphosisMetaSkillTypesDat>>? byDescription;
    private Dictionary<string, List<MetamorphosisMetaSkillTypesDat>>? byUnavailableArt;
    private Dictionary<string, List<MetamorphosisMetaSkillTypesDat>>? byUnknown32;
    private Dictionary<string, List<MetamorphosisMetaSkillTypesDat>>? byAvailableArt;
    private Dictionary<int, List<MetamorphosisMetaSkillTypesDat>>? byItemisedSample;
    private Dictionary<string, List<MetamorphosisMetaSkillTypesDat>>? byBodypartName;
    private Dictionary<int, List<MetamorphosisMetaSkillTypesDat>>? byUnknown72;
    private Dictionary<int, List<MetamorphosisMetaSkillTypesDat>>? byAchievementItemsKeys;
    private Dictionary<string, List<MetamorphosisMetaSkillTypesDat>>? byBodypartNamePlural;
    private Dictionary<int, List<MetamorphosisMetaSkillTypesDat>>? byUnknown100;

    /// <summary>
    /// Initializes a new instance of the <see cref="MetamorphosisMetaSkillTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MetamorphosisMetaSkillTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MetamorphosisMetaSkillTypesDat? item)
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
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
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out MetamorphosisMetaSkillTypesDat? item)
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
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
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillTypesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out MetamorphosisMetaSkillTypesDat? item)
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
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
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillTypesDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.UnavailableArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnavailableArt(string? key, out MetamorphosisMetaSkillTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnavailableArt(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.UnavailableArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnavailableArt(string? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        if (byUnavailableArt is null)
        {
            byUnavailableArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.UnavailableArt;

                if (!byUnavailableArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnavailableArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnavailableArt.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byUnavailableArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillTypesDat>> GetManyToManyByUnavailableArt(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnavailableArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(string? key, out MetamorphosisMetaSkillTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(string? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillTypesDat>> GetManyToManyByUnknown32(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.AvailableArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAvailableArt(string? key, out MetamorphosisMetaSkillTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAvailableArt(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.AvailableArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAvailableArt(string? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        if (byAvailableArt is null)
        {
            byAvailableArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.AvailableArt;

                if (!byAvailableArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAvailableArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAvailableArt.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byAvailableArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillTypesDat>> GetManyToManyByAvailableArt(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAvailableArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.ItemisedSample"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemisedSample(int? key, out MetamorphosisMetaSkillTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemisedSample(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.ItemisedSample"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemisedSample(int? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        if (byItemisedSample is null)
        {
            byItemisedSample = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemisedSample;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemisedSample.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemisedSample.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemisedSample.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byItemisedSample"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillTypesDat>> GetManyToManyByItemisedSample(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemisedSample(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.BodypartName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBodypartName(string? key, out MetamorphosisMetaSkillTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBodypartName(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.BodypartName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBodypartName(string? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        if (byBodypartName is null)
        {
            byBodypartName = new();
            foreach (var item in Items)
            {
                var itemKey = item.BodypartName;

                if (!byBodypartName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBodypartName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBodypartName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byBodypartName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillTypesDat>> GetManyToManyByBodypartName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBodypartName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out MetamorphosisMetaSkillTypesDat? item)
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
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
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillTypesDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys(int? key, out MetamorphosisMetaSkillTypesDat? item)
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys(int? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
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
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byAchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillTypesDat>> GetManyToManyByAchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.BodypartNamePlural"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBodypartNamePlural(string? key, out MetamorphosisMetaSkillTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBodypartNamePlural(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.BodypartNamePlural"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBodypartNamePlural(string? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        if (byBodypartNamePlural is null)
        {
            byBodypartNamePlural = new();
            foreach (var item in Items)
            {
                var itemKey = item.BodypartNamePlural;

                if (!byBodypartNamePlural.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBodypartNamePlural.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBodypartNamePlural.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byBodypartNamePlural"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillTypesDat>> GetManyToManyByBodypartNamePlural(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBodypartNamePlural(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(int? key, out MetamorphosisMetaSkillTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown100(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(int? key, out IReadOnlyList<MetamorphosisMetaSkillTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        if (byUnknown100 is null)
        {
            byUnknown100 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown100;

                if (!byUnknown100.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown100.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown100.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillTypesDat"/> with <see cref="MetamorphosisMetaSkillTypesDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillTypesDat>> GetManyToManyByUnknown100(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillTypesDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MetamorphosisMetaSkillTypesDat[] Load()
    {
        const string filePath = "Data/MetamorphosisMetaSkillTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MetamorphosisMetaSkillTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading UnavailableArt
            (var unavailableartLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AvailableArt
            (var availableartLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ItemisedSample
            (var itemisedsampleLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BodypartName
            (var bodypartnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading BodypartNamePlural
            (var bodypartnamepluralLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MetamorphosisMetaSkillTypesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Description = descriptionLoading,
                UnavailableArt = unavailableartLoading,
                Unknown32 = unknown32Loading,
                AvailableArt = availableartLoading,
                ItemisedSample = itemisedsampleLoading,
                BodypartName = bodypartnameLoading,
                Unknown72 = unknown72Loading,
                AchievementItemsKeys = achievementitemskeysLoading,
                BodypartNamePlural = bodypartnamepluralLoading,
                Unknown100 = unknown100Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
