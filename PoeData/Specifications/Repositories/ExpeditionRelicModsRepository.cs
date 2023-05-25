using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ExpeditionRelicModsDat"/> related data and helper methods.
/// </summary>
public sealed class ExpeditionRelicModsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ExpeditionRelicModsDat> Items { get; }

    private Dictionary<int, List<ExpeditionRelicModsDat>>? byMod;
    private Dictionary<int, List<ExpeditionRelicModsDat>>? byCategories;
    private Dictionary<int, List<ExpeditionRelicModsDat>>? byDestroyAchievements;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpeditionRelicModsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ExpeditionRelicModsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionRelicModsDat"/> with <see cref="ExpeditionRelicModsDat.Mod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMod(int? key, out ExpeditionRelicModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMod(key, out var items))
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
    /// Tries to get <see cref="ExpeditionRelicModsDat"/> with <see cref="ExpeditionRelicModsDat.Mod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMod(int? key, out IReadOnlyList<ExpeditionRelicModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionRelicModsDat>();
            return false;
        }

        if (byMod is null)
        {
            byMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionRelicModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionRelicModsDat"/> with <see cref="ExpeditionRelicModsDat.byMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionRelicModsDat>> GetManyToManyByMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionRelicModsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionRelicModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionRelicModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionRelicModsDat"/> with <see cref="ExpeditionRelicModsDat.Categories"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCategories(int? key, out ExpeditionRelicModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCategories(key, out var items))
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
    /// Tries to get <see cref="ExpeditionRelicModsDat"/> with <see cref="ExpeditionRelicModsDat.Categories"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCategories(int? key, out IReadOnlyList<ExpeditionRelicModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionRelicModsDat>();
            return false;
        }

        if (byCategories is null)
        {
            byCategories = new();
            foreach (var item in Items)
            {
                var itemKey = item.Categories;
                foreach (var listKey in itemKey)
                {
                    if (!byCategories.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCategories.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCategories.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionRelicModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionRelicModsDat"/> with <see cref="ExpeditionRelicModsDat.byCategories"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionRelicModsDat>> GetManyToManyByCategories(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionRelicModsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionRelicModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCategories(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionRelicModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionRelicModsDat"/> with <see cref="ExpeditionRelicModsDat.DestroyAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDestroyAchievements(int? key, out ExpeditionRelicModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDestroyAchievements(key, out var items))
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
    /// Tries to get <see cref="ExpeditionRelicModsDat"/> with <see cref="ExpeditionRelicModsDat.DestroyAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDestroyAchievements(int? key, out IReadOnlyList<ExpeditionRelicModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionRelicModsDat>();
            return false;
        }

        if (byDestroyAchievements is null)
        {
            byDestroyAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.DestroyAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byDestroyAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byDestroyAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byDestroyAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionRelicModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionRelicModsDat"/> with <see cref="ExpeditionRelicModsDat.byDestroyAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionRelicModsDat>> GetManyToManyByDestroyAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionRelicModsDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionRelicModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDestroyAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionRelicModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ExpeditionRelicModsDat[] Load()
    {
        const string filePath = "Data/ExpeditionRelicMods.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionRelicModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Mod
            (var modLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Categories
            (var tempcategoriesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var categoriesLoading = tempcategoriesLoading.AsReadOnly();

            // loading DestroyAchievements
            (var tempdestroyachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var destroyachievementsLoading = tempdestroyachievementsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionRelicModsDat()
            {
                Mod = modLoading,
                Categories = categoriesLoading,
                DestroyAchievements = destroyachievementsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
