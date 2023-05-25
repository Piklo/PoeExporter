using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="WarbandsMapGraphDat"/> related data and helper methods.
/// </summary>
public sealed class WarbandsMapGraphRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<WarbandsMapGraphDat> Items { get; }

    private Dictionary<int, List<WarbandsMapGraphDat>>? byWorldAreasKey;
    private Dictionary<int, List<WarbandsMapGraphDat>>? byConnections;

    /// <summary>
    /// Initializes a new instance of the <see cref="WarbandsMapGraphRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal WarbandsMapGraphRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsMapGraphDat"/> with <see cref="WarbandsMapGraphDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out WarbandsMapGraphDat? item)
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
    /// Tries to get <see cref="WarbandsMapGraphDat"/> with <see cref="WarbandsMapGraphDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<WarbandsMapGraphDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsMapGraphDat>();
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
            items = Array.Empty<WarbandsMapGraphDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsMapGraphDat"/> with <see cref="WarbandsMapGraphDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsMapGraphDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsMapGraphDat>>();
        }

        var items = new List<ResultItem<int, WarbandsMapGraphDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsMapGraphDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsMapGraphDat"/> with <see cref="WarbandsMapGraphDat.Connections"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByConnections(int? key, out WarbandsMapGraphDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByConnections(key, out var items))
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
    /// Tries to get <see cref="WarbandsMapGraphDat"/> with <see cref="WarbandsMapGraphDat.Connections"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByConnections(int? key, out IReadOnlyList<WarbandsMapGraphDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsMapGraphDat>();
            return false;
        }

        if (byConnections is null)
        {
            byConnections = new();
            foreach (var item in Items)
            {
                var itemKey = item.Connections;
                foreach (var listKey in itemKey)
                {
                    if (!byConnections.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byConnections.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byConnections.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsMapGraphDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsMapGraphDat"/> with <see cref="WarbandsMapGraphDat.byConnections"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsMapGraphDat>> GetManyToManyByConnections(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsMapGraphDat>>();
        }

        var items = new List<ResultItem<int, WarbandsMapGraphDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByConnections(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsMapGraphDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private WarbandsMapGraphDat[] Load()
    {
        const string filePath = "Data/WarbandsMapGraph.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WarbandsMapGraphDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Connections
            (var tempconnectionsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var connectionsLoading = tempconnectionsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WarbandsMapGraphDat()
            {
                WorldAreasKey = worldareaskeyLoading,
                Connections = connectionsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
