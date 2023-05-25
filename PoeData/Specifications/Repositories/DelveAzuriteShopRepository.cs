using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DelveAzuriteShopDat"/> related data and helper methods.
/// </summary>
public sealed class DelveAzuriteShopRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DelveAzuriteShopDat> Items { get; }

    private Dictionary<int, List<DelveAzuriteShopDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<DelveAzuriteShopDat>>? bySpawnWeight;
    private Dictionary<int, List<DelveAzuriteShopDat>>? byCost;
    private Dictionary<int, List<DelveAzuriteShopDat>>? byMinDepth;
    private Dictionary<bool, List<DelveAzuriteShopDat>>? byIsEnabled;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveAzuriteShopRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DelveAzuriteShopRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out DelveAzuriteShopDat? item)
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
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<DelveAzuriteShopDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveAzuriteShopDat>();
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
            items = Array.Empty<DelveAzuriteShopDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveAzuriteShopDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveAzuriteShopDat>>();
        }

        var items = new List<ResultItem<int, DelveAzuriteShopDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveAzuriteShopDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out DelveAzuriteShopDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight(key, out var items))
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
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<DelveAzuriteShopDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveAzuriteShopDat>();
            return false;
        }

        if (bySpawnWeight is null)
        {
            bySpawnWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight;

                if (!bySpawnWeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpawnWeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpawnWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveAzuriteShopDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveAzuriteShopDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveAzuriteShopDat>>();
        }

        var items = new List<ResultItem<int, DelveAzuriteShopDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveAzuriteShopDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.Cost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost(int? key, out DelveAzuriteShopDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost(key, out var items))
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
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.Cost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost(int? key, out IReadOnlyList<DelveAzuriteShopDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveAzuriteShopDat>();
            return false;
        }

        if (byCost is null)
        {
            byCost = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost;

                if (!byCost.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCost.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCost.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveAzuriteShopDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.byCost"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveAzuriteShopDat>> GetManyToManyByCost(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveAzuriteShopDat>>();
        }

        var items = new List<ResultItem<int, DelveAzuriteShopDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveAzuriteShopDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.MinDepth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinDepth(int? key, out DelveAzuriteShopDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinDepth(key, out var items))
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
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.MinDepth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinDepth(int? key, out IReadOnlyList<DelveAzuriteShopDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveAzuriteShopDat>();
            return false;
        }

        if (byMinDepth is null)
        {
            byMinDepth = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinDepth;

                if (!byMinDepth.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinDepth.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinDepth.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveAzuriteShopDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.byMinDepth"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveAzuriteShopDat>> GetManyToManyByMinDepth(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveAzuriteShopDat>>();
        }

        var items = new List<ResultItem<int, DelveAzuriteShopDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinDepth(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveAzuriteShopDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.IsEnabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsEnabled(bool? key, out DelveAzuriteShopDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsEnabled(key, out var items))
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
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.IsEnabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsEnabled(bool? key, out IReadOnlyList<DelveAzuriteShopDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveAzuriteShopDat>();
            return false;
        }

        if (byIsEnabled is null)
        {
            byIsEnabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsEnabled;

                if (!byIsEnabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsEnabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsEnabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveAzuriteShopDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveAzuriteShopDat"/> with <see cref="DelveAzuriteShopDat.byIsEnabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveAzuriteShopDat>> GetManyToManyByIsEnabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveAzuriteShopDat>>();
        }

        var items = new List<ResultItem<bool, DelveAzuriteShopDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsEnabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveAzuriteShopDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DelveAzuriteShopDat[] Load()
    {
        const string filePath = "Data/DelveAzuriteShop.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveAzuriteShopDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Cost
            (var costLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinDepth
            (var mindepthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveAzuriteShopDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                SpawnWeight = spawnweightLoading,
                Cost = costLoading,
                MinDepth = mindepthLoading,
                IsEnabled = isenabledLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
