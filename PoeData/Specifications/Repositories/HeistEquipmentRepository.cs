using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistEquipmentDat"/> related data and helper methods.
/// </summary>
public sealed class HeistEquipmentRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistEquipmentDat> Items { get; }

    private Dictionary<int, List<HeistEquipmentDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<HeistEquipmentDat>>? byRequiredJob_HeistJobsKey;
    private Dictionary<int, List<HeistEquipmentDat>>? byRequiredLevel;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistEquipmentRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistEquipmentRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistEquipmentDat"/> with <see cref="HeistEquipmentDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out HeistEquipmentDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="HeistEquipmentDat"/> with <see cref="HeistEquipmentDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<HeistEquipmentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistEquipmentDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistEquipmentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistEquipmentDat"/> with <see cref="HeistEquipmentDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistEquipmentDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistEquipmentDat>>();
        }

        var items = new List<ResultItem<int, HeistEquipmentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistEquipmentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistEquipmentDat"/> with <see cref="HeistEquipmentDat.RequiredJob_HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRequiredJob_HeistJobsKey(int? key, out HeistEquipmentDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRequiredJob_HeistJobsKey(key, out var items))
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
    /// Tries to get <see cref="HeistEquipmentDat"/> with <see cref="HeistEquipmentDat.RequiredJob_HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRequiredJob_HeistJobsKey(int? key, out IReadOnlyList<HeistEquipmentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistEquipmentDat>();
            return false;
        }

        if (byRequiredJob_HeistJobsKey is null)
        {
            byRequiredJob_HeistJobsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.RequiredJob_HeistJobsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRequiredJob_HeistJobsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRequiredJob_HeistJobsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRequiredJob_HeistJobsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistEquipmentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistEquipmentDat"/> with <see cref="HeistEquipmentDat.byRequiredJob_HeistJobsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistEquipmentDat>> GetManyToManyByRequiredJob_HeistJobsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistEquipmentDat>>();
        }

        var items = new List<ResultItem<int, HeistEquipmentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRequiredJob_HeistJobsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistEquipmentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistEquipmentDat"/> with <see cref="HeistEquipmentDat.RequiredLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRequiredLevel(int? key, out HeistEquipmentDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRequiredLevel(key, out var items))
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
    /// Tries to get <see cref="HeistEquipmentDat"/> with <see cref="HeistEquipmentDat.RequiredLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRequiredLevel(int? key, out IReadOnlyList<HeistEquipmentDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistEquipmentDat>();
            return false;
        }

        if (byRequiredLevel is null)
        {
            byRequiredLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.RequiredLevel;

                if (!byRequiredLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRequiredLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRequiredLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistEquipmentDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistEquipmentDat"/> with <see cref="HeistEquipmentDat.byRequiredLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistEquipmentDat>> GetManyToManyByRequiredLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistEquipmentDat>>();
        }

        var items = new List<ResultItem<int, HeistEquipmentDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRequiredLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistEquipmentDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistEquipmentDat[] Load()
    {
        const string filePath = "Data/HeistEquipment.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistEquipmentDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading RequiredJob_HeistJobsKey
            (var requiredjob_heistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading RequiredLevel
            (var requiredlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistEquipmentDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                RequiredJob_HeistJobsKey = requiredjob_heistjobskeyLoading,
                RequiredLevel = requiredlevelLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
