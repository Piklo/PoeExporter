using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UniqueJewelLimitsDat"/> related data and helper methods.
/// </summary>
public sealed class UniqueJewelLimitsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UniqueJewelLimitsDat> Items { get; }

    private Dictionary<int, List<UniqueJewelLimitsDat>>? byJewelName;
    private Dictionary<int, List<UniqueJewelLimitsDat>>? byLimit;

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueJewelLimitsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UniqueJewelLimitsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UniqueJewelLimitsDat"/> with <see cref="UniqueJewelLimitsDat.JewelName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByJewelName(int? key, out UniqueJewelLimitsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByJewelName(key, out var items))
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
    /// Tries to get <see cref="UniqueJewelLimitsDat"/> with <see cref="UniqueJewelLimitsDat.JewelName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByJewelName(int? key, out IReadOnlyList<UniqueJewelLimitsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueJewelLimitsDat>();
            return false;
        }

        if (byJewelName is null)
        {
            byJewelName = new();
            foreach (var item in Items)
            {
                var itemKey = item.JewelName;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byJewelName.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byJewelName.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byJewelName.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueJewelLimitsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueJewelLimitsDat"/> with <see cref="UniqueJewelLimitsDat.byJewelName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueJewelLimitsDat>> GetManyToManyByJewelName(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueJewelLimitsDat>>();
        }

        var items = new List<ResultItem<int, UniqueJewelLimitsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByJewelName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueJewelLimitsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueJewelLimitsDat"/> with <see cref="UniqueJewelLimitsDat.Limit"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLimit(int? key, out UniqueJewelLimitsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLimit(key, out var items))
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
    /// Tries to get <see cref="UniqueJewelLimitsDat"/> with <see cref="UniqueJewelLimitsDat.Limit"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLimit(int? key, out IReadOnlyList<UniqueJewelLimitsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueJewelLimitsDat>();
            return false;
        }

        if (byLimit is null)
        {
            byLimit = new();
            foreach (var item in Items)
            {
                var itemKey = item.Limit;

                if (!byLimit.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLimit.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLimit.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueJewelLimitsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueJewelLimitsDat"/> with <see cref="UniqueJewelLimitsDat.byLimit"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueJewelLimitsDat>> GetManyToManyByLimit(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueJewelLimitsDat>>();
        }

        var items = new List<ResultItem<int, UniqueJewelLimitsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLimit(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueJewelLimitsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UniqueJewelLimitsDat[] Load()
    {
        const string filePath = "Data/UniqueJewelLimits.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueJewelLimitsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading JewelName
            (var jewelnameLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Limit
            (var limitLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueJewelLimitsDat()
            {
                JewelName = jewelnameLoading,
                Limit = limitLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
