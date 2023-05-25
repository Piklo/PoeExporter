using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="NPCsDat"/> related data and helper methods.
/// </summary>
public sealed class NPCsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<NPCsDat> Items { get; }

    private Dictionary<string, List<NPCsDat>>? byId;
    private Dictionary<string, List<NPCsDat>>? byName;
    private Dictionary<string, List<NPCsDat>>? byMetadata;
    private Dictionary<int, List<NPCsDat>>? byUnknown24;
    private Dictionary<int, List<NPCsDat>>? byNPCMasterKey;
    private Dictionary<string, List<NPCsDat>>? byShortName;
    private Dictionary<int, List<NPCsDat>>? byUnknown64;
    private Dictionary<int, List<NPCsDat>>? byNPCAudios1;
    private Dictionary<int, List<NPCsDat>>? byNPCAudios2;
    private Dictionary<int, List<NPCsDat>>? byUnknown100;
    private Dictionary<int, List<NPCsDat>>? byUnknown104;
    private Dictionary<int, List<NPCsDat>>? byPortrait;
    private Dictionary<int, List<NPCsDat>>? byDialogueStyle;
    private Dictionary<bool, List<NPCsDat>>? byUnknown144;
    private Dictionary<int, List<NPCsDat>>? byUnknown145;
    private Dictionary<string, List<NPCsDat>>? byGender;

    /// <summary>
    /// Initializes a new instance of the <see cref="NPCsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal NPCsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out NPCsDat? item)
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
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
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCsDat>>();
        }

        var items = new List<ResultItem<string, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out NPCsDat? item)
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
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
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCsDat>>();
        }

        var items = new List<ResultItem<string, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Metadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMetadata(string? key, out NPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMetadata(key, out var items))
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Metadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMetadata(string? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byMetadata is null)
        {
            byMetadata = new();
            foreach (var item in Items)
            {
                var itemKey = item.Metadata;

                if (!byMetadata.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMetadata.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMetadata.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byMetadata"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCsDat>> GetManyToManyByMetadata(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCsDat>>();
        }

        var items = new List<ResultItem<string, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMetadata(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out NPCsDat? item)
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown24.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCsDat>>();
        }

        var items = new List<ResultItem<int, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.NPCMasterKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCMasterKey(int? key, out NPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCMasterKey(key, out var items))
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.NPCMasterKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCMasterKey(int? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byNPCMasterKey is null)
        {
            byNPCMasterKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCMasterKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCMasterKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCMasterKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCMasterKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byNPCMasterKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCsDat>> GetManyToManyByNPCMasterKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCsDat>>();
        }

        var items = new List<ResultItem<int, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCMasterKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.ShortName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShortName(string? key, out NPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShortName(key, out var items))
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.ShortName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShortName(string? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byShortName is null)
        {
            byShortName = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShortName;

                if (!byShortName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShortName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShortName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byShortName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCsDat>> GetManyToManyByShortName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCsDat>>();
        }

        var items = new List<ResultItem<string, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShortName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out NPCsDat? item)
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
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
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCsDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCsDat>>();
        }

        var items = new List<ResultItem<int, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.NPCAudios1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCAudios1(int? key, out NPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCAudios1(key, out var items))
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.NPCAudios1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCAudios1(int? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byNPCAudios1 is null)
        {
            byNPCAudios1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCAudios1;
                foreach (var listKey in itemKey)
                {
                    if (!byNPCAudios1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNPCAudios1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNPCAudios1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byNPCAudios1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCsDat>> GetManyToManyByNPCAudios1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCsDat>>();
        }

        var items = new List<ResultItem<int, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCAudios1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.NPCAudios2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCAudios2(int? key, out NPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCAudios2(key, out var items))
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.NPCAudios2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCAudios2(int? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byNPCAudios2 is null)
        {
            byNPCAudios2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCAudios2;
                foreach (var listKey in itemKey)
                {
                    if (!byNPCAudios2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNPCAudios2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNPCAudios2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byNPCAudios2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCsDat>> GetManyToManyByNPCAudios2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCsDat>>();
        }

        var items = new List<ResultItem<int, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCAudios2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(int? key, out NPCsDat? item)
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(int? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
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
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCsDat>> GetManyToManyByUnknown100(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCsDat>>();
        }

        var items = new List<ResultItem<int, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(int? key, out NPCsDat? item)
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(int? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
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
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCsDat>> GetManyToManyByUnknown104(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCsDat>>();
        }

        var items = new List<ResultItem<int, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Portrait"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPortrait(int? key, out NPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPortrait(key, out var items))
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Portrait"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPortrait(int? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byPortrait is null)
        {
            byPortrait = new();
            foreach (var item in Items)
            {
                var itemKey = item.Portrait;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPortrait.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPortrait.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPortrait.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byPortrait"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCsDat>> GetManyToManyByPortrait(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCsDat>>();
        }

        var items = new List<ResultItem<int, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPortrait(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.DialogueStyle"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDialogueStyle(int? key, out NPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDialogueStyle(key, out var items))
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.DialogueStyle"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDialogueStyle(int? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byDialogueStyle is null)
        {
            byDialogueStyle = new();
            foreach (var item in Items)
            {
                var itemKey = item.DialogueStyle;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDialogueStyle.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDialogueStyle.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDialogueStyle.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byDialogueStyle"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCsDat>> GetManyToManyByDialogueStyle(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCsDat>>();
        }

        var items = new List<ResultItem<int, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDialogueStyle(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown144(bool? key, out NPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown144(key, out var items))
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown144(bool? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byUnknown144 is null)
        {
            byUnknown144 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown144;

                if (!byUnknown144.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown144.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown144.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byUnknown144"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCsDat>> GetManyToManyByUnknown144(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCsDat>>();
        }

        var items = new List<ResultItem<bool, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown144(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown145"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown145(int? key, out NPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown145(key, out var items))
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Unknown145"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown145(int? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byUnknown145 is null)
        {
            byUnknown145 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown145;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown145.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown145.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown145.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byUnknown145"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCsDat>> GetManyToManyByUnknown145(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCsDat>>();
        }

        var items = new List<ResultItem<int, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown145(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Gender"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGender(string? key, out NPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGender(key, out var items))
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
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.Gender"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGender(string? key, out IReadOnlyList<NPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        if (byGender is null)
        {
            byGender = new();
            foreach (var item in Items)
            {
                var itemKey = item.Gender;

                if (!byGender.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGender.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGender.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCsDat"/> with <see cref="NPCsDat.byGender"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCsDat>> GetManyToManyByGender(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCsDat>>();
        }

        var items = new List<ResultItem<string, NPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGender(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private NPCsDat[] Load()
    {
        const string filePath = "Data/NPCs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Metadata
            (var metadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading NPCMasterKey
            (var npcmasterkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ShortName
            (var shortnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading NPCAudios1
            (var tempnpcaudios1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npcaudios1Loading = tempnpcaudios1Loading.AsReadOnly();

            // loading NPCAudios2
            (var tempnpcaudios2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npcaudios2Loading = tempnpcaudios2Loading.AsReadOnly();

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Portrait
            (var portraitLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DialogueStyle
            (var dialoguestyleLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown145
            (var unknown145Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Gender
            (var genderLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCsDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Metadata = metadataLoading,
                Unknown24 = unknown24Loading,
                NPCMasterKey = npcmasterkeyLoading,
                ShortName = shortnameLoading,
                Unknown64 = unknown64Loading,
                NPCAudios1 = npcaudios1Loading,
                NPCAudios2 = npcaudios2Loading,
                Unknown100 = unknown100Loading,
                Unknown104 = unknown104Loading,
                Portrait = portraitLoading,
                DialogueStyle = dialoguestyleLoading,
                Unknown144 = unknown144Loading,
                Unknown145 = unknown145Loading,
                Gender = genderLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
