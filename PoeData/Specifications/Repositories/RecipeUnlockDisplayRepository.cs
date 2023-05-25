using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="RecipeUnlockDisplayDat"/> related data and helper methods.
/// </summary>
public sealed class RecipeUnlockDisplayRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<RecipeUnlockDisplayDat> Items { get; }

    private Dictionary<int, List<RecipeUnlockDisplayDat>>? byRecipeId;
    private Dictionary<string, List<RecipeUnlockDisplayDat>>? byDescription;
    private Dictionary<int, List<RecipeUnlockDisplayDat>>? byCraftingItemClassCategoriesKeys;
    private Dictionary<string, List<RecipeUnlockDisplayDat>>? byUnlockDescription;
    private Dictionary<int, List<RecipeUnlockDisplayDat>>? byRank;
    private Dictionary<int, List<RecipeUnlockDisplayDat>>? byUnlockArea;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecipeUnlockDisplayRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal RecipeUnlockDisplayRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.RecipeId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRecipeId(int? key, out RecipeUnlockDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRecipeId(key, out var items))
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
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.RecipeId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRecipeId(int? key, out IReadOnlyList<RecipeUnlockDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        if (byRecipeId is null)
        {
            byRecipeId = new();
            foreach (var item in Items)
            {
                var itemKey = item.RecipeId;

                if (!byRecipeId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRecipeId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRecipeId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.byRecipeId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RecipeUnlockDisplayDat>> GetManyToManyByRecipeId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RecipeUnlockDisplayDat>>();
        }

        var items = new List<ResultItem<int, RecipeUnlockDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRecipeId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RecipeUnlockDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out RecipeUnlockDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<RecipeUnlockDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RecipeUnlockDisplayDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RecipeUnlockDisplayDat>>();
        }

        var items = new List<ResultItem<string, RecipeUnlockDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RecipeUnlockDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.CraftingItemClassCategoriesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCraftingItemClassCategoriesKeys(int? key, out RecipeUnlockDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCraftingItemClassCategoriesKeys(key, out var items))
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
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.CraftingItemClassCategoriesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCraftingItemClassCategoriesKeys(int? key, out IReadOnlyList<RecipeUnlockDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        if (byCraftingItemClassCategoriesKeys is null)
        {
            byCraftingItemClassCategoriesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.CraftingItemClassCategoriesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byCraftingItemClassCategoriesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCraftingItemClassCategoriesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCraftingItemClassCategoriesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.byCraftingItemClassCategoriesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RecipeUnlockDisplayDat>> GetManyToManyByCraftingItemClassCategoriesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RecipeUnlockDisplayDat>>();
        }

        var items = new List<ResultItem<int, RecipeUnlockDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCraftingItemClassCategoriesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RecipeUnlockDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.UnlockDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnlockDescription(string? key, out RecipeUnlockDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnlockDescription(key, out var items))
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
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.UnlockDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnlockDescription(string? key, out IReadOnlyList<RecipeUnlockDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        if (byUnlockDescription is null)
        {
            byUnlockDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.UnlockDescription;

                if (!byUnlockDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnlockDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnlockDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.byUnlockDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RecipeUnlockDisplayDat>> GetManyToManyByUnlockDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RecipeUnlockDisplayDat>>();
        }

        var items = new List<ResultItem<string, RecipeUnlockDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnlockDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RecipeUnlockDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.Rank"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRank(int? key, out RecipeUnlockDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRank(key, out var items))
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
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.Rank"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRank(int? key, out IReadOnlyList<RecipeUnlockDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        if (byRank is null)
        {
            byRank = new();
            foreach (var item in Items)
            {
                var itemKey = item.Rank;

                if (!byRank.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRank.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRank.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.byRank"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RecipeUnlockDisplayDat>> GetManyToManyByRank(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RecipeUnlockDisplayDat>>();
        }

        var items = new List<ResultItem<int, RecipeUnlockDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRank(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RecipeUnlockDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.UnlockArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnlockArea(int? key, out RecipeUnlockDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnlockArea(key, out var items))
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
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.UnlockArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnlockArea(int? key, out IReadOnlyList<RecipeUnlockDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        if (byUnlockArea is null)
        {
            byUnlockArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.UnlockArea;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnlockArea.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnlockArea.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnlockArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RecipeUnlockDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockDisplayDat"/> with <see cref="RecipeUnlockDisplayDat.byUnlockArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RecipeUnlockDisplayDat>> GetManyToManyByUnlockArea(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RecipeUnlockDisplayDat>>();
        }

        var items = new List<ResultItem<int, RecipeUnlockDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnlockArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RecipeUnlockDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private RecipeUnlockDisplayDat[] Load()
    {
        const string filePath = "Data/RecipeUnlockDisplay.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RecipeUnlockDisplayDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading RecipeId
            (var recipeidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CraftingItemClassCategoriesKeys
            (var tempcraftingitemclasscategorieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var craftingitemclasscategorieskeysLoading = tempcraftingitemclasscategorieskeysLoading.AsReadOnly();

            // loading UnlockDescription
            (var unlockdescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Rank
            (var rankLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UnlockArea
            (var unlockareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RecipeUnlockDisplayDat()
            {
                RecipeId = recipeidLoading,
                Description = descriptionLoading,
                CraftingItemClassCategoriesKeys = craftingitemclasscategorieskeysLoading,
                UnlockDescription = unlockdescriptionLoading,
                Rank = rankLoading,
                UnlockArea = unlockareaLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
