using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="NPCFollowerVariationsDat"/> related data and helper methods.
/// </summary>
public sealed class NPCFollowerVariationsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<NPCFollowerVariationsDat> Items { get; }

    private Dictionary<int, List<NPCFollowerVariationsDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<NPCFollowerVariationsDat>>? byMiscAnimatedKey0;
    private Dictionary<int, List<NPCFollowerVariationsDat>>? byMiscAnimatedKey1;
    private Dictionary<bool, List<NPCFollowerVariationsDat>>? byUnknown48;
    private Dictionary<bool, List<NPCFollowerVariationsDat>>? byUnknown49;
    private Dictionary<int, List<NPCFollowerVariationsDat>>? byUnknown50;
    private Dictionary<int, List<NPCFollowerVariationsDat>>? byUnknown54;
    private Dictionary<int, List<NPCFollowerVariationsDat>>? byUnknown58;
    private Dictionary<int, List<NPCFollowerVariationsDat>>? byUnknown62;
    private Dictionary<bool, List<NPCFollowerVariationsDat>>? byUnknown66;
    private Dictionary<bool, List<NPCFollowerVariationsDat>>? byUnknown67;
    private Dictionary<int, List<NPCFollowerVariationsDat>>? byUnknown68;
    private Dictionary<int, List<NPCFollowerVariationsDat>>? byUnknown84;
    private Dictionary<int, List<NPCFollowerVariationsDat>>? byUnknown100;
    private Dictionary<bool, List<NPCFollowerVariationsDat>>? byUnknown104;
    private Dictionary<bool, List<NPCFollowerVariationsDat>>? byUnknown105;
    private Dictionary<int, List<NPCFollowerVariationsDat>>? byUnknown106;

    /// <summary>
    /// Initializes a new instance of the <see cref="NPCFollowerVariationsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal NPCFollowerVariationsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out NPCFollowerVariationsDat? item)
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
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
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.MiscAnimatedKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey0(int? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey0(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.MiscAnimatedKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey0(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byMiscAnimatedKey0 is null)
        {
            byMiscAnimatedKey0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byMiscAnimatedKey0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByMiscAnimatedKey0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.MiscAnimatedKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey1(int? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey1(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.MiscAnimatedKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey1(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byMiscAnimatedKey1 is null)
        {
            byMiscAnimatedKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byMiscAnimatedKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByMiscAnimatedKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(bool? key, out NPCFollowerVariationsDat? item)
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(bool? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
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
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCFollowerVariationsDat>> GetManyToManyByUnknown48(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<bool, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown49(bool? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown49(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown49(bool? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byUnknown49 is null)
        {
            byUnknown49 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown49;

                if (!byUnknown49.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown49.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown49.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown49"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCFollowerVariationsDat>> GetManyToManyByUnknown49(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<bool, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown49(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown50"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown50(int? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown50(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown50"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown50(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byUnknown50 is null)
        {
            byUnknown50 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown50;

                if (!byUnknown50.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown50.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown50.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown50"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByUnknown50(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown50(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown54"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown54(int? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown54(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown54"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown54(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byUnknown54 is null)
        {
            byUnknown54 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown54;

                if (!byUnknown54.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown54.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown54.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown54"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByUnknown54(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown54(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown58"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown58(int? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown58(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown58"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown58(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byUnknown58 is null)
        {
            byUnknown58 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown58;

                if (!byUnknown58.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown58.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown58.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown58"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByUnknown58(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown58(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown62(int? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown62(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown62(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byUnknown62 is null)
        {
            byUnknown62 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown62;

                if (!byUnknown62.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown62.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown62.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown62"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByUnknown62(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown62(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown66(bool? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown66(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown66(bool? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byUnknown66 is null)
        {
            byUnknown66 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown66;

                if (!byUnknown66.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown66.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown66.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown66"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCFollowerVariationsDat>> GetManyToManyByUnknown66(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<bool, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown66(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown67(bool? key, out NPCFollowerVariationsDat? item)
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown67(bool? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
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
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown67"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCFollowerVariationsDat>> GetManyToManyByUnknown67(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<bool, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown67(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(int? key, out NPCFollowerVariationsDat? item)
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
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
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByUnknown68(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown84(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byUnknown84 is null)
        {
            byUnknown84 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown84;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown84.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown84.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown84.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(int? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown100(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byUnknown100 is null)
        {
            byUnknown100 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown100;

                if (!byUnknown100.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown100.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown100.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByUnknown100(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(bool? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown104(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(bool? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;

                if (!byUnknown104.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown104.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCFollowerVariationsDat>> GetManyToManyByUnknown104(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<bool, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown105(bool? key, out NPCFollowerVariationsDat? item)
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown105(bool? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
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
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown105"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCFollowerVariationsDat>> GetManyToManyByUnknown105(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<bool, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown105(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown106(int? key, out NPCFollowerVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown106(key, out var items))
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
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown106(int? key, out IReadOnlyList<NPCFollowerVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        if (byUnknown106 is null)
        {
            byUnknown106 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown106;

                if (!byUnknown106.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown106.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown106.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCFollowerVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCFollowerVariationsDat"/> with <see cref="NPCFollowerVariationsDat.byUnknown106"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCFollowerVariationsDat>> GetManyToManyByUnknown106(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCFollowerVariationsDat>>();
        }

        var items = new List<ResultItem<int, NPCFollowerVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown106(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCFollowerVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private NPCFollowerVariationsDat[] Load()
    {
        const string filePath = "Data/NPCFollowerVariations.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCFollowerVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimatedKey0
            (var miscanimatedkey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimatedKey1
            (var miscanimatedkey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown54
            (var unknown54Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown58
            (var unknown58Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown67
            (var unknown67Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown68
            (var tempunknown68Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown68Loading = tempunknown68Loading.AsReadOnly();

            // loading Unknown84
            (var tempunknown84Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown84Loading = tempunknown84Loading.AsReadOnly();

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCFollowerVariationsDat()
            {
                MonsterVarietiesKey = monstervarietieskeyLoading,
                MiscAnimatedKey0 = miscanimatedkey0Loading,
                MiscAnimatedKey1 = miscanimatedkey1Loading,
                Unknown48 = unknown48Loading,
                Unknown49 = unknown49Loading,
                Unknown50 = unknown50Loading,
                Unknown54 = unknown54Loading,
                Unknown58 = unknown58Loading,
                Unknown62 = unknown62Loading,
                Unknown66 = unknown66Loading,
                Unknown67 = unknown67Loading,
                Unknown68 = unknown68Loading,
                Unknown84 = unknown84Loading,
                Unknown100 = unknown100Loading,
                Unknown104 = unknown104Loading,
                Unknown105 = unknown105Loading,
                Unknown106 = unknown106Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
