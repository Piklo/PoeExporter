using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="TradeMarketCategoryListAllClassDat"/> related data and helper methods.
/// </summary>
public sealed class TradeMarketCategoryListAllClassRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<TradeMarketCategoryListAllClassDat> Items { get; }

    private Dictionary<int, List<TradeMarketCategoryListAllClassDat>>? byTradeCategory;
    private Dictionary<int, List<TradeMarketCategoryListAllClassDat>>? byItemClass;

    /// <summary>
    /// Initializes a new instance of the <see cref="TradeMarketCategoryListAllClassRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal TradeMarketCategoryListAllClassRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="TradeMarketCategoryListAllClassDat"/> with <see cref="TradeMarketCategoryListAllClassDat.TradeCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTradeCategory(int? key, out TradeMarketCategoryListAllClassDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTradeCategory(key, out var items))
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
    /// Tries to get <see cref="TradeMarketCategoryListAllClassDat"/> with <see cref="TradeMarketCategoryListAllClassDat.TradeCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTradeCategory(int? key, out IReadOnlyList<TradeMarketCategoryListAllClassDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TradeMarketCategoryListAllClassDat>();
            return false;
        }

        if (byTradeCategory is null)
        {
            byTradeCategory = new();
            foreach (var item in Items)
            {
                var itemKey = item.TradeCategory;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTradeCategory.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTradeCategory.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTradeCategory.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TradeMarketCategoryListAllClassDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TradeMarketCategoryListAllClassDat"/> with <see cref="TradeMarketCategoryListAllClassDat.byTradeCategory"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TradeMarketCategoryListAllClassDat>> GetManyToManyByTradeCategory(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TradeMarketCategoryListAllClassDat>>();
        }

        var items = new List<ResultItem<int, TradeMarketCategoryListAllClassDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTradeCategory(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TradeMarketCategoryListAllClassDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TradeMarketCategoryListAllClassDat"/> with <see cref="TradeMarketCategoryListAllClassDat.ItemClass"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClass(int? key, out TradeMarketCategoryListAllClassDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemClass(key, out var items))
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
    /// Tries to get <see cref="TradeMarketCategoryListAllClassDat"/> with <see cref="TradeMarketCategoryListAllClassDat.ItemClass"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClass(int? key, out IReadOnlyList<TradeMarketCategoryListAllClassDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TradeMarketCategoryListAllClassDat>();
            return false;
        }

        if (byItemClass is null)
        {
            byItemClass = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemClass;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemClass.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemClass.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemClass.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TradeMarketCategoryListAllClassDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TradeMarketCategoryListAllClassDat"/> with <see cref="TradeMarketCategoryListAllClassDat.byItemClass"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TradeMarketCategoryListAllClassDat>> GetManyToManyByItemClass(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TradeMarketCategoryListAllClassDat>>();
        }

        var items = new List<ResultItem<int, TradeMarketCategoryListAllClassDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClass(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TradeMarketCategoryListAllClassDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private TradeMarketCategoryListAllClassDat[] Load()
    {
        const string filePath = "Data/TradeMarketCategoryListAllClass.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TradeMarketCategoryListAllClassDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading TradeCategory
            (var tradecategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemClass
            (var itemclassLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TradeMarketCategoryListAllClassDat()
            {
                TradeCategory = tradecategoryLoading,
                ItemClass = itemclassLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
