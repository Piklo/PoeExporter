using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SentinelTaggedMonsterStatsDat"/> related data and helper methods.
/// </summary>
public sealed class SentinelTaggedMonsterStatsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SentinelTaggedMonsterStatsDat> Items { get; }

    private Dictionary<int, List<SentinelTaggedMonsterStatsDat>>? byTaggedStat;
    private Dictionary<int, List<SentinelTaggedMonsterStatsDat>>? byUnknown16;
    private Dictionary<int, List<SentinelTaggedMonsterStatsDat>>? byUnknown32;
    private Dictionary<int, List<SentinelTaggedMonsterStatsDat>>? byUnknown48;
    private Dictionary<int, List<SentinelTaggedMonsterStatsDat>>? byUnknown64;

    /// <summary>
    /// Initializes a new instance of the <see cref="SentinelTaggedMonsterStatsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SentinelTaggedMonsterStatsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.TaggedStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTaggedStat(int? key, out SentinelTaggedMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTaggedStat(key, out var items))
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
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.TaggedStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTaggedStat(int? key, out IReadOnlyList<SentinelTaggedMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelTaggedMonsterStatsDat>();
            return false;
        }

        if (byTaggedStat is null)
        {
            byTaggedStat = new();
            foreach (var item in Items)
            {
                var itemKey = item.TaggedStat;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTaggedStat.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTaggedStat.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTaggedStat.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelTaggedMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.byTaggedStat"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelTaggedMonsterStatsDat>> GetManyToManyByTaggedStat(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelTaggedMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, SentinelTaggedMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTaggedStat(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelTaggedMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out SentinelTaggedMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<SentinelTaggedMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelTaggedMonsterStatsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown16.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelTaggedMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelTaggedMonsterStatsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelTaggedMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, SentinelTaggedMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelTaggedMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out SentinelTaggedMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<SentinelTaggedMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelTaggedMonsterStatsDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown32.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown32.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelTaggedMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelTaggedMonsterStatsDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelTaggedMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, SentinelTaggedMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelTaggedMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out SentinelTaggedMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown48(key, out var items))
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
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<SentinelTaggedMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelTaggedMonsterStatsDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown48.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelTaggedMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelTaggedMonsterStatsDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelTaggedMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, SentinelTaggedMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelTaggedMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out SentinelTaggedMonsterStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<SentinelTaggedMonsterStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelTaggedMonsterStatsDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown64.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelTaggedMonsterStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelTaggedMonsterStatsDat"/> with <see cref="SentinelTaggedMonsterStatsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelTaggedMonsterStatsDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelTaggedMonsterStatsDat>>();
        }

        var items = new List<ResultItem<int, SentinelTaggedMonsterStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelTaggedMonsterStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SentinelTaggedMonsterStatsDat[] Load()
    {
        const string filePath = "Data/SentinelTaggedMonsterStats.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SentinelTaggedMonsterStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading TaggedStat
            (var taggedstatLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var tempunknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown32Loading = tempunknown32Loading.AsReadOnly();

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SentinelTaggedMonsterStatsDat()
            {
                TaggedStat = taggedstatLoading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
