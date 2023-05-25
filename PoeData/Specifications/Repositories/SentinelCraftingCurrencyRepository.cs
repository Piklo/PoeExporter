using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SentinelCraftingCurrencyDat"/> related data and helper methods.
/// </summary>
public sealed class SentinelCraftingCurrencyRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SentinelCraftingCurrencyDat> Items { get; }

    private Dictionary<int, List<SentinelCraftingCurrencyDat>>? byCurrency;
    private Dictionary<int, List<SentinelCraftingCurrencyDat>>? byType;

    /// <summary>
    /// Initializes a new instance of the <see cref="SentinelCraftingCurrencyRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SentinelCraftingCurrencyRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SentinelCraftingCurrencyDat"/> with <see cref="SentinelCraftingCurrencyDat.Currency"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCurrency(int? key, out SentinelCraftingCurrencyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCurrency(key, out var items))
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
    /// Tries to get <see cref="SentinelCraftingCurrencyDat"/> with <see cref="SentinelCraftingCurrencyDat.Currency"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCurrency(int? key, out IReadOnlyList<SentinelCraftingCurrencyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelCraftingCurrencyDat>();
            return false;
        }

        if (byCurrency is null)
        {
            byCurrency = new();
            foreach (var item in Items)
            {
                var itemKey = item.Currency;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCurrency.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCurrency.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCurrency.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelCraftingCurrencyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelCraftingCurrencyDat"/> with <see cref="SentinelCraftingCurrencyDat.byCurrency"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelCraftingCurrencyDat>> GetManyToManyByCurrency(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelCraftingCurrencyDat>>();
        }

        var items = new List<ResultItem<int, SentinelCraftingCurrencyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCurrency(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelCraftingCurrencyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelCraftingCurrencyDat"/> with <see cref="SentinelCraftingCurrencyDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByType(int? key, out SentinelCraftingCurrencyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByType(key, out var items))
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
    /// Tries to get <see cref="SentinelCraftingCurrencyDat"/> with <see cref="SentinelCraftingCurrencyDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByType(int? key, out IReadOnlyList<SentinelCraftingCurrencyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelCraftingCurrencyDat>();
            return false;
        }

        if (byType is null)
        {
            byType = new();
            foreach (var item in Items)
            {
                var itemKey = item.Type;

                if (!byType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelCraftingCurrencyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelCraftingCurrencyDat"/> with <see cref="SentinelCraftingCurrencyDat.byType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelCraftingCurrencyDat>> GetManyToManyByType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelCraftingCurrencyDat>>();
        }

        var items = new List<ResultItem<int, SentinelCraftingCurrencyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelCraftingCurrencyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SentinelCraftingCurrencyDat[] Load()
    {
        const string filePath = "Data/SentinelCraftingCurrency.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SentinelCraftingCurrencyDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Currency
            (var currencyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SentinelCraftingCurrencyDat()
            {
                Currency = currencyLoading,
                Type = typeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
