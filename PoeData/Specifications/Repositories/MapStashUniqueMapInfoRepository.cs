using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapStashUniqueMapInfoDat"/> related data and helper methods.
/// </summary>
public sealed class MapStashUniqueMapInfoRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapStashUniqueMapInfoDat> Items { get; }

    private Dictionary<int, List<MapStashUniqueMapInfoDat>>? byUniqueMap;
    private Dictionary<int, List<MapStashUniqueMapInfoDat>>? byBaseItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapStashUniqueMapInfoRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapStashUniqueMapInfoRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapStashUniqueMapInfoDat"/> with <see cref="MapStashUniqueMapInfoDat.UniqueMap"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUniqueMap(int? key, out MapStashUniqueMapInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUniqueMap(key, out var items))
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
    /// Tries to get <see cref="MapStashUniqueMapInfoDat"/> with <see cref="MapStashUniqueMapInfoDat.UniqueMap"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUniqueMap(int? key, out IReadOnlyList<MapStashUniqueMapInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashUniqueMapInfoDat>();
            return false;
        }

        if (byUniqueMap is null)
        {
            byUniqueMap = new();
            foreach (var item in Items)
            {
                var itemKey = item.UniqueMap;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUniqueMap.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUniqueMap.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUniqueMap.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapStashUniqueMapInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashUniqueMapInfoDat"/> with <see cref="MapStashUniqueMapInfoDat.byUniqueMap"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapStashUniqueMapInfoDat>> GetManyToManyByUniqueMap(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapStashUniqueMapInfoDat>>();
        }

        var items = new List<ResultItem<int, MapStashUniqueMapInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUniqueMap(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapStashUniqueMapInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashUniqueMapInfoDat"/> with <see cref="MapStashUniqueMapInfoDat.BaseItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItem(int? key, out MapStashUniqueMapInfoDat? item)
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
    /// Tries to get <see cref="MapStashUniqueMapInfoDat"/> with <see cref="MapStashUniqueMapInfoDat.BaseItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItem(int? key, out IReadOnlyList<MapStashUniqueMapInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashUniqueMapInfoDat>();
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
            items = Array.Empty<MapStashUniqueMapInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashUniqueMapInfoDat"/> with <see cref="MapStashUniqueMapInfoDat.byBaseItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapStashUniqueMapInfoDat>> GetManyToManyByBaseItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapStashUniqueMapInfoDat>>();
        }

        var items = new List<ResultItem<int, MapStashUniqueMapInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapStashUniqueMapInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapStashUniqueMapInfoDat[] Load()
    {
        const string filePath = "Data/MapStashUniqueMapInfo.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapStashUniqueMapInfoDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading UniqueMap
            (var uniquemapLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BaseItem
            (var baseitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapStashUniqueMapInfoDat()
            {
                UniqueMap = uniquemapLoading,
                BaseItem = baseitemLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
