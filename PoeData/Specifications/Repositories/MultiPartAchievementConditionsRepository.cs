using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MultiPartAchievementConditionsDat"/> related data and helper methods.
/// </summary>
public sealed class MultiPartAchievementConditionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MultiPartAchievementConditionsDat> Items { get; }

    private Dictionary<string, List<MultiPartAchievementConditionsDat>>? byId;
    private Dictionary<int, List<MultiPartAchievementConditionsDat>>? byMultiPartAchievementsKey1;
    private Dictionary<int, List<MultiPartAchievementConditionsDat>>? byMultiPartAchievementsKey2;
    private Dictionary<int, List<MultiPartAchievementConditionsDat>>? byUnknown40;
    private Dictionary<int, List<MultiPartAchievementConditionsDat>>? byUnknown44;

    /// <summary>
    /// Initializes a new instance of the <see cref="MultiPartAchievementConditionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MultiPartAchievementConditionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MultiPartAchievementConditionsDat? item)
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
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MultiPartAchievementConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MultiPartAchievementConditionsDat>();
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

        if (!byId.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MultiPartAchievementConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MultiPartAchievementConditionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MultiPartAchievementConditionsDat>>();
        }

        var items = new List<ResultItem<string, MultiPartAchievementConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MultiPartAchievementConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.MultiPartAchievementsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMultiPartAchievementsKey1(int? key, out MultiPartAchievementConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMultiPartAchievementsKey1(key, out var items))
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
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.MultiPartAchievementsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMultiPartAchievementsKey1(int? key, out IReadOnlyList<MultiPartAchievementConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MultiPartAchievementConditionsDat>();
            return false;
        }

        if (byMultiPartAchievementsKey1 is null)
        {
            byMultiPartAchievementsKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MultiPartAchievementsKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMultiPartAchievementsKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMultiPartAchievementsKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMultiPartAchievementsKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MultiPartAchievementConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.byMultiPartAchievementsKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MultiPartAchievementConditionsDat>> GetManyToManyByMultiPartAchievementsKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MultiPartAchievementConditionsDat>>();
        }

        var items = new List<ResultItem<int, MultiPartAchievementConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMultiPartAchievementsKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MultiPartAchievementConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.MultiPartAchievementsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMultiPartAchievementsKey2(int? key, out MultiPartAchievementConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMultiPartAchievementsKey2(key, out var items))
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
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.MultiPartAchievementsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMultiPartAchievementsKey2(int? key, out IReadOnlyList<MultiPartAchievementConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MultiPartAchievementConditionsDat>();
            return false;
        }

        if (byMultiPartAchievementsKey2 is null)
        {
            byMultiPartAchievementsKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MultiPartAchievementsKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMultiPartAchievementsKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMultiPartAchievementsKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMultiPartAchievementsKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MultiPartAchievementConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.byMultiPartAchievementsKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MultiPartAchievementConditionsDat>> GetManyToManyByMultiPartAchievementsKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MultiPartAchievementConditionsDat>>();
        }

        var items = new List<ResultItem<int, MultiPartAchievementConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMultiPartAchievementsKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MultiPartAchievementConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out MultiPartAchievementConditionsDat? item)
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
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<MultiPartAchievementConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MultiPartAchievementConditionsDat>();
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
            items = Array.Empty<MultiPartAchievementConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MultiPartAchievementConditionsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MultiPartAchievementConditionsDat>>();
        }

        var items = new List<ResultItem<int, MultiPartAchievementConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MultiPartAchievementConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out MultiPartAchievementConditionsDat? item)
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
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<MultiPartAchievementConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MultiPartAchievementConditionsDat>();
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
            items = Array.Empty<MultiPartAchievementConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MultiPartAchievementConditionsDat"/> with <see cref="MultiPartAchievementConditionsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MultiPartAchievementConditionsDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MultiPartAchievementConditionsDat>>();
        }

        var items = new List<ResultItem<int, MultiPartAchievementConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MultiPartAchievementConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MultiPartAchievementConditionsDat[] Load()
    {
        const string filePath = "Data/MultiPartAchievementConditions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MultiPartAchievementConditionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MultiPartAchievementsKey1
            (var multipartachievementskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MultiPartAchievementsKey2
            (var multipartachievementskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MultiPartAchievementConditionsDat()
            {
                Id = idLoading,
                MultiPartAchievementsKey1 = multipartachievementskey1Loading,
                MultiPartAchievementsKey2 = multipartachievementskey2Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
