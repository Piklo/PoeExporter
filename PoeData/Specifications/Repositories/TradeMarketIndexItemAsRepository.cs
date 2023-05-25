using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="TradeMarketIndexItemAsDat"/> related data and helper methods.
/// </summary>
public sealed class TradeMarketIndexItemAsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<TradeMarketIndexItemAsDat> Items { get; }

    private Dictionary<int, List<TradeMarketIndexItemAsDat>>? byItem;
    private Dictionary<int, List<TradeMarketIndexItemAsDat>>? byIndexAs;

    /// <summary>
    /// Initializes a new instance of the <see cref="TradeMarketIndexItemAsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal TradeMarketIndexItemAsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="TradeMarketIndexItemAsDat"/> with <see cref="TradeMarketIndexItemAsDat.Item"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItem(int? key, out TradeMarketIndexItemAsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItem(key, out var items))
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
    /// Tries to get <see cref="TradeMarketIndexItemAsDat"/> with <see cref="TradeMarketIndexItemAsDat.Item"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItem(int? key, out IReadOnlyList<TradeMarketIndexItemAsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TradeMarketIndexItemAsDat>();
            return false;
        }

        if (byItem is null)
        {
            byItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.Item;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TradeMarketIndexItemAsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TradeMarketIndexItemAsDat"/> with <see cref="TradeMarketIndexItemAsDat.byItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TradeMarketIndexItemAsDat>> GetManyToManyByItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TradeMarketIndexItemAsDat>>();
        }

        var items = new List<ResultItem<int, TradeMarketIndexItemAsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TradeMarketIndexItemAsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TradeMarketIndexItemAsDat"/> with <see cref="TradeMarketIndexItemAsDat.IndexAs"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIndexAs(int? key, out TradeMarketIndexItemAsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIndexAs(key, out var items))
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
    /// Tries to get <see cref="TradeMarketIndexItemAsDat"/> with <see cref="TradeMarketIndexItemAsDat.IndexAs"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIndexAs(int? key, out IReadOnlyList<TradeMarketIndexItemAsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TradeMarketIndexItemAsDat>();
            return false;
        }

        if (byIndexAs is null)
        {
            byIndexAs = new();
            foreach (var item in Items)
            {
                var itemKey = item.IndexAs;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byIndexAs.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byIndexAs.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byIndexAs.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TradeMarketIndexItemAsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TradeMarketIndexItemAsDat"/> with <see cref="TradeMarketIndexItemAsDat.byIndexAs"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TradeMarketIndexItemAsDat>> GetManyToManyByIndexAs(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TradeMarketIndexItemAsDat>>();
        }

        var items = new List<ResultItem<int, TradeMarketIndexItemAsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIndexAs(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TradeMarketIndexItemAsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private TradeMarketIndexItemAsDat[] Load()
    {
        const string filePath = "Data/TradeMarketIndexItemAs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TradeMarketIndexItemAsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Item
            (var itemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IndexAs
            (var indexasLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TradeMarketIndexItemAsDat()
            {
                Item = itemLoading,
                IndexAs = indexasLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
