using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LeagueInfoDat"/> related data and helper methods.
/// </summary>
public sealed class LeagueInfoRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LeagueInfoDat> Items { get; }

    private Dictionary<string, List<LeagueInfoDat>>? byId;
    private Dictionary<string, List<LeagueInfoDat>>? byPanelImage;
    private Dictionary<string, List<LeagueInfoDat>>? byHeaderImage;
    private Dictionary<string, List<LeagueInfoDat>>? byScreenshots;
    private Dictionary<string, List<LeagueInfoDat>>? byDescription;
    private Dictionary<string, List<LeagueInfoDat>>? byLeague;
    private Dictionary<bool, List<LeagueInfoDat>>? byUnknown56;
    private Dictionary<string, List<LeagueInfoDat>>? byTrailerVideoLink;
    private Dictionary<string, List<LeagueInfoDat>>? byBackgroundImage;
    private Dictionary<bool, List<LeagueInfoDat>>? byUnknown73;
    private Dictionary<bool, List<LeagueInfoDat>>? byUnknown74;
    private Dictionary<string, List<LeagueInfoDat>>? byPanelItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="LeagueInfoRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LeagueInfoRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out LeagueInfoDat? item)
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
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
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueInfoDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<string, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.PanelImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPanelImage(string? key, out LeagueInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPanelImage(key, out var items))
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.PanelImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPanelImage(string? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        if (byPanelImage is null)
        {
            byPanelImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.PanelImage;

                if (!byPanelImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPanelImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPanelImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byPanelImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueInfoDat>> GetManyToManyByPanelImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<string, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPanelImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.HeaderImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeaderImage(string? key, out LeagueInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeaderImage(key, out var items))
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.HeaderImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeaderImage(string? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        if (byHeaderImage is null)
        {
            byHeaderImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeaderImage;

                if (!byHeaderImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeaderImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeaderImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byHeaderImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueInfoDat>> GetManyToManyByHeaderImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<string, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeaderImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Screenshots"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScreenshots(string? key, out LeagueInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScreenshots(key, out var items))
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Screenshots"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScreenshots(string? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        if (byScreenshots is null)
        {
            byScreenshots = new();
            foreach (var item in Items)
            {
                var itemKey = item.Screenshots;
                foreach (var listKey in itemKey)
                {
                    if (!byScreenshots.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byScreenshots.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byScreenshots.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byScreenshots"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueInfoDat>> GetManyToManyByScreenshots(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<string, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScreenshots(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out LeagueInfoDat? item)
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
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
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueInfoDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<string, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.League"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLeague(string? key, out LeagueInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLeague(key, out var items))
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.League"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLeague(string? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        if (byLeague is null)
        {
            byLeague = new();
            foreach (var item in Items)
            {
                var itemKey = item.League;

                if (!byLeague.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLeague.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLeague.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byLeague"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueInfoDat>> GetManyToManyByLeague(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<string, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLeague(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(bool? key, out LeagueInfoDat? item)
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(bool? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
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
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LeagueInfoDat>> GetManyToManyByUnknown56(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<bool, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.TrailerVideoLink"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTrailerVideoLink(string? key, out LeagueInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTrailerVideoLink(key, out var items))
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.TrailerVideoLink"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTrailerVideoLink(string? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        if (byTrailerVideoLink is null)
        {
            byTrailerVideoLink = new();
            foreach (var item in Items)
            {
                var itemKey = item.TrailerVideoLink;

                if (!byTrailerVideoLink.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTrailerVideoLink.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTrailerVideoLink.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byTrailerVideoLink"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueInfoDat>> GetManyToManyByTrailerVideoLink(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<string, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTrailerVideoLink(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.BackgroundImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBackgroundImage(string? key, out LeagueInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBackgroundImage(key, out var items))
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.BackgroundImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBackgroundImage(string? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        if (byBackgroundImage is null)
        {
            byBackgroundImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.BackgroundImage;

                if (!byBackgroundImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBackgroundImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBackgroundImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byBackgroundImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueInfoDat>> GetManyToManyByBackgroundImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<string, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBackgroundImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown73(bool? key, out LeagueInfoDat? item)
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown73(bool? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
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
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byUnknown73"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LeagueInfoDat>> GetManyToManyByUnknown73(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<bool, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown73(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown74(bool? key, out LeagueInfoDat? item)
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown74(bool? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
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
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byUnknown74"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LeagueInfoDat>> GetManyToManyByUnknown74(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<bool, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown74(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.PanelItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPanelItems(string? key, out LeagueInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPanelItems(key, out var items))
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
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.PanelItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPanelItems(string? key, out IReadOnlyList<LeagueInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        if (byPanelItems is null)
        {
            byPanelItems = new();
            foreach (var item in Items)
            {
                var itemKey = item.PanelItems;
                foreach (var listKey in itemKey)
                {
                    if (!byPanelItems.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPanelItems.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPanelItems.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LeagueInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueInfoDat"/> with <see cref="LeagueInfoDat.byPanelItems"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueInfoDat>> GetManyToManyByPanelItems(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueInfoDat>>();
        }

        var items = new List<ResultItem<string, LeagueInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPanelItems(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LeagueInfoDat[] Load()
    {
        const string filePath = "Data/LeagueInfo.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LeagueInfoDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PanelImage
            (var panelimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeaderImage
            (var headerimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Screenshots
            (var tempscreenshotsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var screenshotsLoading = tempscreenshotsLoading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading League
            (var leagueLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TrailerVideoLink
            (var trailervideolinkLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BackgroundImage
            (var backgroundimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading PanelItems
            (var temppanelitemsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var panelitemsLoading = temppanelitemsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LeagueInfoDat()
            {
                Id = idLoading,
                PanelImage = panelimageLoading,
                HeaderImage = headerimageLoading,
                Screenshots = screenshotsLoading,
                Description = descriptionLoading,
                League = leagueLoading,
                Unknown56 = unknown56Loading,
                TrailerVideoLink = trailervideolinkLoading,
                BackgroundImage = backgroundimageLoading,
                Unknown73 = unknown73Loading,
                Unknown74 = unknown74Loading,
                PanelItems = panelitemsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
