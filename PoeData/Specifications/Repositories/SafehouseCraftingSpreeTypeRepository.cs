using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SafehouseCraftingSpreeTypeDat"/> related data and helper methods.
/// </summary>
public sealed class SafehouseCraftingSpreeTypeRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SafehouseCraftingSpreeTypeDat> Items { get; }

    private Dictionary<string, List<SafehouseCraftingSpreeTypeDat>>? byId;
    private Dictionary<int, List<SafehouseCraftingSpreeTypeDat>>? byCurrencies;
    private Dictionary<int, List<SafehouseCraftingSpreeTypeDat>>? byCurrencyCount;
    private Dictionary<int, List<SafehouseCraftingSpreeTypeDat>>? byUnknown40;
    private Dictionary<bool, List<SafehouseCraftingSpreeTypeDat>>? byDisabled;
    private Dictionary<string, List<SafehouseCraftingSpreeTypeDat>>? byItemClassText;

    /// <summary>
    /// Initializes a new instance of the <see cref="SafehouseCraftingSpreeTypeRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SafehouseCraftingSpreeTypeRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SafehouseCraftingSpreeTypeDat? item)
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
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SafehouseCraftingSpreeTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
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
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SafehouseCraftingSpreeTypeDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SafehouseCraftingSpreeTypeDat>>();
        }

        var items = new List<ResultItem<string, SafehouseCraftingSpreeTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SafehouseCraftingSpreeTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.Currencies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCurrencies(int? key, out SafehouseCraftingSpreeTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCurrencies(key, out var items))
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
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.Currencies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCurrencies(int? key, out IReadOnlyList<SafehouseCraftingSpreeTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        if (byCurrencies is null)
        {
            byCurrencies = new();
            foreach (var item in Items)
            {
                var itemKey = item.Currencies;
                foreach (var listKey in itemKey)
                {
                    if (!byCurrencies.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCurrencies.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCurrencies.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.byCurrencies"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SafehouseCraftingSpreeTypeDat>> GetManyToManyByCurrencies(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SafehouseCraftingSpreeTypeDat>>();
        }

        var items = new List<ResultItem<int, SafehouseCraftingSpreeTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCurrencies(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SafehouseCraftingSpreeTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.CurrencyCount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCurrencyCount(int? key, out SafehouseCraftingSpreeTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCurrencyCount(key, out var items))
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
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.CurrencyCount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCurrencyCount(int? key, out IReadOnlyList<SafehouseCraftingSpreeTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        if (byCurrencyCount is null)
        {
            byCurrencyCount = new();
            foreach (var item in Items)
            {
                var itemKey = item.CurrencyCount;
                foreach (var listKey in itemKey)
                {
                    if (!byCurrencyCount.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCurrencyCount.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCurrencyCount.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.byCurrencyCount"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SafehouseCraftingSpreeTypeDat>> GetManyToManyByCurrencyCount(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SafehouseCraftingSpreeTypeDat>>();
        }

        var items = new List<ResultItem<int, SafehouseCraftingSpreeTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCurrencyCount(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SafehouseCraftingSpreeTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out SafehouseCraftingSpreeTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<SafehouseCraftingSpreeTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown40.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown40.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SafehouseCraftingSpreeTypeDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SafehouseCraftingSpreeTypeDat>>();
        }

        var items = new List<ResultItem<int, SafehouseCraftingSpreeTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SafehouseCraftingSpreeTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.Disabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisabled(bool? key, out SafehouseCraftingSpreeTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisabled(key, out var items))
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
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.Disabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisabled(bool? key, out IReadOnlyList<SafehouseCraftingSpreeTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        if (byDisabled is null)
        {
            byDisabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.Disabled;

                if (!byDisabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDisabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDisabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.byDisabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SafehouseCraftingSpreeTypeDat>> GetManyToManyByDisabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SafehouseCraftingSpreeTypeDat>>();
        }

        var items = new List<ResultItem<bool, SafehouseCraftingSpreeTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SafehouseCraftingSpreeTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.ItemClassText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClassText(string? key, out SafehouseCraftingSpreeTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemClassText(key, out var items))
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
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.ItemClassText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClassText(string? key, out IReadOnlyList<SafehouseCraftingSpreeTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        if (byItemClassText is null)
        {
            byItemClassText = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemClassText;

                if (!byItemClassText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byItemClassText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byItemClassText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SafehouseCraftingSpreeTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeTypeDat"/> with <see cref="SafehouseCraftingSpreeTypeDat.byItemClassText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SafehouseCraftingSpreeTypeDat>> GetManyToManyByItemClassText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SafehouseCraftingSpreeTypeDat>>();
        }

        var items = new List<ResultItem<string, SafehouseCraftingSpreeTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClassText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SafehouseCraftingSpreeTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SafehouseCraftingSpreeTypeDat[] Load()
    {
        const string filePath = "Data/SafehouseCraftingSpreeType.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SafehouseCraftingSpreeTypeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Currencies
            (var tempcurrenciesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var currenciesLoading = tempcurrenciesLoading.AsReadOnly();

            // loading CurrencyCount
            (var tempcurrencycountLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var currencycountLoading = tempcurrencycountLoading.AsReadOnly();

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Disabled
            (var disabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ItemClassText
            (var itemclasstextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SafehouseCraftingSpreeTypeDat()
            {
                Id = idLoading,
                Currencies = currenciesLoading,
                CurrencyCount = currencycountLoading,
                Unknown40 = unknown40Loading,
                Disabled = disabledLoading,
                ItemClassText = itemclasstextLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
