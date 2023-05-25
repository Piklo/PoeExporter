using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BestiaryNetsDat"/> related data and helper methods.
/// </summary>
public sealed class BestiaryNetsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BestiaryNetsDat> Items { get; }

    private Dictionary<int, List<BestiaryNetsDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<BestiaryNetsDat>>? byUnknown16;
    private Dictionary<int, List<BestiaryNetsDat>>? byCaptureMinLevel;
    private Dictionary<int, List<BestiaryNetsDat>>? byCaptureMaxLevel;
    private Dictionary<int, List<BestiaryNetsDat>>? byDropMinLevel;
    private Dictionary<int, List<BestiaryNetsDat>>? byDropMaxLevel;
    private Dictionary<int, List<BestiaryNetsDat>>? byUnknown36;
    private Dictionary<bool, List<BestiaryNetsDat>>? byIsEnabled;

    /// <summary>
    /// Initializes a new instance of the <see cref="BestiaryNetsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BestiaryNetsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out BestiaryNetsDat? item)
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
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<BestiaryNetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryNetsDat>();
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
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryNetsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryNetsDat>>();
        }

        var items = new List<ResultItem<int, BestiaryNetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryNetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out BestiaryNetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<BestiaryNetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryNetsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryNetsDat>>();
        }

        var items = new List<ResultItem<int, BestiaryNetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryNetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.CaptureMinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCaptureMinLevel(int? key, out BestiaryNetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCaptureMinLevel(key, out var items))
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
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.CaptureMinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCaptureMinLevel(int? key, out IReadOnlyList<BestiaryNetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        if (byCaptureMinLevel is null)
        {
            byCaptureMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.CaptureMinLevel;

                if (!byCaptureMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCaptureMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCaptureMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.byCaptureMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryNetsDat>> GetManyToManyByCaptureMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryNetsDat>>();
        }

        var items = new List<ResultItem<int, BestiaryNetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCaptureMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryNetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.CaptureMaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCaptureMaxLevel(int? key, out BestiaryNetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCaptureMaxLevel(key, out var items))
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
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.CaptureMaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCaptureMaxLevel(int? key, out IReadOnlyList<BestiaryNetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        if (byCaptureMaxLevel is null)
        {
            byCaptureMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.CaptureMaxLevel;

                if (!byCaptureMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCaptureMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCaptureMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.byCaptureMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryNetsDat>> GetManyToManyByCaptureMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryNetsDat>>();
        }

        var items = new List<ResultItem<int, BestiaryNetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCaptureMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryNetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.DropMinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDropMinLevel(int? key, out BestiaryNetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDropMinLevel(key, out var items))
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
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.DropMinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDropMinLevel(int? key, out IReadOnlyList<BestiaryNetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        if (byDropMinLevel is null)
        {
            byDropMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.DropMinLevel;

                if (!byDropMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDropMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDropMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.byDropMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryNetsDat>> GetManyToManyByDropMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryNetsDat>>();
        }

        var items = new List<ResultItem<int, BestiaryNetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDropMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryNetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.DropMaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDropMaxLevel(int? key, out BestiaryNetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDropMaxLevel(key, out var items))
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
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.DropMaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDropMaxLevel(int? key, out IReadOnlyList<BestiaryNetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        if (byDropMaxLevel is null)
        {
            byDropMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.DropMaxLevel;

                if (!byDropMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDropMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDropMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.byDropMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryNetsDat>> GetManyToManyByDropMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryNetsDat>>();
        }

        var items = new List<ResultItem<int, BestiaryNetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDropMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryNetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out BestiaryNetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown36(key, out var items))
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
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<BestiaryNetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        if (byUnknown36 is null)
        {
            byUnknown36 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown36;

                if (!byUnknown36.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown36.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown36.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BestiaryNetsDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BestiaryNetsDat>>();
        }

        var items = new List<ResultItem<int, BestiaryNetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BestiaryNetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.IsEnabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsEnabled(bool? key, out BestiaryNetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsEnabled(key, out var items))
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
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.IsEnabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsEnabled(bool? key, out IReadOnlyList<BestiaryNetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        if (byIsEnabled is null)
        {
            byIsEnabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsEnabled;

                if (!byIsEnabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsEnabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsEnabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BestiaryNetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryNetsDat"/> with <see cref="BestiaryNetsDat.byIsEnabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BestiaryNetsDat>> GetManyToManyByIsEnabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BestiaryNetsDat>>();
        }

        var items = new List<ResultItem<bool, BestiaryNetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsEnabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BestiaryNetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BestiaryNetsDat[] Load()
    {
        const string filePath = "Data/BestiaryNets.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryNetsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CaptureMinLevel
            (var captureminlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CaptureMaxLevel
            (var capturemaxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DropMinLevel
            (var dropminlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DropMaxLevel
            (var dropmaxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryNetsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Unknown16 = unknown16Loading,
                CaptureMinLevel = captureminlevelLoading,
                CaptureMaxLevel = capturemaxlevelLoading,
                DropMinLevel = dropminlevelLoading,
                DropMaxLevel = dropmaxlevelLoading,
                Unknown36 = unknown36Loading,
                IsEnabled = isenabledLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
