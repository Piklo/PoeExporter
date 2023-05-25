using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DelveCraftingTagsDat"/> related data and helper methods.
/// </summary>
public sealed class DelveCraftingTagsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DelveCraftingTagsDat> Items { get; }

    private Dictionary<int, List<DelveCraftingTagsDat>>? byTagsKey;
    private Dictionary<string, List<DelveCraftingTagsDat>>? byItemClass;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveCraftingTagsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DelveCraftingTagsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingTagsDat"/> with <see cref="DelveCraftingTagsDat.TagsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTagsKey(int? key, out DelveCraftingTagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTagsKey(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingTagsDat"/> with <see cref="DelveCraftingTagsDat.TagsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTagsKey(int? key, out IReadOnlyList<DelveCraftingTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingTagsDat>();
            return false;
        }

        if (byTagsKey is null)
        {
            byTagsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.TagsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTagsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTagsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTagsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingTagsDat"/> with <see cref="DelveCraftingTagsDat.byTagsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingTagsDat>> GetManyToManyByTagsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingTagsDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTagsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingTagsDat"/> with <see cref="DelveCraftingTagsDat.ItemClass"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClass(string? key, out DelveCraftingTagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemClass(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingTagsDat"/> with <see cref="DelveCraftingTagsDat.ItemClass"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClass(string? key, out IReadOnlyList<DelveCraftingTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingTagsDat>();
            return false;
        }

        if (byItemClass is null)
        {
            byItemClass = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemClass;

                if (!byItemClass.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byItemClass.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byItemClass.TryGetValue(key, out var temp))
        {
            items = Array.Empty<DelveCraftingTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingTagsDat"/> with <see cref="DelveCraftingTagsDat.byItemClass"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DelveCraftingTagsDat>> GetManyToManyByItemClass(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DelveCraftingTagsDat>>();
        }

        var items = new List<ResultItem<string, DelveCraftingTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClass(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DelveCraftingTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DelveCraftingTagsDat[] Load()
    {
        const string filePath = "Data/DelveCraftingTags.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveCraftingTagsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading TagsKey
            (var tagskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemClass
            (var itemclassLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveCraftingTagsDat()
            {
                TagsKey = tagskeyLoading,
                ItemClass = itemclassLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
