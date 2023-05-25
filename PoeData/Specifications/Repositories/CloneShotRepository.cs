using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CloneShotDat"/> related data and helper methods.
/// </summary>
public sealed class CloneShotRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CloneShotDat> Items { get; }

    private Dictionary<int, List<CloneShotDat>>? byId;
    private Dictionary<int, List<CloneShotDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<CloneShotDat>>? byMiscAnimated1;
    private Dictionary<int, List<CloneShotDat>>? byMiscAnimated2;
    private Dictionary<int, List<CloneShotDat>>? byMiscAnimated3;

    /// <summary>
    /// Initializes a new instance of the <see cref="CloneShotRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CloneShotRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(int? key, out CloneShotDat? item)
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
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(int? key, out IReadOnlyList<CloneShotDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CloneShotDat>();
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
            items = Array.Empty<CloneShotDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CloneShotDat>> GetManyToManyById(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CloneShotDat>>();
        }

        var items = new List<ResultItem<int, CloneShotDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CloneShotDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out CloneShotDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<CloneShotDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CloneShotDat>();
            return false;
        }

        if (byMonsterVarietiesKey is null)
        {
            byMonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CloneShotDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CloneShotDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CloneShotDat>>();
        }

        var items = new List<ResultItem<int, CloneShotDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CloneShotDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.MiscAnimated1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimated1(int? key, out CloneShotDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimated1(key, out var items))
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
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.MiscAnimated1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimated1(int? key, out IReadOnlyList<CloneShotDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CloneShotDat>();
            return false;
        }

        if (byMiscAnimated1 is null)
        {
            byMiscAnimated1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimated1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimated1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimated1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimated1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CloneShotDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.byMiscAnimated1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CloneShotDat>> GetManyToManyByMiscAnimated1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CloneShotDat>>();
        }

        var items = new List<ResultItem<int, CloneShotDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimated1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CloneShotDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.MiscAnimated2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimated2(int? key, out CloneShotDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimated2(key, out var items))
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
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.MiscAnimated2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimated2(int? key, out IReadOnlyList<CloneShotDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CloneShotDat>();
            return false;
        }

        if (byMiscAnimated2 is null)
        {
            byMiscAnimated2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimated2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimated2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimated2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimated2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CloneShotDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.byMiscAnimated2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CloneShotDat>> GetManyToManyByMiscAnimated2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CloneShotDat>>();
        }

        var items = new List<ResultItem<int, CloneShotDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimated2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CloneShotDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.MiscAnimated3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimated3(int? key, out CloneShotDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimated3(key, out var items))
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
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.MiscAnimated3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimated3(int? key, out IReadOnlyList<CloneShotDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CloneShotDat>();
            return false;
        }

        if (byMiscAnimated3 is null)
        {
            byMiscAnimated3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimated3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimated3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimated3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimated3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CloneShotDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CloneShotDat"/> with <see cref="CloneShotDat.byMiscAnimated3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CloneShotDat>> GetManyToManyByMiscAnimated3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CloneShotDat>>();
        }

        var items = new List<ResultItem<int, CloneShotDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimated3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CloneShotDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CloneShotDat[] Load()
    {
        const string filePath = "Data/CloneShot.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CloneShotDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimated1
            (var miscanimated1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimated2
            (var miscanimated2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimated3
            (var miscanimated3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CloneShotDat()
            {
                Id = idLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                MiscAnimated1 = miscanimated1Loading,
                MiscAnimated2 = miscanimated2Loading,
                MiscAnimated3 = miscanimated3Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
