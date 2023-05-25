using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SkillArtVariationsDat"/> related data and helper methods.
/// </summary>
public sealed class SkillArtVariationsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SkillArtVariationsDat> Items { get; }

    private Dictionary<string, List<SkillArtVariationsDat>>? byId;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown8;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown24;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown40;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown56;
    private Dictionary<string, List<SkillArtVariationsDat>>? byVariants;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown88;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown104;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown120;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown136;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown152;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown168;
    private Dictionary<int, List<SkillArtVariationsDat>>? byUnknown184;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkillArtVariationsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SkillArtVariationsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SkillArtVariationsDat? item)
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
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
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SkillArtVariationsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<string, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out SkillArtVariationsDat? item)
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown8.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown8.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out SkillArtVariationsDat? item)
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown24.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown24.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out SkillArtVariationsDat? item)
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown40.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown40.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out SkillArtVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown56.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown56.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Variants"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVariants(string? key, out SkillArtVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVariants(key, out var items))
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Variants"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVariants(string? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byVariants is null)
        {
            byVariants = new();
            foreach (var item in Items)
            {
                var itemKey = item.Variants;
                foreach (var listKey in itemKey)
                {
                    if (!byVariants.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byVariants.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byVariants.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byVariants"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SkillArtVariationsDat>> GetManyToManyByVariants(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<string, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVariants(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out SkillArtVariationsDat? item)
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown88.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown88.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(int? key, out SkillArtVariationsDat? item)
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown104.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown104.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown104(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown120(int? key, out SkillArtVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown120(key, out var items))
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown120(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown120 is null)
        {
            byUnknown120 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown120;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown120.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown120.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown120.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown120"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown120(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown120(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown136"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown136(int? key, out SkillArtVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown136(key, out var items))
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown136"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown136(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown136 is null)
        {
            byUnknown136 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown136;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown136.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown136.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown136.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown136"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown136(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown136(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown152"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown152(int? key, out SkillArtVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown152(key, out var items))
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown152"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown152(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown152 is null)
        {
            byUnknown152 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown152;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown152.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown152.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown152.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown152"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown152(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown152(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown168"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown168(int? key, out SkillArtVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown168(key, out var items))
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown168"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown168(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown168 is null)
        {
            byUnknown168 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown168;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown168.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown168.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown168.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown168"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown168(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown168(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown184"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown184(int? key, out SkillArtVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown184(key, out var items))
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
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.Unknown184"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown184(int? key, out IReadOnlyList<SkillArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        if (byUnknown184 is null)
        {
            byUnknown184 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown184;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown184.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown184.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown184.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillArtVariationsDat"/> with <see cref="SkillArtVariationsDat.byUnknown184"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillArtVariationsDat>> GetManyToManyByUnknown184(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown184(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SkillArtVariationsDat[] Load()
    {
        const string filePath = "Data/SkillArtVariations.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillArtVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var tempunknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown8Loading = tempunknown8Loading.AsReadOnly();

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading Variants
            (var tempvariantsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var variantsLoading = tempvariantsLoading.AsReadOnly();

            // loading Unknown88
            (var tempunknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown88Loading = tempunknown88Loading.AsReadOnly();

            // loading Unknown104
            (var tempunknown104Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown104Loading = tempunknown104Loading.AsReadOnly();

            // loading Unknown120
            (var tempunknown120Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown120Loading = tempunknown120Loading.AsReadOnly();

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown152
            (var tempunknown152Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown152Loading = tempunknown152Loading.AsReadOnly();

            // loading Unknown168
            (var tempunknown168Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown168Loading = tempunknown168Loading.AsReadOnly();

            // loading Unknown184
            (var tempunknown184Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown184Loading = tempunknown184Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillArtVariationsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                Variants = variantsLoading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
                Unknown120 = unknown120Loading,
                Unknown136 = unknown136Loading,
                Unknown152 = unknown152Loading,
                Unknown168 = unknown168Loading,
                Unknown184 = unknown184Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
