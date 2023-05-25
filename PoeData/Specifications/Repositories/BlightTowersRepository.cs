using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BlightTowersDat"/> related data and helper methods.
/// </summary>
public sealed class BlightTowersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BlightTowersDat> Items { get; }

    private Dictionary<string, List<BlightTowersDat>>? byId;
    private Dictionary<string, List<BlightTowersDat>>? byName;
    private Dictionary<string, List<BlightTowersDat>>? byDescription;
    private Dictionary<string, List<BlightTowersDat>>? byIcon;
    private Dictionary<int, List<BlightTowersDat>>? byNextUpgradeOptions;
    private Dictionary<int, List<BlightTowersDat>>? byUnknown48;
    private Dictionary<string, List<BlightTowersDat>>? byTier;
    private Dictionary<int, List<BlightTowersDat>>? byRadius;
    private Dictionary<int, List<BlightTowersDat>>? byUnknown64;
    private Dictionary<int, List<BlightTowersDat>>? bySpendResourceAchievement;
    private Dictionary<int, List<BlightTowersDat>>? byStatsKey;
    private Dictionary<int, List<BlightTowersDat>>? byStatsKeys;
    private Dictionary<int, List<BlightTowersDat>>? byStatsKeys2;
    private Dictionary<bool, List<BlightTowersDat>>? byUnknown132;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightTowersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BlightTowersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BlightTowersDat? item)
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
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
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightTowersDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightTowersDat>>();
        }

        var items = new List<ResultItem<string, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out BlightTowersDat? item)
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
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
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightTowersDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightTowersDat>>();
        }

        var items = new List<ResultItem<string, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out BlightTowersDat? item)
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
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
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightTowersDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightTowersDat>>();
        }

        var items = new List<ResultItem<string, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon(string? key, out BlightTowersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIcon(key, out var items))
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon(string? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        if (byIcon is null)
        {
            byIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.Icon;

                if (!byIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightTowersDat>> GetManyToManyByIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightTowersDat>>();
        }

        var items = new List<ResultItem<string, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.NextUpgradeOptions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNextUpgradeOptions(int? key, out BlightTowersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNextUpgradeOptions(key, out var items))
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.NextUpgradeOptions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNextUpgradeOptions(int? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        if (byNextUpgradeOptions is null)
        {
            byNextUpgradeOptions = new();
            foreach (var item in Items)
            {
                var itemKey = item.NextUpgradeOptions;
                foreach (var listKey in itemKey)
                {
                    if (!byNextUpgradeOptions.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNextUpgradeOptions.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNextUpgradeOptions.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byNextUpgradeOptions"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowersDat>> GetManyToManyByNextUpgradeOptions(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowersDat>>();
        }

        var items = new List<ResultItem<int, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNextUpgradeOptions(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out BlightTowersDat? item)
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
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
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowersDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowersDat>>();
        }

        var items = new List<ResultItem<int, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(string? key, out BlightTowersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier(key, out var items))
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(string? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        if (byTier is null)
        {
            byTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier;

                if (!byTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightTowersDat>> GetManyToManyByTier(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightTowersDat>>();
        }

        var items = new List<ResultItem<string, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Radius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRadius(int? key, out BlightTowersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRadius(key, out var items))
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Radius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRadius(int? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        if (byRadius is null)
        {
            byRadius = new();
            foreach (var item in Items)
            {
                var itemKey = item.Radius;

                if (!byRadius.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRadius.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRadius.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byRadius"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowersDat>> GetManyToManyByRadius(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowersDat>>();
        }

        var items = new List<ResultItem<int, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRadius(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out BlightTowersDat? item)
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
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
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowersDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowersDat>>();
        }

        var items = new List<ResultItem<int, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.SpendResourceAchievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpendResourceAchievement(int? key, out BlightTowersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpendResourceAchievement(key, out var items))
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.SpendResourceAchievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpendResourceAchievement(int? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        if (bySpendResourceAchievement is null)
        {
            bySpendResourceAchievement = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpendResourceAchievement;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySpendResourceAchievement.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySpendResourceAchievement.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySpendResourceAchievement.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.bySpendResourceAchievement"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowersDat>> GetManyToManyBySpendResourceAchievement(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowersDat>>();
        }

        var items = new List<ResultItem<int, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpendResourceAchievement(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey(int? key, out BlightTowersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey(key, out var items))
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey(int? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        if (byStatsKey is null)
        {
            byStatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byStatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowersDat>> GetManyToManyByStatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowersDat>>();
        }

        var items = new List<ResultItem<int, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys(int? key, out BlightTowersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKeys(key, out var items))
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys(int? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        if (byStatsKeys is null)
        {
            byStatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byStatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowersDat>> GetManyToManyByStatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowersDat>>();
        }

        var items = new List<ResultItem<int, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.StatsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys2(int? key, out BlightTowersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKeys2(key, out var items))
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.StatsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys2(int? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        if (byStatsKeys2 is null)
        {
            byStatsKeys2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKeys2;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsKeys2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsKeys2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsKeys2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byStatsKeys2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowersDat>> GetManyToManyByStatsKeys2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowersDat>>();
        }

        var items = new List<ResultItem<int, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Unknown132"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown132(bool? key, out BlightTowersDat? item)
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
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.Unknown132"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown132(bool? key, out IReadOnlyList<BlightTowersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        if (byUnknown132 is null)
        {
            byUnknown132 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown132;

                if (!byUnknown132.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown132.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown132.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightTowersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowersDat"/> with <see cref="BlightTowersDat.byUnknown132"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BlightTowersDat>> GetManyToManyByUnknown132(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BlightTowersDat>>();
        }

        var items = new List<ResultItem<bool, BlightTowersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown132(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BlightTowersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BlightTowersDat[] Load()
    {
        const string filePath = "Data/BlightTowers.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightTowersDat[tableRows];
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

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NextUpgradeOptions
            (var tempnextupgradeoptionsLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var nextupgradeoptionsLoading = tempnextupgradeoptionsLoading.AsReadOnly();

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Radius
            (var radiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpendResourceAchievement
            (var spendresourceachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatsKeys2
            (var tempstatskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeys2Loading = tempstatskeys2Loading.AsReadOnly();

            // loading Unknown132
            (var unknown132Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightTowersDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Description = descriptionLoading,
                Icon = iconLoading,
                NextUpgradeOptions = nextupgradeoptionsLoading,
                Unknown48 = unknown48Loading,
                Tier = tierLoading,
                Radius = radiusLoading,
                Unknown64 = unknown64Loading,
                SpendResourceAchievement = spendresourceachievementLoading,
                StatsKey = statskeyLoading,
                StatsKeys = statskeysLoading,
                StatsKeys2 = statskeys2Loading,
                Unknown132 = unknown132Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
