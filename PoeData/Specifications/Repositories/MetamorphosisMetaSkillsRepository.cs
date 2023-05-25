using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MetamorphosisMetaSkillsDat"/> related data and helper methods.
/// </summary>
public sealed class MetamorphosisMetaSkillsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MetamorphosisMetaSkillsDat> Items { get; }

    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byMonster;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? bySkillType;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown32;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown48;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown64;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown80;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byAnimation;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byStats;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byStatsValues;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown144;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown148;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byGrantedEffects;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown180;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown184;
    private Dictionary<string, List<MetamorphosisMetaSkillsDat>>? byScript1;
    private Dictionary<string, List<MetamorphosisMetaSkillsDat>>? byScript2;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byMods;
    private Dictionary<string, List<MetamorphosisMetaSkillsDat>>? byName;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown240;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown244;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown260;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown264;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown268;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown284;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown300;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byUnknown316;
    private Dictionary<int, List<MetamorphosisMetaSkillsDat>>? byMiscAnimations;
    private Dictionary<bool, List<MetamorphosisMetaSkillsDat>>? byUnknown348;

    /// <summary>
    /// Initializes a new instance of the <see cref="MetamorphosisMetaSkillsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MetamorphosisMetaSkillsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Monster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonster(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonster(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Monster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonster(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byMonster is null)
        {
            byMonster = new();
            foreach (var item in Items)
            {
                var itemKey = item.Monster;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonster.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonster.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonster.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byMonster"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByMonster(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonster(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.SkillType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillType(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillType(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.SkillType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillType(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (bySkillType is null)
        {
            bySkillType = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillType;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySkillType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySkillType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.bySkillType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyBySkillType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out MetamorphosisMetaSkillsDat? item)
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown32.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out MetamorphosisMetaSkillsDat? item)
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown48.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown48.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out MetamorphosisMetaSkillsDat? item)
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown64.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown64.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown80(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown80 is null)
        {
            byUnknown80 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown80;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown80.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown80.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown80.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown80(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Animation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAnimation(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAnimation(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Animation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAnimation(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byAnimation is null)
        {
            byAnimation = new();
            foreach (var item in Items)
            {
                var itemKey = item.Animation;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAnimation.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAnimation.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAnimation.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byAnimation"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByAnimation(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAnimation(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Stats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStats(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStats(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Stats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStats(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byStats is null)
        {
            byStats = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stats;
                foreach (var listKey in itemKey)
                {
                    if (!byStats.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStats.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStats.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byStats"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByStats(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStats(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.StatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsValues(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsValues(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.StatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsValues(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byStatsValues is null)
        {
            byStatsValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsValues;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byStatsValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByStatsValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown144(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown144(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown144(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown144 is null)
        {
            byUnknown144 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown144;

                if (!byUnknown144.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown144.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown144.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown144"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown144(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown144(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown148(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown148(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown148(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown148 is null)
        {
            byUnknown148 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown148;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown148.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown148.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown148.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown148"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown148(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown148(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.GrantedEffects"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffects(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffects(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.GrantedEffects"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffects(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byGrantedEffects is null)
        {
            byGrantedEffects = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffects;
                foreach (var listKey in itemKey)
                {
                    if (!byGrantedEffects.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byGrantedEffects.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byGrantedEffects.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byGrantedEffects"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByGrantedEffects(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffects(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown180"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown180(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown180(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown180"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown180(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown180 is null)
        {
            byUnknown180 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown180;

                if (!byUnknown180.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown180.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown180.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown180"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown180(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown180(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown184"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown184(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown184(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown184"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown184(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown184 is null)
        {
            byUnknown184 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown184;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown184.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown184.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown184.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown184"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown184(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown184(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Script1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScript1(string? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScript1(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Script1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScript1(string? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byScript1 is null)
        {
            byScript1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Script1;

                if (!byScript1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScript1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScript1.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byScript1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillsDat>> GetManyToManyByScript1(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScript1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Script2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScript2(string? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScript2(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Script2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScript2(string? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byScript2 is null)
        {
            byScript2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Script2;

                if (!byScript2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScript2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScript2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byScript2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillsDat>> GetManyToManyByScript2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScript2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Mods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMods(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMods(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Mods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMods(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byMods is null)
        {
            byMods = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mods;
                foreach (var listKey in itemKey)
                {
                    if (!byMods.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMods.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMods.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byMods"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByMods(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMods(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out MetamorphosisMetaSkillsDat? item)
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
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
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisMetaSkillsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown240"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown240(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown240(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown240"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown240(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown240 is null)
        {
            byUnknown240 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown240;

                if (!byUnknown240.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown240.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown240.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown240"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown240(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown240(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown244"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown244(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown244(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown244"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown244(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown244 is null)
        {
            byUnknown244 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown244;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown244.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown244.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown244.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown244"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown244(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown244(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown260"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown260(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown260(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown260"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown260(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown260 is null)
        {
            byUnknown260 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown260;

                if (!byUnknown260.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown260.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown260.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown260"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown260(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown260(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown264"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown264(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown264(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown264"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown264(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown264 is null)
        {
            byUnknown264 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown264;

                if (!byUnknown264.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown264.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown264.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown264"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown264(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown264(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown268"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown268(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown268(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown268"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown268(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown268 is null)
        {
            byUnknown268 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown268;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown268.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown268.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown268.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown268"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown268(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown268(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown284"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown284(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown284(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown284"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown284(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown284 is null)
        {
            byUnknown284 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown284;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown284.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown284.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown284.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown284"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown284(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown284(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown300"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown300(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown300(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown300"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown300(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown300 is null)
        {
            byUnknown300 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown300;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown300.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown300.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown300.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown300"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown300(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown300(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown316"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown316(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown316(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown316"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown316(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown316 is null)
        {
            byUnknown316 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown316;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown316.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown316.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown316.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown316"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown316(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown316(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.MiscAnimations"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimations(int? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimations(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.MiscAnimations"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimations(int? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byMiscAnimations is null)
        {
            byMiscAnimations = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimations;
                foreach (var listKey in itemKey)
                {
                    if (!byMiscAnimations.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMiscAnimations.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMiscAnimations.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byMiscAnimations"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisMetaSkillsDat>> GetManyToManyByMiscAnimations(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimations(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown348"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown348(bool? key, out MetamorphosisMetaSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown348(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.Unknown348"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown348(bool? key, out IReadOnlyList<MetamorphosisMetaSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        if (byUnknown348 is null)
        {
            byUnknown348 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown348;

                if (!byUnknown348.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown348.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown348.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisMetaSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisMetaSkillsDat"/> with <see cref="MetamorphosisMetaSkillsDat.byUnknown348"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MetamorphosisMetaSkillsDat>> GetManyToManyByUnknown348(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MetamorphosisMetaSkillsDat>>();
        }

        var items = new List<ResultItem<bool, MetamorphosisMetaSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown348(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MetamorphosisMetaSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MetamorphosisMetaSkillsDat[] Load()
    {
        const string filePath = "Data/MetamorphosisMetaSkills.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MetamorphosisMetaSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Monster
            (var monsterLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SkillType
            (var skilltypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var tempunknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown48Loading = tempunknown48Loading.AsReadOnly();

            // loading Unknown64
            (var tempunknown64Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown64Loading = tempunknown64Loading.AsReadOnly();

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Animation
            (var animationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading StatsValues
            (var tempstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statsvaluesLoading = tempstatsvaluesLoading.AsReadOnly();

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading GrantedEffects
            (var tempgrantedeffectsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var grantedeffectsLoading = tempgrantedeffectsLoading.AsReadOnly();

            // loading Unknown180
            (var unknown180Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown184
            (var unknown184Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Script1
            (var script1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Script2
            (var script2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Mods
            (var tempmodsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modsLoading = tempmodsLoading.AsReadOnly();

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown240
            (var unknown240Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown244
            (var tempunknown244Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown244Loading = tempunknown244Loading.AsReadOnly();

            // loading Unknown260
            (var unknown260Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown264
            (var unknown264Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown268
            (var tempunknown268Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown268Loading = tempunknown268Loading.AsReadOnly();

            // loading Unknown284
            (var tempunknown284Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown284Loading = tempunknown284Loading.AsReadOnly();

            // loading Unknown300
            (var tempunknown300Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown300Loading = tempunknown300Loading.AsReadOnly();

            // loading Unknown316
            (var tempunknown316Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown316Loading = tempunknown316Loading.AsReadOnly();

            // loading MiscAnimations
            (var tempmiscanimationsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var miscanimationsLoading = tempmiscanimationsLoading.AsReadOnly();

            // loading Unknown348
            (var unknown348Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MetamorphosisMetaSkillsDat()
            {
                Monster = monsterLoading,
                SkillType = skilltypeLoading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Unknown80 = unknown80Loading,
                Animation = animationLoading,
                Stats = statsLoading,
                StatsValues = statsvaluesLoading,
                Unknown144 = unknown144Loading,
                Unknown148 = unknown148Loading,
                GrantedEffects = grantedeffectsLoading,
                Unknown180 = unknown180Loading,
                Unknown184 = unknown184Loading,
                Script1 = script1Loading,
                Script2 = script2Loading,
                Mods = modsLoading,
                Name = nameLoading,
                Unknown240 = unknown240Loading,
                Unknown244 = unknown244Loading,
                Unknown260 = unknown260Loading,
                Unknown264 = unknown264Loading,
                Unknown268 = unknown268Loading,
                Unknown284 = unknown284Loading,
                Unknown300 = unknown300Loading,
                Unknown316 = unknown316Loading,
                MiscAnimations = miscanimationsLoading,
                Unknown348 = unknown348Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
