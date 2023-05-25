using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AdditionalLifeScalingDat"/> related data and helper methods.
/// </summary>
public sealed class AdditionalLifeScalingRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AdditionalLifeScalingDat> Items { get; }

    private Dictionary<int, List<AdditionalLifeScalingDat>>? byIntId;
    private Dictionary<string, List<AdditionalLifeScalingDat>>? byID;
    private Dictionary<string, List<AdditionalLifeScalingDat>>? byDatFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdditionalLifeScalingRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AdditionalLifeScalingRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalLifeScalingDat"/> with <see cref="AdditionalLifeScalingDat.IntId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIntId(int? key, out AdditionalLifeScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIntId(key, out var items))
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
    /// Tries to get <see cref="AdditionalLifeScalingDat"/> with <see cref="AdditionalLifeScalingDat.IntId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIntId(int? key, out IReadOnlyList<AdditionalLifeScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalLifeScalingDat>();
            return false;
        }

        if (byIntId is null)
        {
            byIntId = new();
            foreach (var item in Items)
            {
                var itemKey = item.IntId;

                if (!byIntId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIntId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIntId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdditionalLifeScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalLifeScalingDat"/> with <see cref="AdditionalLifeScalingDat.byIntId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdditionalLifeScalingDat>> GetManyToManyByIntId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdditionalLifeScalingDat>>();
        }

        var items = new List<ResultItem<int, AdditionalLifeScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIntId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdditionalLifeScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalLifeScalingDat"/> with <see cref="AdditionalLifeScalingDat.ID"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByID(string? key, out AdditionalLifeScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByID(key, out var items))
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
    /// Tries to get <see cref="AdditionalLifeScalingDat"/> with <see cref="AdditionalLifeScalingDat.ID"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByID(string? key, out IReadOnlyList<AdditionalLifeScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalLifeScalingDat>();
            return false;
        }

        if (byID is null)
        {
            byID = new();
            foreach (var item in Items)
            {
                var itemKey = item.ID;

                if (!byID.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byID.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byID.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AdditionalLifeScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalLifeScalingDat"/> with <see cref="AdditionalLifeScalingDat.byID"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AdditionalLifeScalingDat>> GetManyToManyByID(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AdditionalLifeScalingDat>>();
        }

        var items = new List<ResultItem<string, AdditionalLifeScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByID(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AdditionalLifeScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalLifeScalingDat"/> with <see cref="AdditionalLifeScalingDat.DatFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDatFile(string? key, out AdditionalLifeScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDatFile(key, out var items))
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
    /// Tries to get <see cref="AdditionalLifeScalingDat"/> with <see cref="AdditionalLifeScalingDat.DatFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDatFile(string? key, out IReadOnlyList<AdditionalLifeScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalLifeScalingDat>();
            return false;
        }

        if (byDatFile is null)
        {
            byDatFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.DatFile;

                if (!byDatFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDatFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDatFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AdditionalLifeScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalLifeScalingDat"/> with <see cref="AdditionalLifeScalingDat.byDatFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AdditionalLifeScalingDat>> GetManyToManyByDatFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AdditionalLifeScalingDat>>();
        }

        var items = new List<ResultItem<string, AdditionalLifeScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDatFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AdditionalLifeScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AdditionalLifeScalingDat[] Load()
    {
        const string filePath = "Data/AdditionalLifeScaling.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AdditionalLifeScalingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ID
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DatFile
            (var datfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AdditionalLifeScalingDat()
            {
                IntId = intidLoading,
                ID = idLoading,
                DatFile = datfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
