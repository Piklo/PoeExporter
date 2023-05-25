using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MavenFightsDat"/> related data and helper methods.
/// </summary>
public sealed class MavenFightsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MavenFightsDat> Items { get; }

    private Dictionary<string, List<MavenFightsDat>>? byId;
    private Dictionary<int, List<MavenFightsDat>>? byWitnessesRequired;
    private Dictionary<int, List<MavenFightsDat>>? byAreaLevel;
    private Dictionary<int, List<MavenFightsDat>>? byUnknown16;
    private Dictionary<int, List<MavenFightsDat>>? byUnknown20;
    private Dictionary<int, List<MavenFightsDat>>? byBaseItemType;
    private Dictionary<int, List<MavenFightsDat>>? byUnknown40;
    private Dictionary<int, List<MavenFightsDat>>? byMinMapTier;
    private Dictionary<int, List<MavenFightsDat>>? byUnknown48;
    private Dictionary<int, List<MavenFightsDat>>? byUnknown64;
    private Dictionary<int, List<MavenFightsDat>>? byWitnessAreas;
    private Dictionary<int, List<MavenFightsDat>>? byUnknown84;
    private Dictionary<int, List<MavenFightsDat>>? byUnknown88;
    private Dictionary<bool, List<MavenFightsDat>>? byUnknown104;
    private Dictionary<int, List<MavenFightsDat>>? byUnknown105;
    private Dictionary<int, List<MavenFightsDat>>? byAchievements1;
    private Dictionary<int, List<MavenFightsDat>>? byAchievements2;

    /// <summary>
    /// Initializes a new instance of the <see cref="MavenFightsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MavenFightsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MavenFightsDat? item)
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
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
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MavenFightsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MavenFightsDat>>();
        }

        var items = new List<ResultItem<string, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.WitnessesRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWitnessesRequired(int? key, out MavenFightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWitnessesRequired(key, out var items))
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.WitnessesRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWitnessesRequired(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byWitnessesRequired is null)
        {
            byWitnessesRequired = new();
            foreach (var item in Items)
            {
                var itemKey = item.WitnessesRequired;

                if (!byWitnessesRequired.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWitnessesRequired.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWitnessesRequired.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byWitnessesRequired"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByWitnessesRequired(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWitnessesRequired(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAreaLevel(int? key, out MavenFightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAreaLevel(key, out var items))
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAreaLevel(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byAreaLevel is null)
        {
            byAreaLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.AreaLevel;

                if (!byAreaLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAreaLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAreaLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byAreaLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByAreaLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAreaLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out MavenFightsDat? item)
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
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
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out MavenFightsDat? item)
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
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
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.BaseItemType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemType(int? key, out MavenFightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemType(key, out var items))
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.BaseItemType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemType(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byBaseItemType is null)
        {
            byBaseItemType = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemType;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byBaseItemType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByBaseItemType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out MavenFightsDat? item)
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
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
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.MinMapTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinMapTier(int? key, out MavenFightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinMapTier(key, out var items))
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.MinMapTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinMapTier(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byMinMapTier is null)
        {
            byMinMapTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinMapTier;

                if (!byMinMapTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinMapTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinMapTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byMinMapTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByMinMapTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinMapTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out MavenFightsDat? item)
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown48.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out MavenFightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.WitnessAreas"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWitnessAreas(int? key, out MavenFightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWitnessAreas(key, out var items))
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.WitnessAreas"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWitnessAreas(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byWitnessAreas is null)
        {
            byWitnessAreas = new();
            foreach (var item in Items)
            {
                var itemKey = item.WitnessAreas;
                foreach (var listKey in itemKey)
                {
                    if (!byWitnessAreas.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWitnessAreas.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWitnessAreas.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byWitnessAreas"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByWitnessAreas(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWitnessAreas(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out MavenFightsDat? item)
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
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
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out MavenFightsDat? item)
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown88.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(bool? key, out MavenFightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown104(key, out var items))
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(bool? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;

                if (!byUnknown104.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown104.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MavenFightsDat>> GetManyToManyByUnknown104(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MavenFightsDat>>();
        }

        var items = new List<ResultItem<bool, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown105(int? key, out MavenFightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown105(key, out var items))
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown105(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byUnknown105 is null)
        {
            byUnknown105 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown105;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown105.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown105.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown105.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byUnknown105"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByUnknown105(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown105(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Achievements1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements1(int? key, out MavenFightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievements1(key, out var items))
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Achievements1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements1(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byAchievements1 is null)
        {
            byAchievements1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievements1;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievements1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievements1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievements1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byAchievements1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByAchievements1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Achievements2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements2(int? key, out MavenFightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievements2(key, out var items))
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
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.Achievements2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements2(int? key, out IReadOnlyList<MavenFightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        if (byAchievements2 is null)
        {
            byAchievements2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievements2;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievements2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievements2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievements2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenFightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenFightsDat"/> with <see cref="MavenFightsDat.byAchievements2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenFightsDat>> GetManyToManyByAchievements2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenFightsDat>>();
        }

        var items = new List<ResultItem<int, MavenFightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenFightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MavenFightsDat[] Load()
    {
        const string filePath = "Data/MavenFights.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MavenFightsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WitnessesRequired
            (var witnessesrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinMapTier
            (var minmaptierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WitnessAreas
            (var tempwitnessareasLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var witnessareasLoading = tempwitnessareasLoading.AsReadOnly();

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown105
            (var tempunknown105Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown105Loading = tempunknown105Loading.AsReadOnly();

            // loading Achievements1
            (var tempachievements1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievements1Loading = tempachievements1Loading.AsReadOnly();

            // loading Achievements2
            (var tempachievements2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievements2Loading = tempachievements2Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MavenFightsDat()
            {
                Id = idLoading,
                WitnessesRequired = witnessesrequiredLoading,
                AreaLevel = arealevelLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                BaseItemType = baseitemtypeLoading,
                Unknown40 = unknown40Loading,
                MinMapTier = minmaptierLoading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                WitnessAreas = witnessareasLoading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
                Unknown105 = unknown105Loading,
                Achievements1 = achievements1Loading,
                Achievements2 = achievements2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
