using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterSpawnerOverridesDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterSpawnerOverridesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterSpawnerOverridesDat> Items { get; }

    private Dictionary<int, List<MonsterSpawnerOverridesDat>>? byUnknown0;
    private Dictionary<int, List<MonsterSpawnerOverridesDat>>? byBase_MonsterVarietiesKey;
    private Dictionary<int, List<MonsterSpawnerOverridesDat>>? byOverride_MonsterVarietiesKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterSpawnerOverridesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterSpawnerOverridesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterSpawnerOverridesDat"/> with <see cref="MonsterSpawnerOverridesDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out MonsterSpawnerOverridesDat? item)
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
    /// Tries to get <see cref="MonsterSpawnerOverridesDat"/> with <see cref="MonsterSpawnerOverridesDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<MonsterSpawnerOverridesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterSpawnerOverridesDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterSpawnerOverridesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterSpawnerOverridesDat"/> with <see cref="MonsterSpawnerOverridesDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterSpawnerOverridesDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterSpawnerOverridesDat>>();
        }

        var items = new List<ResultItem<int, MonsterSpawnerOverridesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterSpawnerOverridesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterSpawnerOverridesDat"/> with <see cref="MonsterSpawnerOverridesDat.Base_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBase_MonsterVarietiesKey(int? key, out MonsterSpawnerOverridesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBase_MonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="MonsterSpawnerOverridesDat"/> with <see cref="MonsterSpawnerOverridesDat.Base_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBase_MonsterVarietiesKey(int? key, out IReadOnlyList<MonsterSpawnerOverridesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterSpawnerOverridesDat>();
            return false;
        }

        if (byBase_MonsterVarietiesKey is null)
        {
            byBase_MonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Base_MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBase_MonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBase_MonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBase_MonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterSpawnerOverridesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterSpawnerOverridesDat"/> with <see cref="MonsterSpawnerOverridesDat.byBase_MonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterSpawnerOverridesDat>> GetManyToManyByBase_MonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterSpawnerOverridesDat>>();
        }

        var items = new List<ResultItem<int, MonsterSpawnerOverridesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBase_MonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterSpawnerOverridesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterSpawnerOverridesDat"/> with <see cref="MonsterSpawnerOverridesDat.Override_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOverride_MonsterVarietiesKey(int? key, out MonsterSpawnerOverridesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOverride_MonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="MonsterSpawnerOverridesDat"/> with <see cref="MonsterSpawnerOverridesDat.Override_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOverride_MonsterVarietiesKey(int? key, out IReadOnlyList<MonsterSpawnerOverridesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterSpawnerOverridesDat>();
            return false;
        }

        if (byOverride_MonsterVarietiesKey is null)
        {
            byOverride_MonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Override_MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byOverride_MonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byOverride_MonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byOverride_MonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterSpawnerOverridesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterSpawnerOverridesDat"/> with <see cref="MonsterSpawnerOverridesDat.byOverride_MonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterSpawnerOverridesDat>> GetManyToManyByOverride_MonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterSpawnerOverridesDat>>();
        }

        var items = new List<ResultItem<int, MonsterSpawnerOverridesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOverride_MonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterSpawnerOverridesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterSpawnerOverridesDat[] Load()
    {
        const string filePath = "Data/MonsterSpawnerOverrides.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterSpawnerOverridesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Base_MonsterVarietiesKey
            (var base_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Override_MonsterVarietiesKey
            (var override_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterSpawnerOverridesDat()
            {
                Unknown0 = unknown0Loading,
                Base_MonsterVarietiesKey = base_monstervarietieskeyLoading,
                Override_MonsterVarietiesKey = override_monstervarietieskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
