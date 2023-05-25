using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HellscapePassiveTreeDat"/> related data and helper methods.
/// </summary>
public sealed class HellscapePassiveTreeRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HellscapePassiveTreeDat> Items { get; }

    private Dictionary<string, List<HellscapePassiveTreeDat>>? byId;
    private Dictionary<int, List<HellscapePassiveTreeDat>>? byAllocationsRequired;
    private Dictionary<int, List<HellscapePassiveTreeDat>>? byPassives;

    /// <summary>
    /// Initializes a new instance of the <see cref="HellscapePassiveTreeRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HellscapePassiveTreeRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HellscapePassiveTreeDat"/> with <see cref="HellscapePassiveTreeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HellscapePassiveTreeDat? item)
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
    /// Tries to get <see cref="HellscapePassiveTreeDat"/> with <see cref="HellscapePassiveTreeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HellscapePassiveTreeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapePassiveTreeDat>();
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
            items = Array.Empty<HellscapePassiveTreeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapePassiveTreeDat"/> with <see cref="HellscapePassiveTreeDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HellscapePassiveTreeDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HellscapePassiveTreeDat>>();
        }

        var items = new List<ResultItem<string, HellscapePassiveTreeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HellscapePassiveTreeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapePassiveTreeDat"/> with <see cref="HellscapePassiveTreeDat.AllocationsRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAllocationsRequired(int? key, out HellscapePassiveTreeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAllocationsRequired(key, out var items))
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
    /// Tries to get <see cref="HellscapePassiveTreeDat"/> with <see cref="HellscapePassiveTreeDat.AllocationsRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAllocationsRequired(int? key, out IReadOnlyList<HellscapePassiveTreeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapePassiveTreeDat>();
            return false;
        }

        if (byAllocationsRequired is null)
        {
            byAllocationsRequired = new();
            foreach (var item in Items)
            {
                var itemKey = item.AllocationsRequired;

                if (!byAllocationsRequired.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAllocationsRequired.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAllocationsRequired.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapePassiveTreeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapePassiveTreeDat"/> with <see cref="HellscapePassiveTreeDat.byAllocationsRequired"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapePassiveTreeDat>> GetManyToManyByAllocationsRequired(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapePassiveTreeDat>>();
        }

        var items = new List<ResultItem<int, HellscapePassiveTreeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAllocationsRequired(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapePassiveTreeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapePassiveTreeDat"/> with <see cref="HellscapePassiveTreeDat.Passives"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassives(int? key, out HellscapePassiveTreeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassives(key, out var items))
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
    /// Tries to get <see cref="HellscapePassiveTreeDat"/> with <see cref="HellscapePassiveTreeDat.Passives"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassives(int? key, out IReadOnlyList<HellscapePassiveTreeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapePassiveTreeDat>();
            return false;
        }

        if (byPassives is null)
        {
            byPassives = new();
            foreach (var item in Items)
            {
                var itemKey = item.Passives;
                foreach (var listKey in itemKey)
                {
                    if (!byPassives.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPassives.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPassives.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapePassiveTreeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapePassiveTreeDat"/> with <see cref="HellscapePassiveTreeDat.byPassives"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapePassiveTreeDat>> GetManyToManyByPassives(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapePassiveTreeDat>>();
        }

        var items = new List<ResultItem<int, HellscapePassiveTreeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassives(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapePassiveTreeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HellscapePassiveTreeDat[] Load()
    {
        const string filePath = "Data/HellscapePassiveTree.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapePassiveTreeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AllocationsRequired
            (var allocationsrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Passives
            (var temppassivesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var passivesLoading = temppassivesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapePassiveTreeDat()
            {
                Id = idLoading,
                AllocationsRequired = allocationsrequiredLoading,
                Passives = passivesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
