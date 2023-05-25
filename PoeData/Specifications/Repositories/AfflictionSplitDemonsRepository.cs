using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AfflictionSplitDemonsDat"/> related data and helper methods.
/// </summary>
public sealed class AfflictionSplitDemonsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AfflictionSplitDemonsDat> Items { get; }

    private Dictionary<int, List<AfflictionSplitDemonsDat>>? byUnknown0;
    private Dictionary<int, List<AfflictionSplitDemonsDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<AfflictionSplitDemonsDat>>? byAfflictionRandomModCategoriesKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="AfflictionSplitDemonsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AfflictionSplitDemonsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionSplitDemonsDat"/> with <see cref="AfflictionSplitDemonsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out AfflictionSplitDemonsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown0(key, out var items))
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
    /// Tries to get <see cref="AfflictionSplitDemonsDat"/> with <see cref="AfflictionSplitDemonsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<AfflictionSplitDemonsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AfflictionSplitDemonsDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;

                if (!byUnknown0.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AfflictionSplitDemonsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionSplitDemonsDat"/> with <see cref="AfflictionSplitDemonsDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AfflictionSplitDemonsDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AfflictionSplitDemonsDat>>();
        }

        var items = new List<ResultItem<int, AfflictionSplitDemonsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AfflictionSplitDemonsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionSplitDemonsDat"/> with <see cref="AfflictionSplitDemonsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out AfflictionSplitDemonsDat? item)
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
    /// Tries to get <see cref="AfflictionSplitDemonsDat"/> with <see cref="AfflictionSplitDemonsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<AfflictionSplitDemonsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AfflictionSplitDemonsDat>();
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
            items = Array.Empty<AfflictionSplitDemonsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionSplitDemonsDat"/> with <see cref="AfflictionSplitDemonsDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AfflictionSplitDemonsDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AfflictionSplitDemonsDat>>();
        }

        var items = new List<ResultItem<int, AfflictionSplitDemonsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AfflictionSplitDemonsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionSplitDemonsDat"/> with <see cref="AfflictionSplitDemonsDat.AfflictionRandomModCategoriesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAfflictionRandomModCategoriesKey(int? key, out AfflictionSplitDemonsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAfflictionRandomModCategoriesKey(key, out var items))
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
    /// Tries to get <see cref="AfflictionSplitDemonsDat"/> with <see cref="AfflictionSplitDemonsDat.AfflictionRandomModCategoriesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAfflictionRandomModCategoriesKey(int? key, out IReadOnlyList<AfflictionSplitDemonsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AfflictionSplitDemonsDat>();
            return false;
        }

        if (byAfflictionRandomModCategoriesKey is null)
        {
            byAfflictionRandomModCategoriesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AfflictionRandomModCategoriesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAfflictionRandomModCategoriesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAfflictionRandomModCategoriesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAfflictionRandomModCategoriesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AfflictionSplitDemonsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionSplitDemonsDat"/> with <see cref="AfflictionSplitDemonsDat.byAfflictionRandomModCategoriesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AfflictionSplitDemonsDat>> GetManyToManyByAfflictionRandomModCategoriesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AfflictionSplitDemonsDat>>();
        }

        var items = new List<ResultItem<int, AfflictionSplitDemonsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAfflictionRandomModCategoriesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AfflictionSplitDemonsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AfflictionSplitDemonsDat[] Load()
    {
        const string filePath = "Data/AfflictionSplitDemons.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AfflictionSplitDemonsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AfflictionRandomModCategoriesKey
            (var afflictionrandommodcategorieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AfflictionSplitDemonsDat()
            {
                Unknown0 = unknown0Loading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                AfflictionRandomModCategoriesKey = afflictionrandommodcategorieskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
