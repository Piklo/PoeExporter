using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ShopPaymentPackagePriceDat"/> related data and helper methods.
/// </summary>
public sealed class ShopPaymentPackagePriceRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ShopPaymentPackagePriceDat> Items { get; }

    private Dictionary<int, List<ShopPaymentPackagePriceDat>>? byShopPaymentPackageKey;
    private Dictionary<int, List<ShopPaymentPackagePriceDat>>? byShopCountryKey;
    private Dictionary<int, List<ShopPaymentPackagePriceDat>>? byPrice;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShopPaymentPackagePriceRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ShopPaymentPackagePriceRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackagePriceDat"/> with <see cref="ShopPaymentPackagePriceDat.ShopPaymentPackageKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShopPaymentPackageKey(int? key, out ShopPaymentPackagePriceDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShopPaymentPackageKey(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackagePriceDat"/> with <see cref="ShopPaymentPackagePriceDat.ShopPaymentPackageKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShopPaymentPackageKey(int? key, out IReadOnlyList<ShopPaymentPackagePriceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackagePriceDat>();
            return false;
        }

        if (byShopPaymentPackageKey is null)
        {
            byShopPaymentPackageKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShopPaymentPackageKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byShopPaymentPackageKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byShopPaymentPackageKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byShopPaymentPackageKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackagePriceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackagePriceDat"/> with <see cref="ShopPaymentPackagePriceDat.byShopPaymentPackageKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackagePriceDat>> GetManyToManyByShopPaymentPackageKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackagePriceDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackagePriceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShopPaymentPackageKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackagePriceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackagePriceDat"/> with <see cref="ShopPaymentPackagePriceDat.ShopCountryKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShopCountryKey(int? key, out ShopPaymentPackagePriceDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShopCountryKey(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackagePriceDat"/> with <see cref="ShopPaymentPackagePriceDat.ShopCountryKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShopCountryKey(int? key, out IReadOnlyList<ShopPaymentPackagePriceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackagePriceDat>();
            return false;
        }

        if (byShopCountryKey is null)
        {
            byShopCountryKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShopCountryKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byShopCountryKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byShopCountryKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byShopCountryKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackagePriceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackagePriceDat"/> with <see cref="ShopPaymentPackagePriceDat.byShopCountryKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackagePriceDat>> GetManyToManyByShopCountryKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackagePriceDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackagePriceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShopCountryKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackagePriceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackagePriceDat"/> with <see cref="ShopPaymentPackagePriceDat.Price"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPrice(int? key, out ShopPaymentPackagePriceDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPrice(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackagePriceDat"/> with <see cref="ShopPaymentPackagePriceDat.Price"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPrice(int? key, out IReadOnlyList<ShopPaymentPackagePriceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackagePriceDat>();
            return false;
        }

        if (byPrice is null)
        {
            byPrice = new();
            foreach (var item in Items)
            {
                var itemKey = item.Price;

                if (!byPrice.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPrice.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPrice.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackagePriceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackagePriceDat"/> with <see cref="ShopPaymentPackagePriceDat.byPrice"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackagePriceDat>> GetManyToManyByPrice(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackagePriceDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackagePriceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPrice(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackagePriceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ShopPaymentPackagePriceDat[] Load()
    {
        const string filePath = "Data/ShopPaymentPackagePrice.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopPaymentPackagePriceDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ShopPaymentPackageKey
            (var shoppaymentpackagekeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ShopCountryKey
            (var shopcountrykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Price
            (var priceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopPaymentPackagePriceDat()
            {
                ShopPaymentPackageKey = shoppaymentpackagekeyLoading,
                ShopCountryKey = shopcountrykeyLoading,
                Price = priceLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
