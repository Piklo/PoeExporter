using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistQuestContractsDat"/> related data and helper methods.
/// </summary>
public sealed class HeistQuestContractsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistQuestContractsDat> Items { get; }

    private Dictionary<int, List<HeistQuestContractsDat>>? byHeistContractsKey;
    private Dictionary<int, List<HeistQuestContractsDat>>? byHeistObjectivesKey;
    private Dictionary<int, List<HeistQuestContractsDat>>? byHeistNPCsKey;
    private Dictionary<int, List<HeistQuestContractsDat>>? byHeistJobsKey;
    private Dictionary<int, List<HeistQuestContractsDat>>? byUnknown64;
    private Dictionary<int, List<HeistQuestContractsDat>>? byUnknown68;
    private Dictionary<int, List<HeistQuestContractsDat>>? byUnknown72;
    private Dictionary<bool, List<HeistQuestContractsDat>>? byUnknown76;
    private Dictionary<int, List<HeistQuestContractsDat>>? byHeistRoomsKey1;
    private Dictionary<int, List<HeistQuestContractsDat>>? byWorldAreasKey;
    private Dictionary<bool, List<HeistQuestContractsDat>>? byUnknown109;
    private Dictionary<bool, List<HeistQuestContractsDat>>? byUnknown110;
    private Dictionary<int, List<HeistQuestContractsDat>>? byUnknown111;
    private Dictionary<int, List<HeistQuestContractsDat>>? byUnknown115;
    private Dictionary<bool, List<HeistQuestContractsDat>>? byUnknown119;
    private Dictionary<bool, List<HeistQuestContractsDat>>? byUnknown120;
    private Dictionary<int, List<HeistQuestContractsDat>>? byHaveObjective;
    private Dictionary<bool, List<HeistQuestContractsDat>>? byUnknown137;
    private Dictionary<int, List<HeistQuestContractsDat>>? byQuestActive;
    private Dictionary<int, List<HeistQuestContractsDat>>? byHaveQuest;
    private Dictionary<int, List<HeistQuestContractsDat>>? byHaveObjective2;
    private Dictionary<bool, List<HeistQuestContractsDat>>? byUnknown186;
    private Dictionary<bool, List<HeistQuestContractsDat>>? byUnknown187;
    private Dictionary<string, List<HeistQuestContractsDat>>? byObjective;
    private Dictionary<bool, List<HeistQuestContractsDat>>? byUnknown196;
    private Dictionary<int, List<HeistQuestContractsDat>>? byBaseItemTypesKey;
    private Dictionary<bool, List<HeistQuestContractsDat>>? byUnknown213;
    private Dictionary<int, List<HeistQuestContractsDat>>? byHeistIntroAreasKey;
    private Dictionary<int, List<HeistQuestContractsDat>>? byUnknown230;
    private Dictionary<int, List<HeistQuestContractsDat>>? byHeistRoomsKey2;
    private Dictionary<string, List<HeistQuestContractsDat>>? byUnknown250;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistQuestContractsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistQuestContractsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistContractsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistContractsKey(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistContractsKey(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistContractsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistContractsKey(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byHeistContractsKey is null)
        {
            byHeistContractsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistContractsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistContractsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistContractsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistContractsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byHeistContractsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByHeistContractsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistContractsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistObjectivesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistObjectivesKey(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistObjectivesKey(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistObjectivesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistObjectivesKey(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byHeistObjectivesKey is null)
        {
            byHeistObjectivesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistObjectivesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistObjectivesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistObjectivesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistObjectivesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byHeistObjectivesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByHeistObjectivesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistObjectivesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistNPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistNPCsKey(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistNPCsKey(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistNPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistNPCsKey(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byHeistNPCsKey is null)
        {
            byHeistNPCsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistNPCsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byHeistNPCsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byHeistNPCsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byHeistNPCsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byHeistNPCsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByHeistNPCsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistNPCsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistJobsKey(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistJobsKey(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistJobsKey(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byHeistJobsKey is null)
        {
            byHeistJobsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistJobsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistJobsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistJobsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistJobsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byHeistJobsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByHeistJobsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistJobsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out HeistQuestContractsDat? item)
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
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
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown68(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;

                if (!byUnknown68.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByUnknown68(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(bool? key, out HeistQuestContractsDat? item)
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(bool? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
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
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistQuestContractsDat>> GetManyToManyByUnknown76(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<bool, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistRoomsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistRoomsKey1(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistRoomsKey1(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistRoomsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistRoomsKey1(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byHeistRoomsKey1 is null)
        {
            byHeistRoomsKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistRoomsKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistRoomsKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistRoomsKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistRoomsKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byHeistRoomsKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByHeistRoomsKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistRoomsKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out HeistQuestContractsDat? item)
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
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
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown109(bool? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown109(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown109(bool? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown109 is null)
        {
            byUnknown109 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown109;

                if (!byUnknown109.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown109.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown109.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown109"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistQuestContractsDat>> GetManyToManyByUnknown109(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<bool, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown109(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown110"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown110(bool? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown110(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown110"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown110(bool? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown110 is null)
        {
            byUnknown110 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown110;

                if (!byUnknown110.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown110.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown110.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown110"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistQuestContractsDat>> GetManyToManyByUnknown110(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<bool, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown110(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown111"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown111(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown111(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown111"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown111(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown111 is null)
        {
            byUnknown111 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown111;

                if (!byUnknown111.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown111.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown111.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown111"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByUnknown111(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown111(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown115(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown115(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown115(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown115 is null)
        {
            byUnknown115 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown115;

                if (!byUnknown115.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown115.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown115.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown115"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByUnknown115(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown115(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown119"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown119(bool? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown119(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown119"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown119(bool? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown119 is null)
        {
            byUnknown119 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown119;

                if (!byUnknown119.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown119.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown119.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown119"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistQuestContractsDat>> GetManyToManyByUnknown119(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<bool, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown119(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown120(bool? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown120(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown120(bool? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown120 is null)
        {
            byUnknown120 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown120;

                if (!byUnknown120.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown120.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown120.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown120"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistQuestContractsDat>> GetManyToManyByUnknown120(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<bool, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown120(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HaveObjective"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHaveObjective(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHaveObjective(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HaveObjective"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHaveObjective(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byHaveObjective is null)
        {
            byHaveObjective = new();
            foreach (var item in Items)
            {
                var itemKey = item.HaveObjective;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHaveObjective.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHaveObjective.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHaveObjective.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byHaveObjective"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByHaveObjective(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHaveObjective(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown137"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown137(bool? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown137(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown137"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown137(bool? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown137 is null)
        {
            byUnknown137 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown137;

                if (!byUnknown137.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown137.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown137.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown137"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistQuestContractsDat>> GetManyToManyByUnknown137(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<bool, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown137(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.QuestActive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestActive(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestActive(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.QuestActive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestActive(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byQuestActive is null)
        {
            byQuestActive = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestActive;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestActive.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestActive.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestActive.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byQuestActive"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByQuestActive(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestActive(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HaveQuest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHaveQuest(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHaveQuest(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HaveQuest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHaveQuest(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byHaveQuest is null)
        {
            byHaveQuest = new();
            foreach (var item in Items)
            {
                var itemKey = item.HaveQuest;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHaveQuest.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHaveQuest.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHaveQuest.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byHaveQuest"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByHaveQuest(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHaveQuest(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HaveObjective2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHaveObjective2(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHaveObjective2(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HaveObjective2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHaveObjective2(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byHaveObjective2 is null)
        {
            byHaveObjective2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HaveObjective2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHaveObjective2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHaveObjective2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHaveObjective2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byHaveObjective2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByHaveObjective2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHaveObjective2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown186"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown186(bool? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown186(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown186"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown186(bool? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown186 is null)
        {
            byUnknown186 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown186;

                if (!byUnknown186.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown186.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown186.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown186"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistQuestContractsDat>> GetManyToManyByUnknown186(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<bool, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown186(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown187"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown187(bool? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown187(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown187"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown187(bool? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown187 is null)
        {
            byUnknown187 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown187;

                if (!byUnknown187.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown187.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown187.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown187"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistQuestContractsDat>> GetManyToManyByUnknown187(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<bool, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown187(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Objective"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByObjective(string? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByObjective(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Objective"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByObjective(string? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byObjective is null)
        {
            byObjective = new();
            foreach (var item in Items)
            {
                var itemKey = item.Objective;

                if (!byObjective.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byObjective.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byObjective.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byObjective"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistQuestContractsDat>> GetManyToManyByObjective(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<string, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByObjective(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown196"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown196(bool? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown196(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown196"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown196(bool? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown196 is null)
        {
            byUnknown196 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown196;

                if (!byUnknown196.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown196.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown196.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown196"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistQuestContractsDat>> GetManyToManyByUnknown196(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<bool, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown196(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown213"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown213(bool? key, out HeistQuestContractsDat? item)
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown213"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown213(bool? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
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
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown213"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistQuestContractsDat>> GetManyToManyByUnknown213(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<bool, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown213(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistIntroAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistIntroAreasKey(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistIntroAreasKey(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistIntroAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistIntroAreasKey(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byHeistIntroAreasKey is null)
        {
            byHeistIntroAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistIntroAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistIntroAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistIntroAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistIntroAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byHeistIntroAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByHeistIntroAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistIntroAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown230"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown230(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown230(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown230"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown230(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown230 is null)
        {
            byUnknown230 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown230;

                if (!byUnknown230.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown230.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown230.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown230"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByUnknown230(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown230(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistRoomsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistRoomsKey2(int? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistRoomsKey2(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.HeistRoomsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistRoomsKey2(int? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byHeistRoomsKey2 is null)
        {
            byHeistRoomsKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistRoomsKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistRoomsKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistRoomsKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistRoomsKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byHeistRoomsKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistQuestContractsDat>> GetManyToManyByHeistRoomsKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<int, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistRoomsKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown250"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown250(string? key, out HeistQuestContractsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown250(key, out var items))
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
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.Unknown250"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown250(string? key, out IReadOnlyList<HeistQuestContractsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        if (byUnknown250 is null)
        {
            byUnknown250 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown250;

                if (!byUnknown250.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown250.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown250.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistQuestContractsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistQuestContractsDat"/> with <see cref="HeistQuestContractsDat.byUnknown250"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistQuestContractsDat>> GetManyToManyByUnknown250(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistQuestContractsDat>>();
        }

        var items = new List<ResultItem<string, HeistQuestContractsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown250(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistQuestContractsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistQuestContractsDat[] Load()
    {
        const string filePath = "Data/HeistQuestContracts.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistQuestContractsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HeistContractsKey
            (var heistcontractskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistObjectivesKey
            (var heistobjectiveskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistNPCsKey
            (var tempheistnpcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var heistnpcskeyLoading = tempheistnpcskeyLoading.AsReadOnly();

            // loading HeistJobsKey
            (var heistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HeistRoomsKey1
            (var heistroomskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown110
            (var unknown110Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown111
            (var unknown111Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown119
            (var unknown119Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HaveObjective
            (var haveobjectiveLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown137
            (var unknown137Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading QuestActive
            (var questactiveLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HaveQuest
            (var havequestLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HaveObjective2
            (var haveobjective2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown186
            (var unknown186Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown187
            (var unknown187Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Objective
            (var objectiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown196
            (var unknown196Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown213
            (var unknown213Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HeistIntroAreasKey
            (var heistintroareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown230
            (var unknown230Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HeistRoomsKey2
            (var heistroomskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown250
            (var unknown250Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistQuestContractsDat()
            {
                HeistContractsKey = heistcontractskeyLoading,
                HeistObjectivesKey = heistobjectiveskeyLoading,
                HeistNPCsKey = heistnpcskeyLoading,
                HeistJobsKey = heistjobskeyLoading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                HeistRoomsKey1 = heistroomskey1Loading,
                WorldAreasKey = worldareaskeyLoading,
                Unknown109 = unknown109Loading,
                Unknown110 = unknown110Loading,
                Unknown111 = unknown111Loading,
                Unknown115 = unknown115Loading,
                Unknown119 = unknown119Loading,
                Unknown120 = unknown120Loading,
                HaveObjective = haveobjectiveLoading,
                Unknown137 = unknown137Loading,
                QuestActive = questactiveLoading,
                HaveQuest = havequestLoading,
                HaveObjective2 = haveobjective2Loading,
                Unknown186 = unknown186Loading,
                Unknown187 = unknown187Loading,
                Objective = objectiveLoading,
                Unknown196 = unknown196Loading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Unknown213 = unknown213Loading,
                HeistIntroAreasKey = heistintroareaskeyLoading,
                Unknown230 = unknown230Loading,
                HeistRoomsKey2 = heistroomskey2Loading,
                Unknown250 = unknown250Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
