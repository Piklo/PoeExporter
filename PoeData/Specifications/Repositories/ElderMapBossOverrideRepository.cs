using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ElderMapBossOverrideDat"/> related data and helper methods.
/// </summary>
public sealed class ElderMapBossOverrideRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ElderMapBossOverrideDat> Items { get; }

    private Dictionary<int, List<ElderMapBossOverrideDat>>? byWorldAreasKey;
    private Dictionary<int, List<ElderMapBossOverrideDat>>? byMonsterVarietiesKeys;
    private Dictionary<string, List<ElderMapBossOverrideDat>>? byTerrainMetadata;

    /// <summary>
    /// Initializes a new instance of the <see cref="ElderMapBossOverrideRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ElderMapBossOverrideRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ElderMapBossOverrideDat"/> with <see cref="ElderMapBossOverrideDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out ElderMapBossOverrideDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="ElderMapBossOverrideDat"/> with <see cref="ElderMapBossOverrideDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<ElderMapBossOverrideDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ElderMapBossOverrideDat>();
            return false;
        }

        if (byWorldAreasKey is null)
        {
            byWorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ElderMapBossOverrideDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ElderMapBossOverrideDat"/> with <see cref="ElderMapBossOverrideDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ElderMapBossOverrideDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ElderMapBossOverrideDat>>();
        }

        var items = new List<ResultItem<int, ElderMapBossOverrideDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ElderMapBossOverrideDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ElderMapBossOverrideDat"/> with <see cref="ElderMapBossOverrideDat.MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKeys(int? key, out ElderMapBossOverrideDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKeys(key, out var items))
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
    /// Tries to get <see cref="ElderMapBossOverrideDat"/> with <see cref="ElderMapBossOverrideDat.MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKeys(int? key, out IReadOnlyList<ElderMapBossOverrideDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ElderMapBossOverrideDat>();
            return false;
        }

        if (byMonsterVarietiesKeys is null)
        {
            byMonsterVarietiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsterVarietiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsterVarietiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsterVarietiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ElderMapBossOverrideDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ElderMapBossOverrideDat"/> with <see cref="ElderMapBossOverrideDat.byMonsterVarietiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ElderMapBossOverrideDat>> GetManyToManyByMonsterVarietiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ElderMapBossOverrideDat>>();
        }

        var items = new List<ResultItem<int, ElderMapBossOverrideDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ElderMapBossOverrideDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ElderMapBossOverrideDat"/> with <see cref="ElderMapBossOverrideDat.TerrainMetadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTerrainMetadata(string? key, out ElderMapBossOverrideDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTerrainMetadata(key, out var items))
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
    /// Tries to get <see cref="ElderMapBossOverrideDat"/> with <see cref="ElderMapBossOverrideDat.TerrainMetadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTerrainMetadata(string? key, out IReadOnlyList<ElderMapBossOverrideDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ElderMapBossOverrideDat>();
            return false;
        }

        if (byTerrainMetadata is null)
        {
            byTerrainMetadata = new();
            foreach (var item in Items)
            {
                var itemKey = item.TerrainMetadata;

                if (!byTerrainMetadata.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTerrainMetadata.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTerrainMetadata.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ElderMapBossOverrideDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ElderMapBossOverrideDat"/> with <see cref="ElderMapBossOverrideDat.byTerrainMetadata"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ElderMapBossOverrideDat>> GetManyToManyByTerrainMetadata(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ElderMapBossOverrideDat>>();
        }

        var items = new List<ResultItem<string, ElderMapBossOverrideDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTerrainMetadata(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ElderMapBossOverrideDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ElderMapBossOverrideDat[] Load()
    {
        const string filePath = "Data/ElderMapBossOverride.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ElderMapBossOverrideDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MonsterVarietiesKeys
            (var tempmonstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monstervarietieskeysLoading = tempmonstervarietieskeysLoading.AsReadOnly();

            // loading TerrainMetadata
            (var terrainmetadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ElderMapBossOverrideDat()
            {
                WorldAreasKey = worldareaskeyLoading,
                MonsterVarietiesKeys = monstervarietieskeysLoading,
                TerrainMetadata = terrainmetadataLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
