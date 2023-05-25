using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DropPoolDat"/> related data and helper methods.
/// </summary>
public sealed class DropPoolRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DropPoolDat> Items { get; }

    private Dictionary<string, List<DropPoolDat>>? byGroup;
    private Dictionary<int, List<DropPoolDat>>? byWeight;
    private Dictionary<int, List<DropPoolDat>>? byUnknown12;
    private Dictionary<int, List<DropPoolDat>>? byWeightHardmode;

    /// <summary>
    /// Initializes a new instance of the <see cref="DropPoolRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DropPoolRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.Group"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGroup(string? key, out DropPoolDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGroup(key, out var items))
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
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.Group"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGroup(string? key, out IReadOnlyList<DropPoolDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DropPoolDat>();
            return false;
        }

        if (byGroup is null)
        {
            byGroup = new();
            foreach (var item in Items)
            {
                var itemKey = item.Group;

                if (!byGroup.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGroup.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGroup.TryGetValue(key, out var temp))
        {
            items = Array.Empty<DropPoolDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.byGroup"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DropPoolDat>> GetManyToManyByGroup(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DropPoolDat>>();
        }

        var items = new List<ResultItem<string, DropPoolDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGroup(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DropPoolDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.Weight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeight(int? key, out DropPoolDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeight(key, out var items))
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
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.Weight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeight(int? key, out IReadOnlyList<DropPoolDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DropPoolDat>();
            return false;
        }

        if (byWeight is null)
        {
            byWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.Weight;

                if (!byWeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DropPoolDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.byWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DropPoolDat>> GetManyToManyByWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DropPoolDat>>();
        }

        var items = new List<ResultItem<int, DropPoolDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DropPoolDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out DropPoolDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown12(key, out var items))
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
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<DropPoolDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DropPoolDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown12.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown12.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DropPoolDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DropPoolDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DropPoolDat>>();
        }

        var items = new List<ResultItem<int, DropPoolDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DropPoolDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.WeightHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeightHardmode(int? key, out DropPoolDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeightHardmode(key, out var items))
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
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.WeightHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeightHardmode(int? key, out IReadOnlyList<DropPoolDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DropPoolDat>();
            return false;
        }

        if (byWeightHardmode is null)
        {
            byWeightHardmode = new();
            foreach (var item in Items)
            {
                var itemKey = item.WeightHardmode;

                if (!byWeightHardmode.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWeightHardmode.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWeightHardmode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DropPoolDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DropPoolDat"/> with <see cref="DropPoolDat.byWeightHardmode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DropPoolDat>> GetManyToManyByWeightHardmode(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DropPoolDat>>();
        }

        var items = new List<ResultItem<int, DropPoolDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeightHardmode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DropPoolDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DropPoolDat[] Load()
    {
        const string filePath = "Data/DropPool.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DropPoolDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Group
            (var groupLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var tempunknown12Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown12Loading = tempunknown12Loading.AsReadOnly();

            // loading WeightHardmode
            (var weighthardmodeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DropPoolDat()
            {
                Group = groupLoading,
                Weight = weightLoading,
                Unknown12 = unknown12Loading,
                WeightHardmode = weighthardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
