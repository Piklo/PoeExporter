using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ShopCurrencyDat"/> related data and helper methods.
/// </summary>
public sealed class ShopCurrencyRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ShopCurrencyDat> Items { get; }

    private Dictionary<string, List<ShopCurrencyDat>>? byCurrencyCode;
    private Dictionary<string, List<ShopCurrencyDat>>? byCurrencySign;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShopCurrencyRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ShopCurrencyRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ShopCurrencyDat"/> with <see cref="ShopCurrencyDat.CurrencyCode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCurrencyCode(string? key, out ShopCurrencyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCurrencyCode(key, out var items))
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
    /// Tries to get <see cref="ShopCurrencyDat"/> with <see cref="ShopCurrencyDat.CurrencyCode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCurrencyCode(string? key, out IReadOnlyList<ShopCurrencyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCurrencyDat>();
            return false;
        }

        if (byCurrencyCode is null)
        {
            byCurrencyCode = new();
            foreach (var item in Items)
            {
                var itemKey = item.CurrencyCode;

                if (!byCurrencyCode.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCurrencyCode.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCurrencyCode.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopCurrencyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCurrencyDat"/> with <see cref="ShopCurrencyDat.byCurrencyCode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopCurrencyDat>> GetManyToManyByCurrencyCode(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopCurrencyDat>>();
        }

        var items = new List<ResultItem<string, ShopCurrencyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCurrencyCode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopCurrencyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCurrencyDat"/> with <see cref="ShopCurrencyDat.CurrencySign"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCurrencySign(string? key, out ShopCurrencyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCurrencySign(key, out var items))
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
    /// Tries to get <see cref="ShopCurrencyDat"/> with <see cref="ShopCurrencyDat.CurrencySign"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCurrencySign(string? key, out IReadOnlyList<ShopCurrencyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCurrencyDat>();
            return false;
        }

        if (byCurrencySign is null)
        {
            byCurrencySign = new();
            foreach (var item in Items)
            {
                var itemKey = item.CurrencySign;

                if (!byCurrencySign.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCurrencySign.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCurrencySign.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopCurrencyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCurrencyDat"/> with <see cref="ShopCurrencyDat.byCurrencySign"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopCurrencyDat>> GetManyToManyByCurrencySign(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopCurrencyDat>>();
        }

        var items = new List<ResultItem<string, ShopCurrencyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCurrencySign(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopCurrencyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ShopCurrencyDat[] Load()
    {
        const string filePath = "Data/ShopCurrency.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopCurrencyDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading CurrencyCode
            (var currencycodeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CurrencySign
            (var currencysignLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopCurrencyDat()
            {
                CurrencyCode = currencycodeLoading,
                CurrencySign = currencysignLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
