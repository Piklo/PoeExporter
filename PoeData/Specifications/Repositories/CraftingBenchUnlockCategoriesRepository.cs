using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CraftingBenchUnlockCategoriesDat"/> related data and helper methods.
/// </summary>
public sealed class CraftingBenchUnlockCategoriesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CraftingBenchUnlockCategoriesDat> Items { get; }

    private Dictionary<string, List<CraftingBenchUnlockCategoriesDat>>? byId;
    private Dictionary<int, List<CraftingBenchUnlockCategoriesDat>>? byUnknown8;
    private Dictionary<int, List<CraftingBenchUnlockCategoriesDat>>? byUnknown12;
    private Dictionary<string, List<CraftingBenchUnlockCategoriesDat>>? byUnlockType;
    private Dictionary<int, List<CraftingBenchUnlockCategoriesDat>>? byCraftingItemClassCategories;
    private Dictionary<string, List<CraftingBenchUnlockCategoriesDat>>? byObtainingDescription;

    /// <summary>
    /// Initializes a new instance of the <see cref="CraftingBenchUnlockCategoriesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CraftingBenchUnlockCategoriesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CraftingBenchUnlockCategoriesDat? item)
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
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CraftingBenchUnlockCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
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
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CraftingBenchUnlockCategoriesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CraftingBenchUnlockCategoriesDat>>();
        }

        var items = new List<ResultItem<string, CraftingBenchUnlockCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CraftingBenchUnlockCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out CraftingBenchUnlockCategoriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<CraftingBenchUnlockCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;

                if (!byUnknown8.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchUnlockCategoriesDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchUnlockCategoriesDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchUnlockCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchUnlockCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out CraftingBenchUnlockCategoriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown12(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<CraftingBenchUnlockCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown12.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown12.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchUnlockCategoriesDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchUnlockCategoriesDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchUnlockCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchUnlockCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.UnlockType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnlockType(string? key, out CraftingBenchUnlockCategoriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnlockType(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.UnlockType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnlockType(string? key, out IReadOnlyList<CraftingBenchUnlockCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        if (byUnlockType is null)
        {
            byUnlockType = new();
            foreach (var item in Items)
            {
                var itemKey = item.UnlockType;

                if (!byUnlockType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnlockType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnlockType.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.byUnlockType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CraftingBenchUnlockCategoriesDat>> GetManyToManyByUnlockType(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CraftingBenchUnlockCategoriesDat>>();
        }

        var items = new List<ResultItem<string, CraftingBenchUnlockCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnlockType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CraftingBenchUnlockCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.CraftingItemClassCategories"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCraftingItemClassCategories(int? key, out CraftingBenchUnlockCategoriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCraftingItemClassCategories(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.CraftingItemClassCategories"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCraftingItemClassCategories(int? key, out IReadOnlyList<CraftingBenchUnlockCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        if (byCraftingItemClassCategories is null)
        {
            byCraftingItemClassCategories = new();
            foreach (var item in Items)
            {
                var itemKey = item.CraftingItemClassCategories;
                foreach (var listKey in itemKey)
                {
                    if (!byCraftingItemClassCategories.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCraftingItemClassCategories.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCraftingItemClassCategories.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.byCraftingItemClassCategories"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchUnlockCategoriesDat>> GetManyToManyByCraftingItemClassCategories(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchUnlockCategoriesDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchUnlockCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCraftingItemClassCategories(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchUnlockCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.ObtainingDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByObtainingDescription(string? key, out CraftingBenchUnlockCategoriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByObtainingDescription(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.ObtainingDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByObtainingDescription(string? key, out IReadOnlyList<CraftingBenchUnlockCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        if (byObtainingDescription is null)
        {
            byObtainingDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.ObtainingDescription;

                if (!byObtainingDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byObtainingDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byObtainingDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CraftingBenchUnlockCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchUnlockCategoriesDat"/> with <see cref="CraftingBenchUnlockCategoriesDat.byObtainingDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CraftingBenchUnlockCategoriesDat>> GetManyToManyByObtainingDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CraftingBenchUnlockCategoriesDat>>();
        }

        var items = new List<ResultItem<string, CraftingBenchUnlockCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByObtainingDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CraftingBenchUnlockCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CraftingBenchUnlockCategoriesDat[] Load()
    {
        const string filePath = "Data/CraftingBenchUnlockCategories.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CraftingBenchUnlockCategoriesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var tempunknown12Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown12Loading = tempunknown12Loading.AsReadOnly();

            // loading UnlockType
            (var unlocktypeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CraftingItemClassCategories
            (var tempcraftingitemclasscategoriesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var craftingitemclasscategoriesLoading = tempcraftingitemclasscategoriesLoading.AsReadOnly();

            // loading ObtainingDescription
            (var obtainingdescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CraftingBenchUnlockCategoriesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                UnlockType = unlocktypeLoading,
                CraftingItemClassCategories = craftingitemclasscategoriesLoading,
                ObtainingDescription = obtainingdescriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
