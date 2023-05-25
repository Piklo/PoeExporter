using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasInfluenceOutcomesDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasInfluenceOutcomesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasInfluenceOutcomesDat> Items { get; }

    private Dictionary<string, List<AtlasInfluenceOutcomesDat>>? byId;
    private Dictionary<int, List<AtlasInfluenceOutcomesDat>>? byUnknown8;
    private Dictionary<int, List<AtlasInfluenceOutcomesDat>>? byType;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasInfluenceOutcomesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasInfluenceOutcomesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasInfluenceOutcomesDat"/> with <see cref="AtlasInfluenceOutcomesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AtlasInfluenceOutcomesDat? item)
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
    /// Tries to get <see cref="AtlasInfluenceOutcomesDat"/> with <see cref="AtlasInfluenceOutcomesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AtlasInfluenceOutcomesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasInfluenceOutcomesDat>();
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
            items = Array.Empty<AtlasInfluenceOutcomesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasInfluenceOutcomesDat"/> with <see cref="AtlasInfluenceOutcomesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasInfluenceOutcomesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasInfluenceOutcomesDat>>();
        }

        var items = new List<ResultItem<string, AtlasInfluenceOutcomesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasInfluenceOutcomesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasInfluenceOutcomesDat"/> with <see cref="AtlasInfluenceOutcomesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out AtlasInfluenceOutcomesDat? item)
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
    /// Tries to get <see cref="AtlasInfluenceOutcomesDat"/> with <see cref="AtlasInfluenceOutcomesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<AtlasInfluenceOutcomesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasInfluenceOutcomesDat>();
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
            items = Array.Empty<AtlasInfluenceOutcomesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasInfluenceOutcomesDat"/> with <see cref="AtlasInfluenceOutcomesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasInfluenceOutcomesDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasInfluenceOutcomesDat>>();
        }

        var items = new List<ResultItem<int, AtlasInfluenceOutcomesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasInfluenceOutcomesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasInfluenceOutcomesDat"/> with <see cref="AtlasInfluenceOutcomesDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByType(int? key, out AtlasInfluenceOutcomesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByType(key, out var items))
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
    /// Tries to get <see cref="AtlasInfluenceOutcomesDat"/> with <see cref="AtlasInfluenceOutcomesDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByType(int? key, out IReadOnlyList<AtlasInfluenceOutcomesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasInfluenceOutcomesDat>();
            return false;
        }

        if (byType is null)
        {
            byType = new();
            foreach (var item in Items)
            {
                var itemKey = item.Type;

                if (!byType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasInfluenceOutcomesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasInfluenceOutcomesDat"/> with <see cref="AtlasInfluenceOutcomesDat.byType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasInfluenceOutcomesDat>> GetManyToManyByType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasInfluenceOutcomesDat>>();
        }

        var items = new List<ResultItem<int, AtlasInfluenceOutcomesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasInfluenceOutcomesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasInfluenceOutcomesDat[] Load()
    {
        const string filePath = "Data/AtlasInfluenceOutcomes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasInfluenceOutcomesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasInfluenceOutcomesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Type = typeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
