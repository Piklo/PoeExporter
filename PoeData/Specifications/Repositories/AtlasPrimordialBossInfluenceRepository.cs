using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasPrimordialBossInfluenceDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasPrimordialBossInfluenceRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasPrimordialBossInfluenceDat> Items { get; }

    private Dictionary<int, List<AtlasPrimordialBossInfluenceDat>>? byBoss;
    private Dictionary<int, List<AtlasPrimordialBossInfluenceDat>>? byProgress;
    private Dictionary<int, List<AtlasPrimordialBossInfluenceDat>>? byMinMapTier;
    private Dictionary<int, List<AtlasPrimordialBossInfluenceDat>>? byUnknown24;
    private Dictionary<int, List<AtlasPrimordialBossInfluenceDat>>? byUnknown28;
    private Dictionary<int, List<AtlasPrimordialBossInfluenceDat>>? byUnknown32;
    private Dictionary<float, List<AtlasPrimordialBossInfluenceDat>>? byUnknown48;
    private Dictionary<int, List<AtlasPrimordialBossInfluenceDat>>? byUnknown52;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasPrimordialBossInfluenceRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasPrimordialBossInfluenceRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Boss"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBoss(int? key, out AtlasPrimordialBossInfluenceDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBoss(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Boss"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBoss(int? key, out IReadOnlyList<AtlasPrimordialBossInfluenceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        if (byBoss is null)
        {
            byBoss = new();
            foreach (var item in Items)
            {
                var itemKey = item.Boss;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBoss.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBoss.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBoss.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.byBoss"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossInfluenceDat>> GetManyToManyByBoss(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBoss(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossInfluenceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Progress"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProgress(int? key, out AtlasPrimordialBossInfluenceDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProgress(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Progress"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProgress(int? key, out IReadOnlyList<AtlasPrimordialBossInfluenceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        if (byProgress is null)
        {
            byProgress = new();
            foreach (var item in Items)
            {
                var itemKey = item.Progress;

                if (!byProgress.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byProgress.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byProgress.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.byProgress"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossInfluenceDat>> GetManyToManyByProgress(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProgress(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossInfluenceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.MinMapTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinMapTier(int? key, out AtlasPrimordialBossInfluenceDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinMapTier(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.MinMapTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinMapTier(int? key, out IReadOnlyList<AtlasPrimordialBossInfluenceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        if (byMinMapTier is null)
        {
            byMinMapTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinMapTier;

                if (!byMinMapTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinMapTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinMapTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.byMinMapTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossInfluenceDat>> GetManyToManyByMinMapTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinMapTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossInfluenceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out AtlasPrimordialBossInfluenceDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<AtlasPrimordialBossInfluenceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
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
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossInfluenceDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossInfluenceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out AtlasPrimordialBossInfluenceDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<AtlasPrimordialBossInfluenceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossInfluenceDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossInfluenceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out AtlasPrimordialBossInfluenceDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<AtlasPrimordialBossInfluenceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
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
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossInfluenceDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossInfluenceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(float? key, out AtlasPrimordialBossInfluenceDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(float? key, out IReadOnlyList<AtlasPrimordialBossInfluenceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
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
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, AtlasPrimordialBossInfluenceDat>> GetManyToManyByUnknown48(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, AtlasPrimordialBossInfluenceDat>>();
        }

        var items = new List<ResultItem<float, AtlasPrimordialBossInfluenceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, AtlasPrimordialBossInfluenceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out AtlasPrimordialBossInfluenceDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<AtlasPrimordialBossInfluenceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown52.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown52.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossInfluenceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossInfluenceDat"/> with <see cref="AtlasPrimordialBossInfluenceDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossInfluenceDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossInfluenceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossInfluenceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasPrimordialBossInfluenceDat[] Load()
    {
        const string filePath = "Data/AtlasPrimordialBossInfluence.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasPrimordialBossInfluenceDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Boss
            (var bossLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Progress
            (var progressLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinMapTier
            (var minmaptierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasPrimordialBossInfluenceDat()
            {
                Boss = bossLoading,
                Progress = progressLoading,
                MinMapTier = minmaptierLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
