using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ExpeditionDealsDat"/> related data and helper methods.
/// </summary>
public sealed class ExpeditionDealsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ExpeditionDealsDat> Items { get; }

    private Dictionary<int, List<ExpeditionDealsDat>>? byId;
    private Dictionary<string, List<ExpeditionDealsDat>>? byFunction;
    private Dictionary<string, List<ExpeditionDealsDat>>? byArguments;
    private Dictionary<int, List<ExpeditionDealsDat>>? byTextAudio;
    private Dictionary<string, List<ExpeditionDealsDat>>? byDescription;
    private Dictionary<int, List<ExpeditionDealsDat>>? byBuyAchievements;
    private Dictionary<int, List<ExpeditionDealsDat>>? byUnknown60;
    private Dictionary<int, List<ExpeditionDealsDat>>? byDealFamily;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpeditionDealsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ExpeditionDealsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(int? key, out ExpeditionDealsDat? item)
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
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(int? key, out IReadOnlyList<ExpeditionDealsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionDealsDat>();
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

        if (!byId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionDealsDat>> GetManyToManyById(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionDealsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionDealsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionDealsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.Function"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFunction(string? key, out ExpeditionDealsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFunction(key, out var items))
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
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.Function"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFunction(string? key, out IReadOnlyList<ExpeditionDealsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        if (byFunction is null)
        {
            byFunction = new();
            foreach (var item in Items)
            {
                var itemKey = item.Function;

                if (!byFunction.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFunction.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFunction.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.byFunction"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExpeditionDealsDat>> GetManyToManyByFunction(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExpeditionDealsDat>>();
        }

        var items = new List<ResultItem<string, ExpeditionDealsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFunction(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExpeditionDealsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.Arguments"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArguments(string? key, out ExpeditionDealsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArguments(key, out var items))
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
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.Arguments"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArguments(string? key, out IReadOnlyList<ExpeditionDealsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        if (byArguments is null)
        {
            byArguments = new();
            foreach (var item in Items)
            {
                var itemKey = item.Arguments;

                if (!byArguments.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArguments.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArguments.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.byArguments"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExpeditionDealsDat>> GetManyToManyByArguments(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExpeditionDealsDat>>();
        }

        var items = new List<ResultItem<string, ExpeditionDealsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArguments(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExpeditionDealsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudio(int? key, out ExpeditionDealsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTextAudio(key, out var items))
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
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudio(int? key, out IReadOnlyList<ExpeditionDealsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        if (byTextAudio is null)
        {
            byTextAudio = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextAudio;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTextAudio.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTextAudio.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTextAudio.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.byTextAudio"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionDealsDat>> GetManyToManyByTextAudio(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionDealsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionDealsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudio(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionDealsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out ExpeditionDealsDat? item)
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
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<ExpeditionDealsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionDealsDat>();
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
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExpeditionDealsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExpeditionDealsDat>>();
        }

        var items = new List<ResultItem<string, ExpeditionDealsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExpeditionDealsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.BuyAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuyAchievements(int? key, out ExpeditionDealsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuyAchievements(key, out var items))
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
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.BuyAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuyAchievements(int? key, out IReadOnlyList<ExpeditionDealsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        if (byBuyAchievements is null)
        {
            byBuyAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuyAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byBuyAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBuyAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBuyAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.byBuyAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionDealsDat>> GetManyToManyByBuyAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionDealsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionDealsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuyAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionDealsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(int? key, out ExpeditionDealsDat? item)
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
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(int? key, out IReadOnlyList<ExpeditionDealsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionDealsDat>();
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
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionDealsDat>> GetManyToManyByUnknown60(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionDealsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionDealsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionDealsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.DealFamily"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDealFamily(int? key, out ExpeditionDealsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDealFamily(key, out var items))
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
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.DealFamily"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDealFamily(int? key, out IReadOnlyList<ExpeditionDealsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        if (byDealFamily is null)
        {
            byDealFamily = new();
            foreach (var item in Items)
            {
                var itemKey = item.DealFamily;

                if (!byDealFamily.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDealFamily.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDealFamily.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionDealsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionDealsDat"/> with <see cref="ExpeditionDealsDat.byDealFamily"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionDealsDat>> GetManyToManyByDealFamily(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionDealsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionDealsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDealFamily(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionDealsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ExpeditionDealsDat[] Load()
    {
        const string filePath = "Data/ExpeditionDeals.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionDealsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Function
            (var functionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Arguments
            (var argumentsLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuyAchievements
            (var tempbuyachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var buyachievementsLoading = tempbuyachievementsLoading.AsReadOnly();

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DealFamily
            (var dealfamilyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionDealsDat()
            {
                Id = idLoading,
                Function = functionLoading,
                Arguments = argumentsLoading,
                TextAudio = textaudioLoading,
                Description = descriptionLoading,
                BuyAchievements = buyachievementsLoading,
                Unknown60 = unknown60Loading,
                DealFamily = dealfamilyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
