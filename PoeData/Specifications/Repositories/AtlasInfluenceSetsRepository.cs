using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasInfluenceSetsDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasInfluenceSetsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasInfluenceSetsDat> Items { get; }

    private Dictionary<string, List<AtlasInfluenceSetsDat>>? byId;
    private Dictionary<int, List<AtlasInfluenceSetsDat>>? byInfluencePacks;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasInfluenceSetsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasInfluenceSetsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasInfluenceSetsDat"/> with <see cref="AtlasInfluenceSetsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AtlasInfluenceSetsDat? item)
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
    /// Tries to get <see cref="AtlasInfluenceSetsDat"/> with <see cref="AtlasInfluenceSetsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AtlasInfluenceSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasInfluenceSetsDat>();
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
            items = Array.Empty<AtlasInfluenceSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasInfluenceSetsDat"/> with <see cref="AtlasInfluenceSetsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasInfluenceSetsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasInfluenceSetsDat>>();
        }

        var items = new List<ResultItem<string, AtlasInfluenceSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasInfluenceSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasInfluenceSetsDat"/> with <see cref="AtlasInfluenceSetsDat.InfluencePacks"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInfluencePacks(int? key, out AtlasInfluenceSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInfluencePacks(key, out var items))
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
    /// Tries to get <see cref="AtlasInfluenceSetsDat"/> with <see cref="AtlasInfluenceSetsDat.InfluencePacks"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInfluencePacks(int? key, out IReadOnlyList<AtlasInfluenceSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasInfluenceSetsDat>();
            return false;
        }

        if (byInfluencePacks is null)
        {
            byInfluencePacks = new();
            foreach (var item in Items)
            {
                var itemKey = item.InfluencePacks;
                foreach (var listKey in itemKey)
                {
                    if (!byInfluencePacks.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byInfluencePacks.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byInfluencePacks.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasInfluenceSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasInfluenceSetsDat"/> with <see cref="AtlasInfluenceSetsDat.byInfluencePacks"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasInfluenceSetsDat>> GetManyToManyByInfluencePacks(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasInfluenceSetsDat>>();
        }

        var items = new List<ResultItem<int, AtlasInfluenceSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInfluencePacks(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasInfluenceSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasInfluenceSetsDat[] Load()
    {
        const string filePath = "Data/AtlasInfluenceSets.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasInfluenceSetsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading InfluencePacks
            (var tempinfluencepacksLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var influencepacksLoading = tempinfluencepacksLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasInfluenceSetsDat()
            {
                Id = idLoading,
                InfluencePacks = influencepacksLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
