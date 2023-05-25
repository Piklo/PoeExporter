using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LabyrinthSecretEffectsDat"/> related data and helper methods.
/// </summary>
public sealed class LabyrinthSecretEffectsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LabyrinthSecretEffectsDat> Items { get; }

    private Dictionary<string, List<LabyrinthSecretEffectsDat>>? byId;
    private Dictionary<int, List<LabyrinthSecretEffectsDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<LabyrinthSecretEffectsDat>>? byBuff_BuffDefinitionsKey;
    private Dictionary<int, List<LabyrinthSecretEffectsDat>>? byBuff_StatValues;
    private Dictionary<string, List<LabyrinthSecretEffectsDat>>? byOTFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="LabyrinthSecretEffectsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LabyrinthSecretEffectsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out LabyrinthSecretEffectsDat? item)
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
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<LabyrinthSecretEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretEffectsDat>();
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
            items = Array.Empty<LabyrinthSecretEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthSecretEffectsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthSecretEffectsDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthSecretEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthSecretEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out LabyrinthSecretEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<LabyrinthSecretEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretEffectsDat>();
            return false;
        }

        if (byMonsterVarietiesKey is null)
        {
            byMonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretEffectsDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretEffectsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.Buff_BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuff_BuffDefinitionsKey(int? key, out LabyrinthSecretEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuff_BuffDefinitionsKey(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.Buff_BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuff_BuffDefinitionsKey(int? key, out IReadOnlyList<LabyrinthSecretEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretEffectsDat>();
            return false;
        }

        if (byBuff_BuffDefinitionsKey is null)
        {
            byBuff_BuffDefinitionsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Buff_BuffDefinitionsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuff_BuffDefinitionsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuff_BuffDefinitionsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuff_BuffDefinitionsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.byBuff_BuffDefinitionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretEffectsDat>> GetManyToManyByBuff_BuffDefinitionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretEffectsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuff_BuffDefinitionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.Buff_StatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuff_StatValues(int? key, out LabyrinthSecretEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuff_StatValues(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.Buff_StatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuff_StatValues(int? key, out IReadOnlyList<LabyrinthSecretEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretEffectsDat>();
            return false;
        }

        if (byBuff_StatValues is null)
        {
            byBuff_StatValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.Buff_StatValues;
                foreach (var listKey in itemKey)
                {
                    if (!byBuff_StatValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBuff_StatValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBuff_StatValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSecretEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.byBuff_StatValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSecretEffectsDat>> GetManyToManyByBuff_StatValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSecretEffectsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSecretEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuff_StatValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSecretEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.OTFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOTFile(string? key, out LabyrinthSecretEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOTFile(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.OTFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOTFile(string? key, out IReadOnlyList<LabyrinthSecretEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSecretEffectsDat>();
            return false;
        }

        if (byOTFile is null)
        {
            byOTFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.OTFile;

                if (!byOTFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOTFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOTFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LabyrinthSecretEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSecretEffectsDat"/> with <see cref="LabyrinthSecretEffectsDat.byOTFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthSecretEffectsDat>> GetManyToManyByOTFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthSecretEffectsDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthSecretEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOTFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthSecretEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LabyrinthSecretEffectsDat[] Load()
    {
        const string filePath = "Data/LabyrinthSecretEffects.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthSecretEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Buff_BuffDefinitionsKey
            (var buff_buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Buff_StatValues
            (var tempbuff_statvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var buff_statvaluesLoading = tempbuff_statvaluesLoading.AsReadOnly();

            // loading OTFile
            (var otfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthSecretEffectsDat()
            {
                Id = idLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                Buff_BuffDefinitionsKey = buff_buffdefinitionskeyLoading,
                Buff_StatValues = buff_statvaluesLoading,
                OTFile = otfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
