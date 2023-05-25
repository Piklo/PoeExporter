using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SpawnAdditionalChestsOrClustersDat"/> related data and helper methods.
/// </summary>
public sealed class SpawnAdditionalChestsOrClustersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SpawnAdditionalChestsOrClustersDat> Items { get; }

    private Dictionary<int, List<SpawnAdditionalChestsOrClustersDat>>? byStatsKey;
    private Dictionary<int, List<SpawnAdditionalChestsOrClustersDat>>? byChestsKey;
    private Dictionary<int, List<SpawnAdditionalChestsOrClustersDat>>? byChestClustersKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpawnAdditionalChestsOrClustersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SpawnAdditionalChestsOrClustersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SpawnAdditionalChestsOrClustersDat"/> with <see cref="SpawnAdditionalChestsOrClustersDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey(int? key, out SpawnAdditionalChestsOrClustersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey(key, out var items))
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
    /// Tries to get <see cref="SpawnAdditionalChestsOrClustersDat"/> with <see cref="SpawnAdditionalChestsOrClustersDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey(int? key, out IReadOnlyList<SpawnAdditionalChestsOrClustersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SpawnAdditionalChestsOrClustersDat>();
            return false;
        }

        if (byStatsKey is null)
        {
            byStatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SpawnAdditionalChestsOrClustersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SpawnAdditionalChestsOrClustersDat"/> with <see cref="SpawnAdditionalChestsOrClustersDat.byStatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SpawnAdditionalChestsOrClustersDat>> GetManyToManyByStatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SpawnAdditionalChestsOrClustersDat>>();
        }

        var items = new List<ResultItem<int, SpawnAdditionalChestsOrClustersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SpawnAdditionalChestsOrClustersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SpawnAdditionalChestsOrClustersDat"/> with <see cref="SpawnAdditionalChestsOrClustersDat.ChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChestsKey(int? key, out SpawnAdditionalChestsOrClustersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChestsKey(key, out var items))
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
    /// Tries to get <see cref="SpawnAdditionalChestsOrClustersDat"/> with <see cref="SpawnAdditionalChestsOrClustersDat.ChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChestsKey(int? key, out IReadOnlyList<SpawnAdditionalChestsOrClustersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SpawnAdditionalChestsOrClustersDat>();
            return false;
        }

        if (byChestsKey is null)
        {
            byChestsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChestsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byChestsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byChestsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byChestsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SpawnAdditionalChestsOrClustersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SpawnAdditionalChestsOrClustersDat"/> with <see cref="SpawnAdditionalChestsOrClustersDat.byChestsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SpawnAdditionalChestsOrClustersDat>> GetManyToManyByChestsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SpawnAdditionalChestsOrClustersDat>>();
        }

        var items = new List<ResultItem<int, SpawnAdditionalChestsOrClustersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChestsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SpawnAdditionalChestsOrClustersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SpawnAdditionalChestsOrClustersDat"/> with <see cref="SpawnAdditionalChestsOrClustersDat.ChestClustersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChestClustersKey(int? key, out SpawnAdditionalChestsOrClustersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChestClustersKey(key, out var items))
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
    /// Tries to get <see cref="SpawnAdditionalChestsOrClustersDat"/> with <see cref="SpawnAdditionalChestsOrClustersDat.ChestClustersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChestClustersKey(int? key, out IReadOnlyList<SpawnAdditionalChestsOrClustersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SpawnAdditionalChestsOrClustersDat>();
            return false;
        }

        if (byChestClustersKey is null)
        {
            byChestClustersKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChestClustersKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byChestClustersKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byChestClustersKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byChestClustersKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SpawnAdditionalChestsOrClustersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SpawnAdditionalChestsOrClustersDat"/> with <see cref="SpawnAdditionalChestsOrClustersDat.byChestClustersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SpawnAdditionalChestsOrClustersDat>> GetManyToManyByChestClustersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SpawnAdditionalChestsOrClustersDat>>();
        }

        var items = new List<ResultItem<int, SpawnAdditionalChestsOrClustersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChestClustersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SpawnAdditionalChestsOrClustersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SpawnAdditionalChestsOrClustersDat[] Load()
    {
        const string filePath = "Data/SpawnAdditionalChestsOrClusters.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SpawnAdditionalChestsOrClustersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ChestClustersKey
            (var chestclusterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SpawnAdditionalChestsOrClustersDat()
            {
                StatsKey = statskeyLoading,
                ChestsKey = chestskeyLoading,
                ChestClustersKey = chestclusterskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
