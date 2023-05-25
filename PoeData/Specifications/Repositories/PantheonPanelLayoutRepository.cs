using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PantheonPanelLayoutDat"/> related data and helper methods.
/// </summary>
public sealed class PantheonPanelLayoutRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PantheonPanelLayoutDat> Items { get; }

    private Dictionary<string, List<PantheonPanelLayoutDat>>? byId;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byX;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byY;
    private Dictionary<bool, List<PantheonPanelLayoutDat>>? byIsMajorGod;
    private Dictionary<string, List<PantheonPanelLayoutDat>>? byCoverImage;
    private Dictionary<string, List<PantheonPanelLayoutDat>>? byGodName2;
    private Dictionary<string, List<PantheonPanelLayoutDat>>? bySelectionImage;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byEffect1_StatsKeys;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byEffect1_Values;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byEffect2_StatsKeys;
    private Dictionary<string, List<PantheonPanelLayoutDat>>? byGodName3;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byEffect3_Values;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byEffect3_StatsKeys;
    private Dictionary<string, List<PantheonPanelLayoutDat>>? byGodName4;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byEffect4_StatsKeys;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byEffect4_Values;
    private Dictionary<string, List<PantheonPanelLayoutDat>>? byGodName1;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byEffect2_Values;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byQuestState1;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byQuestState2;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byQuestState3;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byQuestState4;
    private Dictionary<bool, List<PantheonPanelLayoutDat>>? byIsDisabled;
    private Dictionary<int, List<PantheonPanelLayoutDat>>? byAchievementItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="PantheonPanelLayoutRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PantheonPanelLayoutRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out PantheonPanelLayoutDat? item)
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
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
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PantheonPanelLayoutDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<string, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.X"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByX(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByX(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.X"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByX(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byX is null)
        {
            byX = new();
            foreach (var item in Items)
            {
                var itemKey = item.X;

                if (!byX.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byX.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byX.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byX"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByX(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByX(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Y"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByY(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByY(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Y"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByY(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byY is null)
        {
            byY = new();
            foreach (var item in Items)
            {
                var itemKey = item.Y;

                if (!byY.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byY.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byY.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byY"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByY(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByY(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.IsMajorGod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsMajorGod(bool? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsMajorGod(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.IsMajorGod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsMajorGod(bool? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byIsMajorGod is null)
        {
            byIsMajorGod = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsMajorGod;

                if (!byIsMajorGod.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsMajorGod.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsMajorGod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byIsMajorGod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PantheonPanelLayoutDat>> GetManyToManyByIsMajorGod(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<bool, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsMajorGod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.CoverImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCoverImage(string? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCoverImage(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.CoverImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCoverImage(string? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byCoverImage is null)
        {
            byCoverImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.CoverImage;

                if (!byCoverImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCoverImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCoverImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byCoverImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PantheonPanelLayoutDat>> GetManyToManyByCoverImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<string, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCoverImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.GodName2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGodName2(string? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGodName2(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.GodName2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGodName2(string? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byGodName2 is null)
        {
            byGodName2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.GodName2;

                if (!byGodName2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGodName2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGodName2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byGodName2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PantheonPanelLayoutDat>> GetManyToManyByGodName2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<string, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGodName2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.SelectionImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySelectionImage(string? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySelectionImage(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.SelectionImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySelectionImage(string? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (bySelectionImage is null)
        {
            bySelectionImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.SelectionImage;

                if (!bySelectionImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySelectionImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySelectionImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.bySelectionImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PantheonPanelLayoutDat>> GetManyToManyBySelectionImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<string, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySelectionImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect1_StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEffect1_StatsKeys(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEffect1_StatsKeys(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect1_StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEffect1_StatsKeys(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byEffect1_StatsKeys is null)
        {
            byEffect1_StatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Effect1_StatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byEffect1_StatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEffect1_StatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEffect1_StatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byEffect1_StatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByEffect1_StatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEffect1_StatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect1_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEffect1_Values(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEffect1_Values(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect1_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEffect1_Values(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byEffect1_Values is null)
        {
            byEffect1_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.Effect1_Values;
                foreach (var listKey in itemKey)
                {
                    if (!byEffect1_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEffect1_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEffect1_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byEffect1_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByEffect1_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEffect1_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect2_StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEffect2_StatsKeys(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEffect2_StatsKeys(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect2_StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEffect2_StatsKeys(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byEffect2_StatsKeys is null)
        {
            byEffect2_StatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Effect2_StatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byEffect2_StatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEffect2_StatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEffect2_StatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byEffect2_StatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByEffect2_StatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEffect2_StatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.GodName3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGodName3(string? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGodName3(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.GodName3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGodName3(string? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byGodName3 is null)
        {
            byGodName3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.GodName3;

                if (!byGodName3.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGodName3.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGodName3.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byGodName3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PantheonPanelLayoutDat>> GetManyToManyByGodName3(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<string, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGodName3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect3_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEffect3_Values(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEffect3_Values(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect3_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEffect3_Values(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byEffect3_Values is null)
        {
            byEffect3_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.Effect3_Values;
                foreach (var listKey in itemKey)
                {
                    if (!byEffect3_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEffect3_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEffect3_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byEffect3_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByEffect3_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEffect3_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect3_StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEffect3_StatsKeys(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEffect3_StatsKeys(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect3_StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEffect3_StatsKeys(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byEffect3_StatsKeys is null)
        {
            byEffect3_StatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Effect3_StatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byEffect3_StatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEffect3_StatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEffect3_StatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byEffect3_StatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByEffect3_StatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEffect3_StatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.GodName4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGodName4(string? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGodName4(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.GodName4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGodName4(string? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byGodName4 is null)
        {
            byGodName4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.GodName4;

                if (!byGodName4.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGodName4.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGodName4.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byGodName4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PantheonPanelLayoutDat>> GetManyToManyByGodName4(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<string, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGodName4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect4_StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEffect4_StatsKeys(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEffect4_StatsKeys(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect4_StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEffect4_StatsKeys(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byEffect4_StatsKeys is null)
        {
            byEffect4_StatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Effect4_StatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byEffect4_StatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEffect4_StatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEffect4_StatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byEffect4_StatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByEffect4_StatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEffect4_StatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect4_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEffect4_Values(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEffect4_Values(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect4_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEffect4_Values(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byEffect4_Values is null)
        {
            byEffect4_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.Effect4_Values;
                foreach (var listKey in itemKey)
                {
                    if (!byEffect4_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEffect4_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEffect4_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byEffect4_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByEffect4_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEffect4_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.GodName1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGodName1(string? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGodName1(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.GodName1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGodName1(string? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byGodName1 is null)
        {
            byGodName1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.GodName1;

                if (!byGodName1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGodName1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGodName1.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byGodName1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PantheonPanelLayoutDat>> GetManyToManyByGodName1(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<string, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGodName1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect2_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEffect2_Values(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEffect2_Values(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.Effect2_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEffect2_Values(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byEffect2_Values is null)
        {
            byEffect2_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.Effect2_Values;
                foreach (var listKey in itemKey)
                {
                    if (!byEffect2_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEffect2_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEffect2_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byEffect2_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByEffect2_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEffect2_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.QuestState1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestState1(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestState1(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.QuestState1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestState1(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byQuestState1 is null)
        {
            byQuestState1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestState1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestState1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestState1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestState1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byQuestState1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByQuestState1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestState1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.QuestState2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestState2(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestState2(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.QuestState2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestState2(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byQuestState2 is null)
        {
            byQuestState2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestState2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestState2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestState2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestState2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byQuestState2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByQuestState2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestState2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.QuestState3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestState3(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestState3(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.QuestState3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestState3(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byQuestState3 is null)
        {
            byQuestState3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestState3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestState3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestState3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestState3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byQuestState3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByQuestState3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestState3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.QuestState4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestState4(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestState4(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.QuestState4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestState4(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byQuestState4 is null)
        {
            byQuestState4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestState4;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestState4.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestState4.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestState4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byQuestState4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByQuestState4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestState4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.IsDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsDisabled(bool? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsDisabled(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.IsDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsDisabled(bool? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byIsDisabled is null)
        {
            byIsDisabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsDisabled;

                if (!byIsDisabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsDisabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsDisabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byIsDisabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PantheonPanelLayoutDat>> GetManyToManyByIsDisabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<bool, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsDisabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.AchievementItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItems(int? key, out PantheonPanelLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItems(key, out var items))
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
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.AchievementItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItems(int? key, out IReadOnlyList<PantheonPanelLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        if (byAchievementItems is null)
        {
            byAchievementItems = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItems;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItems.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItems.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItems.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PantheonPanelLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PantheonPanelLayoutDat"/> with <see cref="PantheonPanelLayoutDat.byAchievementItems"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PantheonPanelLayoutDat>> GetManyToManyByAchievementItems(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PantheonPanelLayoutDat>>();
        }

        var items = new List<ResultItem<int, PantheonPanelLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItems(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PantheonPanelLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PantheonPanelLayoutDat[] Load()
    {
        const string filePath = "Data/PantheonPanelLayout.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PantheonPanelLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading X
            (var xLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Y
            (var yLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsMajorGod
            (var ismajorgodLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CoverImage
            (var coverimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GodName2
            (var godname2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SelectionImage
            (var selectionimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Effect1_StatsKeys
            (var tempeffect1_statskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var effect1_statskeysLoading = tempeffect1_statskeysLoading.AsReadOnly();

            // loading Effect1_Values
            (var tempeffect1_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var effect1_valuesLoading = tempeffect1_valuesLoading.AsReadOnly();

            // loading Effect2_StatsKeys
            (var tempeffect2_statskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var effect2_statskeysLoading = tempeffect2_statskeysLoading.AsReadOnly();

            // loading GodName3
            (var godname3Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Effect3_Values
            (var tempeffect3_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var effect3_valuesLoading = tempeffect3_valuesLoading.AsReadOnly();

            // loading Effect3_StatsKeys
            (var tempeffect3_statskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var effect3_statskeysLoading = tempeffect3_statskeysLoading.AsReadOnly();

            // loading GodName4
            (var godname4Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Effect4_StatsKeys
            (var tempeffect4_statskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var effect4_statskeysLoading = tempeffect4_statskeysLoading.AsReadOnly();

            // loading Effect4_Values
            (var tempeffect4_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var effect4_valuesLoading = tempeffect4_valuesLoading.AsReadOnly();

            // loading GodName1
            (var godname1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Effect2_Values
            (var tempeffect2_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var effect2_valuesLoading = tempeffect2_valuesLoading.AsReadOnly();

            // loading QuestState1
            (var queststate1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading QuestState2
            (var queststate2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading QuestState3
            (var queststate3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading QuestState4
            (var queststate4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsDisabled
            (var isdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AchievementItems
            (var tempachievementitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemsLoading = tempachievementitemsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PantheonPanelLayoutDat()
            {
                Id = idLoading,
                X = xLoading,
                Y = yLoading,
                IsMajorGod = ismajorgodLoading,
                CoverImage = coverimageLoading,
                GodName2 = godname2Loading,
                SelectionImage = selectionimageLoading,
                Effect1_StatsKeys = effect1_statskeysLoading,
                Effect1_Values = effect1_valuesLoading,
                Effect2_StatsKeys = effect2_statskeysLoading,
                GodName3 = godname3Loading,
                Effect3_Values = effect3_valuesLoading,
                Effect3_StatsKeys = effect3_statskeysLoading,
                GodName4 = godname4Loading,
                Effect4_StatsKeys = effect4_statskeysLoading,
                Effect4_Values = effect4_valuesLoading,
                GodName1 = godname1Loading,
                Effect2_Values = effect2_valuesLoading,
                QuestState1 = queststate1Loading,
                QuestState2 = queststate2Loading,
                QuestState3 = queststate3Loading,
                QuestState4 = queststate4Loading,
                IsDisabled = isdisabledLoading,
                AchievementItems = achievementitemsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
