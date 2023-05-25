using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ItemCostPerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class ItemCostPerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ItemCostPerLevelDat> Items { get; }

    private Dictionary<int, List<ItemCostPerLevelDat>>? byContract_BaseItemTypesKey;
    private Dictionary<int, List<ItemCostPerLevelDat>>? byLevel;
    private Dictionary<int, List<ItemCostPerLevelDat>>? byCost;
    private Dictionary<int, List<ItemCostPerLevelDat>>? byCostHardmode;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemCostPerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ItemCostPerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.Contract_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByContract_BaseItemTypesKey(int? key, out ItemCostPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByContract_BaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.Contract_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByContract_BaseItemTypesKey(int? key, out IReadOnlyList<ItemCostPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostPerLevelDat>();
            return false;
        }

        if (byContract_BaseItemTypesKey is null)
        {
            byContract_BaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Contract_BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byContract_BaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byContract_BaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byContract_BaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.byContract_BaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostPerLevelDat>> GetManyToManyByContract_BaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostPerLevelDat>>();
        }

        var items = new List<ResultItem<int, ItemCostPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByContract_BaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out ItemCostPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevel(key, out var items))
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
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<ItemCostPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostPerLevelDat>();
            return false;
        }

        if (byLevel is null)
        {
            byLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.Level;

                if (!byLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostPerLevelDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostPerLevelDat>>();
        }

        var items = new List<ResultItem<int, ItemCostPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.Cost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost(int? key, out ItemCostPerLevelDat? item)
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
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.Cost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost(int? key, out IReadOnlyList<ItemCostPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostPerLevelDat>();
            return false;
        }

        if (byCost is null)
        {
            byCost = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCost.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCost.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCost.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.byCost"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostPerLevelDat>> GetManyToManyByCost(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostPerLevelDat>>();
        }

        var items = new List<ResultItem<int, ItemCostPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.CostHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCostHardmode(int? key, out ItemCostPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCostHardmode(key, out var items))
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
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.CostHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCostHardmode(int? key, out IReadOnlyList<ItemCostPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostPerLevelDat>();
            return false;
        }

        if (byCostHardmode is null)
        {
            byCostHardmode = new();
            foreach (var item in Items)
            {
                var itemKey = item.CostHardmode;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCostHardmode.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCostHardmode.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCostHardmode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostPerLevelDat"/> with <see cref="ItemCostPerLevelDat.byCostHardmode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostPerLevelDat>> GetManyToManyByCostHardmode(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostPerLevelDat>>();
        }

        var items = new List<ResultItem<int, ItemCostPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCostHardmode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ItemCostPerLevelDat[] Load()
    {
        const string filePath = "Data/ItemCostPerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemCostPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Contract_BaseItemTypesKey
            (var contract_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Cost
            (var costLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading CostHardmode
            (var costhardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemCostPerLevelDat()
            {
                Contract_BaseItemTypesKey = contract_baseitemtypeskeyLoading,
                Level = levelLoading,
                Cost = costLoading,
                CostHardmode = costhardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
