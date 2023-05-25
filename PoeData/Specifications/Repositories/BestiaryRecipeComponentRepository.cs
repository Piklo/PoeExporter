using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BestiaryRecipeComponentDat"/> related data and helper methods.
/// </summary>
public sealed class BestiaryRecipeComponentRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BestiaryRecipeComponentDat> Items { get; }

    private Dictionary<string, List<BestiaryRecipeComponentDat>>? byId;
    private Dictionary<int, List<BestiaryRecipeComponentDat>>? byMinLevel;
    private Dictionary<int, List<BestiaryRecipeComponentDat>>? byBestiaryFamiliesKey;
    private Dictionary<int, List<BestiaryRecipeComponentDat>>? byBestiaryGroupsKey;
    private Dictionary<int, List<BestiaryRecipeComponentDat>>? byModsKey;
    private Dictionary<int, List<BestiaryRecipeComponentDat>>? byBestiaryCapturableMonstersKey;
    private Dictionary<int, List<BestiaryRecipeComponentDat>>? byBeastRarity;
    private Dictionary<int, List<BestiaryRecipeComponentDat>>? byBestiaryGenusKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="BestiaryRecipeComponentRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BestiaryRecipeComponentRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BestiaryRecipeComponentDat? item)
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
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BestiaryRecipeComponentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
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
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryRecipeComponentDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryRecipeComponentDat>>();
        }

        var items = new List<ResultItem<string, BestiaryRecipeComponentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryRecipeComponentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out BestiaryRecipeComponentDat? item)
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
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<BestiaryRecipeComponentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
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
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipeComponentDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipeComponentDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipeComponentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipeComponentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.BestiaryFamiliesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBestiaryFamiliesKey(int? key, out BestiaryRecipeComponentDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBestiaryFamiliesKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.BestiaryFamiliesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBestiaryFamiliesKey(int? key, out IReadOnlyList<BestiaryRecipeComponentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        if (byBestiaryFamiliesKey is null)
        {
            byBestiaryFamiliesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BestiaryFamiliesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBestiaryFamiliesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBestiaryFamiliesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBestiaryFamiliesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.byBestiaryFamiliesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipeComponentDat>> GetManyToManyByBestiaryFamiliesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipeComponentDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipeComponentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBestiaryFamiliesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipeComponentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.BestiaryGroupsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBestiaryGroupsKey(int? key, out BestiaryRecipeComponentDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBestiaryGroupsKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.BestiaryGroupsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBestiaryGroupsKey(int? key, out IReadOnlyList<BestiaryRecipeComponentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        if (byBestiaryGroupsKey is null)
        {
            byBestiaryGroupsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BestiaryGroupsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBestiaryGroupsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBestiaryGroupsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBestiaryGroupsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.byBestiaryGroupsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipeComponentDat>> GetManyToManyByBestiaryGroupsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipeComponentDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipeComponentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBestiaryGroupsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipeComponentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKey(int? key, out BestiaryRecipeComponentDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKey(int? key, out IReadOnlyList<BestiaryRecipeComponentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        if (byModsKey is null)
        {
            byModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.byModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipeComponentDat>> GetManyToManyByModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipeComponentDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipeComponentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipeComponentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.BestiaryCapturableMonstersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBestiaryCapturableMonstersKey(int? key, out BestiaryRecipeComponentDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBestiaryCapturableMonstersKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.BestiaryCapturableMonstersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBestiaryCapturableMonstersKey(int? key, out IReadOnlyList<BestiaryRecipeComponentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        if (byBestiaryCapturableMonstersKey is null)
        {
            byBestiaryCapturableMonstersKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BestiaryCapturableMonstersKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBestiaryCapturableMonstersKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBestiaryCapturableMonstersKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBestiaryCapturableMonstersKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.byBestiaryCapturableMonstersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipeComponentDat>> GetManyToManyByBestiaryCapturableMonstersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipeComponentDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipeComponentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBestiaryCapturableMonstersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipeComponentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.BeastRarity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBeastRarity(int? key, out BestiaryRecipeComponentDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBeastRarity(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.BeastRarity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBeastRarity(int? key, out IReadOnlyList<BestiaryRecipeComponentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        if (byBeastRarity is null)
        {
            byBeastRarity = new();
            foreach (var item in Items)
            {
                var itemKey = item.BeastRarity;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBeastRarity.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBeastRarity.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBeastRarity.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.byBeastRarity"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipeComponentDat>> GetManyToManyByBeastRarity(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipeComponentDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipeComponentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBeastRarity(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipeComponentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.BestiaryGenusKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBestiaryGenusKey(int? key, out BestiaryRecipeComponentDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBestiaryGenusKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.BestiaryGenusKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBestiaryGenusKey(int? key, out IReadOnlyList<BestiaryRecipeComponentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        if (byBestiaryGenusKey is null)
        {
            byBestiaryGenusKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BestiaryGenusKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBestiaryGenusKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBestiaryGenusKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBestiaryGenusKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipeComponentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeComponentDat"/> with <see cref="BestiaryRecipeComponentDat.byBestiaryGenusKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipeComponentDat>> GetManyToManyByBestiaryGenusKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipeComponentDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipeComponentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBestiaryGenusKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipeComponentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BestiaryRecipeComponentDat[] Load()
    {
        const string filePath = "Data/BestiaryRecipeComponent.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryRecipeComponentDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BestiaryFamiliesKey
            (var bestiaryfamilieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BestiaryGroupsKey
            (var bestiarygroupskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BestiaryCapturableMonstersKey
            (var bestiarycapturablemonsterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BeastRarity
            (var beastrarityLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BestiaryGenusKey
            (var bestiarygenuskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryRecipeComponentDat()
            {
                Id = idLoading,
                MinLevel = minlevelLoading,
                BestiaryFamiliesKey = bestiaryfamilieskeyLoading,
                BestiaryGroupsKey = bestiarygroupskeyLoading,
                ModsKey = modskeyLoading,
                BestiaryCapturableMonstersKey = bestiarycapturablemonsterskeyLoading,
                BeastRarity = beastrarityLoading,
                BestiaryGenusKey = bestiarygenuskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
