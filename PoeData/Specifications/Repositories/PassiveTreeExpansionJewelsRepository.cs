using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PassiveTreeExpansionJewelsDat"/> related data and helper methods.
/// </summary>
public sealed class PassiveTreeExpansionJewelsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PassiveTreeExpansionJewelsDat> Items { get; }

    private Dictionary<int, List<PassiveTreeExpansionJewelsDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<PassiveTreeExpansionJewelsDat>>? byPassiveTreeExpansionJewelSizesKey;
    private Dictionary<int, List<PassiveTreeExpansionJewelsDat>>? byMinNodes;
    private Dictionary<int, List<PassiveTreeExpansionJewelsDat>>? byMaxNodes;
    private Dictionary<int, List<PassiveTreeExpansionJewelsDat>>? bySmallIndices;
    private Dictionary<int, List<PassiveTreeExpansionJewelsDat>>? byNotableIndices;
    private Dictionary<int, List<PassiveTreeExpansionJewelsDat>>? bySocketIndices;
    private Dictionary<int, List<PassiveTreeExpansionJewelsDat>>? byTotalIndices;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassiveTreeExpansionJewelsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PassiveTreeExpansionJewelsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out PassiveTreeExpansionJewelsDat? item)
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
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<PassiveTreeExpansionJewelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
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
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionJewelsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionJewelsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionJewelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionJewelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.PassiveTreeExpansionJewelSizesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveTreeExpansionJewelSizesKey(int? key, out PassiveTreeExpansionJewelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveTreeExpansionJewelSizesKey(key, out var items))
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
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.PassiveTreeExpansionJewelSizesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveTreeExpansionJewelSizesKey(int? key, out IReadOnlyList<PassiveTreeExpansionJewelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        if (byPassiveTreeExpansionJewelSizesKey is null)
        {
            byPassiveTreeExpansionJewelSizesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveTreeExpansionJewelSizesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPassiveTreeExpansionJewelSizesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPassiveTreeExpansionJewelSizesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveTreeExpansionJewelSizesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.byPassiveTreeExpansionJewelSizesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionJewelsDat>> GetManyToManyByPassiveTreeExpansionJewelSizesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionJewelsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionJewelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveTreeExpansionJewelSizesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionJewelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.MinNodes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinNodes(int? key, out PassiveTreeExpansionJewelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinNodes(key, out var items))
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
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.MinNodes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinNodes(int? key, out IReadOnlyList<PassiveTreeExpansionJewelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        if (byMinNodes is null)
        {
            byMinNodes = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinNodes;

                if (!byMinNodes.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinNodes.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinNodes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.byMinNodes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionJewelsDat>> GetManyToManyByMinNodes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionJewelsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionJewelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinNodes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionJewelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.MaxNodes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxNodes(int? key, out PassiveTreeExpansionJewelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxNodes(key, out var items))
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
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.MaxNodes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxNodes(int? key, out IReadOnlyList<PassiveTreeExpansionJewelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        if (byMaxNodes is null)
        {
            byMaxNodes = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxNodes;

                if (!byMaxNodes.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxNodes.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxNodes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.byMaxNodes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionJewelsDat>> GetManyToManyByMaxNodes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionJewelsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionJewelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxNodes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionJewelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.SmallIndices"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySmallIndices(int? key, out PassiveTreeExpansionJewelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySmallIndices(key, out var items))
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
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.SmallIndices"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySmallIndices(int? key, out IReadOnlyList<PassiveTreeExpansionJewelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        if (bySmallIndices is null)
        {
            bySmallIndices = new();
            foreach (var item in Items)
            {
                var itemKey = item.SmallIndices;
                foreach (var listKey in itemKey)
                {
                    if (!bySmallIndices.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySmallIndices.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySmallIndices.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.bySmallIndices"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionJewelsDat>> GetManyToManyBySmallIndices(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionJewelsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionJewelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySmallIndices(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionJewelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.NotableIndices"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNotableIndices(int? key, out PassiveTreeExpansionJewelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNotableIndices(key, out var items))
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
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.NotableIndices"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNotableIndices(int? key, out IReadOnlyList<PassiveTreeExpansionJewelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        if (byNotableIndices is null)
        {
            byNotableIndices = new();
            foreach (var item in Items)
            {
                var itemKey = item.NotableIndices;
                foreach (var listKey in itemKey)
                {
                    if (!byNotableIndices.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNotableIndices.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNotableIndices.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.byNotableIndices"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionJewelsDat>> GetManyToManyByNotableIndices(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionJewelsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionJewelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNotableIndices(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionJewelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.SocketIndices"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySocketIndices(int? key, out PassiveTreeExpansionJewelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySocketIndices(key, out var items))
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
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.SocketIndices"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySocketIndices(int? key, out IReadOnlyList<PassiveTreeExpansionJewelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        if (bySocketIndices is null)
        {
            bySocketIndices = new();
            foreach (var item in Items)
            {
                var itemKey = item.SocketIndices;
                foreach (var listKey in itemKey)
                {
                    if (!bySocketIndices.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySocketIndices.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySocketIndices.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.bySocketIndices"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionJewelsDat>> GetManyToManyBySocketIndices(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionJewelsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionJewelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySocketIndices(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionJewelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.TotalIndices"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTotalIndices(int? key, out PassiveTreeExpansionJewelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTotalIndices(key, out var items))
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
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.TotalIndices"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTotalIndices(int? key, out IReadOnlyList<PassiveTreeExpansionJewelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        if (byTotalIndices is null)
        {
            byTotalIndices = new();
            foreach (var item in Items)
            {
                var itemKey = item.TotalIndices;

                if (!byTotalIndices.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTotalIndices.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTotalIndices.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionJewelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionJewelsDat"/> with <see cref="PassiveTreeExpansionJewelsDat.byTotalIndices"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionJewelsDat>> GetManyToManyByTotalIndices(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionJewelsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionJewelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTotalIndices(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionJewelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PassiveTreeExpansionJewelsDat[] Load()
    {
        const string filePath = "Data/PassiveTreeExpansionJewels.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveTreeExpansionJewelsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PassiveTreeExpansionJewelSizesKey
            (var passivetreeexpansionjewelsizeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MinNodes
            (var minnodesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxNodes
            (var maxnodesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SmallIndices
            (var tempsmallindicesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var smallindicesLoading = tempsmallindicesLoading.AsReadOnly();

            // loading NotableIndices
            (var tempnotableindicesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var notableindicesLoading = tempnotableindicesLoading.AsReadOnly();

            // loading SocketIndices
            (var tempsocketindicesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var socketindicesLoading = tempsocketindicesLoading.AsReadOnly();

            // loading TotalIndices
            (var totalindicesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveTreeExpansionJewelsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                PassiveTreeExpansionJewelSizesKey = passivetreeexpansionjewelsizeskeyLoading,
                MinNodes = minnodesLoading,
                MaxNodes = maxnodesLoading,
                SmallIndices = smallindicesLoading,
                NotableIndices = notableindicesLoading,
                SocketIndices = socketindicesLoading,
                TotalIndices = totalindicesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
