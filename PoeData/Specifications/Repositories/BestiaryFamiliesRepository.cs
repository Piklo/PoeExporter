using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BestiaryFamiliesDat"/> related data and helper methods.
/// </summary>
public sealed class BestiaryFamiliesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BestiaryFamiliesDat> Items { get; }

    private Dictionary<string, List<BestiaryFamiliesDat>>? byId;
    private Dictionary<string, List<BestiaryFamiliesDat>>? byName;
    private Dictionary<string, List<BestiaryFamiliesDat>>? byIcon;
    private Dictionary<string, List<BestiaryFamiliesDat>>? byIconSmall;
    private Dictionary<string, List<BestiaryFamiliesDat>>? byIllustration;
    private Dictionary<string, List<BestiaryFamiliesDat>>? byPageArt;
    private Dictionary<string, List<BestiaryFamiliesDat>>? byFlavourText;
    private Dictionary<bool, List<BestiaryFamiliesDat>>? byUnknown56;
    private Dictionary<int, List<BestiaryFamiliesDat>>? byTagsKey;
    private Dictionary<int, List<BestiaryFamiliesDat>>? byUnknown73;
    private Dictionary<int, List<BestiaryFamiliesDat>>? byModsKeys;
    private Dictionary<int, List<BestiaryFamiliesDat>>? byCurrencyItemsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="BestiaryFamiliesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BestiaryFamiliesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BestiaryFamiliesDat? item)
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
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
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryFamiliesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out BestiaryFamiliesDat? item)
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
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
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryFamiliesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon(string? key, out BestiaryFamiliesDat? item)
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon(string? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
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
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryFamiliesDat>> GetManyToManyByIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.IconSmall"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIconSmall(string? key, out BestiaryFamiliesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIconSmall(key, out var items))
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.IconSmall"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIconSmall(string? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        if (byIconSmall is null)
        {
            byIconSmall = new();
            foreach (var item in Items)
            {
                var itemKey = item.IconSmall;

                if (!byIconSmall.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIconSmall.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIconSmall.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byIconSmall"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryFamiliesDat>> GetManyToManyByIconSmall(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIconSmall(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Illustration"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIllustration(string? key, out BestiaryFamiliesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIllustration(key, out var items))
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Illustration"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIllustration(string? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        if (byIllustration is null)
        {
            byIllustration = new();
            foreach (var item in Items)
            {
                var itemKey = item.Illustration;

                if (!byIllustration.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIllustration.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIllustration.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byIllustration"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryFamiliesDat>> GetManyToManyByIllustration(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIllustration(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.PageArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPageArt(string? key, out BestiaryFamiliesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPageArt(key, out var items))
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.PageArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPageArt(string? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        if (byPageArt is null)
        {
            byPageArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.PageArt;

                if (!byPageArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPageArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPageArt.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byPageArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryFamiliesDat>> GetManyToManyByPageArt(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPageArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourText(string? key, out BestiaryFamiliesDat? item)
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourText(string? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
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
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byFlavourText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryFamiliesDat>> GetManyToManyByFlavourText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(bool? key, out BestiaryFamiliesDat? item)
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(bool? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
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
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BestiaryFamiliesDat>> GetManyToManyByUnknown56(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<bool, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.TagsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTagsKey(int? key, out BestiaryFamiliesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTagsKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.TagsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTagsKey(int? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        if (byTagsKey is null)
        {
            byTagsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.TagsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTagsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTagsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTagsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byTagsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryFamiliesDat>> GetManyToManyByTagsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTagsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown73(int? key, out BestiaryFamiliesDat? item)
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown73(int? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
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
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byUnknown73"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryFamiliesDat>> GetManyToManyByUnknown73(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown73(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKeys(int? key, out BestiaryFamiliesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKeys(key, out var items))
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKeys(int? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        if (byModsKeys is null)
        {
            byModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryFamiliesDat>> GetManyToManyByModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.CurrencyItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCurrencyItemsKey(int? key, out BestiaryFamiliesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCurrencyItemsKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.CurrencyItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCurrencyItemsKey(int? key, out IReadOnlyList<BestiaryFamiliesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        if (byCurrencyItemsKey is null)
        {
            byCurrencyItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.CurrencyItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCurrencyItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCurrencyItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCurrencyItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryFamiliesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryFamiliesDat"/> with <see cref="BestiaryFamiliesDat.byCurrencyItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryFamiliesDat>> GetManyToManyByCurrencyItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryFamiliesDat>>();
        }

        var items = new List<ResultItem<int, BestiaryFamiliesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCurrencyItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryFamiliesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BestiaryFamiliesDat[] Load()
    {
        const string filePath = "Data/BestiaryFamilies.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryFamiliesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IconSmall
            (var iconsmallLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Illustration
            (var illustrationLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PageArt
            (var pageartLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TagsKey
            (var tagskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading CurrencyItemsKey
            (var currencyitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryFamiliesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Icon = iconLoading,
                IconSmall = iconsmallLoading,
                Illustration = illustrationLoading,
                PageArt = pageartLoading,
                FlavourText = flavourtextLoading,
                Unknown56 = unknown56Loading,
                TagsKey = tagskeyLoading,
                Unknown73 = unknown73Loading,
                ModsKeys = modskeysLoading,
                CurrencyItemsKey = currencyitemskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
