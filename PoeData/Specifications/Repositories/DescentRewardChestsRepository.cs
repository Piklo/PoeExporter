using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DescentRewardChestsDat"/> related data and helper methods.
/// </summary>
public sealed class DescentRewardChestsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DescentRewardChestsDat> Items { get; }

    private Dictionary<string, List<DescentRewardChestsDat>>? byId;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys1;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys2;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys3;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys4;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys5;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys6;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys7;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys8;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys9;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys10;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys11;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys12;
    private Dictionary<int, List<DescentRewardChestsDat>>? byWorldAreasKey;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys13;
    private Dictionary<int, List<DescentRewardChestsDat>>? byBaseItemTypesKeys14;

    /// <summary>
    /// Initializes a new instance of the <see cref="DescentRewardChestsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DescentRewardChestsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out DescentRewardChestsDat? item)
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
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
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DescentRewardChestsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<string, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys1(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys1(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys1(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys1 is null)
        {
            byBaseItemTypesKeys1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys1;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys2(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys2(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys2(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys2 is null)
        {
            byBaseItemTypesKeys2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys2;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys3(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys3(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys3(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys3 is null)
        {
            byBaseItemTypesKeys3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys3;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys3.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys3.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys4(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys4(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys4(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys4 is null)
        {
            byBaseItemTypesKeys4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys4;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys4.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys4.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys5(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys5(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys5(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys5 is null)
        {
            byBaseItemTypesKeys5 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys5;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys5.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys5.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys5.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys5"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys5(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys5(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys6(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys6(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys6(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys6 is null)
        {
            byBaseItemTypesKeys6 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys6;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys6.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys6.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys6.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys6"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys6(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys6(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys7"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys7(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys7(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys7"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys7(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys7 is null)
        {
            byBaseItemTypesKeys7 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys7;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys7.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys7.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys7.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys7"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys7(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys7(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys8(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys8(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys8(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys8 is null)
        {
            byBaseItemTypesKeys8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys8;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys8.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys8.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys9"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys9(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys9(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys9"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys9(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys9 is null)
        {
            byBaseItemTypesKeys9 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys9;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys9.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys9.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys9.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys9"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys9(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys9(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys10"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys10(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys10(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys10"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys10(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys10 is null)
        {
            byBaseItemTypesKeys10 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys10;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys10.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys10.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys10.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys10"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys10(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys10(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys11"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys11(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys11(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys11"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys11(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys11 is null)
        {
            byBaseItemTypesKeys11 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys11;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys11.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys11.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys11.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys11"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys11(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys11(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys12(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys12(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys12(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys12 is null)
        {
            byBaseItemTypesKeys12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys12;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys12.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys12.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byWorldAreasKey is null)
        {
            byWorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys13"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys13(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys13(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys13"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys13(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys13 is null)
        {
            byBaseItemTypesKeys13 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys13;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys13.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys13.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys13.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys13"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys13(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys13(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys14"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys14(int? key, out DescentRewardChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys14(key, out var items))
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
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.BaseItemTypesKeys14"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys14(int? key, out IReadOnlyList<DescentRewardChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys14 is null)
        {
            byBaseItemTypesKeys14 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys14;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys14.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys14.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys14.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DescentRewardChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DescentRewardChestsDat"/> with <see cref="DescentRewardChestsDat.byBaseItemTypesKeys14"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DescentRewardChestsDat>> GetManyToManyByBaseItemTypesKeys14(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DescentRewardChestsDat>>();
        }

        var items = new List<ResultItem<int, DescentRewardChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys14(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DescentRewardChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DescentRewardChestsDat[] Load()
    {
        const string filePath = "Data/DescentRewardChests.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DescentRewardChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKeys1
            (var tempbaseitemtypeskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys1Loading = tempbaseitemtypeskeys1Loading.AsReadOnly();

            // loading BaseItemTypesKeys2
            (var tempbaseitemtypeskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys2Loading = tempbaseitemtypeskeys2Loading.AsReadOnly();

            // loading BaseItemTypesKeys3
            (var tempbaseitemtypeskeys3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys3Loading = tempbaseitemtypeskeys3Loading.AsReadOnly();

            // loading BaseItemTypesKeys4
            (var tempbaseitemtypeskeys4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys4Loading = tempbaseitemtypeskeys4Loading.AsReadOnly();

            // loading BaseItemTypesKeys5
            (var tempbaseitemtypeskeys5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys5Loading = tempbaseitemtypeskeys5Loading.AsReadOnly();

            // loading BaseItemTypesKeys6
            (var tempbaseitemtypeskeys6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys6Loading = tempbaseitemtypeskeys6Loading.AsReadOnly();

            // loading BaseItemTypesKeys7
            (var tempbaseitemtypeskeys7Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys7Loading = tempbaseitemtypeskeys7Loading.AsReadOnly();

            // loading BaseItemTypesKeys8
            (var tempbaseitemtypeskeys8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys8Loading = tempbaseitemtypeskeys8Loading.AsReadOnly();

            // loading BaseItemTypesKeys9
            (var tempbaseitemtypeskeys9Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys9Loading = tempbaseitemtypeskeys9Loading.AsReadOnly();

            // loading BaseItemTypesKeys10
            (var tempbaseitemtypeskeys10Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys10Loading = tempbaseitemtypeskeys10Loading.AsReadOnly();

            // loading BaseItemTypesKeys11
            (var tempbaseitemtypeskeys11Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys11Loading = tempbaseitemtypeskeys11Loading.AsReadOnly();

            // loading BaseItemTypesKeys12
            (var tempbaseitemtypeskeys12Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys12Loading = tempbaseitemtypeskeys12Loading.AsReadOnly();

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BaseItemTypesKeys13
            (var tempbaseitemtypeskeys13Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys13Loading = tempbaseitemtypeskeys13Loading.AsReadOnly();

            // loading BaseItemTypesKeys14
            (var tempbaseitemtypeskeys14Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeys14Loading = tempbaseitemtypeskeys14Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DescentRewardChestsDat()
            {
                Id = idLoading,
                BaseItemTypesKeys1 = baseitemtypeskeys1Loading,
                BaseItemTypesKeys2 = baseitemtypeskeys2Loading,
                BaseItemTypesKeys3 = baseitemtypeskeys3Loading,
                BaseItemTypesKeys4 = baseitemtypeskeys4Loading,
                BaseItemTypesKeys5 = baseitemtypeskeys5Loading,
                BaseItemTypesKeys6 = baseitemtypeskeys6Loading,
                BaseItemTypesKeys7 = baseitemtypeskeys7Loading,
                BaseItemTypesKeys8 = baseitemtypeskeys8Loading,
                BaseItemTypesKeys9 = baseitemtypeskeys9Loading,
                BaseItemTypesKeys10 = baseitemtypeskeys10Loading,
                BaseItemTypesKeys11 = baseitemtypeskeys11Loading,
                BaseItemTypesKeys12 = baseitemtypeskeys12Loading,
                WorldAreasKey = worldareaskeyLoading,
                BaseItemTypesKeys13 = baseitemtypeskeys13Loading,
                BaseItemTypesKeys14 = baseitemtypeskeys14Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
