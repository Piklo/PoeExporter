using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SafehouseCraftingSpreeCurrenciesDat"/> related data and helper methods.
/// </summary>
public sealed class SafehouseCraftingSpreeCurrenciesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SafehouseCraftingSpreeCurrenciesDat> Items { get; }

    private Dictionary<string, List<SafehouseCraftingSpreeCurrenciesDat>>? byId;
    private Dictionary<int, List<SafehouseCraftingSpreeCurrenciesDat>>? byBaseItemTypesKey;
    private Dictionary<bool, List<SafehouseCraftingSpreeCurrenciesDat>>? byHasSpecificBaseItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="SafehouseCraftingSpreeCurrenciesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SafehouseCraftingSpreeCurrenciesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeCurrenciesDat"/> with <see cref="SafehouseCraftingSpreeCurrenciesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SafehouseCraftingSpreeCurrenciesDat? item)
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
    /// Tries to get <see cref="SafehouseCraftingSpreeCurrenciesDat"/> with <see cref="SafehouseCraftingSpreeCurrenciesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SafehouseCraftingSpreeCurrenciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseCraftingSpreeCurrenciesDat>();
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
            items = Array.Empty<SafehouseCraftingSpreeCurrenciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeCurrenciesDat"/> with <see cref="SafehouseCraftingSpreeCurrenciesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SafehouseCraftingSpreeCurrenciesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SafehouseCraftingSpreeCurrenciesDat>>();
        }

        var items = new List<ResultItem<string, SafehouseCraftingSpreeCurrenciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SafehouseCraftingSpreeCurrenciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeCurrenciesDat"/> with <see cref="SafehouseCraftingSpreeCurrenciesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out SafehouseCraftingSpreeCurrenciesDat? item)
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
    /// Tries to get <see cref="SafehouseCraftingSpreeCurrenciesDat"/> with <see cref="SafehouseCraftingSpreeCurrenciesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<SafehouseCraftingSpreeCurrenciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseCraftingSpreeCurrenciesDat>();
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
            items = Array.Empty<SafehouseCraftingSpreeCurrenciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeCurrenciesDat"/> with <see cref="SafehouseCraftingSpreeCurrenciesDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SafehouseCraftingSpreeCurrenciesDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SafehouseCraftingSpreeCurrenciesDat>>();
        }

        var items = new List<ResultItem<int, SafehouseCraftingSpreeCurrenciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SafehouseCraftingSpreeCurrenciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeCurrenciesDat"/> with <see cref="SafehouseCraftingSpreeCurrenciesDat.HasSpecificBaseItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHasSpecificBaseItem(bool? key, out SafehouseCraftingSpreeCurrenciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHasSpecificBaseItem(key, out var items))
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
    /// Tries to get <see cref="SafehouseCraftingSpreeCurrenciesDat"/> with <see cref="SafehouseCraftingSpreeCurrenciesDat.HasSpecificBaseItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHasSpecificBaseItem(bool? key, out IReadOnlyList<SafehouseCraftingSpreeCurrenciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseCraftingSpreeCurrenciesDat>();
            return false;
        }

        if (byHasSpecificBaseItem is null)
        {
            byHasSpecificBaseItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.HasSpecificBaseItem;

                if (!byHasSpecificBaseItem.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHasSpecificBaseItem.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHasSpecificBaseItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SafehouseCraftingSpreeCurrenciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseCraftingSpreeCurrenciesDat"/> with <see cref="SafehouseCraftingSpreeCurrenciesDat.byHasSpecificBaseItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SafehouseCraftingSpreeCurrenciesDat>> GetManyToManyByHasSpecificBaseItem(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SafehouseCraftingSpreeCurrenciesDat>>();
        }

        var items = new List<ResultItem<bool, SafehouseCraftingSpreeCurrenciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHasSpecificBaseItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SafehouseCraftingSpreeCurrenciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SafehouseCraftingSpreeCurrenciesDat[] Load()
    {
        const string filePath = "Data/SafehouseCraftingSpreeCurrencies.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SafehouseCraftingSpreeCurrenciesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HasSpecificBaseItem
            (var hasspecificbaseitemLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SafehouseCraftingSpreeCurrenciesDat()
            {
                Id = idLoading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
                HasSpecificBaseItem = hasspecificbaseitemLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
