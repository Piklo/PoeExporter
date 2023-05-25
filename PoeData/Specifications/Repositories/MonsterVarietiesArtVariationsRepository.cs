using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterVarietiesArtVariationsDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterVarietiesArtVariationsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterVarietiesArtVariationsDat> Items { get; }

    private Dictionary<string, List<MonsterVarietiesArtVariationsDat>>? byId;
    private Dictionary<int, List<MonsterVarietiesArtVariationsDat>>? byUnknown8;
    private Dictionary<int, List<MonsterVarietiesArtVariationsDat>>? byUnknown12;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterVarietiesArtVariationsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterVarietiesArtVariationsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesArtVariationsDat"/> with <see cref="MonsterVarietiesArtVariationsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MonsterVarietiesArtVariationsDat? item)
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
    /// Tries to get <see cref="MonsterVarietiesArtVariationsDat"/> with <see cref="MonsterVarietiesArtVariationsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MonsterVarietiesArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesArtVariationsDat>();
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
            items = Array.Empty<MonsterVarietiesArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesArtVariationsDat"/> with <see cref="MonsterVarietiesArtVariationsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesArtVariationsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesArtVariationsDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesArtVariationsDat"/> with <see cref="MonsterVarietiesArtVariationsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out MonsterVarietiesArtVariationsDat? item)
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
    /// Tries to get <see cref="MonsterVarietiesArtVariationsDat"/> with <see cref="MonsterVarietiesArtVariationsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<MonsterVarietiesArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesArtVariationsDat>();
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
            items = Array.Empty<MonsterVarietiesArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesArtVariationsDat"/> with <see cref="MonsterVarietiesArtVariationsDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesArtVariationsDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesArtVariationsDat"/> with <see cref="MonsterVarietiesArtVariationsDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out MonsterVarietiesArtVariationsDat? item)
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
    /// Tries to get <see cref="MonsterVarietiesArtVariationsDat"/> with <see cref="MonsterVarietiesArtVariationsDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<MonsterVarietiesArtVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesArtVariationsDat>();
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
            items = Array.Empty<MonsterVarietiesArtVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesArtVariationsDat"/> with <see cref="MonsterVarietiesArtVariationsDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesArtVariationsDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesArtVariationsDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesArtVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesArtVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterVarietiesArtVariationsDat[] Load()
    {
        const string filePath = "Data/MonsterVarietiesArtVariations.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterVarietiesArtVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var tempunknown12Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown12Loading = tempunknown12Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterVarietiesArtVariationsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
