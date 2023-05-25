using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="StrDexIntMissionExtraRequirementDat"/> related data and helper methods.
/// </summary>
public sealed class StrDexIntMissionExtraRequirementRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<StrDexIntMissionExtraRequirementDat> Items { get; }

    private Dictionary<string, List<StrDexIntMissionExtraRequirementDat>>? byId;
    private Dictionary<int, List<StrDexIntMissionExtraRequirementDat>>? bySpawnWeight;
    private Dictionary<int, List<StrDexIntMissionExtraRequirementDat>>? byMinLevel;
    private Dictionary<int, List<StrDexIntMissionExtraRequirementDat>>? byMaxLevel;
    private Dictionary<int, List<StrDexIntMissionExtraRequirementDat>>? byTimeLimit;
    private Dictionary<bool, List<StrDexIntMissionExtraRequirementDat>>? byHasTimeBonusPerKill;
    private Dictionary<bool, List<StrDexIntMissionExtraRequirementDat>>? byHasTimeBonusPerObjectTagged;
    private Dictionary<bool, List<StrDexIntMissionExtraRequirementDat>>? byHasLimitedPortals;
    private Dictionary<int, List<StrDexIntMissionExtraRequirementDat>>? byNPCTalkKey;
    private Dictionary<int, List<StrDexIntMissionExtraRequirementDat>>? byTimeLimitBonusFromObjective;
    private Dictionary<int, List<StrDexIntMissionExtraRequirementDat>>? byObjectCount;
    private Dictionary<int, List<StrDexIntMissionExtraRequirementDat>>? byUnknown51;
    private Dictionary<bool, List<StrDexIntMissionExtraRequirementDat>>? byUnknown67;
    private Dictionary<int, List<StrDexIntMissionExtraRequirementDat>>? byUnknown68;

    /// <summary>
    /// Initializes a new instance of the <see cref="StrDexIntMissionExtraRequirementRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal StrDexIntMissionExtraRequirementRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out StrDexIntMissionExtraRequirementDat? item)
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
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
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, StrDexIntMissionExtraRequirementDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<string, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (bySpawnWeight is null)
        {
            bySpawnWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight;

                if (!bySpawnWeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpawnWeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpawnWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionExtraRequirementDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLevel(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byMinLevel is null)
        {
            byMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLevel;

                if (!byMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionExtraRequirementDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxLevel(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byMaxLevel is null)
        {
            byMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxLevel;

                if (!byMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionExtraRequirementDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.TimeLimit"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTimeLimit(int? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTimeLimit(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.TimeLimit"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTimeLimit(int? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byTimeLimit is null)
        {
            byTimeLimit = new();
            foreach (var item in Items)
            {
                var itemKey = item.TimeLimit;

                if (!byTimeLimit.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTimeLimit.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTimeLimit.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byTimeLimit"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionExtraRequirementDat>> GetManyToManyByTimeLimit(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTimeLimit(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.HasTimeBonusPerKill"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHasTimeBonusPerKill(bool? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHasTimeBonusPerKill(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.HasTimeBonusPerKill"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHasTimeBonusPerKill(bool? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byHasTimeBonusPerKill is null)
        {
            byHasTimeBonusPerKill = new();
            foreach (var item in Items)
            {
                var itemKey = item.HasTimeBonusPerKill;

                if (!byHasTimeBonusPerKill.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHasTimeBonusPerKill.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHasTimeBonusPerKill.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byHasTimeBonusPerKill"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrDexIntMissionExtraRequirementDat>> GetManyToManyByHasTimeBonusPerKill(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<bool, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHasTimeBonusPerKill(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.HasTimeBonusPerObjectTagged"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHasTimeBonusPerObjectTagged(bool? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHasTimeBonusPerObjectTagged(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.HasTimeBonusPerObjectTagged"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHasTimeBonusPerObjectTagged(bool? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byHasTimeBonusPerObjectTagged is null)
        {
            byHasTimeBonusPerObjectTagged = new();
            foreach (var item in Items)
            {
                var itemKey = item.HasTimeBonusPerObjectTagged;

                if (!byHasTimeBonusPerObjectTagged.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHasTimeBonusPerObjectTagged.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHasTimeBonusPerObjectTagged.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byHasTimeBonusPerObjectTagged"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrDexIntMissionExtraRequirementDat>> GetManyToManyByHasTimeBonusPerObjectTagged(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<bool, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHasTimeBonusPerObjectTagged(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.HasLimitedPortals"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHasLimitedPortals(bool? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHasLimitedPortals(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.HasLimitedPortals"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHasLimitedPortals(bool? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byHasLimitedPortals is null)
        {
            byHasLimitedPortals = new();
            foreach (var item in Items)
            {
                var itemKey = item.HasLimitedPortals;

                if (!byHasLimitedPortals.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHasLimitedPortals.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHasLimitedPortals.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byHasLimitedPortals"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrDexIntMissionExtraRequirementDat>> GetManyToManyByHasLimitedPortals(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<bool, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHasLimitedPortals(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.NPCTalkKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCTalkKey(int? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCTalkKey(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.NPCTalkKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCTalkKey(int? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byNPCTalkKey is null)
        {
            byNPCTalkKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCTalkKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCTalkKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCTalkKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCTalkKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byNPCTalkKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionExtraRequirementDat>> GetManyToManyByNPCTalkKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCTalkKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.TimeLimitBonusFromObjective"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTimeLimitBonusFromObjective(int? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTimeLimitBonusFromObjective(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.TimeLimitBonusFromObjective"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTimeLimitBonusFromObjective(int? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byTimeLimitBonusFromObjective is null)
        {
            byTimeLimitBonusFromObjective = new();
            foreach (var item in Items)
            {
                var itemKey = item.TimeLimitBonusFromObjective;

                if (!byTimeLimitBonusFromObjective.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTimeLimitBonusFromObjective.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTimeLimitBonusFromObjective.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byTimeLimitBonusFromObjective"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionExtraRequirementDat>> GetManyToManyByTimeLimitBonusFromObjective(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTimeLimitBonusFromObjective(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.ObjectCount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByObjectCount(int? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByObjectCount(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.ObjectCount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByObjectCount(int? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byObjectCount is null)
        {
            byObjectCount = new();
            foreach (var item in Items)
            {
                var itemKey = item.ObjectCount;

                if (!byObjectCount.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byObjectCount.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byObjectCount.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byObjectCount"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionExtraRequirementDat>> GetManyToManyByObjectCount(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByObjectCount(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.Unknown51"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown51(int? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown51(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.Unknown51"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown51(int? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byUnknown51 is null)
        {
            byUnknown51 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown51;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown51.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown51.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown51.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byUnknown51"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionExtraRequirementDat>> GetManyToManyByUnknown51(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown51(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown67(bool? key, out StrDexIntMissionExtraRequirementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown67(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown67(bool? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byUnknown67 is null)
        {
            byUnknown67 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown67;

                if (!byUnknown67.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown67.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown67.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byUnknown67"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrDexIntMissionExtraRequirementDat>> GetManyToManyByUnknown67(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<bool, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown67(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(int? key, out StrDexIntMissionExtraRequirementDat? item)
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
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(int? key, out IReadOnlyList<StrDexIntMissionExtraRequirementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown68.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown68.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionExtraRequirementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionExtraRequirementDat"/> with <see cref="StrDexIntMissionExtraRequirementDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionExtraRequirementDat>> GetManyToManyByUnknown68(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionExtraRequirementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionExtraRequirementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private StrDexIntMissionExtraRequirementDat[] Load()
    {
        const string filePath = "Data/StrDexIntMissionExtraRequirement.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StrDexIntMissionExtraRequirementDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TimeLimit
            (var timelimitLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HasTimeBonusPerKill
            (var hastimebonusperkillLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HasTimeBonusPerObjectTagged
            (var hastimebonusperobjecttaggedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HasLimitedPortals
            (var haslimitedportalsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading NPCTalkKey
            (var npctalkkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TimeLimitBonusFromObjective
            (var timelimitbonusfromobjectiveLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ObjectCount
            (var objectcountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown51
            (var tempunknown51Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown51Loading = tempunknown51Loading.AsReadOnly();

            // loading Unknown67
            (var unknown67Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown68
            (var tempunknown68Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown68Loading = tempunknown68Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StrDexIntMissionExtraRequirementDat()
            {
                Id = idLoading,
                SpawnWeight = spawnweightLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                TimeLimit = timelimitLoading,
                HasTimeBonusPerKill = hastimebonusperkillLoading,
                HasTimeBonusPerObjectTagged = hastimebonusperobjecttaggedLoading,
                HasLimitedPortals = haslimitedportalsLoading,
                NPCTalkKey = npctalkkeyLoading,
                TimeLimitBonusFromObjective = timelimitbonusfromobjectiveLoading,
                ObjectCount = objectcountLoading,
                Unknown51 = unknown51Loading,
                Unknown67 = unknown67Loading,
                Unknown68 = unknown68Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
