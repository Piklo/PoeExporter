using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="StatsDat"/> related data and helper methods.
/// </summary>
public sealed class StatsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<StatsDat> Items { get; }

    private Dictionary<string, List<StatsDat>>? byId;
    private Dictionary<bool, List<StatsDat>>? byUnknown8;
    private Dictionary<bool, List<StatsDat>>? byIsLocal;
    private Dictionary<bool, List<StatsDat>>? byIsWeaponLocal;
    private Dictionary<int, List<StatsDat>>? bySemantics;
    private Dictionary<string, List<StatsDat>>? byText;
    private Dictionary<bool, List<StatsDat>>? byUnknown23;
    private Dictionary<bool, List<StatsDat>>? byIsVirtual;
    private Dictionary<int, List<StatsDat>>? byMainHandAlias_StatsKey;
    private Dictionary<int, List<StatsDat>>? byOffHandAlias_StatsKey;
    private Dictionary<bool, List<StatsDat>>? byUnknown41;
    private Dictionary<int, List<StatsDat>>? byHASH32;
    private Dictionary<string, List<StatsDat>>? byBelongsActiveSkillsKey;
    private Dictionary<int, List<StatsDat>>? byCategory;
    private Dictionary<bool, List<StatsDat>>? byUnknown78;
    private Dictionary<bool, List<StatsDat>>? byUnknown79;
    private Dictionary<bool, List<StatsDat>>? byIsScalable;
    private Dictionary<int, List<StatsDat>>? byContextFlags;
    private Dictionary<int, List<StatsDat>>? byUnknown97;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal StatsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out StatsDat? item)
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
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
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, StatsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, StatsDat>>();
        }

        var items = new List<ResultItem<string, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(bool? key, out StatsDat? item)
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(bool? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
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
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StatsDat>> GetManyToManyByUnknown8(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StatsDat>>();
        }

        var items = new List<ResultItem<bool, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.IsLocal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsLocal(bool? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsLocal(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.IsLocal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsLocal(bool? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byIsLocal is null)
        {
            byIsLocal = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsLocal;

                if (!byIsLocal.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsLocal.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsLocal.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byIsLocal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StatsDat>> GetManyToManyByIsLocal(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StatsDat>>();
        }

        var items = new List<ResultItem<bool, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsLocal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.IsWeaponLocal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsWeaponLocal(bool? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsWeaponLocal(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.IsWeaponLocal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsWeaponLocal(bool? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byIsWeaponLocal is null)
        {
            byIsWeaponLocal = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsWeaponLocal;

                if (!byIsWeaponLocal.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsWeaponLocal.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsWeaponLocal.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byIsWeaponLocal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StatsDat>> GetManyToManyByIsWeaponLocal(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StatsDat>>();
        }

        var items = new List<ResultItem<bool, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsWeaponLocal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Semantics"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySemantics(int? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySemantics(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Semantics"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySemantics(int? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (bySemantics is null)
        {
            bySemantics = new();
            foreach (var item in Items)
            {
                var itemKey = item.Semantics;

                if (!bySemantics.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySemantics.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySemantics.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.bySemantics"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StatsDat>> GetManyToManyBySemantics(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StatsDat>>();
        }

        var items = new List<ResultItem<int, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySemantics(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByText(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byText is null)
        {
            byText = new();
            foreach (var item in Items)
            {
                var itemKey = item.Text;

                if (!byText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, StatsDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, StatsDat>>();
        }

        var items = new List<ResultItem<string, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown23"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown23(bool? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown23(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown23"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown23(bool? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byUnknown23 is null)
        {
            byUnknown23 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown23;

                if (!byUnknown23.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown23.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown23.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byUnknown23"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StatsDat>> GetManyToManyByUnknown23(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StatsDat>>();
        }

        var items = new List<ResultItem<bool, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown23(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.IsVirtual"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsVirtual(bool? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsVirtual(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.IsVirtual"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsVirtual(bool? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byIsVirtual is null)
        {
            byIsVirtual = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsVirtual;

                if (!byIsVirtual.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsVirtual.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsVirtual.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byIsVirtual"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StatsDat>> GetManyToManyByIsVirtual(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StatsDat>>();
        }

        var items = new List<ResultItem<bool, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsVirtual(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.MainHandAlias_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMainHandAlias_StatsKey(int? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMainHandAlias_StatsKey(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.MainHandAlias_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMainHandAlias_StatsKey(int? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byMainHandAlias_StatsKey is null)
        {
            byMainHandAlias_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MainHandAlias_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMainHandAlias_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMainHandAlias_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMainHandAlias_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byMainHandAlias_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StatsDat>> GetManyToManyByMainHandAlias_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StatsDat>>();
        }

        var items = new List<ResultItem<int, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMainHandAlias_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.OffHandAlias_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOffHandAlias_StatsKey(int? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOffHandAlias_StatsKey(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.OffHandAlias_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOffHandAlias_StatsKey(int? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byOffHandAlias_StatsKey is null)
        {
            byOffHandAlias_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.OffHandAlias_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byOffHandAlias_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byOffHandAlias_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byOffHandAlias_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byOffHandAlias_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StatsDat>> GetManyToManyByOffHandAlias_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StatsDat>>();
        }

        var items = new List<ResultItem<int, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOffHandAlias_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown41(bool? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown41(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown41(bool? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byUnknown41 is null)
        {
            byUnknown41 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown41;

                if (!byUnknown41.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown41.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown41.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byUnknown41"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StatsDat>> GetManyToManyByUnknown41(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StatsDat>>();
        }

        var items = new List<ResultItem<bool, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown41(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.HASH32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH32(int? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHASH32(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.HASH32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH32(int? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byHASH32 is null)
        {
            byHASH32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HASH32;

                if (!byHASH32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHASH32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHASH32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byHASH32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StatsDat>> GetManyToManyByHASH32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StatsDat>>();
        }

        var items = new List<ResultItem<int, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.BelongsActiveSkillsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBelongsActiveSkillsKey(string? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBelongsActiveSkillsKey(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.BelongsActiveSkillsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBelongsActiveSkillsKey(string? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byBelongsActiveSkillsKey is null)
        {
            byBelongsActiveSkillsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BelongsActiveSkillsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byBelongsActiveSkillsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBelongsActiveSkillsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBelongsActiveSkillsKey.TryGetValue(key, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byBelongsActiveSkillsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, StatsDat>> GetManyToManyByBelongsActiveSkillsKey(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, StatsDat>>();
        }

        var items = new List<ResultItem<string, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBelongsActiveSkillsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Category"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCategory(int? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCategory(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Category"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCategory(int? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byCategory is null)
        {
            byCategory = new();
            foreach (var item in Items)
            {
                var itemKey = item.Category;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCategory.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCategory.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCategory.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byCategory"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StatsDat>> GetManyToManyByCategory(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StatsDat>>();
        }

        var items = new List<ResultItem<int, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCategory(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown78(bool? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown78(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown78(bool? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byUnknown78 is null)
        {
            byUnknown78 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown78;

                if (!byUnknown78.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown78.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown78.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byUnknown78"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StatsDat>> GetManyToManyByUnknown78(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StatsDat>>();
        }

        var items = new List<ResultItem<bool, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown78(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown79(bool? key, out StatsDat? item)
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown79(bool? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
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
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byUnknown79"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StatsDat>> GetManyToManyByUnknown79(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StatsDat>>();
        }

        var items = new List<ResultItem<bool, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown79(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.IsScalable"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsScalable(bool? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsScalable(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.IsScalable"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsScalable(bool? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byIsScalable is null)
        {
            byIsScalable = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsScalable;

                if (!byIsScalable.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsScalable.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsScalable.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byIsScalable"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StatsDat>> GetManyToManyByIsScalable(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StatsDat>>();
        }

        var items = new List<ResultItem<bool, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsScalable(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.ContextFlags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByContextFlags(int? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByContextFlags(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.ContextFlags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByContextFlags(int? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byContextFlags is null)
        {
            byContextFlags = new();
            foreach (var item in Items)
            {
                var itemKey = item.ContextFlags;
                foreach (var listKey in itemKey)
                {
                    if (!byContextFlags.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byContextFlags.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byContextFlags.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byContextFlags"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StatsDat>> GetManyToManyByContextFlags(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StatsDat>>();
        }

        var items = new List<ResultItem<int, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByContextFlags(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown97(int? key, out StatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown97(key, out var items))
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
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown97(int? key, out IReadOnlyList<StatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        if (byUnknown97 is null)
        {
            byUnknown97 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown97;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown97.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown97.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown97.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatsDat"/> with <see cref="StatsDat.byUnknown97"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StatsDat>> GetManyToManyByUnknown97(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StatsDat>>();
        }

        var items = new List<ResultItem<int, StatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown97(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private StatsDat[] Load()
    {
        const string filePath = "Data/Stats.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsLocal
            (var islocalLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsWeaponLocal
            (var isweaponlocalLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Semantics
            (var semanticsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown23
            (var unknown23Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsVirtual
            (var isvirtualLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MainHandAlias_StatsKey
            (var mainhandalias_statskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading OffHandAlias_StatsKey
            (var offhandalias_statskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HASH32
            (var hash32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BelongsActiveSkillsKey
            (var tempbelongsactiveskillskeyLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var belongsactiveskillskeyLoading = tempbelongsactiveskillskeyLoading.AsReadOnly();

            // loading Category
            (var categoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsScalable
            (var isscalableLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ContextFlags
            (var tempcontextflagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var contextflagsLoading = tempcontextflagsLoading.AsReadOnly();

            // loading Unknown97
            (var tempunknown97Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown97Loading = tempunknown97Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StatsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                IsLocal = islocalLoading,
                IsWeaponLocal = isweaponlocalLoading,
                Semantics = semanticsLoading,
                Text = textLoading,
                Unknown23 = unknown23Loading,
                IsVirtual = isvirtualLoading,
                MainHandAlias_StatsKey = mainhandalias_statskeyLoading,
                OffHandAlias_StatsKey = offhandalias_statskeyLoading,
                Unknown41 = unknown41Loading,
                HASH32 = hash32Loading,
                BelongsActiveSkillsKey = belongsactiveskillskeyLoading,
                Category = categoryLoading,
                Unknown78 = unknown78Loading,
                Unknown79 = unknown79Loading,
                IsScalable = isscalableLoading,
                ContextFlags = contextflagsLoading,
                Unknown97 = unknown97Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
