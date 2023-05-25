using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MiscEffectPacksDat"/> related data and helper methods.
/// </summary>
public sealed class MiscEffectPacksRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MiscEffectPacksDat> Items { get; }

    private Dictionary<string, List<MiscEffectPacksDat>>? byId;
    private Dictionary<string, List<MiscEffectPacksDat>>? byEPKFile;
    private Dictionary<int, List<MiscEffectPacksDat>>? byUnknown16;
    private Dictionary<int, List<MiscEffectPacksDat>>? byUnknown20;
    private Dictionary<int, List<MiscEffectPacksDat>>? byUnknown24;
    private Dictionary<int, List<MiscEffectPacksDat>>? byPreloadGroups;
    private Dictionary<bool, List<MiscEffectPacksDat>>? byUnknown44;
    private Dictionary<string, List<MiscEffectPacksDat>>? byPlayerOnly_EPKFile;
    private Dictionary<bool, List<MiscEffectPacksDat>>? byUnknown53;

    /// <summary>
    /// Initializes a new instance of the <see cref="MiscEffectPacksRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MiscEffectPacksRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MiscEffectPacksDat? item)
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
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MiscEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MiscEffectPacksDat>();
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
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MiscEffectPacksDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MiscEffectPacksDat>>();
        }

        var items = new List<ResultItem<string, MiscEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MiscEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEPKFile(string? key, out MiscEffectPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEPKFile(key, out var items))
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
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEPKFile(string? key, out IReadOnlyList<MiscEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        if (byEPKFile is null)
        {
            byEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.EPKFile;

                if (!byEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.byEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MiscEffectPacksDat>> GetManyToManyByEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MiscEffectPacksDat>>();
        }

        var items = new List<ResultItem<string, MiscEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MiscEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out MiscEffectPacksDat? item)
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
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<MiscEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MiscEffectPacksDat>();
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
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MiscEffectPacksDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MiscEffectPacksDat>>();
        }

        var items = new List<ResultItem<int, MiscEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MiscEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out MiscEffectPacksDat? item)
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
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<MiscEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MiscEffectPacksDat>();
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
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MiscEffectPacksDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MiscEffectPacksDat>>();
        }

        var items = new List<ResultItem<int, MiscEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MiscEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out MiscEffectPacksDat? item)
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
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<MiscEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MiscEffectPacksDat>();
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
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MiscEffectPacksDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MiscEffectPacksDat>>();
        }

        var items = new List<ResultItem<int, MiscEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MiscEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.PreloadGroups"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPreloadGroups(int? key, out MiscEffectPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPreloadGroups(key, out var items))
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
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.PreloadGroups"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPreloadGroups(int? key, out IReadOnlyList<MiscEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        if (byPreloadGroups is null)
        {
            byPreloadGroups = new();
            foreach (var item in Items)
            {
                var itemKey = item.PreloadGroups;
                foreach (var listKey in itemKey)
                {
                    if (!byPreloadGroups.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPreloadGroups.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPreloadGroups.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.byPreloadGroups"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MiscEffectPacksDat>> GetManyToManyByPreloadGroups(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MiscEffectPacksDat>>();
        }

        var items = new List<ResultItem<int, MiscEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPreloadGroups(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MiscEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(bool? key, out MiscEffectPacksDat? item)
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
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(bool? key, out IReadOnlyList<MiscEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MiscEffectPacksDat>();
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
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MiscEffectPacksDat>> GetManyToManyByUnknown44(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MiscEffectPacksDat>>();
        }

        var items = new List<ResultItem<bool, MiscEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MiscEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.PlayerOnly_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPlayerOnly_EPKFile(string? key, out MiscEffectPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPlayerOnly_EPKFile(key, out var items))
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
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.PlayerOnly_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPlayerOnly_EPKFile(string? key, out IReadOnlyList<MiscEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        if (byPlayerOnly_EPKFile is null)
        {
            byPlayerOnly_EPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.PlayerOnly_EPKFile;

                if (!byPlayerOnly_EPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPlayerOnly_EPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPlayerOnly_EPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.byPlayerOnly_EPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MiscEffectPacksDat>> GetManyToManyByPlayerOnly_EPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MiscEffectPacksDat>>();
        }

        var items = new List<ResultItem<string, MiscEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPlayerOnly_EPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MiscEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Unknown53"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown53(bool? key, out MiscEffectPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown53(key, out var items))
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
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.Unknown53"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown53(bool? key, out IReadOnlyList<MiscEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        if (byUnknown53 is null)
        {
            byUnknown53 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown53;

                if (!byUnknown53.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown53.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown53.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MiscEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MiscEffectPacksDat"/> with <see cref="MiscEffectPacksDat.byUnknown53"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MiscEffectPacksDat>> GetManyToManyByUnknown53(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MiscEffectPacksDat>>();
        }

        var items = new List<ResultItem<bool, MiscEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown53(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MiscEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MiscEffectPacksDat[] Load()
    {
        const string filePath = "Data/MiscEffectPacks.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MiscEffectPacksDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EPKFile
            (var epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PreloadGroups
            (var temppreloadgroupsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var preloadgroupsLoading = temppreloadgroupsLoading.AsReadOnly();

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading PlayerOnly_EPKFile
            (var playeronly_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown53
            (var unknown53Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MiscEffectPacksDat()
            {
                Id = idLoading,
                EPKFile = epkfileLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                PreloadGroups = preloadgroupsLoading,
                Unknown44 = unknown44Loading,
                PlayerOnly_EPKFile = playeronly_epkfileLoading,
                Unknown53 = unknown53Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
