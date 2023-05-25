using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasUpgradesInventoryLayoutDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasUpgradesInventoryLayoutRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasUpgradesInventoryLayoutDat> Items { get; }

    private Dictionary<string, List<AtlasUpgradesInventoryLayoutDat>>? byId;
    private Dictionary<int, List<AtlasUpgradesInventoryLayoutDat>>? byUnknown8;
    private Dictionary<int, List<AtlasUpgradesInventoryLayoutDat>>? byVoidstone;
    private Dictionary<int, List<AtlasUpgradesInventoryLayoutDat>>? byUnknown28;
    private Dictionary<string, List<AtlasUpgradesInventoryLayoutDat>>? byObjective;
    private Dictionary<int, List<AtlasUpgradesInventoryLayoutDat>>? byGrantAtlasUpgrade;
    private Dictionary<int, List<AtlasUpgradesInventoryLayoutDat>>? byUnknown56;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasUpgradesInventoryLayoutRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasUpgradesInventoryLayoutRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AtlasUpgradesInventoryLayoutDat? item)
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
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AtlasUpgradesInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
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
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasUpgradesInventoryLayoutDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasUpgradesInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<string, AtlasUpgradesInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasUpgradesInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out AtlasUpgradesInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<AtlasUpgradesInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;

                if (!byUnknown8.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasUpgradesInventoryLayoutDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasUpgradesInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, AtlasUpgradesInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasUpgradesInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Voidstone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVoidstone(int? key, out AtlasUpgradesInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVoidstone(key, out var items))
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
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Voidstone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVoidstone(int? key, out IReadOnlyList<AtlasUpgradesInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        if (byVoidstone is null)
        {
            byVoidstone = new();
            foreach (var item in Items)
            {
                var itemKey = item.Voidstone;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byVoidstone.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byVoidstone.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byVoidstone.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.byVoidstone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasUpgradesInventoryLayoutDat>> GetManyToManyByVoidstone(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasUpgradesInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, AtlasUpgradesInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVoidstone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasUpgradesInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out AtlasUpgradesInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<AtlasUpgradesInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasUpgradesInventoryLayoutDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasUpgradesInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, AtlasUpgradesInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasUpgradesInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Objective"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByObjective(string? key, out AtlasUpgradesInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByObjective(key, out var items))
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
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Objective"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByObjective(string? key, out IReadOnlyList<AtlasUpgradesInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        if (byObjective is null)
        {
            byObjective = new();
            foreach (var item in Items)
            {
                var itemKey = item.Objective;

                if (!byObjective.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byObjective.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byObjective.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.byObjective"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasUpgradesInventoryLayoutDat>> GetManyToManyByObjective(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasUpgradesInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<string, AtlasUpgradesInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByObjective(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasUpgradesInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.GrantAtlasUpgrade"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantAtlasUpgrade(int? key, out AtlasUpgradesInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantAtlasUpgrade(key, out var items))
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
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.GrantAtlasUpgrade"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantAtlasUpgrade(int? key, out IReadOnlyList<AtlasUpgradesInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        if (byGrantAtlasUpgrade is null)
        {
            byGrantAtlasUpgrade = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantAtlasUpgrade;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGrantAtlasUpgrade.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGrantAtlasUpgrade.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGrantAtlasUpgrade.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.byGrantAtlasUpgrade"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasUpgradesInventoryLayoutDat>> GetManyToManyByGrantAtlasUpgrade(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasUpgradesInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, AtlasUpgradesInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantAtlasUpgrade(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasUpgradesInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out AtlasUpgradesInventoryLayoutDat? item)
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
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<AtlasUpgradesInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown56.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasUpgradesInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasUpgradesInventoryLayoutDat"/> with <see cref="AtlasUpgradesInventoryLayoutDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasUpgradesInventoryLayoutDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasUpgradesInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, AtlasUpgradesInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasUpgradesInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasUpgradesInventoryLayoutDat[] Load()
    {
        const string filePath = "Data/AtlasUpgradesInventoryLayout.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasUpgradesInventoryLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Voidstone
            (var voidstoneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Objective
            (var objectiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GrantAtlasUpgrade
            (var grantatlasupgradeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasUpgradesInventoryLayoutDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Voidstone = voidstoneLoading,
                Unknown28 = unknown28Loading,
                Objective = objectiveLoading,
                GrantAtlasUpgrade = grantatlasupgradeLoading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
