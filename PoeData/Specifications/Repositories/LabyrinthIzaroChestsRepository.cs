using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LabyrinthIzaroChestsDat"/> related data and helper methods.
/// </summary>
public sealed class LabyrinthIzaroChestsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LabyrinthIzaroChestsDat> Items { get; }

    private Dictionary<string, List<LabyrinthIzaroChestsDat>>? byId;
    private Dictionary<int, List<LabyrinthIzaroChestsDat>>? byChestsKey;
    private Dictionary<int, List<LabyrinthIzaroChestsDat>>? bySpawnWeight;
    private Dictionary<int, List<LabyrinthIzaroChestsDat>>? byMinLabyrinthTier;
    private Dictionary<int, List<LabyrinthIzaroChestsDat>>? byMaxLabyrinthTier;

    /// <summary>
    /// Initializes a new instance of the <see cref="LabyrinthIzaroChestsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LabyrinthIzaroChestsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out LabyrinthIzaroChestsDat? item)
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
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<LabyrinthIzaroChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthIzaroChestsDat>();
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
            items = Array.Empty<LabyrinthIzaroChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthIzaroChestsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthIzaroChestsDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthIzaroChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthIzaroChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.ChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChestsKey(int? key, out LabyrinthIzaroChestsDat? item)
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
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.ChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChestsKey(int? key, out IReadOnlyList<LabyrinthIzaroChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthIzaroChestsDat>();
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
            items = Array.Empty<LabyrinthIzaroChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.byChestsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthIzaroChestsDat>> GetManyToManyByChestsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthIzaroChestsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthIzaroChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChestsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthIzaroChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out LabyrinthIzaroChestsDat? item)
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
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<LabyrinthIzaroChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthIzaroChestsDat>();
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
            items = Array.Empty<LabyrinthIzaroChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthIzaroChestsDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthIzaroChestsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthIzaroChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthIzaroChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.MinLabyrinthTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLabyrinthTier(int? key, out LabyrinthIzaroChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLabyrinthTier(key, out var items))
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
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.MinLabyrinthTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLabyrinthTier(int? key, out IReadOnlyList<LabyrinthIzaroChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthIzaroChestsDat>();
            return false;
        }

        if (byMinLabyrinthTier is null)
        {
            byMinLabyrinthTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLabyrinthTier;

                if (!byMinLabyrinthTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLabyrinthTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLabyrinthTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthIzaroChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.byMinLabyrinthTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthIzaroChestsDat>> GetManyToManyByMinLabyrinthTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthIzaroChestsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthIzaroChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLabyrinthTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthIzaroChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.MaxLabyrinthTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLabyrinthTier(int? key, out LabyrinthIzaroChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxLabyrinthTier(key, out var items))
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
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.MaxLabyrinthTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLabyrinthTier(int? key, out IReadOnlyList<LabyrinthIzaroChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthIzaroChestsDat>();
            return false;
        }

        if (byMaxLabyrinthTier is null)
        {
            byMaxLabyrinthTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxLabyrinthTier;

                if (!byMaxLabyrinthTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxLabyrinthTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxLabyrinthTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthIzaroChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthIzaroChestsDat"/> with <see cref="LabyrinthIzaroChestsDat.byMaxLabyrinthTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthIzaroChestsDat>> GetManyToManyByMaxLabyrinthTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthIzaroChestsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthIzaroChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLabyrinthTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthIzaroChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LabyrinthIzaroChestsDat[] Load()
    {
        const string filePath = "Data/LabyrinthIzaroChests.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthIzaroChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLabyrinthTier
            (var minlabyrinthtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLabyrinthTier
            (var maxlabyrinthtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthIzaroChestsDat()
            {
                Id = idLoading,
                ChestsKey = chestskeyLoading,
                SpawnWeight = spawnweightLoading,
                MinLabyrinthTier = minlabyrinthtierLoading,
                MaxLabyrinthTier = maxlabyrinthtierLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
