using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapDeviceRecipesDat"/> related data and helper methods.
/// </summary>
public sealed class MapDeviceRecipesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapDeviceRecipesDat> Items { get; }

    private Dictionary<string, List<MapDeviceRecipesDat>>? byId;
    private Dictionary<int, List<MapDeviceRecipesDat>>? byRecipeItems;
    private Dictionary<int, List<MapDeviceRecipesDat>>? byWorldArea;
    private Dictionary<int, List<MapDeviceRecipesDat>>? byMicrotransactionPortalVariation;
    private Dictionary<int, List<MapDeviceRecipesDat>>? byAreaLevel;
    private Dictionary<int, List<MapDeviceRecipesDat>>? byUnknown60;
    private Dictionary<int, List<MapDeviceRecipesDat>>? byUnknown76;
    private Dictionary<bool, List<MapDeviceRecipesDat>>? byUnknown80;
    private Dictionary<bool, List<MapDeviceRecipesDat>>? byUnknown81;
    private Dictionary<bool, List<MapDeviceRecipesDat>>? byUnknown82;
    private Dictionary<int, List<MapDeviceRecipesDat>>? byOpenAchievemnts;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapDeviceRecipesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapDeviceRecipesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MapDeviceRecipesDat? item)
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
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
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapDeviceRecipesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<string, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.RecipeItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRecipeItems(int? key, out MapDeviceRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRecipeItems(key, out var items))
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.RecipeItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRecipeItems(int? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        if (byRecipeItems is null)
        {
            byRecipeItems = new();
            foreach (var item in Items)
            {
                var itemKey = item.RecipeItems;
                foreach (var listKey in itemKey)
                {
                    if (!byRecipeItems.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byRecipeItems.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byRecipeItems.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byRecipeItems"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapDeviceRecipesDat>> GetManyToManyByRecipeItems(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<int, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRecipeItems(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.WorldArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldArea(int? key, out MapDeviceRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldArea(key, out var items))
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.WorldArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldArea(int? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        if (byWorldArea is null)
        {
            byWorldArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldArea;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldArea.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldArea.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byWorldArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapDeviceRecipesDat>> GetManyToManyByWorldArea(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<int, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.MicrotransactionPortalVariation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMicrotransactionPortalVariation(int? key, out MapDeviceRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMicrotransactionPortalVariation(key, out var items))
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.MicrotransactionPortalVariation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMicrotransactionPortalVariation(int? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        if (byMicrotransactionPortalVariation is null)
        {
            byMicrotransactionPortalVariation = new();
            foreach (var item in Items)
            {
                var itemKey = item.MicrotransactionPortalVariation;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMicrotransactionPortalVariation.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMicrotransactionPortalVariation.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMicrotransactionPortalVariation.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byMicrotransactionPortalVariation"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapDeviceRecipesDat>> GetManyToManyByMicrotransactionPortalVariation(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<int, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMicrotransactionPortalVariation(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAreaLevel(int? key, out MapDeviceRecipesDat? item)
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAreaLevel(int? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
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
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byAreaLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapDeviceRecipesDat>> GetManyToManyByAreaLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<int, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAreaLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(int? key, out MapDeviceRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown60(key, out var items))
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(int? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        if (byUnknown60 is null)
        {
            byUnknown60 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown60;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown60.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown60.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown60.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapDeviceRecipesDat>> GetManyToManyByUnknown60(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<int, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(int? key, out MapDeviceRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown76(key, out var items))
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(int? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        if (byUnknown76 is null)
        {
            byUnknown76 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown76;

                if (!byUnknown76.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown76.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown76.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapDeviceRecipesDat>> GetManyToManyByUnknown76(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<int, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(bool? key, out MapDeviceRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown80(key, out var items))
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(bool? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        if (byUnknown80 is null)
        {
            byUnknown80 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown80;

                if (!byUnknown80.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown80.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown80.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MapDeviceRecipesDat>> GetManyToManyByUnknown80(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<bool, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Unknown81"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown81(bool? key, out MapDeviceRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown81(key, out var items))
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Unknown81"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown81(bool? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        if (byUnknown81 is null)
        {
            byUnknown81 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown81;

                if (!byUnknown81.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown81.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown81.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byUnknown81"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MapDeviceRecipesDat>> GetManyToManyByUnknown81(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<bool, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown81(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown82(bool? key, out MapDeviceRecipesDat? item)
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown82(bool? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
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
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byUnknown82"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MapDeviceRecipesDat>> GetManyToManyByUnknown82(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<bool, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown82(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.OpenAchievemnts"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOpenAchievemnts(int? key, out MapDeviceRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOpenAchievemnts(key, out var items))
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
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.OpenAchievemnts"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOpenAchievemnts(int? key, out IReadOnlyList<MapDeviceRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        if (byOpenAchievemnts is null)
        {
            byOpenAchievemnts = new();
            foreach (var item in Items)
            {
                var itemKey = item.OpenAchievemnts;
                foreach (var listKey in itemKey)
                {
                    if (!byOpenAchievemnts.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byOpenAchievemnts.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byOpenAchievemnts.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapDeviceRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapDeviceRecipesDat"/> with <see cref="MapDeviceRecipesDat.byOpenAchievemnts"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapDeviceRecipesDat>> GetManyToManyByOpenAchievemnts(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapDeviceRecipesDat>>();
        }

        var items = new List<ResultItem<int, MapDeviceRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOpenAchievemnts(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapDeviceRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapDeviceRecipesDat[] Load()
    {
        const string filePath = "Data/MapDeviceRecipes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapDeviceRecipesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RecipeItems
            (var temprecipeitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var recipeitemsLoading = temprecipeitemsLoading.AsReadOnly();

            // loading WorldArea
            (var worldareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MicrotransactionPortalVariation
            (var microtransactionportalvariationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown81
            (var unknown81Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading OpenAchievemnts
            (var tempopenachievemntsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var openachievemntsLoading = tempopenachievemntsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapDeviceRecipesDat()
            {
                Id = idLoading,
                RecipeItems = recipeitemsLoading,
                WorldArea = worldareaLoading,
                MicrotransactionPortalVariation = microtransactionportalvariationLoading,
                AreaLevel = arealevelLoading,
                Unknown60 = unknown60Loading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
                Unknown81 = unknown81Loading,
                Unknown82 = unknown82Loading,
                OpenAchievemnts = openachievemntsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
