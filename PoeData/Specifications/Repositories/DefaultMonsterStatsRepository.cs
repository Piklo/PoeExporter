using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DefaultMonsterStatsDat"/> related data and helper methods.
/// </summary>
public sealed class DefaultMonsterStatsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DefaultMonsterStatsDat> Items { get; }

    private Dictionary<string, List<DefaultMonsterStatsDat>>? byDisplayLevel;
    private Dictionary<float, List<DefaultMonsterStatsDat>>? byDamage;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byEvasion;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byAccuracy;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byLife;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byExperience;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byAllyLife;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byUnknown32;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byDifficulty;
    private Dictionary<float, List<DefaultMonsterStatsDat>>? byDamage2;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byUnknown44;
    private Dictionary<float, List<DefaultMonsterStatsDat>>? byUnknown48;
    private Dictionary<float, List<DefaultMonsterStatsDat>>? byUnknown52;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byUnknown56;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byUnknown60;
    private Dictionary<int, List<DefaultMonsterStatsDat>>? byArmour;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultMonsterStatsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DefaultMonsterStatsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.DisplayLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplayLevel(string? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplayLevel(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.DisplayLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplayLevel(string? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byDisplayLevel is null)
        {
            byDisplayLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.DisplayLevel;

                if (!byDisplayLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDisplayLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplayLevel.TryGetValue(key, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byDisplayLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DefaultMonsterStatsDat>> GetManyToManyByDisplayLevel(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<string, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplayLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Damage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDamage(float? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDamage(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Damage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDamage(float? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byDamage is null)
        {
            byDamage = new();
            foreach (var item in Items)
            {
                var itemKey = item.Damage;

                if (!byDamage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDamage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDamage.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byDamage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, DefaultMonsterStatsDat>> GetManyToManyByDamage(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<float, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDamage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Evasion"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEvasion(int? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEvasion(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Evasion"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEvasion(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byEvasion is null)
        {
            byEvasion = new();
            foreach (var item in Items)
            {
                var itemKey = item.Evasion;

                if (!byEvasion.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEvasion.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEvasion.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byEvasion"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByEvasion(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEvasion(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Accuracy"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAccuracy(int? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAccuracy(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Accuracy"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAccuracy(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byAccuracy is null)
        {
            byAccuracy = new();
            foreach (var item in Items)
            {
                var itemKey = item.Accuracy;

                if (!byAccuracy.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAccuracy.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAccuracy.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byAccuracy"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByAccuracy(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAccuracy(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Life"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLife(int? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLife(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Life"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLife(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byLife is null)
        {
            byLife = new();
            foreach (var item in Items)
            {
                var itemKey = item.Life;

                if (!byLife.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLife.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLife.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byLife"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByLife(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLife(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Experience"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExperience(int? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExperience(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Experience"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExperience(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byExperience is null)
        {
            byExperience = new();
            foreach (var item in Items)
            {
                var itemKey = item.Experience;

                if (!byExperience.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byExperience.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byExperience.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byExperience"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByExperience(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExperience(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.AllyLife"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAllyLife(int? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAllyLife(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.AllyLife"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAllyLife(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byAllyLife is null)
        {
            byAllyLife = new();
            foreach (var item in Items)
            {
                var itemKey = item.AllyLife;

                if (!byAllyLife.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAllyLife.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAllyLife.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byAllyLife"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByAllyLife(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAllyLife(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Difficulty"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDifficulty(int? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDifficulty(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Difficulty"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDifficulty(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byDifficulty is null)
        {
            byDifficulty = new();
            foreach (var item in Items)
            {
                var itemKey = item.Difficulty;

                if (!byDifficulty.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDifficulty.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDifficulty.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byDifficulty"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByDifficulty(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDifficulty(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Damage2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDamage2(float? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDamage2(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Damage2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDamage2(float? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byDamage2 is null)
        {
            byDamage2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Damage2;

                if (!byDamage2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDamage2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDamage2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byDamage2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, DefaultMonsterStatsDat>> GetManyToManyByDamage2(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<float, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDamage2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out DefaultMonsterStatsDat? item)
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
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
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(float? key, out DefaultMonsterStatsDat? item)
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(float? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;

                if (!byUnknown48.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, DefaultMonsterStatsDat>> GetManyToManyByUnknown48(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<float, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(float? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown52(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(float? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;

                if (!byUnknown52.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown52.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, DefaultMonsterStatsDat>> GetManyToManyByUnknown52(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<float, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out DefaultMonsterStatsDat? item)
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
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
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(int? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown60(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byUnknown60 is null)
        {
            byUnknown60 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown60;

                if (!byUnknown60.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown60.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown60.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByUnknown60(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Armour"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArmour(int? key, out DefaultMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArmour(key, out var items))
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
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.Armour"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArmour(int? key, out IReadOnlyList<DefaultMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        if (byArmour is null)
        {
            byArmour = new();
            foreach (var item in Items)
            {
                var itemKey = item.Armour;

                if (!byArmour.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArmour.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArmour.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DefaultMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DefaultMonsterStatsDat"/> with <see cref="DefaultMonsterStatsDat.byArmour"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DefaultMonsterStatsDat>> GetManyToManyByArmour(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DefaultMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, DefaultMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArmour(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DefaultMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DefaultMonsterStatsDat[] Load()
    {
        const string filePath = "Data/DefaultMonsterStats.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DefaultMonsterStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading DisplayLevel
            (var displaylevelLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Damage
            (var damageLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Evasion
            (var evasionLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Accuracy
            (var accuracyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Life
            (var lifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Experience
            (var experienceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AllyLife
            (var allylifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Difficulty
            (var difficultyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Damage2
            (var damage2Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Armour
            (var armourLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DefaultMonsterStatsDat()
            {
                DisplayLevel = displaylevelLoading,
                Damage = damageLoading,
                Evasion = evasionLoading,
                Accuracy = accuracyLoading,
                Life = lifeLoading,
                Experience = experienceLoading,
                AllyLife = allylifeLoading,
                Unknown32 = unknown32Loading,
                Difficulty = difficultyLoading,
                Damage2 = damage2Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Armour = armourLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
