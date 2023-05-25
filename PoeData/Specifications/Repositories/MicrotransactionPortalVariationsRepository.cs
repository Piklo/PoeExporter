using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MicrotransactionPortalVariationsDat"/> related data and helper methods.
/// </summary>
public sealed class MicrotransactionPortalVariationsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MicrotransactionPortalVariationsDat> Items { get; }

    private Dictionary<int, List<MicrotransactionPortalVariationsDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<MicrotransactionPortalVariationsDat>>? byId;
    private Dictionary<string, List<MicrotransactionPortalVariationsDat>>? byAOFile;
    private Dictionary<string, List<MicrotransactionPortalVariationsDat>>? byMapAOFile;
    private Dictionary<float, List<MicrotransactionPortalVariationsDat>>? byUnknown36;
    private Dictionary<int, List<MicrotransactionPortalVariationsDat>>? byMiscObject;
    private Dictionary<string, List<MicrotransactionPortalVariationsDat>>? byPortalEffect;
    private Dictionary<float, List<MicrotransactionPortalVariationsDat>>? byUnknown64;
    private Dictionary<string, List<MicrotransactionPortalVariationsDat>>? byUnknown68;
    private Dictionary<string, List<MicrotransactionPortalVariationsDat>>? byPortalEffectLarge;
    private Dictionary<string, List<MicrotransactionPortalVariationsDat>>? byUnknown84;
    private Dictionary<string, List<MicrotransactionPortalVariationsDat>>? byUnknown92;
    private Dictionary<string, List<MicrotransactionPortalVariationsDat>>? byUnknown100;
    private Dictionary<int, List<MicrotransactionPortalVariationsDat>>? byUnknown108;
    private Dictionary<int, List<MicrotransactionPortalVariationsDat>>? byUnknown112;

    /// <summary>
    /// Initializes a new instance of the <see cref="MicrotransactionPortalVariationsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MicrotransactionPortalVariationsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out MicrotransactionPortalVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionPortalVariationsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(int? key, out MicrotransactionPortalVariationsDat? item)
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(int? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
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

        if (!byId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionPortalVariationsDat>> GetManyToManyById(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFile(string? key, out MicrotransactionPortalVariationsDat? item)
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFile(string? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
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
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byAOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionPortalVariationsDat>> GetManyToManyByAOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.MapAOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapAOFile(string? key, out MicrotransactionPortalVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapAOFile(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.MapAOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapAOFile(string? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        if (byMapAOFile is null)
        {
            byMapAOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapAOFile;

                if (!byMapAOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMapAOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMapAOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byMapAOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionPortalVariationsDat>> GetManyToManyByMapAOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapAOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(float? key, out MicrotransactionPortalVariationsDat? item)
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(float? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
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
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, MicrotransactionPortalVariationsDat>> GetManyToManyByUnknown36(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<float, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.MiscObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscObject(int? key, out MicrotransactionPortalVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscObject(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.MiscObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscObject(int? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        if (byMiscObject is null)
        {
            byMiscObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscObject;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscObject.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscObject.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscObject.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byMiscObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionPortalVariationsDat>> GetManyToManyByMiscObject(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.PortalEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPortalEffect(string? key, out MicrotransactionPortalVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPortalEffect(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.PortalEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPortalEffect(string? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        if (byPortalEffect is null)
        {
            byPortalEffect = new();
            foreach (var item in Items)
            {
                var itemKey = item.PortalEffect;

                if (!byPortalEffect.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPortalEffect.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPortalEffect.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byPortalEffect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionPortalVariationsDat>> GetManyToManyByPortalEffect(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPortalEffect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(float? key, out MicrotransactionPortalVariationsDat? item)
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(float? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
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
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, MicrotransactionPortalVariationsDat>> GetManyToManyByUnknown64(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<float, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(string? key, out MicrotransactionPortalVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown68(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(string? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;

                if (!byUnknown68.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionPortalVariationsDat>> GetManyToManyByUnknown68(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.PortalEffectLarge"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPortalEffectLarge(string? key, out MicrotransactionPortalVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPortalEffectLarge(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.PortalEffectLarge"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPortalEffectLarge(string? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        if (byPortalEffectLarge is null)
        {
            byPortalEffectLarge = new();
            foreach (var item in Items)
            {
                var itemKey = item.PortalEffectLarge;

                if (!byPortalEffectLarge.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPortalEffectLarge.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPortalEffectLarge.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byPortalEffectLarge"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionPortalVariationsDat>> GetManyToManyByPortalEffectLarge(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPortalEffectLarge(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(string? key, out MicrotransactionPortalVariationsDat? item)
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(string? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
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

        if (!byUnknown84.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionPortalVariationsDat>> GetManyToManyByUnknown84(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(string? key, out MicrotransactionPortalVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown92(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(string? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        if (byUnknown92 is null)
        {
            byUnknown92 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown92;

                if (!byUnknown92.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown92.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown92.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionPortalVariationsDat>> GetManyToManyByUnknown92(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(string? key, out MicrotransactionPortalVariationsDat? item)
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(string? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
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

        if (!byUnknown100.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionPortalVariationsDat>> GetManyToManyByUnknown100(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(int? key, out MicrotransactionPortalVariationsDat? item)
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(int? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
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
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionPortalVariationsDat>> GetManyToManyByUnknown108(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown112(int? key, out MicrotransactionPortalVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown112(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown112(int? key, out IReadOnlyList<MicrotransactionPortalVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        if (byUnknown112 is null)
        {
            byUnknown112 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown112;

                if (!byUnknown112.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown112.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown112.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionPortalVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPortalVariationsDat"/> with <see cref="MicrotransactionPortalVariationsDat.byUnknown112"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionPortalVariationsDat>> GetManyToManyByUnknown112(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionPortalVariationsDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionPortalVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown112(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionPortalVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MicrotransactionPortalVariationsDat[] Load()
    {
        const string filePath = "Data/MicrotransactionPortalVariations.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionPortalVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MapAOFile
            (var mapaofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading MiscObject
            (var miscobjectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PortalEffect
            (var portaleffectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PortalEffectLarge
            (var portaleffectlargeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionPortalVariationsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Id = idLoading,
                AOFile = aofileLoading,
                MapAOFile = mapaofileLoading,
                Unknown36 = unknown36Loading,
                MiscObject = miscobjectLoading,
                PortalEffect = portaleffectLoading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                PortalEffectLarge = portaleffectlargeLoading,
                Unknown84 = unknown84Loading,
                Unknown92 = unknown92Loading,
                Unknown100 = unknown100Loading,
                Unknown108 = unknown108Loading,
                Unknown112 = unknown112Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
