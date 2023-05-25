using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LegionChestTypesDat"/> related data and helper methods.
/// </summary>
public sealed class LegionChestTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LegionChestTypesDat> Items { get; }

    private Dictionary<int, List<LegionChestTypesDat>>? byUnknown0;
    private Dictionary<int, List<LegionChestTypesDat>>? byChest;
    private Dictionary<int, List<LegionChestTypesDat>>? byHardmodeChest;
    private Dictionary<int, List<LegionChestTypesDat>>? byUnknown48;
    private Dictionary<int, List<LegionChestTypesDat>>? byFaction;

    /// <summary>
    /// Initializes a new instance of the <see cref="LegionChestTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LegionChestTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out LegionChestTypesDat? item)
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
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<LegionChestTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionChestTypesDat>();
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
            items = Array.Empty<LegionChestTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionChestTypesDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionChestTypesDat>>();
        }

        var items = new List<ResultItem<int, LegionChestTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionChestTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.Chest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChest(int? key, out LegionChestTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChest(key, out var items))
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
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.Chest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChest(int? key, out IReadOnlyList<LegionChestTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionChestTypesDat>();
            return false;
        }

        if (byChest is null)
        {
            byChest = new();
            foreach (var item in Items)
            {
                var itemKey = item.Chest;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byChest.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byChest.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byChest.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionChestTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.byChest"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionChestTypesDat>> GetManyToManyByChest(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionChestTypesDat>>();
        }

        var items = new List<ResultItem<int, LegionChestTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChest(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionChestTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.HardmodeChest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHardmodeChest(int? key, out LegionChestTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHardmodeChest(key, out var items))
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
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.HardmodeChest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHardmodeChest(int? key, out IReadOnlyList<LegionChestTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionChestTypesDat>();
            return false;
        }

        if (byHardmodeChest is null)
        {
            byHardmodeChest = new();
            foreach (var item in Items)
            {
                var itemKey = item.HardmodeChest;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHardmodeChest.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHardmodeChest.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHardmodeChest.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionChestTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.byHardmodeChest"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionChestTypesDat>> GetManyToManyByHardmodeChest(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionChestTypesDat>>();
        }

        var items = new List<ResultItem<int, LegionChestTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHardmodeChest(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionChestTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out LegionChestTypesDat? item)
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
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<LegionChestTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionChestTypesDat>();
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
            items = Array.Empty<LegionChestTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionChestTypesDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionChestTypesDat>>();
        }

        var items = new List<ResultItem<int, LegionChestTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionChestTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.Faction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFaction(int? key, out LegionChestTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFaction(key, out var items))
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
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.Faction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFaction(int? key, out IReadOnlyList<LegionChestTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionChestTypesDat>();
            return false;
        }

        if (byFaction is null)
        {
            byFaction = new();
            foreach (var item in Items)
            {
                var itemKey = item.Faction;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFaction.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFaction.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFaction.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionChestTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionChestTypesDat"/> with <see cref="LegionChestTypesDat.byFaction"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionChestTypesDat>> GetManyToManyByFaction(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionChestTypesDat>>();
        }

        var items = new List<ResultItem<int, LegionChestTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFaction(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionChestTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LegionChestTypesDat[] Load()
    {
        const string filePath = "Data/LegionChestTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LegionChestTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Chest
            (var chestLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HardmodeChest
            (var hardmodechestLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Faction
            (var factionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LegionChestTypesDat()
            {
                Unknown0 = unknown0Loading,
                Chest = chestLoading,
                HardmodeChest = hardmodechestLoading,
                Unknown48 = unknown48Loading,
                Faction = factionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
