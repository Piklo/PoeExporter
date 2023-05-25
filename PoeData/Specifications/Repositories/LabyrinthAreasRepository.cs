using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LabyrinthAreasDat"/> related data and helper methods.
/// </summary>
public sealed class LabyrinthAreasRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LabyrinthAreasDat> Items { get; }

    private Dictionary<string, List<LabyrinthAreasDat>>? byId;
    private Dictionary<int, List<LabyrinthAreasDat>>? byNormal_WorldAreasKeys;
    private Dictionary<int, List<LabyrinthAreasDat>>? byCruel_WorldAreasKeys;
    private Dictionary<int, List<LabyrinthAreasDat>>? byMerciless_WorldAreasKeys;
    private Dictionary<int, List<LabyrinthAreasDat>>? byEndgame_WorldAreasKeys;
    private Dictionary<int, List<LabyrinthAreasDat>>? byUnknown72;

    /// <summary>
    /// Initializes a new instance of the <see cref="LabyrinthAreasRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LabyrinthAreasRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out LabyrinthAreasDat? item)
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
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<LabyrinthAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthAreasDat>();
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
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthAreasDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthAreasDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Normal_WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNormal_WorldAreasKeys(int? key, out LabyrinthAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNormal_WorldAreasKeys(key, out var items))
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
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Normal_WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNormal_WorldAreasKeys(int? key, out IReadOnlyList<LabyrinthAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        if (byNormal_WorldAreasKeys is null)
        {
            byNormal_WorldAreasKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Normal_WorldAreasKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byNormal_WorldAreasKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNormal_WorldAreasKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNormal_WorldAreasKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.byNormal_WorldAreasKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthAreasDat>> GetManyToManyByNormal_WorldAreasKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthAreasDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNormal_WorldAreasKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Cruel_WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCruel_WorldAreasKeys(int? key, out LabyrinthAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCruel_WorldAreasKeys(key, out var items))
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
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Cruel_WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCruel_WorldAreasKeys(int? key, out IReadOnlyList<LabyrinthAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        if (byCruel_WorldAreasKeys is null)
        {
            byCruel_WorldAreasKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cruel_WorldAreasKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byCruel_WorldAreasKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCruel_WorldAreasKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCruel_WorldAreasKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.byCruel_WorldAreasKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthAreasDat>> GetManyToManyByCruel_WorldAreasKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthAreasDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCruel_WorldAreasKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Merciless_WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMerciless_WorldAreasKeys(int? key, out LabyrinthAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMerciless_WorldAreasKeys(key, out var items))
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
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Merciless_WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMerciless_WorldAreasKeys(int? key, out IReadOnlyList<LabyrinthAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        if (byMerciless_WorldAreasKeys is null)
        {
            byMerciless_WorldAreasKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Merciless_WorldAreasKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMerciless_WorldAreasKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMerciless_WorldAreasKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMerciless_WorldAreasKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.byMerciless_WorldAreasKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthAreasDat>> GetManyToManyByMerciless_WorldAreasKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthAreasDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMerciless_WorldAreasKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Endgame_WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEndgame_WorldAreasKeys(int? key, out LabyrinthAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEndgame_WorldAreasKeys(key, out var items))
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
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Endgame_WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEndgame_WorldAreasKeys(int? key, out IReadOnlyList<LabyrinthAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        if (byEndgame_WorldAreasKeys is null)
        {
            byEndgame_WorldAreasKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Endgame_WorldAreasKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byEndgame_WorldAreasKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEndgame_WorldAreasKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEndgame_WorldAreasKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.byEndgame_WorldAreasKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthAreasDat>> GetManyToManyByEndgame_WorldAreasKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthAreasDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEndgame_WorldAreasKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out LabyrinthAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<LabyrinthAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthAreasDat"/> with <see cref="LabyrinthAreasDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthAreasDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthAreasDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LabyrinthAreasDat[] Load()
    {
        const string filePath = "Data/LabyrinthAreas.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthAreasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Normal_WorldAreasKeys
            (var tempnormal_worldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var normal_worldareaskeysLoading = tempnormal_worldareaskeysLoading.AsReadOnly();

            // loading Cruel_WorldAreasKeys
            (var tempcruel_worldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cruel_worldareaskeysLoading = tempcruel_worldareaskeysLoading.AsReadOnly();

            // loading Merciless_WorldAreasKeys
            (var tempmerciless_worldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var merciless_worldareaskeysLoading = tempmerciless_worldareaskeysLoading.AsReadOnly();

            // loading Endgame_WorldAreasKeys
            (var tempendgame_worldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var endgame_worldareaskeysLoading = tempendgame_worldareaskeysLoading.AsReadOnly();

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthAreasDat()
            {
                Id = idLoading,
                Normal_WorldAreasKeys = normal_worldareaskeysLoading,
                Cruel_WorldAreasKeys = cruel_worldareaskeysLoading,
                Merciless_WorldAreasKeys = merciless_worldareaskeysLoading,
                Endgame_WorldAreasKeys = endgame_worldareaskeysLoading,
                Unknown72 = unknown72Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
