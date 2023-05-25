using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ExpeditionNPCsDat"/> related data and helper methods.
/// </summary>
public sealed class ExpeditionNPCsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ExpeditionNPCsDat> Items { get; }

    private Dictionary<string, List<ExpeditionNPCsDat>>? byId;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byNPCs;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byRerollItem;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byUnknown40;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byUnknown44;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byUnknown48;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byFaction;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byReroll;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byAllBombsPlaced;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byBombPlacedRemnant;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byBombPlacedTreasure;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byBombPlacedMonsters;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byBombPlacedGeneric;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byEncounterComplete;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byUnknown192;
    private Dictionary<int, List<ExpeditionNPCsDat>>? byUnknown196;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpeditionNPCsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ExpeditionNPCsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ExpeditionNPCsDat? item)
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
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
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExpeditionNPCsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<string, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.NPCs"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCs(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCs(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.NPCs"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCs(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byNPCs is null)
        {
            byNPCs = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCs;
                foreach (var listKey in itemKey)
                {
                    if (!byNPCs.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNPCs.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNPCs.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byNPCs"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByNPCs(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCs(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.RerollItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRerollItem(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRerollItem(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.RerollItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRerollItem(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byRerollItem is null)
        {
            byRerollItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.RerollItem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRerollItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRerollItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRerollItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byRerollItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByRerollItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRerollItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out ExpeditionNPCsDat? item)
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
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
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out ExpeditionNPCsDat? item)
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
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
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown48(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown48.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Faction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFaction(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFaction(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Faction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFaction(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byFaction is null)
        {
            byFaction = new();
            foreach (var item in Items)
            {
                var itemKey = item.Faction;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFaction.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFaction.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFaction.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byFaction"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByFaction(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFaction(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Reroll"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByReroll(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByReroll(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Reroll"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByReroll(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byReroll is null)
        {
            byReroll = new();
            foreach (var item in Items)
            {
                var itemKey = item.Reroll;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byReroll.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byReroll.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byReroll.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byReroll"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByReroll(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByReroll(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.AllBombsPlaced"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAllBombsPlaced(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAllBombsPlaced(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.AllBombsPlaced"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAllBombsPlaced(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byAllBombsPlaced is null)
        {
            byAllBombsPlaced = new();
            foreach (var item in Items)
            {
                var itemKey = item.AllBombsPlaced;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAllBombsPlaced.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAllBombsPlaced.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAllBombsPlaced.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byAllBombsPlaced"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByAllBombsPlaced(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAllBombsPlaced(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.BombPlacedRemnant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBombPlacedRemnant(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBombPlacedRemnant(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.BombPlacedRemnant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBombPlacedRemnant(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byBombPlacedRemnant is null)
        {
            byBombPlacedRemnant = new();
            foreach (var item in Items)
            {
                var itemKey = item.BombPlacedRemnant;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBombPlacedRemnant.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBombPlacedRemnant.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBombPlacedRemnant.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byBombPlacedRemnant"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByBombPlacedRemnant(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBombPlacedRemnant(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.BombPlacedTreasure"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBombPlacedTreasure(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBombPlacedTreasure(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.BombPlacedTreasure"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBombPlacedTreasure(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byBombPlacedTreasure is null)
        {
            byBombPlacedTreasure = new();
            foreach (var item in Items)
            {
                var itemKey = item.BombPlacedTreasure;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBombPlacedTreasure.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBombPlacedTreasure.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBombPlacedTreasure.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byBombPlacedTreasure"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByBombPlacedTreasure(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBombPlacedTreasure(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.BombPlacedMonsters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBombPlacedMonsters(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBombPlacedMonsters(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.BombPlacedMonsters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBombPlacedMonsters(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byBombPlacedMonsters is null)
        {
            byBombPlacedMonsters = new();
            foreach (var item in Items)
            {
                var itemKey = item.BombPlacedMonsters;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBombPlacedMonsters.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBombPlacedMonsters.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBombPlacedMonsters.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byBombPlacedMonsters"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByBombPlacedMonsters(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBombPlacedMonsters(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.BombPlacedGeneric"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBombPlacedGeneric(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBombPlacedGeneric(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.BombPlacedGeneric"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBombPlacedGeneric(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byBombPlacedGeneric is null)
        {
            byBombPlacedGeneric = new();
            foreach (var item in Items)
            {
                var itemKey = item.BombPlacedGeneric;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBombPlacedGeneric.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBombPlacedGeneric.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBombPlacedGeneric.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byBombPlacedGeneric"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByBombPlacedGeneric(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBombPlacedGeneric(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.EncounterComplete"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEncounterComplete(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEncounterComplete(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.EncounterComplete"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEncounterComplete(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byEncounterComplete is null)
        {
            byEncounterComplete = new();
            foreach (var item in Items)
            {
                var itemKey = item.EncounterComplete;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEncounterComplete.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEncounterComplete.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEncounterComplete.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byEncounterComplete"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByEncounterComplete(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEncounterComplete(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Unknown192"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown192(int? key, out ExpeditionNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown192(key, out var items))
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Unknown192"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown192(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        if (byUnknown192 is null)
        {
            byUnknown192 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown192;

                if (!byUnknown192.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown192.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown192.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byUnknown192"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByUnknown192(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown192(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Unknown196"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown196(int? key, out ExpeditionNPCsDat? item)
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
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.Unknown196"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown196(int? key, out IReadOnlyList<ExpeditionNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionNPCsDat>();
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
            items = Array.Empty<ExpeditionNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionNPCsDat"/> with <see cref="ExpeditionNPCsDat.byUnknown196"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionNPCsDat>> GetManyToManyByUnknown196(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionNPCsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown196(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ExpeditionNPCsDat[] Load()
    {
        const string filePath = "Data/ExpeditionNPCs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionNPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NPCs
            (var tempnpcsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npcsLoading = tempnpcsLoading.AsReadOnly();

            // loading RerollItem
            (var rerollitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Faction
            (var factionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Reroll
            (var rerollLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AllBombsPlaced
            (var allbombsplacedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BombPlacedRemnant
            (var bombplacedremnantLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BombPlacedTreasure
            (var bombplacedtreasureLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BombPlacedMonsters
            (var bombplacedmonstersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BombPlacedGeneric
            (var bombplacedgenericLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading EncounterComplete
            (var encountercompleteLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown192
            (var unknown192Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown196
            (var unknown196Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionNPCsDat()
            {
                Id = idLoading,
                NPCs = npcsLoading,
                RerollItem = rerollitemLoading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Faction = factionLoading,
                Reroll = rerollLoading,
                AllBombsPlaced = allbombsplacedLoading,
                BombPlacedRemnant = bombplacedremnantLoading,
                BombPlacedTreasure = bombplacedtreasureLoading,
                BombPlacedMonsters = bombplacedmonstersLoading,
                BombPlacedGeneric = bombplacedgenericLoading,
                EncounterComplete = encountercompleteLoading,
                Unknown192 = unknown192Loading,
                Unknown196 = unknown196Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
