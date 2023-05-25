using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CraftingBenchSortCategoriesDat"/> related data and helper methods.
/// </summary>
public sealed class CraftingBenchSortCategoriesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CraftingBenchSortCategoriesDat> Items { get; }

    private Dictionary<string, List<CraftingBenchSortCategoriesDat>>? byId;
    private Dictionary<int, List<CraftingBenchSortCategoriesDat>>? byName;
    private Dictionary<bool, List<CraftingBenchSortCategoriesDat>>? byIsVisible;

    /// <summary>
    /// Initializes a new instance of the <see cref="CraftingBenchSortCategoriesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CraftingBenchSortCategoriesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchSortCategoriesDat"/> with <see cref="CraftingBenchSortCategoriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CraftingBenchSortCategoriesDat? item)
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
    /// Tries to get <see cref="CraftingBenchSortCategoriesDat"/> with <see cref="CraftingBenchSortCategoriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CraftingBenchSortCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchSortCategoriesDat>();
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
            items = Array.Empty<CraftingBenchSortCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchSortCategoriesDat"/> with <see cref="CraftingBenchSortCategoriesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CraftingBenchSortCategoriesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CraftingBenchSortCategoriesDat>>();
        }

        var items = new List<ResultItem<string, CraftingBenchSortCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CraftingBenchSortCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchSortCategoriesDat"/> with <see cref="CraftingBenchSortCategoriesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(int? key, out CraftingBenchSortCategoriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByName(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchSortCategoriesDat"/> with <see cref="CraftingBenchSortCategoriesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(int? key, out IReadOnlyList<CraftingBenchSortCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchSortCategoriesDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byName.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchSortCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchSortCategoriesDat"/> with <see cref="CraftingBenchSortCategoriesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchSortCategoriesDat>> GetManyToManyByName(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchSortCategoriesDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchSortCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchSortCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchSortCategoriesDat"/> with <see cref="CraftingBenchSortCategoriesDat.IsVisible"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsVisible(bool? key, out CraftingBenchSortCategoriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsVisible(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchSortCategoriesDat"/> with <see cref="CraftingBenchSortCategoriesDat.IsVisible"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsVisible(bool? key, out IReadOnlyList<CraftingBenchSortCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchSortCategoriesDat>();
            return false;
        }

        if (byIsVisible is null)
        {
            byIsVisible = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsVisible;

                if (!byIsVisible.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsVisible.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsVisible.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchSortCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchSortCategoriesDat"/> with <see cref="CraftingBenchSortCategoriesDat.byIsVisible"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CraftingBenchSortCategoriesDat>> GetManyToManyByIsVisible(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CraftingBenchSortCategoriesDat>>();
        }

        var items = new List<ResultItem<bool, CraftingBenchSortCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsVisible(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CraftingBenchSortCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CraftingBenchSortCategoriesDat[] Load()
    {
        const string filePath = "Data/CraftingBenchSortCategories.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CraftingBenchSortCategoriesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsVisible
            (var isvisibleLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CraftingBenchSortCategoriesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                IsVisible = isvisibleLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
