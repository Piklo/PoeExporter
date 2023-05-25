using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasExileInfluenceDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasExileInfluenceRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasExileInfluenceDat> Items { get; }

    private Dictionary<int, List<AtlasExileInfluenceDat>>? byConqueror;
    private Dictionary<int, List<AtlasExileInfluenceDat>>? bySets;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasExileInfluenceRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasExileInfluenceRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExileInfluenceDat"/> with <see cref="AtlasExileInfluenceDat.Conqueror"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByConqueror(int? key, out AtlasExileInfluenceDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByConqueror(key, out var items))
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
    /// Tries to get <see cref="AtlasExileInfluenceDat"/> with <see cref="AtlasExileInfluenceDat.Conqueror"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByConqueror(int? key, out IReadOnlyList<AtlasExileInfluenceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasExileInfluenceDat>();
            return false;
        }

        if (byConqueror is null)
        {
            byConqueror = new();
            foreach (var item in Items)
            {
                var itemKey = item.Conqueror;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byConqueror.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byConqueror.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byConqueror.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasExileInfluenceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExileInfluenceDat"/> with <see cref="AtlasExileInfluenceDat.byConqueror"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasExileInfluenceDat>> GetManyToManyByConqueror(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasExileInfluenceDat>>();
        }

        var items = new List<ResultItem<int, AtlasExileInfluenceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByConqueror(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasExileInfluenceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExileInfluenceDat"/> with <see cref="AtlasExileInfluenceDat.Sets"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySets(int? key, out AtlasExileInfluenceDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySets(key, out var items))
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
    /// Tries to get <see cref="AtlasExileInfluenceDat"/> with <see cref="AtlasExileInfluenceDat.Sets"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySets(int? key, out IReadOnlyList<AtlasExileInfluenceDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasExileInfluenceDat>();
            return false;
        }

        if (bySets is null)
        {
            bySets = new();
            foreach (var item in Items)
            {
                var itemKey = item.Sets;
                foreach (var listKey in itemKey)
                {
                    if (!bySets.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySets.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySets.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasExileInfluenceDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExileInfluenceDat"/> with <see cref="AtlasExileInfluenceDat.bySets"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasExileInfluenceDat>> GetManyToManyBySets(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasExileInfluenceDat>>();
        }

        var items = new List<ResultItem<int, AtlasExileInfluenceDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySets(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasExileInfluenceDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasExileInfluenceDat[] Load()
    {
        const string filePath = "Data/AtlasExileInfluence.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasExileInfluenceDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Conqueror
            (var conquerorLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Sets
            (var tempsetsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var setsLoading = tempsetsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasExileInfluenceDat()
            {
                Conqueror = conquerorLoading,
                Sets = setsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
