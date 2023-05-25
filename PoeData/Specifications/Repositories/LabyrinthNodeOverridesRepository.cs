using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LabyrinthNodeOverridesDat"/> related data and helper methods.
/// </summary>
public sealed class LabyrinthNodeOverridesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LabyrinthNodeOverridesDat> Items { get; }

    private Dictionary<string, List<LabyrinthNodeOverridesDat>>? byId1;
    private Dictionary<string, List<LabyrinthNodeOverridesDat>>? byId2;
    private Dictionary<int, List<LabyrinthNodeOverridesDat>>? byUnknown16;
    private Dictionary<int, List<LabyrinthNodeOverridesDat>>? byUnknown32;

    /// <summary>
    /// Initializes a new instance of the <see cref="LabyrinthNodeOverridesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LabyrinthNodeOverridesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.Id1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById1(string? key, out LabyrinthNodeOverridesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyById1(key, out var items))
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
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.Id1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById1(string? key, out IReadOnlyList<LabyrinthNodeOverridesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthNodeOverridesDat>();
            return false;
        }

        if (byId1 is null)
        {
            byId1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Id1;

                if (!byId1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byId1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byId1.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LabyrinthNodeOverridesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.byId1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthNodeOverridesDat>> GetManyToManyById1(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthNodeOverridesDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthNodeOverridesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthNodeOverridesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.Id2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById2(string? key, out LabyrinthNodeOverridesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyById2(key, out var items))
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
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.Id2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById2(string? key, out IReadOnlyList<LabyrinthNodeOverridesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthNodeOverridesDat>();
            return false;
        }

        if (byId2 is null)
        {
            byId2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Id2;

                if (!byId2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byId2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byId2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LabyrinthNodeOverridesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.byId2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthNodeOverridesDat>> GetManyToManyById2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthNodeOverridesDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthNodeOverridesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthNodeOverridesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out LabyrinthNodeOverridesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<LabyrinthNodeOverridesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthNodeOverridesDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown16.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown16.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthNodeOverridesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthNodeOverridesDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthNodeOverridesDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthNodeOverridesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthNodeOverridesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out LabyrinthNodeOverridesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<LabyrinthNodeOverridesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthNodeOverridesDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown32.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown32.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthNodeOverridesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthNodeOverridesDat"/> with <see cref="LabyrinthNodeOverridesDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthNodeOverridesDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthNodeOverridesDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthNodeOverridesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthNodeOverridesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LabyrinthNodeOverridesDat[] Load()
    {
        const string filePath = "Data/LabyrinthNodeOverrides.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthNodeOverridesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id1
            (var id1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Id2
            (var id2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var tempunknown16Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown16Loading = tempunknown16Loading.AsReadOnly();

            // loading Unknown32
            (var tempunknown32Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown32Loading = tempunknown32Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthNodeOverridesDat()
            {
                Id1 = id1Loading,
                Id2 = id2Loading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
