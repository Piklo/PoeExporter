using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ArchnemesisRecipesDat"/> related data and helper methods.
/// </summary>
public sealed class ArchnemesisRecipesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ArchnemesisRecipesDat> Items { get; }

    private Dictionary<int, List<ArchnemesisRecipesDat>>? byResult;
    private Dictionary<int, List<ArchnemesisRecipesDat>>? byRecipe;
    private Dictionary<int, List<ArchnemesisRecipesDat>>? byUnknown32;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchnemesisRecipesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ArchnemesisRecipesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisRecipesDat"/> with <see cref="ArchnemesisRecipesDat.Result"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByResult(int? key, out ArchnemesisRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByResult(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisRecipesDat"/> with <see cref="ArchnemesisRecipesDat.Result"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByResult(int? key, out IReadOnlyList<ArchnemesisRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisRecipesDat>();
            return false;
        }

        if (byResult is null)
        {
            byResult = new();
            foreach (var item in Items)
            {
                var itemKey = item.Result;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byResult.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byResult.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byResult.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchnemesisRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisRecipesDat"/> with <see cref="ArchnemesisRecipesDat.byResult"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArchnemesisRecipesDat>> GetManyToManyByResult(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArchnemesisRecipesDat>>();
        }

        var items = new List<ResultItem<int, ArchnemesisRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByResult(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArchnemesisRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisRecipesDat"/> with <see cref="ArchnemesisRecipesDat.Recipe"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRecipe(int? key, out ArchnemesisRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRecipe(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisRecipesDat"/> with <see cref="ArchnemesisRecipesDat.Recipe"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRecipe(int? key, out IReadOnlyList<ArchnemesisRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisRecipesDat>();
            return false;
        }

        if (byRecipe is null)
        {
            byRecipe = new();
            foreach (var item in Items)
            {
                var itemKey = item.Recipe;
                foreach (var listKey in itemKey)
                {
                    if (!byRecipe.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byRecipe.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byRecipe.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchnemesisRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisRecipesDat"/> with <see cref="ArchnemesisRecipesDat.byRecipe"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArchnemesisRecipesDat>> GetManyToManyByRecipe(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArchnemesisRecipesDat>>();
        }

        var items = new List<ResultItem<int, ArchnemesisRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRecipe(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArchnemesisRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisRecipesDat"/> with <see cref="ArchnemesisRecipesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out ArchnemesisRecipesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisRecipesDat"/> with <see cref="ArchnemesisRecipesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<ArchnemesisRecipesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisRecipesDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchnemesisRecipesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisRecipesDat"/> with <see cref="ArchnemesisRecipesDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArchnemesisRecipesDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArchnemesisRecipesDat>>();
        }

        var items = new List<ResultItem<int, ArchnemesisRecipesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArchnemesisRecipesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ArchnemesisRecipesDat[] Load()
    {
        const string filePath = "Data/ArchnemesisRecipes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArchnemesisRecipesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Result
            (var resultLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Recipe
            (var temprecipeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var recipeLoading = temprecipeLoading.AsReadOnly();

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArchnemesisRecipesDat()
            {
                Result = resultLoading,
                Recipe = recipeLoading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
