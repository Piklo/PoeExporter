using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SurgeTypesDat"/> related data and helper methods.
/// </summary>
public sealed class SurgeTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SurgeTypesDat> Items { get; }

    private Dictionary<string, List<SurgeTypesDat>>? byId;
    private Dictionary<int, List<SurgeTypesDat>>? bySurgeEffects;
    private Dictionary<int, List<SurgeTypesDat>>? byIntId;

    /// <summary>
    /// Initializes a new instance of the <see cref="SurgeTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SurgeTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SurgeTypesDat"/> with <see cref="SurgeTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SurgeTypesDat? item)
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
    /// Tries to get <see cref="SurgeTypesDat"/> with <see cref="SurgeTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SurgeTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SurgeTypesDat>();
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
            items = Array.Empty<SurgeTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SurgeTypesDat"/> with <see cref="SurgeTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SurgeTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SurgeTypesDat>>();
        }

        var items = new List<ResultItem<string, SurgeTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SurgeTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SurgeTypesDat"/> with <see cref="SurgeTypesDat.SurgeEffects"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySurgeEffects(int? key, out SurgeTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySurgeEffects(key, out var items))
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
    /// Tries to get <see cref="SurgeTypesDat"/> with <see cref="SurgeTypesDat.SurgeEffects"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySurgeEffects(int? key, out IReadOnlyList<SurgeTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SurgeTypesDat>();
            return false;
        }

        if (bySurgeEffects is null)
        {
            bySurgeEffects = new();
            foreach (var item in Items)
            {
                var itemKey = item.SurgeEffects;
                foreach (var listKey in itemKey)
                {
                    if (!bySurgeEffects.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySurgeEffects.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySurgeEffects.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SurgeTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SurgeTypesDat"/> with <see cref="SurgeTypesDat.bySurgeEffects"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SurgeTypesDat>> GetManyToManyBySurgeEffects(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SurgeTypesDat>>();
        }

        var items = new List<ResultItem<int, SurgeTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySurgeEffects(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SurgeTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SurgeTypesDat"/> with <see cref="SurgeTypesDat.IntId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIntId(int? key, out SurgeTypesDat? item)
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
    /// Tries to get <see cref="SurgeTypesDat"/> with <see cref="SurgeTypesDat.IntId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIntId(int? key, out IReadOnlyList<SurgeTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SurgeTypesDat>();
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
            items = Array.Empty<SurgeTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SurgeTypesDat"/> with <see cref="SurgeTypesDat.byIntId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SurgeTypesDat>> GetManyToManyByIntId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SurgeTypesDat>>();
        }

        var items = new List<ResultItem<int, SurgeTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIntId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SurgeTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SurgeTypesDat[] Load()
    {
        const string filePath = "Data/SurgeTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SurgeTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SurgeEffects
            (var tempsurgeeffectsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var surgeeffectsLoading = tempsurgeeffectsLoading.AsReadOnly();

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SurgeTypesDat()
            {
                Id = idLoading,
                SurgeEffects = surgeeffectsLoading,
                IntId = intidLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
