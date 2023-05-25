using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BreachstoneUpgradesDat"/> related data and helper methods.
/// </summary>
public sealed class BreachstoneUpgradesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BreachstoneUpgradesDat> Items { get; }

    private Dictionary<int, List<BreachstoneUpgradesDat>>? byBaseItemTypesKey0;
    private Dictionary<int, List<BreachstoneUpgradesDat>>? byBaseItemTypesKey1;
    private Dictionary<int, List<BreachstoneUpgradesDat>>? byBaseItemTypesKey2;
    private Dictionary<int, List<BreachstoneUpgradesDat>>? byBaseItemTypesKey3;
    private Dictionary<int, List<BreachstoneUpgradesDat>>? byUnknown64;

    /// <summary>
    /// Initializes a new instance of the <see cref="BreachstoneUpgradesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BreachstoneUpgradesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.BaseItemTypesKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey0(int? key, out BreachstoneUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey0(key, out var items))
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
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.BaseItemTypesKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey0(int? key, out IReadOnlyList<BreachstoneUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachstoneUpgradesDat>();
            return false;
        }

        if (byBaseItemTypesKey0 is null)
        {
            byBaseItemTypesKey0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BreachstoneUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.byBaseItemTypesKey0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachstoneUpgradesDat>> GetManyToManyByBaseItemTypesKey0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachstoneUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BreachstoneUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachstoneUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.BaseItemTypesKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey1(int? key, out BreachstoneUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey1(key, out var items))
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
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.BaseItemTypesKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey1(int? key, out IReadOnlyList<BreachstoneUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachstoneUpgradesDat>();
            return false;
        }

        if (byBaseItemTypesKey1 is null)
        {
            byBaseItemTypesKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BreachstoneUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.byBaseItemTypesKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachstoneUpgradesDat>> GetManyToManyByBaseItemTypesKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachstoneUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BreachstoneUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachstoneUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.BaseItemTypesKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey2(int? key, out BreachstoneUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey2(key, out var items))
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
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.BaseItemTypesKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey2(int? key, out IReadOnlyList<BreachstoneUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachstoneUpgradesDat>();
            return false;
        }

        if (byBaseItemTypesKey2 is null)
        {
            byBaseItemTypesKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BreachstoneUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.byBaseItemTypesKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachstoneUpgradesDat>> GetManyToManyByBaseItemTypesKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachstoneUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BreachstoneUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachstoneUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.BaseItemTypesKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey3(int? key, out BreachstoneUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey3(key, out var items))
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
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.BaseItemTypesKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey3(int? key, out IReadOnlyList<BreachstoneUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachstoneUpgradesDat>();
            return false;
        }

        if (byBaseItemTypesKey3 is null)
        {
            byBaseItemTypesKey3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BreachstoneUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.byBaseItemTypesKey3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachstoneUpgradesDat>> GetManyToManyByBaseItemTypesKey3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachstoneUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BreachstoneUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachstoneUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out BreachstoneUpgradesDat? item)
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
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<BreachstoneUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachstoneUpgradesDat>();
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
            items = Array.Empty<BreachstoneUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachstoneUpgradesDat"/> with <see cref="BreachstoneUpgradesDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachstoneUpgradesDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachstoneUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BreachstoneUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachstoneUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BreachstoneUpgradesDat[] Load()
    {
        const string filePath = "Data/BreachstoneUpgrades.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BreachstoneUpgradesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey0
            (var baseitemtypeskey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BaseItemTypesKey1
            (var baseitemtypeskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BaseItemTypesKey2
            (var baseitemtypeskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BaseItemTypesKey3
            (var baseitemtypeskey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BreachstoneUpgradesDat()
            {
                BaseItemTypesKey0 = baseitemtypeskey0Loading,
                BaseItemTypesKey1 = baseitemtypeskey1Loading,
                BaseItemTypesKey2 = baseitemtypeskey2Loading,
                BaseItemTypesKey3 = baseitemtypeskey3Loading,
                Unknown64 = unknown64Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
