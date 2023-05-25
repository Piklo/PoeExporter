using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BetrayalDialogueDat"/> related data and helper methods.
/// </summary>
public sealed class BetrayalDialogueRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BetrayalDialogueDat> Items { get; }

    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown0;
    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown16;
    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown20;
    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown24;
    private Dictionary<int, List<BetrayalDialogueDat>>? byBetrayalTargetsKey;
    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown56;
    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown60;
    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown76;
    private Dictionary<int, List<BetrayalDialogueDat>>? byBetrayalUpgradesKey;
    private Dictionary<bool, List<BetrayalDialogueDat>>? byUnknown108;
    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown109;
    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown125;
    private Dictionary<bool, List<BetrayalDialogueDat>>? byUnknown141;
    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown142;
    private Dictionary<int, List<BetrayalDialogueDat>>? byNPCTextAudioKey;
    private Dictionary<int, List<BetrayalDialogueDat>>? byUnknown174;

    /// <summary>
    /// Initializes a new instance of the <see cref="BetrayalDialogueRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BetrayalDialogueRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out BetrayalDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown0(key, out var items))
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out BetrayalDialogueDat? item)
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
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
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out BetrayalDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown20(key, out var items))
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;

                if (!byUnknown20.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out BetrayalDialogueDat? item)
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown24.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown24.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.BetrayalTargetsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalTargetsKey(int? key, out BetrayalDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalTargetsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.BetrayalTargetsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalTargetsKey(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byBetrayalTargetsKey is null)
        {
            byBetrayalTargetsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalTargetsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBetrayalTargetsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBetrayalTargetsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalTargetsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byBetrayalTargetsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByBetrayalTargetsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalTargetsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out BetrayalDialogueDat? item)
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
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
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(int? key, out BetrayalDialogueDat? item)
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown60 is null)
        {
            byUnknown60 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown60;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown60.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown60.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown60.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown60(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(int? key, out BetrayalDialogueDat? item)
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown76 is null)
        {
            byUnknown76 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown76;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown76.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown76.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown76.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown76(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.BetrayalUpgradesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalUpgradesKey(int? key, out BetrayalDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalUpgradesKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.BetrayalUpgradesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalUpgradesKey(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byBetrayalUpgradesKey is null)
        {
            byBetrayalUpgradesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalUpgradesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBetrayalUpgradesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBetrayalUpgradesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalUpgradesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byBetrayalUpgradesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByBetrayalUpgradesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalUpgradesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(bool? key, out BetrayalDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown108(key, out var items))
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(bool? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown108 is null)
        {
            byUnknown108 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown108;

                if (!byUnknown108.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown108.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown108.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BetrayalDialogueDat>> GetManyToManyByUnknown108(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<bool, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown109(int? key, out BetrayalDialogueDat? item)
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown109(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown109 is null)
        {
            byUnknown109 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown109;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown109.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown109.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown109.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown109"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown109(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown109(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown125"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown125(int? key, out BetrayalDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown125(key, out var items))
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown125"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown125(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown125 is null)
        {
            byUnknown125 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown125;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown125.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown125.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown125.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown125"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown125(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown125(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown141"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown141(bool? key, out BetrayalDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown141(key, out var items))
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown141"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown141(bool? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown141 is null)
        {
            byUnknown141 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown141;

                if (!byUnknown141.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown141.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown141.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown141"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BetrayalDialogueDat>> GetManyToManyByUnknown141(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<bool, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown141(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown142(int? key, out BetrayalDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown142(key, out var items))
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown142(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown142 is null)
        {
            byUnknown142 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown142;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown142.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown142.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown142.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown142"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown142(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown142(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.NPCTextAudioKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCTextAudioKey(int? key, out BetrayalDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCTextAudioKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.NPCTextAudioKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCTextAudioKey(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byNPCTextAudioKey is null)
        {
            byNPCTextAudioKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCTextAudioKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCTextAudioKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCTextAudioKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCTextAudioKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byNPCTextAudioKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByNPCTextAudioKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCTextAudioKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown174"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown174(int? key, out BetrayalDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown174(key, out var items))
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
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.Unknown174"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown174(int? key, out IReadOnlyList<BetrayalDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        if (byUnknown174 is null)
        {
            byUnknown174 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown174;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown174.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown174.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown174.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalDialogueDat"/> with <see cref="BetrayalDialogueDat.byUnknown174"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalDialogueDat>> GetManyToManyByUnknown174(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalDialogueDat>>();
        }

        var items = new List<ResultItem<int, BetrayalDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown174(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BetrayalDialogueDat[] Load()
    {
        const string filePath = "Data/BetrayalDialogue.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalDialogueDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            // loading BetrayalTargetsKey
            (var betrayaltargetskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown76
            (var tempunknown76Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown76Loading = tempunknown76Loading.AsReadOnly();

            // loading BetrayalUpgradesKey
            (var betrayalupgradeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown109
            (var tempunknown109Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown109Loading = tempunknown109Loading.AsReadOnly();

            // loading Unknown125
            (var tempunknown125Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown125Loading = tempunknown125Loading.AsReadOnly();

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown142
            (var tempunknown142Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown142Loading = tempunknown142Loading.AsReadOnly();

            // loading NPCTextAudioKey
            (var npctextaudiokeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown174
            (var tempunknown174Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown174Loading = tempunknown174Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalDialogueDat()
            {
                Unknown0 = unknown0Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                BetrayalTargetsKey = betrayaltargetskeyLoading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown76 = unknown76Loading,
                BetrayalUpgradesKey = betrayalupgradeskeyLoading,
                Unknown108 = unknown108Loading,
                Unknown109 = unknown109Loading,
                Unknown125 = unknown125Loading,
                Unknown141 = unknown141Loading,
                Unknown142 = unknown142Loading,
                NPCTextAudioKey = npctextaudiokeyLoading,
                Unknown174 = unknown174Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
