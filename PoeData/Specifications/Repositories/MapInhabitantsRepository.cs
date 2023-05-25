using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapInhabitantsDat"/> related data and helper methods.
/// </summary>
public sealed class MapInhabitantsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapInhabitantsDat> Items { get; }

    private Dictionary<int, List<MapInhabitantsDat>>? byStatsKey;
    private Dictionary<int, List<MapInhabitantsDat>>? byMonsterPacksKeys;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapInhabitantsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapInhabitantsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapInhabitantsDat"/> with <see cref="MapInhabitantsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey(int? key, out MapInhabitantsDat? item)
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
    /// Tries to get <see cref="MapInhabitantsDat"/> with <see cref="MapInhabitantsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey(int? key, out IReadOnlyList<MapInhabitantsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapInhabitantsDat>();
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
            items = Array.Empty<MapInhabitantsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapInhabitantsDat"/> with <see cref="MapInhabitantsDat.byStatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapInhabitantsDat>> GetManyToManyByStatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapInhabitantsDat>>();
        }

        var items = new List<ResultItem<int, MapInhabitantsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapInhabitantsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapInhabitantsDat"/> with <see cref="MapInhabitantsDat.MonsterPacksKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterPacksKeys(int? key, out MapInhabitantsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterPacksKeys(key, out var items))
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
    /// Tries to get <see cref="MapInhabitantsDat"/> with <see cref="MapInhabitantsDat.MonsterPacksKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterPacksKeys(int? key, out IReadOnlyList<MapInhabitantsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapInhabitantsDat>();
            return false;
        }

        if (byMonsterPacksKeys is null)
        {
            byMonsterPacksKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterPacksKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsterPacksKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsterPacksKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsterPacksKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapInhabitantsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapInhabitantsDat"/> with <see cref="MapInhabitantsDat.byMonsterPacksKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapInhabitantsDat>> GetManyToManyByMonsterPacksKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapInhabitantsDat>>();
        }

        var items = new List<ResultItem<int, MapInhabitantsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterPacksKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapInhabitantsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapInhabitantsDat[] Load()
    {
        const string filePath = "Data/MapInhabitants.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapInhabitantsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MonsterPacksKeys
            (var tempmonsterpackskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterpackskeysLoading = tempmonsterpackskeysLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapInhabitantsDat()
            {
                StatsKey = statskeyLoading,
                MonsterPacksKeys = monsterpackskeysLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
