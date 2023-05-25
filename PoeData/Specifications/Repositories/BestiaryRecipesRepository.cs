using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BestiaryRecipesDat"/> related data and helper methods.
/// </summary>
public sealed class BestiaryRecipesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BestiaryRecipesDat> Items { get; }

    private Dictionary<string, List<BestiaryRecipesDat>>? byId;
    private Dictionary<string, List<BestiaryRecipesDat>>? byDescription;
    private Dictionary<int, List<BestiaryRecipesDat>>? byBestiaryRecipeComponentKeys;
    private Dictionary<string, List<BestiaryRecipesDat>>? byNotes;
    private Dictionary<int, List<BestiaryRecipesDat>>? byCategory;
    private Dictionary<bool, List<BestiaryRecipesDat>>? byUnknown56;
    private Dictionary<int, List<BestiaryRecipesDat>>? byAchievements;
    private Dictionary<bool, List<BestiaryRecipesDat>>? byUnknown73;
    private Dictionary<int, List<BestiaryRecipesDat>>? byUnknown74;
    private Dictionary<int, List<BestiaryRecipesDat>>? byUnknown78;
    private Dictionary<int, List<BestiaryRecipesDat>>? byUnknown82;
    private Dictionary<int, List<BestiaryRecipesDat>>? byFlaskMod;

    /// <summary>
    /// Initializes a new instance of the <see cref="BestiaryRecipesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BestiaryRecipesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BestiaryRecipesDat? item)
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
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
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryRecipesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out BestiaryRecipesDat? item)
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
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
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryRecipesDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.BestiaryRecipeComponentKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBestiaryRecipeComponentKeys(int? key, out BestiaryRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBestiaryRecipeComponentKeys(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.BestiaryRecipeComponentKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBestiaryRecipeComponentKeys(int? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        if (byBestiaryRecipeComponentKeys is null)
        {
            byBestiaryRecipeComponentKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.BestiaryRecipeComponentKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byBestiaryRecipeComponentKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBestiaryRecipeComponentKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBestiaryRecipeComponentKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byBestiaryRecipeComponentKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipesDat>> GetManyToManyByBestiaryRecipeComponentKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBestiaryRecipeComponentKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Notes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNotes(string? key, out BestiaryRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNotes(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Notes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNotes(string? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        if (byNotes is null)
        {
            byNotes = new();
            foreach (var item in Items)
            {
                var itemKey = item.Notes;

                if (!byNotes.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNotes.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNotes.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byNotes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryRecipesDat>> GetManyToManyByNotes(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNotes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Category"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCategory(int? key, out BestiaryRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCategory(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Category"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCategory(int? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        if (byCategory is null)
        {
            byCategory = new();
            foreach (var item in Items)
            {
                var itemKey = item.Category;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCategory.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCategory.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCategory.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byCategory"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipesDat>> GetManyToManyByCategory(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCategory(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(bool? key, out BestiaryRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(bool? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;

                if (!byUnknown56.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BestiaryRecipesDat>> GetManyToManyByUnknown56(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<bool, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements(int? key, out BestiaryRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievements(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements(int? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        if (byAchievements is null)
        {
            byAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievements;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipesDat>> GetManyToManyByAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown73(bool? key, out BestiaryRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown73(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown73(bool? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        if (byUnknown73 is null)
        {
            byUnknown73 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown73;

                if (!byUnknown73.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown73.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown73.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byUnknown73"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BestiaryRecipesDat>> GetManyToManyByUnknown73(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<bool, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown73(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown74(int? key, out BestiaryRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown74(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown74(int? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        if (byUnknown74 is null)
        {
            byUnknown74 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown74;

                if (!byUnknown74.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown74.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown74.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byUnknown74"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipesDat>> GetManyToManyByUnknown74(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown74(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown78(int? key, out BestiaryRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown78(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown78(int? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        if (byUnknown78 is null)
        {
            byUnknown78 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown78;

                if (!byUnknown78.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown78.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown78.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byUnknown78"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipesDat>> GetManyToManyByUnknown78(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown78(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown82(int? key, out BestiaryRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown82(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown82(int? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        if (byUnknown82 is null)
        {
            byUnknown82 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown82;

                if (!byUnknown82.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown82.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown82.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byUnknown82"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipesDat>> GetManyToManyByUnknown82(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown82(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.FlaskMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlaskMod(int? key, out BestiaryRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlaskMod(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.FlaskMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlaskMod(int? key, out IReadOnlyList<BestiaryRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        if (byFlaskMod is null)
        {
            byFlaskMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlaskMod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFlaskMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFlaskMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFlaskMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipesDat"/> with <see cref="BestiaryRecipesDat.byFlaskMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryRecipesDat>> GetManyToManyByFlaskMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryRecipesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlaskMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BestiaryRecipesDat[] Load()
    {
        const string filePath = "Data/BestiaryRecipes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryRecipesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BestiaryRecipeComponentKeys
            (var tempbestiaryrecipecomponentkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var bestiaryrecipecomponentkeysLoading = tempbestiaryrecipecomponentkeysLoading.AsReadOnly();

            // loading Notes
            (var notesLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Category
            (var categoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FlaskMod
            (var flaskmodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryRecipesDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                BestiaryRecipeComponentKeys = bestiaryrecipecomponentkeysLoading,
                Notes = notesLoading,
                Category = categoryLoading,
                Unknown56 = unknown56Loading,
                Achievements = achievementsLoading,
                Unknown73 = unknown73Loading,
                Unknown74 = unknown74Loading,
                Unknown78 = unknown78Loading,
                Unknown82 = unknown82Loading,
                FlaskMod = flaskmodLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
