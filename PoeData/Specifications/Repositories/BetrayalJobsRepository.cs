using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BetrayalJobsDat"/> related data and helper methods.
/// </summary>
public sealed class BetrayalJobsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BetrayalJobsDat> Items { get; }

    private Dictionary<string, List<BetrayalJobsDat>>? byId;
    private Dictionary<string, List<BetrayalJobsDat>>? byText;
    private Dictionary<int, List<BetrayalJobsDat>>? byExtraTerrainFeaturesKey;
    private Dictionary<string, List<BetrayalJobsDat>>? byArt;
    private Dictionary<int, List<BetrayalJobsDat>>? byUnknown40;
    private Dictionary<int, List<BetrayalJobsDat>>? byUnknown44;
    private Dictionary<int, List<BetrayalJobsDat>>? byWorldAreasKey;
    private Dictionary<int, List<BetrayalJobsDat>>? byCompletion_AchievementItemsKey;
    private Dictionary<int, List<BetrayalJobsDat>>? byOpenChests_AchievementItemsKey;
    private Dictionary<int, List<BetrayalJobsDat>>? byMissionCompletion_AcheivementItemsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="BetrayalJobsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BetrayalJobsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BetrayalJobsDat? item)
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
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BetrayalJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalJobsDat>();
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
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalJobsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalJobsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out BetrayalJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByText(key, out var items))
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
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<BetrayalJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        if (byText is null)
        {
            byText = new();
            foreach (var item in Items)
            {
                var itemKey = item.Text;

                if (!byText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalJobsDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalJobsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.ExtraTerrainFeaturesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExtraTerrainFeaturesKey(int? key, out BetrayalJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExtraTerrainFeaturesKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.ExtraTerrainFeaturesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExtraTerrainFeaturesKey(int? key, out IReadOnlyList<BetrayalJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        if (byExtraTerrainFeaturesKey is null)
        {
            byExtraTerrainFeaturesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ExtraTerrainFeaturesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byExtraTerrainFeaturesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byExtraTerrainFeaturesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byExtraTerrainFeaturesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.byExtraTerrainFeaturesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalJobsDat>> GetManyToManyByExtraTerrainFeaturesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalJobsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExtraTerrainFeaturesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArt(string? key, out BetrayalJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArt(key, out var items))
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
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArt(string? key, out IReadOnlyList<BetrayalJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        if (byArt is null)
        {
            byArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.Art;

                if (!byArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArt.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.byArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalJobsDat>> GetManyToManyByArt(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalJobsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out BetrayalJobsDat? item)
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
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<BetrayalJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalJobsDat>();
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
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalJobsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalJobsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out BetrayalJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown44(key, out var items))
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
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<BetrayalJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        if (byUnknown44 is null)
        {
            byUnknown44 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown44;

                if (!byUnknown44.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown44.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown44.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalJobsDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalJobsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out BetrayalJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<BetrayalJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        if (byWorldAreasKey is null)
        {
            byWorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalJobsDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalJobsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Completion_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCompletion_AchievementItemsKey(int? key, out BetrayalJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCompletion_AchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.Completion_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCompletion_AchievementItemsKey(int? key, out IReadOnlyList<BetrayalJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        if (byCompletion_AchievementItemsKey is null)
        {
            byCompletion_AchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Completion_AchievementItemsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byCompletion_AchievementItemsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCompletion_AchievementItemsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCompletion_AchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.byCompletion_AchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalJobsDat>> GetManyToManyByCompletion_AchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalJobsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCompletion_AchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.OpenChests_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOpenChests_AchievementItemsKey(int? key, out BetrayalJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOpenChests_AchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.OpenChests_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOpenChests_AchievementItemsKey(int? key, out IReadOnlyList<BetrayalJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        if (byOpenChests_AchievementItemsKey is null)
        {
            byOpenChests_AchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.OpenChests_AchievementItemsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byOpenChests_AchievementItemsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byOpenChests_AchievementItemsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byOpenChests_AchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.byOpenChests_AchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalJobsDat>> GetManyToManyByOpenChests_AchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalJobsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOpenChests_AchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.MissionCompletion_AcheivementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMissionCompletion_AcheivementItemsKey(int? key, out BetrayalJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMissionCompletion_AcheivementItemsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.MissionCompletion_AcheivementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMissionCompletion_AcheivementItemsKey(int? key, out IReadOnlyList<BetrayalJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        if (byMissionCompletion_AcheivementItemsKey is null)
        {
            byMissionCompletion_AcheivementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MissionCompletion_AcheivementItemsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byMissionCompletion_AcheivementItemsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMissionCompletion_AcheivementItemsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMissionCompletion_AcheivementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalJobsDat"/> with <see cref="BetrayalJobsDat.byMissionCompletion_AcheivementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalJobsDat>> GetManyToManyByMissionCompletion_AcheivementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalJobsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMissionCompletion_AcheivementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BetrayalJobsDat[] Load()
    {
        const string filePath = "Data/BetrayalJobs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalJobsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ExtraTerrainFeaturesKey
            (var extraterrainfeatureskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Art
            (var artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Completion_AchievementItemsKey
            (var tempcompletion_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var completion_achievementitemskeyLoading = tempcompletion_achievementitemskeyLoading.AsReadOnly();

            // loading OpenChests_AchievementItemsKey
            (var tempopenchests_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var openchests_achievementitemskeyLoading = tempopenchests_achievementitemskeyLoading.AsReadOnly();

            // loading MissionCompletion_AcheivementItemsKey
            (var tempmissioncompletion_acheivementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var missioncompletion_acheivementitemskeyLoading = tempmissioncompletion_acheivementitemskeyLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalJobsDat()
            {
                Id = idLoading,
                Text = textLoading,
                ExtraTerrainFeaturesKey = extraterrainfeatureskeyLoading,
                Art = artLoading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                WorldAreasKey = worldareaskeyLoading,
                Completion_AchievementItemsKey = completion_achievementitemskeyLoading,
                OpenChests_AchievementItemsKey = openchests_achievementitemskeyLoading,
                MissionCompletion_AcheivementItemsKey = missioncompletion_acheivementitemskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
