using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DelveResourcePerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class DelveResourcePerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DelveResourcePerLevelDat> Items { get; }

    private Dictionary<int, List<DelveResourcePerLevelDat>>? byAreaLevel;
    private Dictionary<int, List<DelveResourcePerLevelDat>>? bySulphite;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveResourcePerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DelveResourcePerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DelveResourcePerLevelDat"/> with <see cref="DelveResourcePerLevelDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAreaLevel(int? key, out DelveResourcePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAreaLevel(key, out var items))
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
    /// Tries to get <see cref="DelveResourcePerLevelDat"/> with <see cref="DelveResourcePerLevelDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAreaLevel(int? key, out IReadOnlyList<DelveResourcePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveResourcePerLevelDat>();
            return false;
        }

        if (byAreaLevel is null)
        {
            byAreaLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.AreaLevel;

                if (!byAreaLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAreaLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAreaLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveResourcePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveResourcePerLevelDat"/> with <see cref="DelveResourcePerLevelDat.byAreaLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveResourcePerLevelDat>> GetManyToManyByAreaLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveResourcePerLevelDat>>();
        }

        var items = new List<ResultItem<int, DelveResourcePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAreaLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveResourcePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveResourcePerLevelDat"/> with <see cref="DelveResourcePerLevelDat.Sulphite"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySulphite(int? key, out DelveResourcePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySulphite(key, out var items))
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
    /// Tries to get <see cref="DelveResourcePerLevelDat"/> with <see cref="DelveResourcePerLevelDat.Sulphite"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySulphite(int? key, out IReadOnlyList<DelveResourcePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveResourcePerLevelDat>();
            return false;
        }

        if (bySulphite is null)
        {
            bySulphite = new();
            foreach (var item in Items)
            {
                var itemKey = item.Sulphite;

                if (!bySulphite.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySulphite.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySulphite.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveResourcePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveResourcePerLevelDat"/> with <see cref="DelveResourcePerLevelDat.bySulphite"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveResourcePerLevelDat>> GetManyToManyBySulphite(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveResourcePerLevelDat>>();
        }

        var items = new List<ResultItem<int, DelveResourcePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySulphite(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveResourcePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DelveResourcePerLevelDat[] Load()
    {
        const string filePath = "Data/DelveResourcePerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveResourcePerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Sulphite
            (var sulphiteLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveResourcePerLevelDat()
            {
                AreaLevel = arealevelLoading,
                Sulphite = sulphiteLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
