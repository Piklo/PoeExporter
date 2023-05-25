using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestSeedItemsDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestSeedItemsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestSeedItemsDat> Items { get; }

    private Dictionary<int, List<HarvestSeedItemsDat>>? byId;
    private Dictionary<int, List<HarvestSeedItemsDat>>? byBaseItem;
    private Dictionary<int, List<HarvestSeedItemsDat>>? byDropStat;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestSeedItemsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestSeedItemsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedItemsDat"/> with <see cref="HarvestSeedItemsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(int? key, out HarvestSeedItemsDat? item)
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
    /// Tries to get <see cref="HarvestSeedItemsDat"/> with <see cref="HarvestSeedItemsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(int? key, out IReadOnlyList<HarvestSeedItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedItemsDat>();
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

        if (!byId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedItemsDat"/> with <see cref="HarvestSeedItemsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedItemsDat>> GetManyToManyById(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedItemsDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedItemsDat"/> with <see cref="HarvestSeedItemsDat.BaseItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItem(int? key, out HarvestSeedItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItem(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedItemsDat"/> with <see cref="HarvestSeedItemsDat.BaseItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItem(int? key, out IReadOnlyList<HarvestSeedItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedItemsDat>();
            return false;
        }

        if (byBaseItem is null)
        {
            byBaseItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedItemsDat"/> with <see cref="HarvestSeedItemsDat.byBaseItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedItemsDat>> GetManyToManyByBaseItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedItemsDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedItemsDat"/> with <see cref="HarvestSeedItemsDat.DropStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDropStat(int? key, out HarvestSeedItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDropStat(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedItemsDat"/> with <see cref="HarvestSeedItemsDat.DropStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDropStat(int? key, out IReadOnlyList<HarvestSeedItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedItemsDat>();
            return false;
        }

        if (byDropStat is null)
        {
            byDropStat = new();
            foreach (var item in Items)
            {
                var itemKey = item.DropStat;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDropStat.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDropStat.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDropStat.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedItemsDat"/> with <see cref="HarvestSeedItemsDat.byDropStat"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedItemsDat>> GetManyToManyByDropStat(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedItemsDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDropStat(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestSeedItemsDat[] Load()
    {
        const string filePath = "Data/HarvestSeedItems.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestSeedItemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseItem
            (var baseitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DropStat
            (var dropstatLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestSeedItemsDat()
            {
                Id = idLoading,
                BaseItem = baseitemLoading,
                DropStat = dropstatLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
