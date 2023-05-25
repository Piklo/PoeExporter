using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SkillSurgeEffectsDat"/> related data and helper methods.
/// </summary>
public sealed class SkillSurgeEffectsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SkillSurgeEffectsDat> Items { get; }

    private Dictionary<int, List<SkillSurgeEffectsDat>>? byGrantedEffectsKey;
    private Dictionary<string, List<SkillSurgeEffectsDat>>? byUnknown16;
    private Dictionary<bool, List<SkillSurgeEffectsDat>>? byUnknown24;
    private Dictionary<bool, List<SkillSurgeEffectsDat>>? byUnknown25;
    private Dictionary<bool, List<SkillSurgeEffectsDat>>? byUnknown26;
    private Dictionary<int, List<SkillSurgeEffectsDat>>? byMiscAnimated;
    private Dictionary<bool, List<SkillSurgeEffectsDat>>? byUnknown43;
    private Dictionary<bool, List<SkillSurgeEffectsDat>>? byUnknown44;
    private Dictionary<int, List<SkillSurgeEffectsDat>>? byUnknown45;
    private Dictionary<bool, List<SkillSurgeEffectsDat>>? byUnknown49;
    private Dictionary<bool, List<SkillSurgeEffectsDat>>? byUnknown50;
    private Dictionary<bool, List<SkillSurgeEffectsDat>>? byUnknown51;
    private Dictionary<bool, List<SkillSurgeEffectsDat>>? byUnknown52;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkillSurgeEffectsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SkillSurgeEffectsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.GrantedEffectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffectsKey(int? key, out SkillSurgeEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffectsKey(key, out var items))
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.GrantedEffectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffectsKey(int? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        if (byGrantedEffectsKey is null)
        {
            byGrantedEffectsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffectsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGrantedEffectsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGrantedEffectsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGrantedEffectsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byGrantedEffectsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillSurgeEffectsDat>> GetManyToManyByGrantedEffectsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<int, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffectsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(string? key, out SkillSurgeEffectsDat? item)
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(string? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
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

        if (!byUnknown16.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SkillSurgeEffectsDat>> GetManyToManyByUnknown16(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<string, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(bool? key, out SkillSurgeEffectsDat? item)
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(bool? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
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
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillSurgeEffectsDat>> GetManyToManyByUnknown24(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<bool, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown25(bool? key, out SkillSurgeEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown25(key, out var items))
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown25(bool? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        if (byUnknown25 is null)
        {
            byUnknown25 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown25;

                if (!byUnknown25.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown25.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown25.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown25"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillSurgeEffectsDat>> GetManyToManyByUnknown25(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<bool, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown25(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown26"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown26(bool? key, out SkillSurgeEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown26(key, out var items))
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown26"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown26(bool? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        if (byUnknown26 is null)
        {
            byUnknown26 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown26;

                if (!byUnknown26.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown26.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown26.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown26"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillSurgeEffectsDat>> GetManyToManyByUnknown26(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<bool, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown26(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.MiscAnimated"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimated(int? key, out SkillSurgeEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimated(key, out var items))
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.MiscAnimated"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimated(int? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        if (byMiscAnimated is null)
        {
            byMiscAnimated = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimated;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimated.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimated.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimated.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byMiscAnimated"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillSurgeEffectsDat>> GetManyToManyByMiscAnimated(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<int, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimated(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown43"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown43(bool? key, out SkillSurgeEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown43(key, out var items))
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown43"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown43(bool? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        if (byUnknown43 is null)
        {
            byUnknown43 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown43;

                if (!byUnknown43.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown43.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown43.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown43"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillSurgeEffectsDat>> GetManyToManyByUnknown43(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<bool, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown43(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(bool? key, out SkillSurgeEffectsDat? item)
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(bool? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
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
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillSurgeEffectsDat>> GetManyToManyByUnknown44(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<bool, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown45(int? key, out SkillSurgeEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown45(key, out var items))
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown45(int? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        if (byUnknown45 is null)
        {
            byUnknown45 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown45;

                if (!byUnknown45.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown45.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown45.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown45"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillSurgeEffectsDat>> GetManyToManyByUnknown45(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<int, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown45(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown49(bool? key, out SkillSurgeEffectsDat? item)
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown49(bool? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
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
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown49"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillSurgeEffectsDat>> GetManyToManyByUnknown49(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<bool, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown49(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown50"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown50(bool? key, out SkillSurgeEffectsDat? item)
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown50"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown50(bool? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
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
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown50"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillSurgeEffectsDat>> GetManyToManyByUnknown50(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<bool, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown50(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown51"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown51(bool? key, out SkillSurgeEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown51(key, out var items))
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown51"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown51(bool? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        if (byUnknown51 is null)
        {
            byUnknown51 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown51;

                if (!byUnknown51.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown51.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown51.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown51"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillSurgeEffectsDat>> GetManyToManyByUnknown51(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<bool, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown51(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(bool? key, out SkillSurgeEffectsDat? item)
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
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(bool? key, out IReadOnlyList<SkillSurgeEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillSurgeEffectsDat>();
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
            items = Array.Empty<SkillSurgeEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillSurgeEffectsDat"/> with <see cref="SkillSurgeEffectsDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillSurgeEffectsDat>> GetManyToManyByUnknown52(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillSurgeEffectsDat>>();
        }

        var items = new List<ResultItem<bool, SkillSurgeEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillSurgeEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SkillSurgeEffectsDat[] Load()
    {
        const string filePath = "Data/SkillSurgeEffects.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillSurgeEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading GrantedEffectsKey
            (var grantedeffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown26
            (var unknown26Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MiscAnimated
            (var miscanimatedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown43
            (var unknown43Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown51
            (var unknown51Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillSurgeEffectsDat()
            {
                GrantedEffectsKey = grantedeffectskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown24 = unknown24Loading,
                Unknown25 = unknown25Loading,
                Unknown26 = unknown26Loading,
                MiscAnimated = miscanimatedLoading,
                Unknown43 = unknown43Loading,
                Unknown44 = unknown44Loading,
                Unknown45 = unknown45Loading,
                Unknown49 = unknown49Loading,
                Unknown50 = unknown50Loading,
                Unknown51 = unknown51Loading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
