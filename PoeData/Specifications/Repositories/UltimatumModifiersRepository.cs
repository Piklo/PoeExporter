using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UltimatumModifiersDat"/> related data and helper methods.
/// </summary>
public sealed class UltimatumModifiersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UltimatumModifiersDat> Items { get; }

    private Dictionary<string, List<UltimatumModifiersDat>>? byId;
    private Dictionary<int, List<UltimatumModifiersDat>>? byTypes;
    private Dictionary<int, List<UltimatumModifiersDat>>? byExtraMods;
    private Dictionary<int, List<UltimatumModifiersDat>>? byTypesFiltered;
    private Dictionary<int, List<UltimatumModifiersDat>>? byDaemonSpawningData;
    private Dictionary<int, List<UltimatumModifiersDat>>? byPreviousTiers;
    private Dictionary<bool, List<UltimatumModifiersDat>>? byUnknown88;
    private Dictionary<int, List<UltimatumModifiersDat>>? byBosses;
    private Dictionary<int, List<UltimatumModifiersDat>>? byRadius;
    private Dictionary<string, List<UltimatumModifiersDat>>? byName;
    private Dictionary<string, List<UltimatumModifiersDat>>? byIcon;
    private Dictionary<int, List<UltimatumModifiersDat>>? byHASH16;
    private Dictionary<int, List<UltimatumModifiersDat>>? byTypesExtra;
    private Dictionary<int, List<UltimatumModifiersDat>>? byMonsterTypesApplyingRuin;
    private Dictionary<int, List<UltimatumModifiersDat>>? byMiscAnimated;
    private Dictionary<int, List<UltimatumModifiersDat>>? byBuffTemplates;
    private Dictionary<int, List<UltimatumModifiersDat>>? byTier;
    private Dictionary<int, List<UltimatumModifiersDat>>? byUnknown185;
    private Dictionary<string, List<UltimatumModifiersDat>>? byDescription;
    private Dictionary<string, List<UltimatumModifiersDat>>? byMonsterSpawners;
    private Dictionary<bool, List<UltimatumModifiersDat>>? byUnknown213;
    private Dictionary<int, List<UltimatumModifiersDat>>? byAchievements;
    private Dictionary<int, List<UltimatumModifiersDat>>? byTextAudio;
    private Dictionary<int, List<UltimatumModifiersDat>>? byUniqueMapMod;

    /// <summary>
    /// Initializes a new instance of the <see cref="UltimatumModifiersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UltimatumModifiersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out UltimatumModifiersDat? item)
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
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
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumModifiersDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<string, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Types"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTypes(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTypes(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Types"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTypes(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byTypes is null)
        {
            byTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.Types;
                foreach (var listKey in itemKey)
                {
                    if (!byTypes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTypes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.ExtraMods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExtraMods(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExtraMods(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.ExtraMods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExtraMods(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byExtraMods is null)
        {
            byExtraMods = new();
            foreach (var item in Items)
            {
                var itemKey = item.ExtraMods;
                foreach (var listKey in itemKey)
                {
                    if (!byExtraMods.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byExtraMods.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byExtraMods.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byExtraMods"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByExtraMods(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExtraMods(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.TypesFiltered"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTypesFiltered(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTypesFiltered(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.TypesFiltered"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTypesFiltered(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byTypesFiltered is null)
        {
            byTypesFiltered = new();
            foreach (var item in Items)
            {
                var itemKey = item.TypesFiltered;
                foreach (var listKey in itemKey)
                {
                    if (!byTypesFiltered.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTypesFiltered.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTypesFiltered.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byTypesFiltered"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByTypesFiltered(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTypesFiltered(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.DaemonSpawningData"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDaemonSpawningData(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDaemonSpawningData(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.DaemonSpawningData"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDaemonSpawningData(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byDaemonSpawningData is null)
        {
            byDaemonSpawningData = new();
            foreach (var item in Items)
            {
                var itemKey = item.DaemonSpawningData;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDaemonSpawningData.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDaemonSpawningData.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDaemonSpawningData.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byDaemonSpawningData"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByDaemonSpawningData(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDaemonSpawningData(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.PreviousTiers"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPreviousTiers(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPreviousTiers(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.PreviousTiers"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPreviousTiers(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byPreviousTiers is null)
        {
            byPreviousTiers = new();
            foreach (var item in Items)
            {
                var itemKey = item.PreviousTiers;
                foreach (var listKey in itemKey)
                {
                    if (!byPreviousTiers.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPreviousTiers.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPreviousTiers.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byPreviousTiers"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByPreviousTiers(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPreviousTiers(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(bool? key, out UltimatumModifiersDat? item)
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(bool? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;

                if (!byUnknown88.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UltimatumModifiersDat>> GetManyToManyByUnknown88(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<bool, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Bosses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBosses(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBosses(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Bosses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBosses(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byBosses is null)
        {
            byBosses = new();
            foreach (var item in Items)
            {
                var itemKey = item.Bosses;
                foreach (var listKey in itemKey)
                {
                    if (!byBosses.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBosses.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBosses.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byBosses"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByBosses(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBosses(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Radius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRadius(int? key, out UltimatumModifiersDat? item)
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Radius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRadius(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
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
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byRadius"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByRadius(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRadius(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out UltimatumModifiersDat? item)
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
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
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumModifiersDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<string, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon(string? key, out UltimatumModifiersDat? item)
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon(string? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
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
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumModifiersDat>> GetManyToManyByIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<string, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH16(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHASH16(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH16(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byHASH16 is null)
        {
            byHASH16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HASH16;

                if (!byHASH16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHASH16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHASH16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byHASH16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByHASH16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.TypesExtra"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTypesExtra(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTypesExtra(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.TypesExtra"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTypesExtra(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byTypesExtra is null)
        {
            byTypesExtra = new();
            foreach (var item in Items)
            {
                var itemKey = item.TypesExtra;
                foreach (var listKey in itemKey)
                {
                    if (!byTypesExtra.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTypesExtra.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTypesExtra.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byTypesExtra"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByTypesExtra(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTypesExtra(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.MonsterTypesApplyingRuin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterTypesApplyingRuin(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterTypesApplyingRuin(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.MonsterTypesApplyingRuin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterTypesApplyingRuin(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byMonsterTypesApplyingRuin is null)
        {
            byMonsterTypesApplyingRuin = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterTypesApplyingRuin;

                if (!byMonsterTypesApplyingRuin.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMonsterTypesApplyingRuin.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterTypesApplyingRuin.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byMonsterTypesApplyingRuin"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByMonsterTypesApplyingRuin(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterTypesApplyingRuin(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.MiscAnimated"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimated(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimated(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.MiscAnimated"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimated(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byMiscAnimated is null)
        {
            byMiscAnimated = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimated;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimated.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimated.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimated.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byMiscAnimated"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByMiscAnimated(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimated(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.BuffTemplates"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffTemplates(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffTemplates(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.BuffTemplates"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffTemplates(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byBuffTemplates is null)
        {
            byBuffTemplates = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffTemplates;
                foreach (var listKey in itemKey)
                {
                    if (!byBuffTemplates.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBuffTemplates.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBuffTemplates.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byBuffTemplates"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByBuffTemplates(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffTemplates(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out UltimatumModifiersDat? item)
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
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

        if (!byTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Unknown185"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown185(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown185(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Unknown185"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown185(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byUnknown185 is null)
        {
            byUnknown185 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown185;

                if (!byUnknown185.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown185.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown185.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byUnknown185"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByUnknown185(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown185(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out UltimatumModifiersDat? item)
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
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
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumModifiersDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<string, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.MonsterSpawners"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterSpawners(string? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterSpawners(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.MonsterSpawners"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterSpawners(string? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byMonsterSpawners is null)
        {
            byMonsterSpawners = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterSpawners;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsterSpawners.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsterSpawners.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsterSpawners.TryGetValue(key, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byMonsterSpawners"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumModifiersDat>> GetManyToManyByMonsterSpawners(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<string, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterSpawners(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Unknown213"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown213(bool? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown213(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Unknown213"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown213(bool? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byUnknown213 is null)
        {
            byUnknown213 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown213;

                if (!byUnknown213.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown213.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown213.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byUnknown213"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UltimatumModifiersDat>> GetManyToManyByUnknown213(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<bool, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown213(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements(int? key, out UltimatumModifiersDat? item)
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
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
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudio(int? key, out UltimatumModifiersDat? item)
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudio(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
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
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byTextAudio"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByTextAudio(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudio(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.UniqueMapMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUniqueMapMod(int? key, out UltimatumModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUniqueMapMod(key, out var items))
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
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.UniqueMapMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUniqueMapMod(int? key, out IReadOnlyList<UltimatumModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        if (byUniqueMapMod is null)
        {
            byUniqueMapMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.UniqueMapMod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUniqueMapMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUniqueMapMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUniqueMapMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumModifiersDat"/> with <see cref="UltimatumModifiersDat.byUniqueMapMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumModifiersDat>> GetManyToManyByUniqueMapMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumModifiersDat>>();
        }

        var items = new List<ResultItem<int, UltimatumModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUniqueMapMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UltimatumModifiersDat[] Load()
    {
        const string filePath = "Data/UltimatumModifiers.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UltimatumModifiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Types
            (var temptypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var typesLoading = temptypesLoading.AsReadOnly();

            // loading ExtraMods
            (var tempextramodsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var extramodsLoading = tempextramodsLoading.AsReadOnly();

            // loading TypesFiltered
            (var temptypesfilteredLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var typesfilteredLoading = temptypesfilteredLoading.AsReadOnly();

            // loading DaemonSpawningData
            (var daemonspawningdataLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PreviousTiers
            (var tempprevioustiersLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var previoustiersLoading = tempprevioustiersLoading.AsReadOnly();

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Bosses
            (var tempbossesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var bossesLoading = tempbossesLoading.AsReadOnly();

            // loading Radius
            (var radiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TypesExtra
            (var temptypesextraLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var typesextraLoading = temptypesextraLoading.AsReadOnly();

            // loading MonsterTypesApplyingRuin
            (var monstertypesapplyingruinLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscAnimated
            (var miscanimatedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BuffTemplates
            (var tempbufftemplatesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var bufftemplatesLoading = tempbufftemplatesLoading.AsReadOnly();

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown185
            (var unknown185Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterSpawners
            (var tempmonsterspawnersLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var monsterspawnersLoading = tempmonsterspawnersLoading.AsReadOnly();

            // loading Unknown213
            (var unknown213Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading UniqueMapMod
            (var uniquemapmodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UltimatumModifiersDat()
            {
                Id = idLoading,
                Types = typesLoading,
                ExtraMods = extramodsLoading,
                TypesFiltered = typesfilteredLoading,
                DaemonSpawningData = daemonspawningdataLoading,
                PreviousTiers = previoustiersLoading,
                Unknown88 = unknown88Loading,
                Bosses = bossesLoading,
                Radius = radiusLoading,
                Name = nameLoading,
                Icon = iconLoading,
                HASH16 = hash16Loading,
                TypesExtra = typesextraLoading,
                MonsterTypesApplyingRuin = monstertypesapplyingruinLoading,
                MiscAnimated = miscanimatedLoading,
                BuffTemplates = bufftemplatesLoading,
                Tier = tierLoading,
                Unknown185 = unknown185Loading,
                Description = descriptionLoading,
                MonsterSpawners = monsterspawnersLoading,
                Unknown213 = unknown213Loading,
                Achievements = achievementsLoading,
                TextAudio = textaudioLoading,
                UniqueMapMod = uniquemapmodLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
