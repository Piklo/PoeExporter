using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestInfrastructureDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestInfrastructureRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestInfrastructureDat> Items { get; }

    private Dictionary<string, List<HarvestInfrastructureDat>>? byObject;
    private Dictionary<int, List<HarvestInfrastructureDat>>? byUnknown8;
    private Dictionary<int, List<HarvestInfrastructureDat>>? byClientStringsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestInfrastructureRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestInfrastructureRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestInfrastructureDat"/> with <see cref="HarvestInfrastructureDat.Object"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByObject(string? key, out HarvestInfrastructureDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByObject(key, out var items))
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
    /// Tries to get <see cref="HarvestInfrastructureDat"/> with <see cref="HarvestInfrastructureDat.Object"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByObject(string? key, out IReadOnlyList<HarvestInfrastructureDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestInfrastructureDat>();
            return false;
        }

        if (byObject is null)
        {
            byObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.Object;

                if (!byObject.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byObject.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byObject.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HarvestInfrastructureDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestInfrastructureDat"/> with <see cref="HarvestInfrastructureDat.byObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestInfrastructureDat>> GetManyToManyByObject(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestInfrastructureDat>>();
        }

        var items = new List<ResultItem<string, HarvestInfrastructureDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestInfrastructureDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestInfrastructureDat"/> with <see cref="HarvestInfrastructureDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out HarvestInfrastructureDat? item)
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
    /// Tries to get <see cref="HarvestInfrastructureDat"/> with <see cref="HarvestInfrastructureDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<HarvestInfrastructureDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestInfrastructureDat>();
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
            items = Array.Empty<HarvestInfrastructureDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestInfrastructureDat"/> with <see cref="HarvestInfrastructureDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestInfrastructureDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestInfrastructureDat>>();
        }

        var items = new List<ResultItem<int, HarvestInfrastructureDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestInfrastructureDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestInfrastructureDat"/> with <see cref="HarvestInfrastructureDat.ClientStringsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClientStringsKey(int? key, out HarvestInfrastructureDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClientStringsKey(key, out var items))
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
    /// Tries to get <see cref="HarvestInfrastructureDat"/> with <see cref="HarvestInfrastructureDat.ClientStringsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClientStringsKey(int? key, out IReadOnlyList<HarvestInfrastructureDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestInfrastructureDat>();
            return false;
        }

        if (byClientStringsKey is null)
        {
            byClientStringsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ClientStringsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byClientStringsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byClientStringsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byClientStringsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestInfrastructureDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestInfrastructureDat"/> with <see cref="HarvestInfrastructureDat.byClientStringsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestInfrastructureDat>> GetManyToManyByClientStringsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestInfrastructureDat>>();
        }

        var items = new List<ResultItem<int, HarvestInfrastructureDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClientStringsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestInfrastructureDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestInfrastructureDat[] Load()
    {
        const string filePath = "Data/HarvestInfrastructure.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestInfrastructureDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Object
            (var objectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ClientStringsKey
            (var clientstringskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestInfrastructureDat()
            {
                Object = objectLoading,
                Unknown8 = unknown8Loading,
                ClientStringsKey = clientstringskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
