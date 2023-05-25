using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MeleeDat"/> related data and helper methods.
/// </summary>
public sealed class MeleeRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MeleeDat> Items { get; }

    private Dictionary<int, List<MeleeDat>>? byActiveSkill;
    private Dictionary<int, List<MeleeDat>>? byUnknown16;
    private Dictionary<int, List<MeleeDat>>? byMiscAnimated;
    private Dictionary<int, List<MeleeDat>>? byMeleeTrailsKey1;
    private Dictionary<int, List<MeleeDat>>? byMeleeTrailsKey2;
    private Dictionary<int, List<MeleeDat>>? byMeleeTrailsKey3;
    private Dictionary<int, List<MeleeDat>>? byMeleeTrailsKey4;
    private Dictionary<int, List<MeleeDat>>? byMeleeTrailsKey5;
    private Dictionary<int, List<MeleeDat>>? byMeleeTrailsKey6;
    private Dictionary<int, List<MeleeDat>>? byMeleeTrailsKey7;
    private Dictionary<bool, List<MeleeDat>>? byUnknown148;
    private Dictionary<string, List<MeleeDat>>? bySurgeEffect_EPKFile;
    private Dictionary<string, List<MeleeDat>>? byUnknown157;
    private Dictionary<string, List<MeleeDat>>? byUnknown165;

    /// <summary>
    /// Initializes a new instance of the <see cref="MeleeRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MeleeRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.ActiveSkill"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByActiveSkill(int? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByActiveSkill(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.ActiveSkill"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByActiveSkill(int? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byActiveSkill is null)
        {
            byActiveSkill = new();
            foreach (var item in Items)
            {
                var itemKey = item.ActiveSkill;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byActiveSkill.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byActiveSkill.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byActiveSkill.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byActiveSkill"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeDat>> GetManyToManyByActiveSkill(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeDat>>();
        }

        var items = new List<ResultItem<int, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByActiveSkill(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out MeleeDat? item)
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
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
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeDat>>();
        }

        var items = new List<ResultItem<int, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MiscAnimated"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimated(int? key, out MeleeDat? item)
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MiscAnimated"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimated(int? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
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
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byMiscAnimated"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeDat>> GetManyToManyByMiscAnimated(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeDat>>();
        }

        var items = new List<ResultItem<int, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimated(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMeleeTrailsKey1(int? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMeleeTrailsKey1(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMeleeTrailsKey1(int? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byMeleeTrailsKey1 is null)
        {
            byMeleeTrailsKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MeleeTrailsKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMeleeTrailsKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMeleeTrailsKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMeleeTrailsKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byMeleeTrailsKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeDat>> GetManyToManyByMeleeTrailsKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeDat>>();
        }

        var items = new List<ResultItem<int, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMeleeTrailsKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMeleeTrailsKey2(int? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMeleeTrailsKey2(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMeleeTrailsKey2(int? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byMeleeTrailsKey2 is null)
        {
            byMeleeTrailsKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MeleeTrailsKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMeleeTrailsKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMeleeTrailsKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMeleeTrailsKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byMeleeTrailsKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeDat>> GetManyToManyByMeleeTrailsKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeDat>>();
        }

        var items = new List<ResultItem<int, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMeleeTrailsKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMeleeTrailsKey3(int? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMeleeTrailsKey3(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMeleeTrailsKey3(int? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byMeleeTrailsKey3 is null)
        {
            byMeleeTrailsKey3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MeleeTrailsKey3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMeleeTrailsKey3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMeleeTrailsKey3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMeleeTrailsKey3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byMeleeTrailsKey3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeDat>> GetManyToManyByMeleeTrailsKey3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeDat>>();
        }

        var items = new List<ResultItem<int, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMeleeTrailsKey3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMeleeTrailsKey4(int? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMeleeTrailsKey4(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMeleeTrailsKey4(int? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byMeleeTrailsKey4 is null)
        {
            byMeleeTrailsKey4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MeleeTrailsKey4;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMeleeTrailsKey4.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMeleeTrailsKey4.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMeleeTrailsKey4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byMeleeTrailsKey4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeDat>> GetManyToManyByMeleeTrailsKey4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeDat>>();
        }

        var items = new List<ResultItem<int, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMeleeTrailsKey4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMeleeTrailsKey5(int? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMeleeTrailsKey5(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMeleeTrailsKey5(int? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byMeleeTrailsKey5 is null)
        {
            byMeleeTrailsKey5 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MeleeTrailsKey5;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMeleeTrailsKey5.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMeleeTrailsKey5.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMeleeTrailsKey5.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byMeleeTrailsKey5"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeDat>> GetManyToManyByMeleeTrailsKey5(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeDat>>();
        }

        var items = new List<ResultItem<int, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMeleeTrailsKey5(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMeleeTrailsKey6(int? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMeleeTrailsKey6(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMeleeTrailsKey6(int? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byMeleeTrailsKey6 is null)
        {
            byMeleeTrailsKey6 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MeleeTrailsKey6;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMeleeTrailsKey6.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMeleeTrailsKey6.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMeleeTrailsKey6.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byMeleeTrailsKey6"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeDat>> GetManyToManyByMeleeTrailsKey6(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeDat>>();
        }

        var items = new List<ResultItem<int, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMeleeTrailsKey6(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey7"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMeleeTrailsKey7(int? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMeleeTrailsKey7(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.MeleeTrailsKey7"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMeleeTrailsKey7(int? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byMeleeTrailsKey7 is null)
        {
            byMeleeTrailsKey7 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MeleeTrailsKey7;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMeleeTrailsKey7.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMeleeTrailsKey7.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMeleeTrailsKey7.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byMeleeTrailsKey7"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeDat>> GetManyToManyByMeleeTrailsKey7(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeDat>>();
        }

        var items = new List<ResultItem<int, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMeleeTrailsKey7(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown148(bool? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown148(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown148(bool? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byUnknown148 is null)
        {
            byUnknown148 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown148;

                if (!byUnknown148.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown148.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown148.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byUnknown148"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MeleeDat>> GetManyToManyByUnknown148(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MeleeDat>>();
        }

        var items = new List<ResultItem<bool, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown148(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.SurgeEffect_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySurgeEffect_EPKFile(string? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySurgeEffect_EPKFile(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.SurgeEffect_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySurgeEffect_EPKFile(string? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (bySurgeEffect_EPKFile is null)
        {
            bySurgeEffect_EPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.SurgeEffect_EPKFile;

                if (!bySurgeEffect_EPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySurgeEffect_EPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySurgeEffect_EPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.bySurgeEffect_EPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MeleeDat>> GetManyToManyBySurgeEffect_EPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MeleeDat>>();
        }

        var items = new List<ResultItem<string, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySurgeEffect_EPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.Unknown157"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown157(string? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown157(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.Unknown157"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown157(string? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byUnknown157 is null)
        {
            byUnknown157 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown157;

                if (!byUnknown157.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown157.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown157.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byUnknown157"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MeleeDat>> GetManyToManyByUnknown157(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MeleeDat>>();
        }

        var items = new List<ResultItem<string, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown157(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.Unknown165"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown165(string? key, out MeleeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown165(key, out var items))
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
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.Unknown165"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown165(string? key, out IReadOnlyList<MeleeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        if (byUnknown165 is null)
        {
            byUnknown165 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown165;

                if (!byUnknown165.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown165.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown165.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MeleeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeDat"/> with <see cref="MeleeDat.byUnknown165"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MeleeDat>> GetManyToManyByUnknown165(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MeleeDat>>();
        }

        var items = new List<ResultItem<string, MeleeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown165(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MeleeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MeleeDat[] Load()
    {
        const string filePath = "Data/Melee.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MeleeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ActiveSkill
            (var activeskillLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscAnimated
            (var miscanimatedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MeleeTrailsKey1
            (var meleetrailskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MeleeTrailsKey2
            (var meleetrailskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MeleeTrailsKey3
            (var meleetrailskey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MeleeTrailsKey4
            (var meleetrailskey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MeleeTrailsKey5
            (var meleetrailskey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MeleeTrailsKey6
            (var meleetrailskey6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MeleeTrailsKey7
            (var meleetrailskey7Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SurgeEffect_EPKFile
            (var surgeeffect_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown157
            (var unknown157Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown165
            (var unknown165Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MeleeDat()
            {
                ActiveSkill = activeskillLoading,
                Unknown16 = unknown16Loading,
                MiscAnimated = miscanimatedLoading,
                MeleeTrailsKey1 = meleetrailskey1Loading,
                MeleeTrailsKey2 = meleetrailskey2Loading,
                MeleeTrailsKey3 = meleetrailskey3Loading,
                MeleeTrailsKey4 = meleetrailskey4Loading,
                MeleeTrailsKey5 = meleetrailskey5Loading,
                MeleeTrailsKey6 = meleetrailskey6Loading,
                MeleeTrailsKey7 = meleetrailskey7Loading,
                Unknown148 = unknown148Loading,
                SurgeEffect_EPKFile = surgeeffect_epkfileLoading,
                Unknown157 = unknown157Loading,
                Unknown165 = unknown165Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
