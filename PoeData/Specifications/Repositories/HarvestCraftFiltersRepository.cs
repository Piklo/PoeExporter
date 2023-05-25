using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestCraftFiltersDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestCraftFiltersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestCraftFiltersDat> Items { get; }

    private Dictionary<string, List<HarvestCraftFiltersDat>>? byId;
    private Dictionary<int, List<HarvestCraftFiltersDat>>? byBaseItem;
    private Dictionary<string, List<HarvestCraftFiltersDat>>? byName;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestCraftFiltersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestCraftFiltersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftFiltersDat"/> with <see cref="HarvestCraftFiltersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HarvestCraftFiltersDat? item)
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
    /// Tries to get <see cref="HarvestCraftFiltersDat"/> with <see cref="HarvestCraftFiltersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HarvestCraftFiltersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftFiltersDat>();
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
            items = Array.Empty<HarvestCraftFiltersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftFiltersDat"/> with <see cref="HarvestCraftFiltersDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftFiltersDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftFiltersDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftFiltersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftFiltersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftFiltersDat"/> with <see cref="HarvestCraftFiltersDat.BaseItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItem(int? key, out HarvestCraftFiltersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItem(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftFiltersDat"/> with <see cref="HarvestCraftFiltersDat.BaseItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItem(int? key, out IReadOnlyList<HarvestCraftFiltersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftFiltersDat>();
            return false;
        }

        if (byBaseItem is null)
        {
            byBaseItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestCraftFiltersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftFiltersDat"/> with <see cref="HarvestCraftFiltersDat.byBaseItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestCraftFiltersDat>> GetManyToManyByBaseItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestCraftFiltersDat>>();
        }

        var items = new List<ResultItem<int, HarvestCraftFiltersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestCraftFiltersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftFiltersDat"/> with <see cref="HarvestCraftFiltersDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out HarvestCraftFiltersDat? item)
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
    /// Tries to get <see cref="HarvestCraftFiltersDat"/> with <see cref="HarvestCraftFiltersDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<HarvestCraftFiltersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftFiltersDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;

                if (!byName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HarvestCraftFiltersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftFiltersDat"/> with <see cref="HarvestCraftFiltersDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftFiltersDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftFiltersDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftFiltersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftFiltersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestCraftFiltersDat[] Load()
    {
        const string filePath = "Data/HarvestCraftFilters.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestCraftFiltersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItem
            (var baseitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestCraftFiltersDat()
            {
                Id = idLoading,
                BaseItem = baseitemLoading,
                Name = nameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
