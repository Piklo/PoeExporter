using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DelveCatchupDepthsDat"/> related data and helper methods.
/// </summary>
public sealed class DelveCatchupDepthsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DelveCatchupDepthsDat> Items { get; }

    private Dictionary<int, List<DelveCatchupDepthsDat>>? byLevel;
    private Dictionary<int, List<DelveCatchupDepthsDat>>? byDepth;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveCatchupDepthsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DelveCatchupDepthsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DelveCatchupDepthsDat"/> with <see cref="DelveCatchupDepthsDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out DelveCatchupDepthsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevel(key, out var items))
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
    /// Tries to get <see cref="DelveCatchupDepthsDat"/> with <see cref="DelveCatchupDepthsDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<DelveCatchupDepthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCatchupDepthsDat>();
            return false;
        }

        if (byLevel is null)
        {
            byLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.Level;

                if (!byLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCatchupDepthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCatchupDepthsDat"/> with <see cref="DelveCatchupDepthsDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCatchupDepthsDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCatchupDepthsDat>>();
        }

        var items = new List<ResultItem<int, DelveCatchupDepthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCatchupDepthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCatchupDepthsDat"/> with <see cref="DelveCatchupDepthsDat.Depth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDepth(int? key, out DelveCatchupDepthsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDepth(key, out var items))
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
    /// Tries to get <see cref="DelveCatchupDepthsDat"/> with <see cref="DelveCatchupDepthsDat.Depth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDepth(int? key, out IReadOnlyList<DelveCatchupDepthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCatchupDepthsDat>();
            return false;
        }

        if (byDepth is null)
        {
            byDepth = new();
            foreach (var item in Items)
            {
                var itemKey = item.Depth;

                if (!byDepth.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDepth.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDepth.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCatchupDepthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCatchupDepthsDat"/> with <see cref="DelveCatchupDepthsDat.byDepth"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCatchupDepthsDat>> GetManyToManyByDepth(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCatchupDepthsDat>>();
        }

        var items = new List<ResultItem<int, DelveCatchupDepthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDepth(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCatchupDepthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DelveCatchupDepthsDat[] Load()
    {
        const string filePath = "Data/DelveCatchupDepths.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveCatchupDepthsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Depth
            (var depthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveCatchupDepthsDat()
            {
                Level = levelLoading,
                Depth = depthLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
