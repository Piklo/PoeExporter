using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="IncursionChestRewardsDat"/> related data and helper methods.
/// </summary>
public sealed class IncursionChestRewardsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<IncursionChestRewardsDat> Items { get; }

    private Dictionary<int, List<IncursionChestRewardsDat>>? byIncursionRoomsKey;
    private Dictionary<int, List<IncursionChestRewardsDat>>? byIncursionChestsKeys;
    private Dictionary<string, List<IncursionChestRewardsDat>>? byChestMarkerMetadata;
    private Dictionary<int, List<IncursionChestRewardsDat>>? byUnknown40;
    private Dictionary<int, List<IncursionChestRewardsDat>>? byUnknown44;
    private Dictionary<int, List<IncursionChestRewardsDat>>? byUnknown48;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncursionChestRewardsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal IncursionChestRewardsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.IncursionRoomsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIncursionRoomsKey(int? key, out IncursionChestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIncursionRoomsKey(key, out var items))
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
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.IncursionRoomsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIncursionRoomsKey(int? key, out IReadOnlyList<IncursionChestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        if (byIncursionRoomsKey is null)
        {
            byIncursionRoomsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.IncursionRoomsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byIncursionRoomsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byIncursionRoomsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byIncursionRoomsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.byIncursionRoomsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestRewardsDat>> GetManyToManyByIncursionRoomsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestRewardsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIncursionRoomsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.IncursionChestsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIncursionChestsKeys(int? key, out IncursionChestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIncursionChestsKeys(key, out var items))
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
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.IncursionChestsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIncursionChestsKeys(int? key, out IReadOnlyList<IncursionChestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        if (byIncursionChestsKeys is null)
        {
            byIncursionChestsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.IncursionChestsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byIncursionChestsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byIncursionChestsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byIncursionChestsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.byIncursionChestsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestRewardsDat>> GetManyToManyByIncursionChestsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestRewardsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIncursionChestsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.ChestMarkerMetadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChestMarkerMetadata(string? key, out IncursionChestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChestMarkerMetadata(key, out var items))
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
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.ChestMarkerMetadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChestMarkerMetadata(string? key, out IReadOnlyList<IncursionChestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        if (byChestMarkerMetadata is null)
        {
            byChestMarkerMetadata = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChestMarkerMetadata;

                if (!byChestMarkerMetadata.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChestMarkerMetadata.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChestMarkerMetadata.TryGetValue(key, out var temp))
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.byChestMarkerMetadata"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionChestRewardsDat>> GetManyToManyByChestMarkerMetadata(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionChestRewardsDat>>();
        }

        var items = new List<ResultItem<string, IncursionChestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChestMarkerMetadata(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionChestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out IncursionChestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<IncursionChestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;

                if (!byUnknown40.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestRewardsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestRewardsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out IncursionChestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown44(key, out var items))
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
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<IncursionChestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        if (byUnknown44 is null)
        {
            byUnknown44 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown44;

                if (!byUnknown44.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown44.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown44.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestRewardsDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestRewardsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out IncursionChestRewardsDat? item)
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
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<IncursionChestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;

                if (!byUnknown48.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionChestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionChestRewardsDat"/> with <see cref="IncursionChestRewardsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionChestRewardsDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionChestRewardsDat>>();
        }

        var items = new List<ResultItem<int, IncursionChestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionChestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private IncursionChestRewardsDat[] Load()
    {
        const string filePath = "Data/IncursionChestRewards.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionChestRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading IncursionRoomsKey
            (var incursionroomskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IncursionChestsKeys
            (var tempincursionchestskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var incursionchestskeysLoading = tempincursionchestskeysLoading.AsReadOnly();

            // loading ChestMarkerMetadata
            (var chestmarkermetadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionChestRewardsDat()
            {
                IncursionRoomsKey = incursionroomskeyLoading,
                IncursionChestsKeys = incursionchestskeysLoading,
                ChestMarkerMetadata = chestmarkermetadataLoading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
