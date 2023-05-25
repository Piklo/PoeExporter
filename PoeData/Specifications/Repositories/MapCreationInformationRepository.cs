using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapCreationInformationDat"/> related data and helper methods.
/// </summary>
public sealed class MapCreationInformationRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapCreationInformationDat> Items { get; }

    private Dictionary<int, List<MapCreationInformationDat>>? byMapsKey;
    private Dictionary<int, List<MapCreationInformationDat>>? byTier;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapCreationInformationRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapCreationInformationRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapCreationInformationDat"/> with <see cref="MapCreationInformationDat.MapsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapsKey(int? key, out MapCreationInformationDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapsKey(key, out var items))
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
    /// Tries to get <see cref="MapCreationInformationDat"/> with <see cref="MapCreationInformationDat.MapsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapsKey(int? key, out IReadOnlyList<MapCreationInformationDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapCreationInformationDat>();
            return false;
        }

        if (byMapsKey is null)
        {
            byMapsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMapsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMapsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMapsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapCreationInformationDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapCreationInformationDat"/> with <see cref="MapCreationInformationDat.byMapsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapCreationInformationDat>> GetManyToManyByMapsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapCreationInformationDat>>();
        }

        var items = new List<ResultItem<int, MapCreationInformationDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapCreationInformationDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapCreationInformationDat"/> with <see cref="MapCreationInformationDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out MapCreationInformationDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier(key, out var items))
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
    /// Tries to get <see cref="MapCreationInformationDat"/> with <see cref="MapCreationInformationDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<MapCreationInformationDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapCreationInformationDat>();
            return false;
        }

        if (byTier is null)
        {
            byTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier;

                if (!byTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapCreationInformationDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapCreationInformationDat"/> with <see cref="MapCreationInformationDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapCreationInformationDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapCreationInformationDat>>();
        }

        var items = new List<ResultItem<int, MapCreationInformationDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapCreationInformationDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapCreationInformationDat[] Load()
    {
        const string filePath = "Data/MapCreationInformation.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapCreationInformationDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MapsKey
            (var mapskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapCreationInformationDat()
            {
                MapsKey = mapskeyLoading,
                Tier = tierLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
