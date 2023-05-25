using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="RecipeUnlockObjectsDat"/> related data and helper methods.
/// </summary>
public sealed class RecipeUnlockObjectsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<RecipeUnlockObjectsDat> Items { get; }

    private Dictionary<int, List<RecipeUnlockObjectsDat>>? byWorldAreasKey;
    private Dictionary<string, List<RecipeUnlockObjectsDat>>? byInheritsFrom;
    private Dictionary<int, List<RecipeUnlockObjectsDat>>? byRecipeId;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecipeUnlockObjectsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal RecipeUnlockObjectsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockObjectsDat"/> with <see cref="RecipeUnlockObjectsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out RecipeUnlockObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="RecipeUnlockObjectsDat"/> with <see cref="RecipeUnlockObjectsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<RecipeUnlockObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RecipeUnlockObjectsDat>();
            return false;
        }

        if (byWorldAreasKey is null)
        {
            byWorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RecipeUnlockObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockObjectsDat"/> with <see cref="RecipeUnlockObjectsDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RecipeUnlockObjectsDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RecipeUnlockObjectsDat>>();
        }

        var items = new List<ResultItem<int, RecipeUnlockObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RecipeUnlockObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockObjectsDat"/> with <see cref="RecipeUnlockObjectsDat.InheritsFrom"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInheritsFrom(string? key, out RecipeUnlockObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInheritsFrom(key, out var items))
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
    /// Tries to get <see cref="RecipeUnlockObjectsDat"/> with <see cref="RecipeUnlockObjectsDat.InheritsFrom"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInheritsFrom(string? key, out IReadOnlyList<RecipeUnlockObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RecipeUnlockObjectsDat>();
            return false;
        }

        if (byInheritsFrom is null)
        {
            byInheritsFrom = new();
            foreach (var item in Items)
            {
                var itemKey = item.InheritsFrom;

                if (!byInheritsFrom.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInheritsFrom.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInheritsFrom.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RecipeUnlockObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockObjectsDat"/> with <see cref="RecipeUnlockObjectsDat.byInheritsFrom"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RecipeUnlockObjectsDat>> GetManyToManyByInheritsFrom(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RecipeUnlockObjectsDat>>();
        }

        var items = new List<ResultItem<string, RecipeUnlockObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInheritsFrom(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RecipeUnlockObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockObjectsDat"/> with <see cref="RecipeUnlockObjectsDat.RecipeId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRecipeId(int? key, out RecipeUnlockObjectsDat? item)
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
    /// Tries to get <see cref="RecipeUnlockObjectsDat"/> with <see cref="RecipeUnlockObjectsDat.RecipeId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRecipeId(int? key, out IReadOnlyList<RecipeUnlockObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RecipeUnlockObjectsDat>();
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
            items = Array.Empty<RecipeUnlockObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RecipeUnlockObjectsDat"/> with <see cref="RecipeUnlockObjectsDat.byRecipeId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RecipeUnlockObjectsDat>> GetManyToManyByRecipeId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RecipeUnlockObjectsDat>>();
        }

        var items = new List<ResultItem<int, RecipeUnlockObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRecipeId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RecipeUnlockObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private RecipeUnlockObjectsDat[] Load()
    {
        const string filePath = "Data/RecipeUnlockObjects.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RecipeUnlockObjectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading InheritsFrom
            (var inheritsfromLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RecipeId
            (var recipeidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RecipeUnlockObjectsDat()
            {
                WorldAreasKey = worldareaskeyLoading,
                InheritsFrom = inheritsfromLoading,
                RecipeId = recipeidLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
