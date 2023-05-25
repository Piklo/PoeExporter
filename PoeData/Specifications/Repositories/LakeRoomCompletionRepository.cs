using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LakeRoomCompletionDat"/> related data and helper methods.
/// </summary>
public sealed class LakeRoomCompletionRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LakeRoomCompletionDat> Items { get; }

    private Dictionary<int, List<LakeRoomCompletionDat>>? byRoom;
    private Dictionary<int, List<LakeRoomCompletionDat>>? byUnknown16;
    private Dictionary<int, List<LakeRoomCompletionDat>>? byUnknown20;
    private Dictionary<int, List<LakeRoomCompletionDat>>? byAchievements;
    private Dictionary<int, List<LakeRoomCompletionDat>>? byUnknown40;
    private Dictionary<int, List<LakeRoomCompletionDat>>? byUnknown56;
    private Dictionary<int, List<LakeRoomCompletionDat>>? byUnknown72;

    /// <summary>
    /// Initializes a new instance of the <see cref="LakeRoomCompletionRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LakeRoomCompletionRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Room"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRoom(int? key, out LakeRoomCompletionDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRoom(key, out var items))
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
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Room"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRoom(int? key, out IReadOnlyList<LakeRoomCompletionDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        if (byRoom is null)
        {
            byRoom = new();
            foreach (var item in Items)
            {
                var itemKey = item.Room;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRoom.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRoom.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRoom.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.byRoom"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LakeRoomCompletionDat>> GetManyToManyByRoom(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LakeRoomCompletionDat>>();
        }

        var items = new List<ResultItem<int, LakeRoomCompletionDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRoom(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LakeRoomCompletionDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out LakeRoomCompletionDat? item)
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
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<LakeRoomCompletionDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LakeRoomCompletionDat>();
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
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LakeRoomCompletionDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LakeRoomCompletionDat>>();
        }

        var items = new List<ResultItem<int, LakeRoomCompletionDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LakeRoomCompletionDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out LakeRoomCompletionDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown20(key, out var items))
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
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<LakeRoomCompletionDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;

                if (!byUnknown20.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LakeRoomCompletionDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LakeRoomCompletionDat>>();
        }

        var items = new List<ResultItem<int, LakeRoomCompletionDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LakeRoomCompletionDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements(int? key, out LakeRoomCompletionDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievements(key, out var items))
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
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements(int? key, out IReadOnlyList<LakeRoomCompletionDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        if (byAchievements is null)
        {
            byAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievements;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.byAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LakeRoomCompletionDat>> GetManyToManyByAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LakeRoomCompletionDat>>();
        }

        var items = new List<ResultItem<int, LakeRoomCompletionDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LakeRoomCompletionDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out LakeRoomCompletionDat? item)
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
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<LakeRoomCompletionDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown40.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown40.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LakeRoomCompletionDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LakeRoomCompletionDat>>();
        }

        var items = new List<ResultItem<int, LakeRoomCompletionDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LakeRoomCompletionDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out LakeRoomCompletionDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<LakeRoomCompletionDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown56.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown56.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LakeRoomCompletionDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LakeRoomCompletionDat>>();
        }

        var items = new List<ResultItem<int, LakeRoomCompletionDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LakeRoomCompletionDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out LakeRoomCompletionDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<LakeRoomCompletionDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LakeRoomCompletionDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LakeRoomCompletionDat"/> with <see cref="LakeRoomCompletionDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LakeRoomCompletionDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LakeRoomCompletionDat>>();
        }

        var items = new List<ResultItem<int, LakeRoomCompletionDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LakeRoomCompletionDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LakeRoomCompletionDat[] Load()
    {
        const string filePath = "Data/LakeRoomCompletion.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LakeRoomCompletionDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Room
            (var roomLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LakeRoomCompletionDat()
            {
                Room = roomLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Achievements = achievementsLoading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
