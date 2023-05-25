using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasPrimordialBossesDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasPrimordialBossesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasPrimordialBossesDat> Items { get; }

    private Dictionary<string, List<AtlasPrimordialBossesDat>>? byId;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byUnknown8;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byUnknown12;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byUnknown16;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byUnknown20;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byInfluenceComplete;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byMiniBossInvitation;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byBossInvitation;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byPickUpKey;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byUnknown88;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byUnknown104;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byTag;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byAltar;
    private Dictionary<int, List<AtlasPrimordialBossesDat>>? byAltarActivated;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasPrimordialBossesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasPrimordialBossesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AtlasPrimordialBossesDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
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
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialBossesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out AtlasPrimordialBossesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;

                if (!byUnknown8.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out AtlasPrimordialBossesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown12(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;

                if (!byUnknown12.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown12.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out AtlasPrimordialBossesDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
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
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out AtlasPrimordialBossesDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
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
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.InfluenceComplete"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInfluenceComplete(int? key, out AtlasPrimordialBossesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInfluenceComplete(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.InfluenceComplete"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInfluenceComplete(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byInfluenceComplete is null)
        {
            byInfluenceComplete = new();
            foreach (var item in Items)
            {
                var itemKey = item.InfluenceComplete;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byInfluenceComplete.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byInfluenceComplete.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byInfluenceComplete.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byInfluenceComplete"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByInfluenceComplete(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInfluenceComplete(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.MiniBossInvitation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiniBossInvitation(int? key, out AtlasPrimordialBossesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiniBossInvitation(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.MiniBossInvitation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiniBossInvitation(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byMiniBossInvitation is null)
        {
            byMiniBossInvitation = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiniBossInvitation;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiniBossInvitation.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiniBossInvitation.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiniBossInvitation.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byMiniBossInvitation"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByMiniBossInvitation(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiniBossInvitation(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.BossInvitation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBossInvitation(int? key, out AtlasPrimordialBossesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBossInvitation(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.BossInvitation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBossInvitation(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byBossInvitation is null)
        {
            byBossInvitation = new();
            foreach (var item in Items)
            {
                var itemKey = item.BossInvitation;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBossInvitation.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBossInvitation.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBossInvitation.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byBossInvitation"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByBossInvitation(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBossInvitation(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.PickUpKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPickUpKey(int? key, out AtlasPrimordialBossesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPickUpKey(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.PickUpKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPickUpKey(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byPickUpKey is null)
        {
            byPickUpKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.PickUpKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPickUpKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPickUpKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPickUpKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byPickUpKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByPickUpKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPickUpKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out AtlasPrimordialBossesDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown88.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(int? key, out AtlasPrimordialBossesDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown104.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown104.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByUnknown104(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Tag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTag(int? key, out AtlasPrimordialBossesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTag(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Tag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTag(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byTag is null)
        {
            byTag = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tag;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTag.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTag.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTag.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byTag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByTag(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Altar"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAltar(int? key, out AtlasPrimordialBossesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAltar(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.Altar"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAltar(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byAltar is null)
        {
            byAltar = new();
            foreach (var item in Items)
            {
                var itemKey = item.Altar;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAltar.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAltar.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAltar.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byAltar"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByAltar(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAltar(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.AltarActivated"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAltarActivated(int? key, out AtlasPrimordialBossesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAltarActivated(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.AltarActivated"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAltarActivated(int? key, out IReadOnlyList<AtlasPrimordialBossesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        if (byAltarActivated is null)
        {
            byAltarActivated = new();
            foreach (var item in Items)
            {
                var itemKey = item.AltarActivated;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAltarActivated.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAltarActivated.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAltarActivated.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossesDat"/> with <see cref="AtlasPrimordialBossesDat.byAltarActivated"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossesDat>> GetManyToManyByAltarActivated(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAltarActivated(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasPrimordialBossesDat[] Load()
    {
        const string filePath = "Data/AtlasPrimordialBosses.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasPrimordialBossesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading InfluenceComplete
            (var influencecompleteLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiniBossInvitation
            (var minibossinvitationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BossInvitation
            (var bossinvitationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PickUpKey
            (var pickupkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Tag
            (var tagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Altar
            (var altarLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AltarActivated
            (var altaractivatedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasPrimordialBossesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                InfluenceComplete = influencecompleteLoading,
                MiniBossInvitation = minibossinvitationLoading,
                BossInvitation = bossinvitationLoading,
                PickUpKey = pickupkeyLoading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
                Tag = tagLoading,
                Altar = altarLoading,
                AltarActivated = altaractivatedLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
