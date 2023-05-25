using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestDurabilityDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestDurabilityRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestDurabilityDat> Items { get; }

    private Dictionary<int, List<HarvestDurabilityDat>>? byHarvestObjectsKey;
    private Dictionary<int, List<HarvestDurabilityDat>>? byDurability;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestDurabilityRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestDurabilityRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestDurabilityDat"/> with <see cref="HarvestDurabilityDat.HarvestObjectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHarvestObjectsKey(int? key, out HarvestDurabilityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHarvestObjectsKey(key, out var items))
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
    /// Tries to get <see cref="HarvestDurabilityDat"/> with <see cref="HarvestDurabilityDat.HarvestObjectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHarvestObjectsKey(int? key, out IReadOnlyList<HarvestDurabilityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestDurabilityDat>();
            return false;
        }

        if (byHarvestObjectsKey is null)
        {
            byHarvestObjectsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HarvestObjectsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHarvestObjectsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHarvestObjectsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHarvestObjectsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestDurabilityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestDurabilityDat"/> with <see cref="HarvestDurabilityDat.byHarvestObjectsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestDurabilityDat>> GetManyToManyByHarvestObjectsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestDurabilityDat>>();
        }

        var items = new List<ResultItem<int, HarvestDurabilityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHarvestObjectsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestDurabilityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestDurabilityDat"/> with <see cref="HarvestDurabilityDat.Durability"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDurability(int? key, out HarvestDurabilityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDurability(key, out var items))
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
    /// Tries to get <see cref="HarvestDurabilityDat"/> with <see cref="HarvestDurabilityDat.Durability"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDurability(int? key, out IReadOnlyList<HarvestDurabilityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestDurabilityDat>();
            return false;
        }

        if (byDurability is null)
        {
            byDurability = new();
            foreach (var item in Items)
            {
                var itemKey = item.Durability;

                if (!byDurability.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDurability.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDurability.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestDurabilityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestDurabilityDat"/> with <see cref="HarvestDurabilityDat.byDurability"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestDurabilityDat>> GetManyToManyByDurability(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestDurabilityDat>>();
        }

        var items = new List<ResultItem<int, HarvestDurabilityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDurability(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestDurabilityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestDurabilityDat[] Load()
    {
        const string filePath = "Data/HarvestDurability.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestDurabilityDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HarvestObjectsKey
            (var harvestobjectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Durability
            (var durabilityLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestDurabilityDat()
            {
                HarvestObjectsKey = harvestobjectskeyLoading,
                Durability = durabilityLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
