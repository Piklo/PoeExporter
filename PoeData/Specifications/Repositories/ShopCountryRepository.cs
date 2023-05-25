using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ShopCountryDat"/> related data and helper methods.
/// </summary>
public sealed class ShopCountryRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ShopCountryDat> Items { get; }

    private Dictionary<string, List<ShopCountryDat>>? byCountryTwoLetterCode;
    private Dictionary<string, List<ShopCountryDat>>? byCountry;
    private Dictionary<int, List<ShopCountryDat>>? byShopCurrencyKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShopCountryRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ShopCountryRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ShopCountryDat"/> with <see cref="ShopCountryDat.CountryTwoLetterCode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCountryTwoLetterCode(string? key, out ShopCountryDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCountryTwoLetterCode(key, out var items))
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
    /// Tries to get <see cref="ShopCountryDat"/> with <see cref="ShopCountryDat.CountryTwoLetterCode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCountryTwoLetterCode(string? key, out IReadOnlyList<ShopCountryDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCountryDat>();
            return false;
        }

        if (byCountryTwoLetterCode is null)
        {
            byCountryTwoLetterCode = new();
            foreach (var item in Items)
            {
                var itemKey = item.CountryTwoLetterCode;

                if (!byCountryTwoLetterCode.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCountryTwoLetterCode.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCountryTwoLetterCode.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopCountryDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCountryDat"/> with <see cref="ShopCountryDat.byCountryTwoLetterCode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopCountryDat>> GetManyToManyByCountryTwoLetterCode(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopCountryDat>>();
        }

        var items = new List<ResultItem<string, ShopCountryDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCountryTwoLetterCode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopCountryDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCountryDat"/> with <see cref="ShopCountryDat.Country"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCountry(string? key, out ShopCountryDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCountry(key, out var items))
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
    /// Tries to get <see cref="ShopCountryDat"/> with <see cref="ShopCountryDat.Country"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCountry(string? key, out IReadOnlyList<ShopCountryDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCountryDat>();
            return false;
        }

        if (byCountry is null)
        {
            byCountry = new();
            foreach (var item in Items)
            {
                var itemKey = item.Country;

                if (!byCountry.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCountry.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCountry.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopCountryDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCountryDat"/> with <see cref="ShopCountryDat.byCountry"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopCountryDat>> GetManyToManyByCountry(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopCountryDat>>();
        }

        var items = new List<ResultItem<string, ShopCountryDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCountry(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopCountryDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCountryDat"/> with <see cref="ShopCountryDat.ShopCurrencyKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShopCurrencyKey(int? key, out ShopCountryDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShopCurrencyKey(key, out var items))
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
    /// Tries to get <see cref="ShopCountryDat"/> with <see cref="ShopCountryDat.ShopCurrencyKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShopCurrencyKey(int? key, out IReadOnlyList<ShopCountryDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCountryDat>();
            return false;
        }

        if (byShopCurrencyKey is null)
        {
            byShopCurrencyKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShopCurrencyKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byShopCurrencyKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byShopCurrencyKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byShopCurrencyKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopCountryDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCountryDat"/> with <see cref="ShopCountryDat.byShopCurrencyKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopCountryDat>> GetManyToManyByShopCurrencyKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopCountryDat>>();
        }

        var items = new List<ResultItem<int, ShopCountryDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShopCurrencyKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopCountryDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ShopCountryDat[] Load()
    {
        const string filePath = "Data/ShopCountry.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopCountryDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading CountryTwoLetterCode
            (var countrytwolettercodeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Country
            (var countryLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShopCurrencyKey
            (var shopcurrencykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopCountryDat()
            {
                CountryTwoLetterCode = countrytwolettercodeLoading,
                Country = countryLoading,
                ShopCurrencyKey = shopcurrencykeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
