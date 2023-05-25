using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HideoutDoodadsDat"/> related data and helper methods.
/// </summary>
public sealed class HideoutDoodadsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HideoutDoodadsDat> Items { get; }

    private Dictionary<int, List<HideoutDoodadsDat>>? byBaseItemTypesKey;
    private Dictionary<string, List<HideoutDoodadsDat>>? byVariation_AOFiles;
    private Dictionary<bool, List<HideoutDoodadsDat>>? byIsNonMasterDoodad;
    private Dictionary<string, List<HideoutDoodadsDat>>? byInheritsFrom;
    private Dictionary<bool, List<HideoutDoodadsDat>>? byUnknown41;
    private Dictionary<bool, List<HideoutDoodadsDat>>? byIsCraftingBench;
    private Dictionary<int, List<HideoutDoodadsDat>>? byTags;
    private Dictionary<bool, List<HideoutDoodadsDat>>? byUnknown59;
    private Dictionary<int, List<HideoutDoodadsDat>>? byUnknown60;
    private Dictionary<int, List<HideoutDoodadsDat>>? byCategory;
    private Dictionary<int, List<HideoutDoodadsDat>>? byUnknown92;
    private Dictionary<bool, List<HideoutDoodadsDat>>? byUnknown96;
    private Dictionary<int, List<HideoutDoodadsDat>>? byUnknown97;
    private Dictionary<bool, List<HideoutDoodadsDat>>? byUnknown113;
    private Dictionary<int, List<HideoutDoodadsDat>>? byUnknown114;

    /// <summary>
    /// Initializes a new instance of the <see cref="HideoutDoodadsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HideoutDoodadsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out HideoutDoodadsDat? item)
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
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
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutDoodadsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<int, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Variation_AOFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVariation_AOFiles(string? key, out HideoutDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVariation_AOFiles(key, out var items))
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Variation_AOFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVariation_AOFiles(string? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byVariation_AOFiles is null)
        {
            byVariation_AOFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.Variation_AOFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byVariation_AOFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byVariation_AOFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byVariation_AOFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byVariation_AOFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HideoutDoodadsDat>> GetManyToManyByVariation_AOFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<string, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVariation_AOFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.IsNonMasterDoodad"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsNonMasterDoodad(bool? key, out HideoutDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsNonMasterDoodad(key, out var items))
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.IsNonMasterDoodad"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsNonMasterDoodad(bool? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byIsNonMasterDoodad is null)
        {
            byIsNonMasterDoodad = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsNonMasterDoodad;

                if (!byIsNonMasterDoodad.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsNonMasterDoodad.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsNonMasterDoodad.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byIsNonMasterDoodad"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HideoutDoodadsDat>> GetManyToManyByIsNonMasterDoodad(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<bool, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsNonMasterDoodad(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.InheritsFrom"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInheritsFrom(string? key, out HideoutDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInheritsFrom(key, out var items))
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.InheritsFrom"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInheritsFrom(string? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byInheritsFrom is null)
        {
            byInheritsFrom = new();
            foreach (var item in Items)
            {
                var itemKey = item.InheritsFrom;

                if (!byInheritsFrom.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInheritsFrom.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInheritsFrom.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byInheritsFrom"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HideoutDoodadsDat>> GetManyToManyByInheritsFrom(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<string, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInheritsFrom(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown41(bool? key, out HideoutDoodadsDat? item)
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown41(bool? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
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
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byUnknown41"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HideoutDoodadsDat>> GetManyToManyByUnknown41(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<bool, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown41(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.IsCraftingBench"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsCraftingBench(bool? key, out HideoutDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsCraftingBench(key, out var items))
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.IsCraftingBench"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsCraftingBench(bool? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byIsCraftingBench is null)
        {
            byIsCraftingBench = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsCraftingBench;

                if (!byIsCraftingBench.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsCraftingBench.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsCraftingBench.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byIsCraftingBench"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HideoutDoodadsDat>> GetManyToManyByIsCraftingBench(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<bool, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsCraftingBench(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Tags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTags(int? key, out HideoutDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTags(key, out var items))
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Tags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTags(int? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byTags is null)
        {
            byTags = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tags;
                foreach (var listKey in itemKey)
                {
                    if (!byTags.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTags.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTags.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byTags"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutDoodadsDat>> GetManyToManyByTags(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<int, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTags(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown59"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown59(bool? key, out HideoutDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown59(key, out var items))
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown59"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown59(bool? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byUnknown59 is null)
        {
            byUnknown59 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown59;

                if (!byUnknown59.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown59.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown59.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byUnknown59"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HideoutDoodadsDat>> GetManyToManyByUnknown59(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<bool, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown59(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(int? key, out HideoutDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown60(key, out var items))
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(int? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byUnknown60 is null)
        {
            byUnknown60 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown60;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown60.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown60.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown60.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutDoodadsDat>> GetManyToManyByUnknown60(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<int, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Category"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCategory(int? key, out HideoutDoodadsDat? item)
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Category"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCategory(int? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
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
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byCategory"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutDoodadsDat>> GetManyToManyByCategory(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<int, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCategory(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(int? key, out HideoutDoodadsDat? item)
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(int? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
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

        if (!byUnknown92.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutDoodadsDat>> GetManyToManyByUnknown92(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<int, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown96(bool? key, out HideoutDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown96(key, out var items))
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown96(bool? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byUnknown96 is null)
        {
            byUnknown96 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown96;

                if (!byUnknown96.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown96.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown96.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byUnknown96"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HideoutDoodadsDat>> GetManyToManyByUnknown96(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<bool, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown96(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown97(int? key, out HideoutDoodadsDat? item)
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown97(int? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byUnknown97 is null)
        {
            byUnknown97 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown97;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown97.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown97.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown97.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byUnknown97"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutDoodadsDat>> GetManyToManyByUnknown97(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<int, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown97(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown113(bool? key, out HideoutDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown113(key, out var items))
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown113(bool? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byUnknown113 is null)
        {
            byUnknown113 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown113;

                if (!byUnknown113.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown113.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown113.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byUnknown113"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HideoutDoodadsDat>> GetManyToManyByUnknown113(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<bool, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown113(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown114"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown114(int? key, out HideoutDoodadsDat? item)
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
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.Unknown114"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown114(int? key, out IReadOnlyList<HideoutDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        if (byUnknown114 is null)
        {
            byUnknown114 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown114;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown114.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown114.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown114.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutDoodadsDat"/> with <see cref="HideoutDoodadsDat.byUnknown114"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutDoodadsDat>> GetManyToManyByUnknown114(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutDoodadsDat>>();
        }

        var items = new List<ResultItem<int, HideoutDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown114(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HideoutDoodadsDat[] Load()
    {
        const string filePath = "Data/HideoutDoodads.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HideoutDoodadsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Variation_AOFiles
            (var tempvariation_aofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var variation_aofilesLoading = tempvariation_aofilesLoading.AsReadOnly();

            // loading IsNonMasterDoodad
            (var isnonmasterdoodadLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading InheritsFrom
            (var inheritsfromLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsCraftingBench
            (var iscraftingbenchLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Tags
            (var temptagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagsLoading = temptagsLoading.AsReadOnly();

            // loading Unknown59
            (var unknown59Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Category
            (var categoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown97
            (var unknown97Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown114
            (var unknown114Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HideoutDoodadsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Variation_AOFiles = variation_aofilesLoading,
                IsNonMasterDoodad = isnonmasterdoodadLoading,
                InheritsFrom = inheritsfromLoading,
                Unknown41 = unknown41Loading,
                IsCraftingBench = iscraftingbenchLoading,
                Tags = tagsLoading,
                Unknown59 = unknown59Loading,
                Unknown60 = unknown60Loading,
                Category = categoryLoading,
                Unknown92 = unknown92Loading,
                Unknown96 = unknown96Loading,
                Unknown97 = unknown97Loading,
                Unknown113 = unknown113Loading,
                Unknown114 = unknown114Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
