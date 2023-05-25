using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="IncursionChestsDat"/> related data and helper methods.
/// </summary>
public sealed class IncursionChestsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<IncursionChestsDat> Items { get; }

    private Dictionary<string, List<IncursionChestsDat>>? byId;
    private Dictionary<int, List<IncursionChestsDat>>? byChestsKey;
    private Dictionary<int, List<IncursionChestsDat>>? byUniqueChestsKey;
    private Dictionary<int, List<IncursionChestsDat>>? byMinLevel;
    private Dictionary<int, List<IncursionChestsDat>>? byMaxLevel;
    private Dictionary<int, List<IncursionChestsDat>>? byWeight;
    private Dictionary<int, List<IncursionChestsDat>>? byUnknown52;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncursionChestsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal IncursionChestsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out IncursionChestsDat? item)
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
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<IncursionChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestsDat>();
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
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionChestsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionChestsDat>>();
        }

        var items = new List<ResultItem<string, IncursionChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.ChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChestsKey(int? key, out IncursionChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChestsKey(key, out var items))
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
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.ChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChestsKey(int? key, out IReadOnlyList<IncursionChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        if (byChestsKey is null)
        {
            byChestsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChestsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byChestsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byChestsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byChestsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.byChestsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestsDat>> GetManyToManyByChestsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChestsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.UniqueChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUniqueChestsKey(int? key, out IncursionChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUniqueChestsKey(key, out var items))
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
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.UniqueChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUniqueChestsKey(int? key, out IReadOnlyList<IncursionChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        if (byUniqueChestsKey is null)
        {
            byUniqueChestsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.UniqueChestsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUniqueChestsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUniqueChestsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUniqueChestsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.byUniqueChestsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestsDat>> GetManyToManyByUniqueChestsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUniqueChestsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out IncursionChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLevel(key, out var items))
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
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<IncursionChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        if (byMinLevel is null)
        {
            byMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLevel;

                if (!byMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestsDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out IncursionChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxLevel(key, out var items))
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
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<IncursionChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        if (byMaxLevel is null)
        {
            byMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxLevel;

                if (!byMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestsDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.Weight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeight(int? key, out IncursionChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeight(key, out var items))
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
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.Weight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeight(int? key, out IReadOnlyList<IncursionChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        if (byWeight is null)
        {
            byWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.Weight;

                if (!byWeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.byWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestsDat>> GetManyToManyByWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out IncursionChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown52(key, out var items))
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
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<IncursionChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;

                if (!byUnknown52.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown52.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestsDat"/> with <see cref="IncursionChestsDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestsDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private IncursionChestsDat[] Load()
    {
        const string filePath = "Data/IncursionChests.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading UniqueChestsKey
            (var uniquechestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionChestsDat()
            {
                Id = idLoading,
                ChestsKey = chestskeyLoading,
                UniqueChestsKey = uniquechestskeyLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                Weight = weightLoading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
