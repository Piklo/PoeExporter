using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BlightCraftingRecipesDat"/> related data and helper methods.
/// </summary>
public sealed class BlightCraftingRecipesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BlightCraftingRecipesDat> Items { get; }

    private Dictionary<string, List<BlightCraftingRecipesDat>>? byId;
    private Dictionary<int, List<BlightCraftingRecipesDat>>? byBlightCraftingItemsKeys;
    private Dictionary<int, List<BlightCraftingRecipesDat>>? byBlightCraftingResultsKey;
    private Dictionary<int, List<BlightCraftingRecipesDat>>? byBlightCraftingTypesKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightCraftingRecipesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BlightCraftingRecipesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BlightCraftingRecipesDat? item)
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
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BlightCraftingRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingRecipesDat>();
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
            items = Array.Empty<BlightCraftingRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightCraftingRecipesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightCraftingRecipesDat>>();
        }

        var items = new List<ResultItem<string, BlightCraftingRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightCraftingRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.BlightCraftingItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBlightCraftingItemsKeys(int? key, out BlightCraftingRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBlightCraftingItemsKeys(key, out var items))
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
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.BlightCraftingItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBlightCraftingItemsKeys(int? key, out IReadOnlyList<BlightCraftingRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingRecipesDat>();
            return false;
        }

        if (byBlightCraftingItemsKeys is null)
        {
            byBlightCraftingItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.BlightCraftingItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byBlightCraftingItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBlightCraftingItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBlightCraftingItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightCraftingRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.byBlightCraftingItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightCraftingRecipesDat>> GetManyToManyByBlightCraftingItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightCraftingRecipesDat>>();
        }

        var items = new List<ResultItem<int, BlightCraftingRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBlightCraftingItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightCraftingRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.BlightCraftingResultsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBlightCraftingResultsKey(int? key, out BlightCraftingRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBlightCraftingResultsKey(key, out var items))
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
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.BlightCraftingResultsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBlightCraftingResultsKey(int? key, out IReadOnlyList<BlightCraftingRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingRecipesDat>();
            return false;
        }

        if (byBlightCraftingResultsKey is null)
        {
            byBlightCraftingResultsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BlightCraftingResultsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBlightCraftingResultsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBlightCraftingResultsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBlightCraftingResultsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightCraftingRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.byBlightCraftingResultsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightCraftingRecipesDat>> GetManyToManyByBlightCraftingResultsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightCraftingRecipesDat>>();
        }

        var items = new List<ResultItem<int, BlightCraftingRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBlightCraftingResultsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightCraftingRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.BlightCraftingTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBlightCraftingTypesKey(int? key, out BlightCraftingRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBlightCraftingTypesKey(key, out var items))
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
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.BlightCraftingTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBlightCraftingTypesKey(int? key, out IReadOnlyList<BlightCraftingRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingRecipesDat>();
            return false;
        }

        if (byBlightCraftingTypesKey is null)
        {
            byBlightCraftingTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BlightCraftingTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBlightCraftingTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBlightCraftingTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBlightCraftingTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightCraftingRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingRecipesDat"/> with <see cref="BlightCraftingRecipesDat.byBlightCraftingTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightCraftingRecipesDat>> GetManyToManyByBlightCraftingTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightCraftingRecipesDat>>();
        }

        var items = new List<ResultItem<int, BlightCraftingRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBlightCraftingTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightCraftingRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BlightCraftingRecipesDat[] Load()
    {
        const string filePath = "Data/BlightCraftingRecipes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightCraftingRecipesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BlightCraftingItemsKeys
            (var tempblightcraftingitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var blightcraftingitemskeysLoading = tempblightcraftingitemskeysLoading.AsReadOnly();

            // loading BlightCraftingResultsKey
            (var blightcraftingresultskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BlightCraftingTypesKey
            (var blightcraftingtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightCraftingRecipesDat()
            {
                Id = idLoading,
                BlightCraftingItemsKeys = blightcraftingitemskeysLoading,
                BlightCraftingResultsKey = blightcraftingresultskeyLoading,
                BlightCraftingTypesKey = blightcraftingtypeskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
