using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ExpeditionFactionsDat"/> related data and helper methods.
/// </summary>
public sealed class ExpeditionFactionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ExpeditionFactionsDat> Items { get; }

    private Dictionary<string, List<ExpeditionFactionsDat>>? byId;
    private Dictionary<string, List<ExpeditionFactionsDat>>? byName;
    private Dictionary<string, List<ExpeditionFactionsDat>>? byFactionFlag;
    private Dictionary<int, List<ExpeditionFactionsDat>>? byUnknown24;
    private Dictionary<string, List<ExpeditionFactionsDat>>? byFactionIcon;
    private Dictionary<int, List<ExpeditionFactionsDat>>? byMonsterVarieties;
    private Dictionary<int, List<ExpeditionFactionsDat>>? byProgress1;
    private Dictionary<int, List<ExpeditionFactionsDat>>? byProgress2Vaal;
    private Dictionary<int, List<ExpeditionFactionsDat>>? byProgress3Final;
    private Dictionary<int, List<ExpeditionFactionsDat>>? byTags;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpeditionFactionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ExpeditionFactionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ExpeditionFactionsDat? item)
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
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ExpeditionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionFactionsDat>();
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
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExpeditionFactionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExpeditionFactionsDat>>();
        }

        var items = new List<ResultItem<string, ExpeditionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExpeditionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out ExpeditionFactionsDat? item)
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
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<ExpeditionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionFactionsDat>();
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
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExpeditionFactionsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExpeditionFactionsDat>>();
        }

        var items = new List<ResultItem<string, ExpeditionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExpeditionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.FactionFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFactionFlag(string? key, out ExpeditionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFactionFlag(key, out var items))
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
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.FactionFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFactionFlag(string? key, out IReadOnlyList<ExpeditionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        if (byFactionFlag is null)
        {
            byFactionFlag = new();
            foreach (var item in Items)
            {
                var itemKey = item.FactionFlag;

                if (!byFactionFlag.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFactionFlag.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFactionFlag.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.byFactionFlag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExpeditionFactionsDat>> GetManyToManyByFactionFlag(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExpeditionFactionsDat>>();
        }

        var items = new List<ResultItem<string, ExpeditionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFactionFlag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExpeditionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out ExpeditionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown24(key, out var items))
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
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<ExpeditionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;

                if (!byUnknown24.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionFactionsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionFactionsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.FactionIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFactionIcon(string? key, out ExpeditionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFactionIcon(key, out var items))
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
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.FactionIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFactionIcon(string? key, out IReadOnlyList<ExpeditionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        if (byFactionIcon is null)
        {
            byFactionIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.FactionIcon;

                if (!byFactionIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFactionIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFactionIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.byFactionIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExpeditionFactionsDat>> GetManyToManyByFactionIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExpeditionFactionsDat>>();
        }

        var items = new List<ResultItem<string, ExpeditionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFactionIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExpeditionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.MonsterVarieties"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarieties(int? key, out ExpeditionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarieties(key, out var items))
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
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.MonsterVarieties"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarieties(int? key, out IReadOnlyList<ExpeditionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        if (byMonsterVarieties is null)
        {
            byMonsterVarieties = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarieties;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterVarieties.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterVarieties.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterVarieties.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.byMonsterVarieties"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionFactionsDat>> GetManyToManyByMonsterVarieties(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionFactionsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarieties(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Progress1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProgress1(int? key, out ExpeditionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProgress1(key, out var items))
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
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Progress1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProgress1(int? key, out IReadOnlyList<ExpeditionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        if (byProgress1 is null)
        {
            byProgress1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Progress1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byProgress1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byProgress1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byProgress1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.byProgress1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionFactionsDat>> GetManyToManyByProgress1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionFactionsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProgress1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Progress2Vaal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProgress2Vaal(int? key, out ExpeditionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProgress2Vaal(key, out var items))
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
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Progress2Vaal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProgress2Vaal(int? key, out IReadOnlyList<ExpeditionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        if (byProgress2Vaal is null)
        {
            byProgress2Vaal = new();
            foreach (var item in Items)
            {
                var itemKey = item.Progress2Vaal;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byProgress2Vaal.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byProgress2Vaal.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byProgress2Vaal.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.byProgress2Vaal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionFactionsDat>> GetManyToManyByProgress2Vaal(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionFactionsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProgress2Vaal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Progress3Final"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProgress3Final(int? key, out ExpeditionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProgress3Final(key, out var items))
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
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Progress3Final"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProgress3Final(int? key, out IReadOnlyList<ExpeditionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        if (byProgress3Final is null)
        {
            byProgress3Final = new();
            foreach (var item in Items)
            {
                var itemKey = item.Progress3Final;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byProgress3Final.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byProgress3Final.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byProgress3Final.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.byProgress3Final"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionFactionsDat>> GetManyToManyByProgress3Final(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionFactionsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProgress3Final(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Tags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTags(int? key, out ExpeditionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTags(key, out var items))
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
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.Tags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTags(int? key, out IReadOnlyList<ExpeditionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        if (byTags is null)
        {
            byTags = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tags;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTags.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTags.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTags.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionFactionsDat"/> with <see cref="ExpeditionFactionsDat.byTags"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionFactionsDat>> GetManyToManyByTags(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionFactionsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTags(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ExpeditionFactionsDat[] Load()
    {
        const string filePath = "Data/ExpeditionFactions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionFactionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FactionFlag
            (var factionflagLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FactionIcon
            (var factioniconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterVarieties
            (var monstervarietiesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Progress1
            (var progress1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Progress2Vaal
            (var progress2vaalLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Progress3Final
            (var progress3finalLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Tags
            (var tagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionFactionsDat()
            {
                Id = idLoading,
                Name = nameLoading,
                FactionFlag = factionflagLoading,
                Unknown24 = unknown24Loading,
                FactionIcon = factioniconLoading,
                MonsterVarieties = monstervarietiesLoading,
                Progress1 = progress1Loading,
                Progress2Vaal = progress2vaalLoading,
                Progress3Final = progress3finalLoading,
                Tags = tagsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
