using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasBaseTypeDropsDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasBaseTypeDropsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasBaseTypeDropsDat> Items { get; }

    private Dictionary<string, List<AtlasBaseTypeDropsDat>>? byId;
    private Dictionary<int, List<AtlasBaseTypeDropsDat>>? byAtlasRegionsKey;
    private Dictionary<int, List<AtlasBaseTypeDropsDat>>? byMinTier;
    private Dictionary<int, List<AtlasBaseTypeDropsDat>>? byMaxTier;
    private Dictionary<int, List<AtlasBaseTypeDropsDat>>? bySpawnWeight_TagsKeys;
    private Dictionary<int, List<AtlasBaseTypeDropsDat>>? bySpawnWeight_Values;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasBaseTypeDropsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasBaseTypeDropsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AtlasBaseTypeDropsDat? item)
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
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AtlasBaseTypeDropsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
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
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasBaseTypeDropsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasBaseTypeDropsDat>>();
        }

        var items = new List<ResultItem<string, AtlasBaseTypeDropsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasBaseTypeDropsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.AtlasRegionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAtlasRegionsKey(int? key, out AtlasBaseTypeDropsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAtlasRegionsKey(key, out var items))
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
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.AtlasRegionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAtlasRegionsKey(int? key, out IReadOnlyList<AtlasBaseTypeDropsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        if (byAtlasRegionsKey is null)
        {
            byAtlasRegionsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AtlasRegionsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAtlasRegionsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAtlasRegionsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAtlasRegionsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.byAtlasRegionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasBaseTypeDropsDat>> GetManyToManyByAtlasRegionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasBaseTypeDropsDat>>();
        }

        var items = new List<ResultItem<int, AtlasBaseTypeDropsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAtlasRegionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasBaseTypeDropsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.MinTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinTier(int? key, out AtlasBaseTypeDropsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinTier(key, out var items))
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
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.MinTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinTier(int? key, out IReadOnlyList<AtlasBaseTypeDropsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        if (byMinTier is null)
        {
            byMinTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinTier;

                if (!byMinTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.byMinTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasBaseTypeDropsDat>> GetManyToManyByMinTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasBaseTypeDropsDat>>();
        }

        var items = new List<ResultItem<int, AtlasBaseTypeDropsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasBaseTypeDropsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.MaxTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxTier(int? key, out AtlasBaseTypeDropsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxTier(key, out var items))
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
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.MaxTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxTier(int? key, out IReadOnlyList<AtlasBaseTypeDropsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        if (byMaxTier is null)
        {
            byMaxTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxTier;

                if (!byMaxTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.byMaxTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasBaseTypeDropsDat>> GetManyToManyByMaxTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasBaseTypeDropsDat>>();
        }

        var items = new List<ResultItem<int, AtlasBaseTypeDropsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasBaseTypeDropsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_TagsKeys(int? key, out AtlasBaseTypeDropsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight_TagsKeys(key, out var items))
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
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_TagsKeys(int? key, out IReadOnlyList<AtlasBaseTypeDropsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        if (bySpawnWeight_TagsKeys is null)
        {
            bySpawnWeight_TagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight_TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!bySpawnWeight_TagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpawnWeight_TagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpawnWeight_TagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.bySpawnWeight_TagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasBaseTypeDropsDat>> GetManyToManyBySpawnWeight_TagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasBaseTypeDropsDat>>();
        }

        var items = new List<ResultItem<int, AtlasBaseTypeDropsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_TagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasBaseTypeDropsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_Values(int? key, out AtlasBaseTypeDropsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight_Values(key, out var items))
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
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_Values(int? key, out IReadOnlyList<AtlasBaseTypeDropsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        if (bySpawnWeight_Values is null)
        {
            bySpawnWeight_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight_Values;
                foreach (var listKey in itemKey)
                {
                    if (!bySpawnWeight_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpawnWeight_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpawnWeight_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasBaseTypeDropsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasBaseTypeDropsDat"/> with <see cref="AtlasBaseTypeDropsDat.bySpawnWeight_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasBaseTypeDropsDat>> GetManyToManyBySpawnWeight_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasBaseTypeDropsDat>>();
        }

        var items = new List<ResultItem<int, AtlasBaseTypeDropsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasBaseTypeDropsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasBaseTypeDropsDat[] Load()
    {
        const string filePath = "Data/AtlasBaseTypeDrops.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasBaseTypeDropsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AtlasRegionsKey
            (var atlasregionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MinTier
            (var mintierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxTier
            (var maxtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasBaseTypeDropsDat()
            {
                Id = idLoading,
                AtlasRegionsKey = atlasregionskeyLoading,
                MinTier = mintierLoading,
                MaxTier = maxtierLoading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
