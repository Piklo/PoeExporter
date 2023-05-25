using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistNPCStatsDat"/> related data and helper methods.
/// </summary>
public sealed class HeistNPCStatsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistNPCStatsDat> Items { get; }

    private Dictionary<int, List<HeistNPCStatsDat>>? byStatsKey;
    private Dictionary<bool, List<HeistNPCStatsDat>>? byUnknown16;
    private Dictionary<bool, List<HeistNPCStatsDat>>? byUnknown17;
    private Dictionary<bool, List<HeistNPCStatsDat>>? byUnknown18;
    private Dictionary<bool, List<HeistNPCStatsDat>>? byUnknown19;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistNPCStatsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistNPCStatsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey(int? key, out HeistNPCStatsDat? item)
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
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey(int? key, out IReadOnlyList<HeistNPCStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCStatsDat>();
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
            items = Array.Empty<HeistNPCStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.byStatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCStatsDat>> GetManyToManyByStatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCStatsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(bool? key, out HeistNPCStatsDat? item)
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
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(bool? key, out IReadOnlyList<HeistNPCStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCStatsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistNPCStatsDat>> GetManyToManyByUnknown16(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistNPCStatsDat>>();
        }

        var items = new List<ResultItem<bool, HeistNPCStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistNPCStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.Unknown17"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown17(bool? key, out HeistNPCStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown17(key, out var items))
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
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.Unknown17"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown17(bool? key, out IReadOnlyList<HeistNPCStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCStatsDat>();
            return false;
        }

        if (byUnknown17 is null)
        {
            byUnknown17 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown17;

                if (!byUnknown17.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown17.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown17.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.byUnknown17"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistNPCStatsDat>> GetManyToManyByUnknown17(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistNPCStatsDat>>();
        }

        var items = new List<ResultItem<bool, HeistNPCStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown17(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistNPCStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.Unknown18"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown18(bool? key, out HeistNPCStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown18(key, out var items))
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
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.Unknown18"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown18(bool? key, out IReadOnlyList<HeistNPCStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCStatsDat>();
            return false;
        }

        if (byUnknown18 is null)
        {
            byUnknown18 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown18;

                if (!byUnknown18.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown18.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown18.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.byUnknown18"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistNPCStatsDat>> GetManyToManyByUnknown18(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistNPCStatsDat>>();
        }

        var items = new List<ResultItem<bool, HeistNPCStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown18(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistNPCStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.Unknown19"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown19(bool? key, out HeistNPCStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown19(key, out var items))
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
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.Unknown19"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown19(bool? key, out IReadOnlyList<HeistNPCStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCStatsDat>();
            return false;
        }

        if (byUnknown19 is null)
        {
            byUnknown19 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown19;

                if (!byUnknown19.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown19.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown19.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCStatsDat"/> with <see cref="HeistNPCStatsDat.byUnknown19"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistNPCStatsDat>> GetManyToManyByUnknown19(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistNPCStatsDat>>();
        }

        var items = new List<ResultItem<bool, HeistNPCStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown19(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistNPCStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistNPCStatsDat[] Load()
    {
        const string filePath = "Data/HeistNPCStats.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistNPCStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown17
            (var unknown17Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown18
            (var unknown18Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown19
            (var unknown19Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistNPCStatsDat()
            {
                StatsKey = statskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown17 = unknown17Loading,
                Unknown18 = unknown18Loading,
                Unknown19 = unknown19Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
