using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="StrongboxesDat"/> related data and helper methods.
/// </summary>
public sealed class StrongboxesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<StrongboxesDat> Items { get; }

    private Dictionary<int, List<StrongboxesDat>>? byChestsKey;
    private Dictionary<int, List<StrongboxesDat>>? bySpawnWeight;
    private Dictionary<int, List<StrongboxesDat>>? byUnknown20;
    private Dictionary<bool, List<StrongboxesDat>>? byIsCartographerBox;
    private Dictionary<bool, List<StrongboxesDat>>? byUnknown25;
    private Dictionary<int, List<StrongboxesDat>>? bySpawnWeightIncrease;
    private Dictionary<int, List<StrongboxesDat>>? bySpawnWeightHardmode;

    /// <summary>
    /// Initializes a new instance of the <see cref="StrongboxesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal StrongboxesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.ChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChestsKey(int? key, out StrongboxesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChestsKey(key, out var items))
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
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.ChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChestsKey(int? key, out IReadOnlyList<StrongboxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        if (byChestsKey is null)
        {
            byChestsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChestsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byChestsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byChestsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byChestsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.byChestsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrongboxesDat>> GetManyToManyByChestsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrongboxesDat>>();
        }

        var items = new List<ResultItem<int, StrongboxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChestsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrongboxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out StrongboxesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight(key, out var items))
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
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<StrongboxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        if (bySpawnWeight is null)
        {
            bySpawnWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight;

                if (!bySpawnWeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpawnWeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpawnWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrongboxesDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrongboxesDat>>();
        }

        var items = new List<ResultItem<int, StrongboxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrongboxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out StrongboxesDat? item)
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
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<StrongboxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrongboxesDat>();
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
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrongboxesDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrongboxesDat>>();
        }

        var items = new List<ResultItem<int, StrongboxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrongboxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.IsCartographerBox"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsCartographerBox(bool? key, out StrongboxesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsCartographerBox(key, out var items))
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
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.IsCartographerBox"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsCartographerBox(bool? key, out IReadOnlyList<StrongboxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        if (byIsCartographerBox is null)
        {
            byIsCartographerBox = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsCartographerBox;

                if (!byIsCartographerBox.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsCartographerBox.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsCartographerBox.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.byIsCartographerBox"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrongboxesDat>> GetManyToManyByIsCartographerBox(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrongboxesDat>>();
        }

        var items = new List<ResultItem<bool, StrongboxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsCartographerBox(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrongboxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown25(bool? key, out StrongboxesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown25(key, out var items))
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
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown25(bool? key, out IReadOnlyList<StrongboxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        if (byUnknown25 is null)
        {
            byUnknown25 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown25;

                if (!byUnknown25.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown25.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown25.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.byUnknown25"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrongboxesDat>> GetManyToManyByUnknown25(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrongboxesDat>>();
        }

        var items = new List<ResultItem<bool, StrongboxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown25(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrongboxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.SpawnWeightIncrease"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeightIncrease(int? key, out StrongboxesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeightIncrease(key, out var items))
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
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.SpawnWeightIncrease"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeightIncrease(int? key, out IReadOnlyList<StrongboxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        if (bySpawnWeightIncrease is null)
        {
            bySpawnWeightIncrease = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeightIncrease;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySpawnWeightIncrease.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySpawnWeightIncrease.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySpawnWeightIncrease.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.bySpawnWeightIncrease"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrongboxesDat>> GetManyToManyBySpawnWeightIncrease(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrongboxesDat>>();
        }

        var items = new List<ResultItem<int, StrongboxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeightIncrease(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrongboxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.SpawnWeightHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeightHardmode(int? key, out StrongboxesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeightHardmode(key, out var items))
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
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.SpawnWeightHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeightHardmode(int? key, out IReadOnlyList<StrongboxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        if (bySpawnWeightHardmode is null)
        {
            bySpawnWeightHardmode = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeightHardmode;

                if (!bySpawnWeightHardmode.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpawnWeightHardmode.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpawnWeightHardmode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrongboxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrongboxesDat"/> with <see cref="StrongboxesDat.bySpawnWeightHardmode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrongboxesDat>> GetManyToManyBySpawnWeightHardmode(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrongboxesDat>>();
        }

        var items = new List<ResultItem<int, StrongboxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeightHardmode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrongboxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private StrongboxesDat[] Load()
    {
        const string filePath = "Data/Strongboxes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StrongboxesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsCartographerBox
            (var iscartographerboxLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SpawnWeightIncrease
            (var spawnweightincreaseLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SpawnWeightHardmode
            (var spawnweighthardmodeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StrongboxesDat()
            {
                ChestsKey = chestskeyLoading,
                SpawnWeight = spawnweightLoading,
                Unknown20 = unknown20Loading,
                IsCartographerBox = iscartographerboxLoading,
                Unknown25 = unknown25Loading,
                SpawnWeightIncrease = spawnweightincreaseLoading,
                SpawnWeightHardmode = spawnweighthardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
