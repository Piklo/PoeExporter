using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="EffectDrivenSkillDat"/> related data and helper methods.
/// </summary>
public sealed class EffectDrivenSkillRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<EffectDrivenSkillDat> Items { get; }

    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown0;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown4;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown20;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown36;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown40;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown44;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown45;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown46;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown47;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown63;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown67;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown71;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown75;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown79;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown83;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown84;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown88;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown89;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown90;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown94;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown98;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown99;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown100;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown101;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown105;
    private Dictionary<bool, List<EffectDrivenSkillDat>>? byUnknown106;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown107;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown111;
    private Dictionary<int, List<EffectDrivenSkillDat>>? byUnknown115;

    /// <summary>
    /// Initializes a new instance of the <see cref="EffectDrivenSkillRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal EffectDrivenSkillRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;

                if (!byUnknown0.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown4(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown4(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown4(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown4 is null)
        {
            byUnknown4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown4;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown4.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown4.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown20.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown20.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
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
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;

                if (!byUnknown40.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(bool? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
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
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown44(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown45(bool? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown45(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
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
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown45"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown45(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown45(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown46(bool? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown46(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown46(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown46 is null)
        {
            byUnknown46 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown46;

                if (!byUnknown46.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown46.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown46.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown46"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown46(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown46(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown47(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown47(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown47(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown47 is null)
        {
            byUnknown47 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown47;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown47.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown47.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown47.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown47"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown47(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown47(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown63"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown63(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown63(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown63"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown63(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown63 is null)
        {
            byUnknown63 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown63;

                if (!byUnknown63.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown63.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown63.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown63"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown63(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown63(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown67(int? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown67(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
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
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown67"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown67(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown67(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown71"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown71(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown71(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown71"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown71(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown71 is null)
        {
            byUnknown71 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown71;

                if (!byUnknown71.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown71.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown71.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown71"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown71(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown71(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown75"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown75(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown75(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown75"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown75(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown75 is null)
        {
            byUnknown75 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown75;

                if (!byUnknown75.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown75.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown75.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown75"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown75(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown75(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown79(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown79(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown79(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown79 is null)
        {
            byUnknown79 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown79;

                if (!byUnknown79.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown79.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown79.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown79"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown79(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown79(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown83"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown83(bool? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown83(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown83"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown83(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown83 is null)
        {
            byUnknown83 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown83;

                if (!byUnknown83.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown83.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown83.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown83"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown83(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown83(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown84 is null)
        {
            byUnknown84 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown84;

                if (!byUnknown84.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown84.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown84.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(bool? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
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
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown88(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown89"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown89(bool? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown89(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown89"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown89(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown89 is null)
        {
            byUnknown89 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown89;

                if (!byUnknown89.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown89.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown89.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown89"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown89(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown89(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown90"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown90(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown90(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown90"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown90(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown90 is null)
        {
            byUnknown90 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown90;

                if (!byUnknown90.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown90.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown90.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown90"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown90(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown90(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown94"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown94(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown94(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown94"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown94(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown94 is null)
        {
            byUnknown94 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown94;

                if (!byUnknown94.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown94.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown94.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown94"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown94(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown94(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown98"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown98(bool? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown98(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown98"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown98(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown98 is null)
        {
            byUnknown98 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown98;

                if (!byUnknown98.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown98.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown98.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown98"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown98(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown98(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown99(bool? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown99(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown99(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown99 is null)
        {
            byUnknown99 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown99;

                if (!byUnknown99.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown99.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown99.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown99"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown99(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown99(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(bool? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
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
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown100(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown101(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown101(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown101(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown101 is null)
        {
            byUnknown101 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown101;

                if (!byUnknown101.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown101.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown101.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown101"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown101(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown101(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown105(bool? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown105(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
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
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown105"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown105(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown105(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown106(bool? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown106(bool? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
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
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown106"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EffectDrivenSkillDat>> GetManyToManyByUnknown106(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<bool, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown106(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown107"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown107(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown107(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown107"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown107(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown107 is null)
        {
            byUnknown107 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown107;

                if (!byUnknown107.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown107.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown107.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown107"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown107(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown107(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown111"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown111(int? key, out EffectDrivenSkillDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown111(key, out var items))
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown111"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown111(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        if (byUnknown111 is null)
        {
            byUnknown111 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown111;

                if (!byUnknown111.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown111.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown111.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown111"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown111(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown111(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown115(int? key, out EffectDrivenSkillDat? item)
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
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown115(int? key, out IReadOnlyList<EffectDrivenSkillDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectDrivenSkillDat>();
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
            items = Array.Empty<EffectDrivenSkillDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectDrivenSkillDat"/> with <see cref="EffectDrivenSkillDat.byUnknown115"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EffectDrivenSkillDat>> GetManyToManyByUnknown115(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EffectDrivenSkillDat>>();
        }

        var items = new List<ResultItem<int, EffectDrivenSkillDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown115(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EffectDrivenSkillDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private EffectDrivenSkillDat[] Load()
    {
        const string filePath = "Data/EffectDrivenSkill.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EffectDrivenSkillDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var tempunknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown4Loading = tempunknown4Loading.AsReadOnly();

            // loading Unknown20
            (var tempunknown20Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown20Loading = tempunknown20Loading.AsReadOnly();

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown47
            (var tempunknown47Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown47Loading = tempunknown47Loading.AsReadOnly();

            // loading Unknown63
            (var unknown63Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown67
            (var unknown67Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown71
            (var unknown71Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown75
            (var unknown75Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown83
            (var unknown83Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown89
            (var unknown89Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown90
            (var unknown90Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown94
            (var unknown94Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown98
            (var unknown98Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown107
            (var unknown107Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown111
            (var unknown111Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EffectDrivenSkillDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown45 = unknown45Loading,
                Unknown46 = unknown46Loading,
                Unknown47 = unknown47Loading,
                Unknown63 = unknown63Loading,
                Unknown67 = unknown67Loading,
                Unknown71 = unknown71Loading,
                Unknown75 = unknown75Loading,
                Unknown79 = unknown79Loading,
                Unknown83 = unknown83Loading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown89 = unknown89Loading,
                Unknown90 = unknown90Loading,
                Unknown94 = unknown94Loading,
                Unknown98 = unknown98Loading,
                Unknown99 = unknown99Loading,
                Unknown100 = unknown100Loading,
                Unknown101 = unknown101Loading,
                Unknown105 = unknown105Loading,
                Unknown106 = unknown106Loading,
                Unknown107 = unknown107Loading,
                Unknown111 = unknown111Loading,
                Unknown115 = unknown115Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
