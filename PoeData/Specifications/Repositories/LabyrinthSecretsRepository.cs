using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LabyrinthSecretsDat"/> related data and helper methods.
/// </summary>
public sealed class LabyrinthSecretsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LabyrinthSecretsDat> Items { get; }

    private Dictionary<string, List<LabyrinthSecretsDat>>? byId;
    private Dictionary<string, List<LabyrinthSecretsDat>>? byId2;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byUnknown16;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byUnknown32;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byUnknown36;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byLabyrinthSecretEffectsKeys0;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byLabyrinthSecretEffectsKeys1;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byLabyrinthSecretEffectsKeys2;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byUnknown88;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byLabyrinthSecretEffectsKeys3;
    private Dictionary<bool, List<LabyrinthSecretsDat>>? byUnknown108;
    private Dictionary<bool, List<LabyrinthSecretsDat>>? byUnknown109;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byUnknown110;
    private Dictionary<bool, List<LabyrinthSecretsDat>>? byUnknown114;
    private Dictionary<bool, List<LabyrinthSecretsDat>>? byUnknown115;
    private Dictionary<bool, List<LabyrinthSecretsDat>>? byUnknown116;
    private Dictionary<string, List<LabyrinthSecretsDat>>? byName;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byAchievementItemsKey;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byLabyrinthTierMinimum;
    private Dictionary<int, List<LabyrinthSecretsDat>>? byLabyrinthTierMaximum;
    private Dictionary<bool, List<LabyrinthSecretsDat>>? byUnknown149;

    /// <summary>
    /// Initializes a new instance of the <see cref="LabyrinthSecretsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LabyrinthSecretsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out LabyrinthSecretsDat? item)
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
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
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthSecretsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Id2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById2(string? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyById2(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Id2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById2(string? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byId2 is null)
        {
            byId2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Id2;

                if (!byId2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byId2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byId2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byId2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthSecretsDat>> GetManyToManyById2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out LabyrinthSecretsDat? item)
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown16.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown16.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out LabyrinthSecretsDat? item)
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
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
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out LabyrinthSecretsDat? item)
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
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
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthSecretEffectsKeys0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthSecretEffectsKeys0(int? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthSecretEffectsKeys0(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthSecretEffectsKeys0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthSecretEffectsKeys0(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byLabyrinthSecretEffectsKeys0 is null)
        {
            byLabyrinthSecretEffectsKeys0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthSecretEffectsKeys0;
                foreach (var listKey in itemKey)
                {
                    if (!byLabyrinthSecretEffectsKeys0.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byLabyrinthSecretEffectsKeys0.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byLabyrinthSecretEffectsKeys0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byLabyrinthSecretEffectsKeys0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByLabyrinthSecretEffectsKeys0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthSecretEffectsKeys0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthSecretEffectsKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthSecretEffectsKeys1(int? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthSecretEffectsKeys1(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthSecretEffectsKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthSecretEffectsKeys1(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byLabyrinthSecretEffectsKeys1 is null)
        {
            byLabyrinthSecretEffectsKeys1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthSecretEffectsKeys1;
                foreach (var listKey in itemKey)
                {
                    if (!byLabyrinthSecretEffectsKeys1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byLabyrinthSecretEffectsKeys1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byLabyrinthSecretEffectsKeys1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byLabyrinthSecretEffectsKeys1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByLabyrinthSecretEffectsKeys1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthSecretEffectsKeys1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthSecretEffectsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthSecretEffectsKeys2(int? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthSecretEffectsKeys2(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthSecretEffectsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthSecretEffectsKeys2(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byLabyrinthSecretEffectsKeys2 is null)
        {
            byLabyrinthSecretEffectsKeys2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthSecretEffectsKeys2;
                foreach (var listKey in itemKey)
                {
                    if (!byLabyrinthSecretEffectsKeys2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byLabyrinthSecretEffectsKeys2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byLabyrinthSecretEffectsKeys2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byLabyrinthSecretEffectsKeys2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByLabyrinthSecretEffectsKeys2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthSecretEffectsKeys2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown88(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;

                if (!byUnknown88.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthSecretEffectsKeys3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthSecretEffectsKeys3(int? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthSecretEffectsKeys3(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthSecretEffectsKeys3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthSecretEffectsKeys3(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byLabyrinthSecretEffectsKeys3 is null)
        {
            byLabyrinthSecretEffectsKeys3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthSecretEffectsKeys3;
                foreach (var listKey in itemKey)
                {
                    if (!byLabyrinthSecretEffectsKeys3.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byLabyrinthSecretEffectsKeys3.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byLabyrinthSecretEffectsKeys3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byLabyrinthSecretEffectsKeys3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByLabyrinthSecretEffectsKeys3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthSecretEffectsKeys3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(bool? key, out LabyrinthSecretsDat? item)
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(bool? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
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
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LabyrinthSecretsDat>> GetManyToManyByUnknown108(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<bool, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown109(bool? key, out LabyrinthSecretsDat? item)
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown109(bool? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byUnknown109 is null)
        {
            byUnknown109 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown109;

                if (!byUnknown109.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown109.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown109.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown109"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LabyrinthSecretsDat>> GetManyToManyByUnknown109(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<bool, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown109(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown110"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown110(int? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown110(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown110"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown110(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byUnknown110 is null)
        {
            byUnknown110 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown110;

                if (!byUnknown110.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown110.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown110.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown110"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByUnknown110(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown110(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown114"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown114(bool? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown114(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown114"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown114(bool? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byUnknown114 is null)
        {
            byUnknown114 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown114;

                if (!byUnknown114.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown114.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown114.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown114"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LabyrinthSecretsDat>> GetManyToManyByUnknown114(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<bool, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown114(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown115(bool? key, out LabyrinthSecretsDat? item)
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown115(bool? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
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
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown115"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LabyrinthSecretsDat>> GetManyToManyByUnknown115(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<bool, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown115(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown116(bool? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown116(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown116(bool? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byUnknown116 is null)
        {
            byUnknown116 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown116;

                if (!byUnknown116.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown116.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown116.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown116"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LabyrinthSecretsDat>> GetManyToManyByUnknown116(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<bool, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown116(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out LabyrinthSecretsDat? item)
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
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
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthSecretsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKey(int? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKey(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byAchievementItemsKey is null)
        {
            byAchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byAchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByAchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthTierMinimum"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthTierMinimum(int? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthTierMinimum(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthTierMinimum"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthTierMinimum(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byLabyrinthTierMinimum is null)
        {
            byLabyrinthTierMinimum = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthTierMinimum;

                if (!byLabyrinthTierMinimum.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLabyrinthTierMinimum.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLabyrinthTierMinimum.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byLabyrinthTierMinimum"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByLabyrinthTierMinimum(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthTierMinimum(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthTierMaximum"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthTierMaximum(int? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthTierMaximum(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.LabyrinthTierMaximum"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthTierMaximum(int? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byLabyrinthTierMaximum is null)
        {
            byLabyrinthTierMaximum = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthTierMaximum;

                if (!byLabyrinthTierMaximum.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLabyrinthTierMaximum.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLabyrinthTierMaximum.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byLabyrinthTierMaximum"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretsDat>> GetManyToManyByLabyrinthTierMaximum(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthTierMaximum(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown149"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown149(bool? key, out LabyrinthSecretsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown149(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.Unknown149"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown149(bool? key, out IReadOnlyList<LabyrinthSecretsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        if (byUnknown149 is null)
        {
            byUnknown149 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown149;

                if (!byUnknown149.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown149.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown149.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretsDat"/> with <see cref="LabyrinthSecretsDat.byUnknown149"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LabyrinthSecretsDat>> GetManyToManyByUnknown149(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LabyrinthSecretsDat>>();
        }

        var items = new List<ResultItem<bool, LabyrinthSecretsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown149(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LabyrinthSecretsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LabyrinthSecretsDat[] Load()
    {
        const string filePath = "Data/LabyrinthSecrets.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthSecretsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Id2
            (var id2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var tempunknown16Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown16Loading = tempunknown16Loading.AsReadOnly();

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LabyrinthSecretEffectsKeys0
            (var templabyrinthsecreteffectskeys0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsecreteffectskeys0Loading = templabyrinthsecreteffectskeys0Loading.AsReadOnly();

            // loading LabyrinthSecretEffectsKeys1
            (var templabyrinthsecreteffectskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsecreteffectskeys1Loading = templabyrinthsecreteffectskeys1Loading.AsReadOnly();

            // loading LabyrinthSecretEffectsKeys2
            (var templabyrinthsecreteffectskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsecreteffectskeys2Loading = templabyrinthsecreteffectskeys2Loading.AsReadOnly();

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LabyrinthSecretEffectsKeys3
            (var templabyrinthsecreteffectskeys3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsecreteffectskeys3Loading = templabyrinthsecreteffectskeys3Loading.AsReadOnly();

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown110
            (var unknown110Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown114
            (var unknown114Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKey
            (var achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading LabyrinthTierMinimum
            (var labyrinthtierminimumLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LabyrinthTierMaximum
            (var labyrinthtiermaximumLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthSecretsDat()
            {
                Id = idLoading,
                Id2 = id2Loading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                LabyrinthSecretEffectsKeys0 = labyrinthsecreteffectskeys0Loading,
                LabyrinthSecretEffectsKeys1 = labyrinthsecreteffectskeys1Loading,
                LabyrinthSecretEffectsKeys2 = labyrinthsecreteffectskeys2Loading,
                Unknown88 = unknown88Loading,
                LabyrinthSecretEffectsKeys3 = labyrinthsecreteffectskeys3Loading,
                Unknown108 = unknown108Loading,
                Unknown109 = unknown109Loading,
                Unknown110 = unknown110Loading,
                Unknown114 = unknown114Loading,
                Unknown115 = unknown115Loading,
                Unknown116 = unknown116Loading,
                Name = nameLoading,
                AchievementItemsKey = achievementitemskeyLoading,
                LabyrinthTierMinimum = labyrinthtierminimumLoading,
                LabyrinthTierMaximum = labyrinthtiermaximumLoading,
                Unknown149 = unknown149Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
