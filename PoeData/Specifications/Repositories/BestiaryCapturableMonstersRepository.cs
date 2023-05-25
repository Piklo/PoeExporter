using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BestiaryCapturableMonstersDat"/> related data and helper methods.
/// </summary>
public sealed class BestiaryCapturableMonstersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BestiaryCapturableMonstersDat> Items { get; }

    private Dictionary<int, List<BestiaryCapturableMonstersDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<BestiaryCapturableMonstersDat>>? byBestiaryGroupsKey;
    private Dictionary<string, List<BestiaryCapturableMonstersDat>>? byName;
    private Dictionary<int, List<BestiaryCapturableMonstersDat>>? byBestiaryEncountersKey;
    private Dictionary<bool, List<BestiaryCapturableMonstersDat>>? byUnknown56;
    private Dictionary<string, List<BestiaryCapturableMonstersDat>>? byIconSmall;
    private Dictionary<string, List<BestiaryCapturableMonstersDat>>? byIcon;
    private Dictionary<int, List<BestiaryCapturableMonstersDat>>? byBoss_MonsterVarietiesKey;
    private Dictionary<int, List<BestiaryCapturableMonstersDat>>? byBestiaryGenusKey;
    private Dictionary<bool, List<BestiaryCapturableMonstersDat>>? byUnknown105;
    private Dictionary<int, List<BestiaryCapturableMonstersDat>>? byBestiaryCapturableMonstersKey;
    private Dictionary<bool, List<BestiaryCapturableMonstersDat>>? byIsDisabled;
    private Dictionary<int, List<BestiaryCapturableMonstersDat>>? byUnknown115;
    private Dictionary<bool, List<BestiaryCapturableMonstersDat>>? byUnknown119;

    /// <summary>
    /// Initializes a new instance of the <see cref="BestiaryCapturableMonstersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BestiaryCapturableMonstersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out BestiaryCapturableMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        if (byMonsterVarietiesKey is null)
        {
            byMonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryCapturableMonstersDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<int, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.BestiaryGroupsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBestiaryGroupsKey(int? key, out BestiaryCapturableMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBestiaryGroupsKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.BestiaryGroupsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBestiaryGroupsKey(int? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        if (byBestiaryGroupsKey is null)
        {
            byBestiaryGroupsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BestiaryGroupsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBestiaryGroupsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBestiaryGroupsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBestiaryGroupsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byBestiaryGroupsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryCapturableMonstersDat>> GetManyToManyByBestiaryGroupsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<int, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBestiaryGroupsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out BestiaryCapturableMonstersDat? item)
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
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
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryCapturableMonstersDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<string, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.BestiaryEncountersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBestiaryEncountersKey(int? key, out BestiaryCapturableMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBestiaryEncountersKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.BestiaryEncountersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBestiaryEncountersKey(int? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        if (byBestiaryEncountersKey is null)
        {
            byBestiaryEncountersKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BestiaryEncountersKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBestiaryEncountersKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBestiaryEncountersKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBestiaryEncountersKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byBestiaryEncountersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryCapturableMonstersDat>> GetManyToManyByBestiaryEncountersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<int, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBestiaryEncountersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(bool? key, out BestiaryCapturableMonstersDat? item)
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(bool? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
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
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BestiaryCapturableMonstersDat>> GetManyToManyByUnknown56(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<bool, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.IconSmall"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIconSmall(string? key, out BestiaryCapturableMonstersDat? item)
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.IconSmall"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIconSmall(string? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
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
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byIconSmall"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryCapturableMonstersDat>> GetManyToManyByIconSmall(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<string, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIconSmall(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon(string? key, out BestiaryCapturableMonstersDat? item)
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon(string? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
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
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryCapturableMonstersDat>> GetManyToManyByIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<string, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Boss_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBoss_MonsterVarietiesKey(int? key, out BestiaryCapturableMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBoss_MonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Boss_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBoss_MonsterVarietiesKey(int? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        if (byBoss_MonsterVarietiesKey is null)
        {
            byBoss_MonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Boss_MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBoss_MonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBoss_MonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBoss_MonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byBoss_MonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryCapturableMonstersDat>> GetManyToManyByBoss_MonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<int, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBoss_MonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.BestiaryGenusKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBestiaryGenusKey(int? key, out BestiaryCapturableMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBestiaryGenusKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.BestiaryGenusKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBestiaryGenusKey(int? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        if (byBestiaryGenusKey is null)
        {
            byBestiaryGenusKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BestiaryGenusKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBestiaryGenusKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBestiaryGenusKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBestiaryGenusKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byBestiaryGenusKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryCapturableMonstersDat>> GetManyToManyByBestiaryGenusKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<int, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBestiaryGenusKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown105(bool? key, out BestiaryCapturableMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown105(key, out var items))
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown105(bool? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        if (byUnknown105 is null)
        {
            byUnknown105 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown105;

                if (!byUnknown105.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown105.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown105.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byUnknown105"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BestiaryCapturableMonstersDat>> GetManyToManyByUnknown105(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<bool, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown105(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.BestiaryCapturableMonstersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBestiaryCapturableMonstersKey(int? key, out BestiaryCapturableMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBestiaryCapturableMonstersKey(key, out var items))
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.BestiaryCapturableMonstersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBestiaryCapturableMonstersKey(int? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        if (byBestiaryCapturableMonstersKey is null)
        {
            byBestiaryCapturableMonstersKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BestiaryCapturableMonstersKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBestiaryCapturableMonstersKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBestiaryCapturableMonstersKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBestiaryCapturableMonstersKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byBestiaryCapturableMonstersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryCapturableMonstersDat>> GetManyToManyByBestiaryCapturableMonstersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<int, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBestiaryCapturableMonstersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.IsDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsDisabled(bool? key, out BestiaryCapturableMonstersDat? item)
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.IsDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsDisabled(bool? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
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
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byIsDisabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BestiaryCapturableMonstersDat>> GetManyToManyByIsDisabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<bool, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsDisabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown115(int? key, out BestiaryCapturableMonstersDat? item)
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown115(int? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
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
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byUnknown115"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryCapturableMonstersDat>> GetManyToManyByUnknown115(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<int, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown115(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Unknown119"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown119(bool? key, out BestiaryCapturableMonstersDat? item)
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
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.Unknown119"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown119(bool? key, out IReadOnlyList<BestiaryCapturableMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryCapturableMonstersDat>();
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
            items = Array.Empty<BestiaryCapturableMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryCapturableMonstersDat"/> with <see cref="BestiaryCapturableMonstersDat.byUnknown119"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BestiaryCapturableMonstersDat>> GetManyToManyByUnknown119(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BestiaryCapturableMonstersDat>>();
        }

        var items = new List<ResultItem<bool, BestiaryCapturableMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown119(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BestiaryCapturableMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BestiaryCapturableMonstersDat[] Load()
    {
        const string filePath = "Data/BestiaryCapturableMonsters.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryCapturableMonstersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BestiaryGroupsKey
            (var bestiarygroupskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BestiaryEncountersKey
            (var bestiaryencounterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IconSmall
            (var iconsmallLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Boss_MonsterVarietiesKey
            (var boss_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BestiaryGenusKey
            (var bestiarygenuskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading BestiaryCapturableMonstersKey
            (var bestiarycapturablemonsterskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading IsDisabled
            (var isdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown119
            (var unknown119Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryCapturableMonstersDat()
            {
                MonsterVarietiesKey = monstervarietieskeyLoading,
                BestiaryGroupsKey = bestiarygroupskeyLoading,
                Name = nameLoading,
                BestiaryEncountersKey = bestiaryencounterskeyLoading,
                Unknown56 = unknown56Loading,
                IconSmall = iconsmallLoading,
                Icon = iconLoading,
                Boss_MonsterVarietiesKey = boss_monstervarietieskeyLoading,
                BestiaryGenusKey = bestiarygenuskeyLoading,
                Unknown105 = unknown105Loading,
                BestiaryCapturableMonstersKey = bestiarycapturablemonsterskeyLoading,
                IsDisabled = isdisabledLoading,
                Unknown115 = unknown115Loading,
                Unknown119 = unknown119Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
