using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="WeaponTypesDat"/> related data and helper methods.
/// </summary>
public sealed class WeaponTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<WeaponTypesDat> Items { get; }

    private Dictionary<int, List<WeaponTypesDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<WeaponTypesDat>>? byCritical;
    private Dictionary<int, List<WeaponTypesDat>>? bySpeed;
    private Dictionary<int, List<WeaponTypesDat>>? byDamageMin;
    private Dictionary<int, List<WeaponTypesDat>>? byDamageMax;
    private Dictionary<int, List<WeaponTypesDat>>? byRangeMax;
    private Dictionary<int, List<WeaponTypesDat>>? byUnknown36;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeaponTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal WeaponTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out WeaponTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<WeaponTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponTypesDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponTypesDat>>();
        }

        var items = new List<ResultItem<int, WeaponTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.Critical"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCritical(int? key, out WeaponTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCritical(key, out var items))
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
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.Critical"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCritical(int? key, out IReadOnlyList<WeaponTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        if (byCritical is null)
        {
            byCritical = new();
            foreach (var item in Items)
            {
                var itemKey = item.Critical;

                if (!byCritical.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCritical.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCritical.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.byCritical"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponTypesDat>> GetManyToManyByCritical(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponTypesDat>>();
        }

        var items = new List<ResultItem<int, WeaponTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCritical(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.Speed"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpeed(int? key, out WeaponTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpeed(key, out var items))
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
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.Speed"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpeed(int? key, out IReadOnlyList<WeaponTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        if (bySpeed is null)
        {
            bySpeed = new();
            foreach (var item in Items)
            {
                var itemKey = item.Speed;

                if (!bySpeed.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpeed.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpeed.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.bySpeed"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponTypesDat>> GetManyToManyBySpeed(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponTypesDat>>();
        }

        var items = new List<ResultItem<int, WeaponTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpeed(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.DamageMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDamageMin(int? key, out WeaponTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDamageMin(key, out var items))
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
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.DamageMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDamageMin(int? key, out IReadOnlyList<WeaponTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        if (byDamageMin is null)
        {
            byDamageMin = new();
            foreach (var item in Items)
            {
                var itemKey = item.DamageMin;

                if (!byDamageMin.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDamageMin.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDamageMin.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.byDamageMin"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponTypesDat>> GetManyToManyByDamageMin(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponTypesDat>>();
        }

        var items = new List<ResultItem<int, WeaponTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDamageMin(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.DamageMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDamageMax(int? key, out WeaponTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDamageMax(key, out var items))
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
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.DamageMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDamageMax(int? key, out IReadOnlyList<WeaponTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        if (byDamageMax is null)
        {
            byDamageMax = new();
            foreach (var item in Items)
            {
                var itemKey = item.DamageMax;

                if (!byDamageMax.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDamageMax.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDamageMax.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.byDamageMax"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponTypesDat>> GetManyToManyByDamageMax(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponTypesDat>>();
        }

        var items = new List<ResultItem<int, WeaponTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDamageMax(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.RangeMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRangeMax(int? key, out WeaponTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRangeMax(key, out var items))
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
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.RangeMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRangeMax(int? key, out IReadOnlyList<WeaponTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        if (byRangeMax is null)
        {
            byRangeMax = new();
            foreach (var item in Items)
            {
                var itemKey = item.RangeMax;

                if (!byRangeMax.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRangeMax.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRangeMax.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.byRangeMax"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponTypesDat>> GetManyToManyByRangeMax(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponTypesDat>>();
        }

        var items = new List<ResultItem<int, WeaponTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRangeMax(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out WeaponTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown36(key, out var items))
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
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<WeaponTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        if (byUnknown36 is null)
        {
            byUnknown36 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown36;

                if (!byUnknown36.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown36.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown36.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponTypesDat"/> with <see cref="WeaponTypesDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponTypesDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponTypesDat>>();
        }

        var items = new List<ResultItem<int, WeaponTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private WeaponTypesDat[] Load()
    {
        const string filePath = "Data/WeaponTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WeaponTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Critical
            (var criticalLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Speed
            (var speedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DamageMin
            (var damageminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DamageMax
            (var damagemaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RangeMax
            (var rangemaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WeaponTypesDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Critical = criticalLoading,
                Speed = speedLoading,
                DamageMin = damageminLoading,
                DamageMax = damagemaxLoading,
                RangeMax = rangemaxLoading,
                Unknown36 = unknown36Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
