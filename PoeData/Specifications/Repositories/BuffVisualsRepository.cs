using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BuffVisualsDat"/> related data and helper methods.
/// </summary>
public sealed class BuffVisualsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BuffVisualsDat> Items { get; }

    private Dictionary<string, List<BuffVisualsDat>>? byId;
    private Dictionary<string, List<BuffVisualsDat>>? byBuffDDSFile;
    private Dictionary<string, List<BuffVisualsDat>>? byEPKFiles1;
    private Dictionary<string, List<BuffVisualsDat>>? byEPKFiles2;
    private Dictionary<int, List<BuffVisualsDat>>? byPreloadGroups;
    private Dictionary<bool, List<BuffVisualsDat>>? byUnknown64;
    private Dictionary<string, List<BuffVisualsDat>>? byBuffName;
    private Dictionary<int, List<BuffVisualsDat>>? byMiscAnimated1;
    private Dictionary<int, List<BuffVisualsDat>>? byMiscAnimated2;
    private Dictionary<string, List<BuffVisualsDat>>? byBuffDescription;
    private Dictionary<string, List<BuffVisualsDat>>? byEPKFile;
    private Dictionary<bool, List<BuffVisualsDat>>? byHasExtraArt;
    private Dictionary<string, List<BuffVisualsDat>>? byExtraArt;
    private Dictionary<int, List<BuffVisualsDat>>? byUnknown130;
    private Dictionary<string, List<BuffVisualsDat>>? byEPKFiles;
    private Dictionary<int, List<BuffVisualsDat>>? byBuffVisualOrbs;
    private Dictionary<int, List<BuffVisualsDat>>? byMiscAnimated3;
    private Dictionary<int, List<BuffVisualsDat>>? byUnknown194;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuffVisualsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BuffVisualsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BuffVisualsDat? item)
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
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
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.BuffDDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffDDSFile(string? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffDDSFile(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.BuffDDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffDDSFile(string? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byBuffDDSFile is null)
        {
            byBuffDDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffDDSFile;

                if (!byBuffDDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBuffDDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffDDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byBuffDDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualsDat>> GetManyToManyByBuffDDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffDDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.EPKFiles1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEPKFiles1(string? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEPKFiles1(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.EPKFiles1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEPKFiles1(string? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byEPKFiles1 is null)
        {
            byEPKFiles1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.EPKFiles1;
                foreach (var listKey in itemKey)
                {
                    if (!byEPKFiles1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEPKFiles1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEPKFiles1.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byEPKFiles1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualsDat>> GetManyToManyByEPKFiles1(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEPKFiles1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.EPKFiles2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEPKFiles2(string? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEPKFiles2(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.EPKFiles2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEPKFiles2(string? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byEPKFiles2 is null)
        {
            byEPKFiles2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.EPKFiles2;
                foreach (var listKey in itemKey)
                {
                    if (!byEPKFiles2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEPKFiles2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEPKFiles2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byEPKFiles2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualsDat>> GetManyToManyByEPKFiles2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEPKFiles2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.PreloadGroups"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPreloadGroups(int? key, out BuffVisualsDat? item)
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.PreloadGroups"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPreloadGroups(int? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
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
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byPreloadGroups"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualsDat>> GetManyToManyByPreloadGroups(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPreloadGroups(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(bool? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(bool? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffVisualsDat>> GetManyToManyByUnknown64(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<bool, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.BuffName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffName(string? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffName(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.BuffName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffName(string? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byBuffName is null)
        {
            byBuffName = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffName;

                if (!byBuffName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBuffName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byBuffName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualsDat>> GetManyToManyByBuffName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.MiscAnimated1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimated1(int? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimated1(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.MiscAnimated1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimated1(int? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byMiscAnimated1 is null)
        {
            byMiscAnimated1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimated1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimated1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimated1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimated1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byMiscAnimated1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualsDat>> GetManyToManyByMiscAnimated1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimated1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.MiscAnimated2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimated2(int? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimated2(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.MiscAnimated2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimated2(int? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byMiscAnimated2 is null)
        {
            byMiscAnimated2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimated2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimated2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimated2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimated2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byMiscAnimated2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualsDat>> GetManyToManyByMiscAnimated2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimated2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.BuffDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffDescription(string? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffDescription(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.BuffDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffDescription(string? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byBuffDescription is null)
        {
            byBuffDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffDescription;

                if (!byBuffDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBuffDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byBuffDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualsDat>> GetManyToManyByBuffDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEPKFile(string? key, out BuffVisualsDat? item)
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEPKFile(string? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
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
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualsDat>> GetManyToManyByEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.HasExtraArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHasExtraArt(bool? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHasExtraArt(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.HasExtraArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHasExtraArt(bool? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byHasExtraArt is null)
        {
            byHasExtraArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.HasExtraArt;

                if (!byHasExtraArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHasExtraArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHasExtraArt.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byHasExtraArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffVisualsDat>> GetManyToManyByHasExtraArt(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<bool, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHasExtraArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.ExtraArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExtraArt(string? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExtraArt(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.ExtraArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExtraArt(string? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byExtraArt is null)
        {
            byExtraArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.ExtraArt;

                if (!byExtraArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byExtraArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byExtraArt.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byExtraArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualsDat>> GetManyToManyByExtraArt(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExtraArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.Unknown130"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown130(int? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown130(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.Unknown130"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown130(int? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byUnknown130 is null)
        {
            byUnknown130 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown130;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown130.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown130.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown130.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byUnknown130"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualsDat>> GetManyToManyByUnknown130(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown130(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.EPKFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEPKFiles(string? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEPKFiles(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.EPKFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEPKFiles(string? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byEPKFiles is null)
        {
            byEPKFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.EPKFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byEPKFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEPKFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEPKFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byEPKFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualsDat>> GetManyToManyByEPKFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEPKFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.BuffVisualOrbs"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisualOrbs(int? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffVisualOrbs(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.BuffVisualOrbs"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisualOrbs(int? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byBuffVisualOrbs is null)
        {
            byBuffVisualOrbs = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffVisualOrbs;
                foreach (var listKey in itemKey)
                {
                    if (!byBuffVisualOrbs.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBuffVisualOrbs.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBuffVisualOrbs.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byBuffVisualOrbs"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualsDat>> GetManyToManyByBuffVisualOrbs(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisualOrbs(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.MiscAnimated3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimated3(int? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimated3(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.MiscAnimated3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimated3(int? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byMiscAnimated3 is null)
        {
            byMiscAnimated3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimated3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimated3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimated3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimated3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byMiscAnimated3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualsDat>> GetManyToManyByMiscAnimated3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimated3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.Unknown194"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown194(int? key, out BuffVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown194(key, out var items))
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
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.Unknown194"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown194(int? key, out IReadOnlyList<BuffVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        if (byUnknown194 is null)
        {
            byUnknown194 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown194;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown194.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown194.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown194.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualsDat"/> with <see cref="BuffVisualsDat.byUnknown194"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualsDat>> GetManyToManyByUnknown194(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown194(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BuffVisualsDat[] Load()
    {
        const string filePath = "Data/BuffVisuals.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BuffVisualsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffDDSFile
            (var buffddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EPKFiles1
            (var tempepkfiles1Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var epkfiles1Loading = tempepkfiles1Loading.AsReadOnly();

            // loading EPKFiles2
            (var tempepkfiles2Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var epkfiles2Loading = tempepkfiles2Loading.AsReadOnly();

            // loading PreloadGroups
            (var temppreloadgroupsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var preloadgroupsLoading = temppreloadgroupsLoading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading BuffName
            (var buffnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscAnimated1
            (var miscanimated1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimated2
            (var miscanimated2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BuffDescription
            (var buffdescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EPKFile
            (var epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HasExtraArt
            (var hasextraartLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ExtraArt
            (var extraartLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown130
            (var tempunknown130Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown130Loading = tempunknown130Loading.AsReadOnly();

            // loading EPKFiles
            (var tempepkfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var epkfilesLoading = tempepkfilesLoading.AsReadOnly();

            // loading BuffVisualOrbs
            (var tempbuffvisualorbsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var buffvisualorbsLoading = tempbuffvisualorbsLoading.AsReadOnly();

            // loading MiscAnimated3
            (var miscanimated3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown194
            (var unknown194Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BuffVisualsDat()
            {
                Id = idLoading,
                BuffDDSFile = buffddsfileLoading,
                EPKFiles1 = epkfiles1Loading,
                EPKFiles2 = epkfiles2Loading,
                PreloadGroups = preloadgroupsLoading,
                Unknown64 = unknown64Loading,
                BuffName = buffnameLoading,
                MiscAnimated1 = miscanimated1Loading,
                MiscAnimated2 = miscanimated2Loading,
                BuffDescription = buffdescriptionLoading,
                EPKFile = epkfileLoading,
                HasExtraArt = hasextraartLoading,
                ExtraArt = extraartLoading,
                Unknown130 = unknown130Loading,
                EPKFiles = epkfilesLoading,
                BuffVisualOrbs = buffvisualorbsLoading,
                MiscAnimated3 = miscanimated3Loading,
                Unknown194 = unknown194Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
