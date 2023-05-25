using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="EclipseModsDat"/> related data and helper methods.
/// </summary>
public sealed class EclipseModsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<EclipseModsDat> Items { get; }

    private Dictionary<string, List<EclipseModsDat>>? byKey;
    private Dictionary<int, List<EclipseModsDat>>? bySpawnWeight_TagsKeys;
    private Dictionary<int, List<EclipseModsDat>>? bySpawnWeight_Values;
    private Dictionary<int, List<EclipseModsDat>>? byModsKey;
    private Dictionary<int, List<EclipseModsDat>>? byMinLevel;
    private Dictionary<int, List<EclipseModsDat>>? byMaxLevel;
    private Dictionary<bool, List<EclipseModsDat>>? byIsPrefix;

    /// <summary>
    /// Initializes a new instance of the <see cref="EclipseModsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal EclipseModsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.Key"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKey(string? key, out EclipseModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKey(key, out var items))
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
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.Key"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKey(string? key, out IReadOnlyList<EclipseModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        if (byKey is null)
        {
            byKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Key;

                if (!byKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byKey.TryGetValue(key, out var temp))
        {
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.byKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EclipseModsDat>> GetManyToManyByKey(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EclipseModsDat>>();
        }

        var items = new List<ResultItem<string, EclipseModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EclipseModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_TagsKeys(int? key, out EclipseModsDat? item)
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
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_TagsKeys(int? key, out IReadOnlyList<EclipseModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EclipseModsDat>();
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
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.bySpawnWeight_TagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EclipseModsDat>> GetManyToManyBySpawnWeight_TagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EclipseModsDat>>();
        }

        var items = new List<ResultItem<int, EclipseModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_TagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EclipseModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_Values(int? key, out EclipseModsDat? item)
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
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_Values(int? key, out IReadOnlyList<EclipseModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EclipseModsDat>();
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
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.bySpawnWeight_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EclipseModsDat>> GetManyToManyBySpawnWeight_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EclipseModsDat>>();
        }

        var items = new List<ResultItem<int, EclipseModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EclipseModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKey(int? key, out EclipseModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKey(key, out var items))
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
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKey(int? key, out IReadOnlyList<EclipseModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        if (byModsKey is null)
        {
            byModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.byModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EclipseModsDat>> GetManyToManyByModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EclipseModsDat>>();
        }

        var items = new List<ResultItem<int, EclipseModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EclipseModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out EclipseModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLevel(key, out var items))
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
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<EclipseModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        if (byMinLevel is null)
        {
            byMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLevel;

                if (!byMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EclipseModsDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EclipseModsDat>>();
        }

        var items = new List<ResultItem<int, EclipseModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EclipseModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out EclipseModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxLevel(key, out var items))
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
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<EclipseModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        if (byMaxLevel is null)
        {
            byMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxLevel;

                if (!byMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EclipseModsDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EclipseModsDat>>();
        }

        var items = new List<ResultItem<int, EclipseModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EclipseModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.IsPrefix"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsPrefix(bool? key, out EclipseModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsPrefix(key, out var items))
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
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.IsPrefix"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsPrefix(bool? key, out IReadOnlyList<EclipseModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        if (byIsPrefix is null)
        {
            byIsPrefix = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsPrefix;

                if (!byIsPrefix.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsPrefix.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsPrefix.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EclipseModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EclipseModsDat"/> with <see cref="EclipseModsDat.byIsPrefix"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EclipseModsDat>> GetManyToManyByIsPrefix(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EclipseModsDat>>();
        }

        var items = new List<ResultItem<bool, EclipseModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsPrefix(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EclipseModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private EclipseModsDat[] Load()
    {
        const string filePath = "Data/EclipseMods.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EclipseModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Key
            (var keyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsPrefix
            (var isprefixLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EclipseModsDat()
            {
                Key = keyLoading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                ModsKey = modskeyLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                IsPrefix = isprefixLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
