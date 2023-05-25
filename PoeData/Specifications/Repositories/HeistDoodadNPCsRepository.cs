using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistDoodadNPCsDat"/> related data and helper methods.
/// </summary>
public sealed class HeistDoodadNPCsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistDoodadNPCsDat> Items { get; }

    private Dictionary<int, List<HeistDoodadNPCsDat>>? byNPCsKey;
    private Dictionary<int, List<HeistDoodadNPCsDat>>? byUnknown16;
    private Dictionary<int, List<HeistDoodadNPCsDat>>? byUnknown20;
    private Dictionary<int, List<HeistDoodadNPCsDat>>? byUnknown24;
    private Dictionary<int, List<HeistDoodadNPCsDat>>? byUnknown28;
    private Dictionary<int, List<HeistDoodadNPCsDat>>? byUnknown32;
    private Dictionary<string, List<HeistDoodadNPCsDat>>? byAOFile;
    private Dictionary<string, List<HeistDoodadNPCsDat>>? byStance;
    private Dictionary<int, List<HeistDoodadNPCsDat>>? byBetrayalTargetsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistDoodadNPCsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistDoodadNPCsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCsKey(int? key, out HeistDoodadNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCsKey(key, out var items))
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
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCsKey(int? key, out IReadOnlyList<HeistDoodadNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        if (byNPCsKey is null)
        {
            byNPCsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.byNPCsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistDoodadNPCsDat>> GetManyToManyByNPCsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistDoodadNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistDoodadNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistDoodadNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out HeistDoodadNPCsDat? item)
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
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<HeistDoodadNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
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
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistDoodadNPCsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistDoodadNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistDoodadNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistDoodadNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out HeistDoodadNPCsDat? item)
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
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<HeistDoodadNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
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
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistDoodadNPCsDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistDoodadNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistDoodadNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistDoodadNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out HeistDoodadNPCsDat? item)
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
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<HeistDoodadNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
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
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistDoodadNPCsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistDoodadNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistDoodadNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistDoodadNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out HeistDoodadNPCsDat? item)
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
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<HeistDoodadNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
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
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistDoodadNPCsDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistDoodadNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistDoodadNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistDoodadNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out HeistDoodadNPCsDat? item)
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
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<HeistDoodadNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
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
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistDoodadNPCsDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistDoodadNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistDoodadNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistDoodadNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFile(string? key, out HeistDoodadNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFile(key, out var items))
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
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFile(string? key, out IReadOnlyList<HeistDoodadNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        if (byAOFile is null)
        {
            byAOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFile;

                if (!byAOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.byAOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistDoodadNPCsDat>> GetManyToManyByAOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistDoodadNPCsDat>>();
        }

        var items = new List<ResultItem<string, HeistDoodadNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistDoodadNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Stance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStance(string? key, out HeistDoodadNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStance(key, out var items))
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
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.Stance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStance(string? key, out IReadOnlyList<HeistDoodadNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        if (byStance is null)
        {
            byStance = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stance;

                if (!byStance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStance.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.byStance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistDoodadNPCsDat>> GetManyToManyByStance(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistDoodadNPCsDat>>();
        }

        var items = new List<ResultItem<string, HeistDoodadNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistDoodadNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.BetrayalTargetsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalTargetsKey(int? key, out HeistDoodadNPCsDat? item)
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
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.BetrayalTargetsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalTargetsKey(int? key, out IReadOnlyList<HeistDoodadNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistDoodadNPCsDat>();
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
            items = Array.Empty<HeistDoodadNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistDoodadNPCsDat"/> with <see cref="HeistDoodadNPCsDat.byBetrayalTargetsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistDoodadNPCsDat>> GetManyToManyByBetrayalTargetsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistDoodadNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistDoodadNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalTargetsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistDoodadNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistDoodadNPCsDat[] Load()
    {
        const string filePath = "Data/HeistDoodadNPCs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistDoodadNPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading NPCsKey
            (var npcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Stance
            (var stanceLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BetrayalTargetsKey
            (var betrayaltargetskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistDoodadNPCsDat()
            {
                NPCsKey = npcskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                AOFile = aofileLoading,
                Stance = stanceLoading,
                BetrayalTargetsKey = betrayaltargetskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
