using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LabyrinthTrinketsDat"/> related data and helper methods.
/// </summary>
public sealed class LabyrinthTrinketsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LabyrinthTrinketsDat> Items { get; }

    private Dictionary<int, List<LabyrinthTrinketsDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<LabyrinthTrinketsDat>>? byLabyrinthSecretsKey;
    private Dictionary<int, List<LabyrinthTrinketsDat>>? byBuff_BuffDefinitionsKey;
    private Dictionary<int, List<LabyrinthTrinketsDat>>? byBuff_StatValues;

    /// <summary>
    /// Initializes a new instance of the <see cref="LabyrinthTrinketsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LabyrinthTrinketsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out LabyrinthTrinketsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<LabyrinthTrinketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthTrinketsDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthTrinketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthTrinketsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthTrinketsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthTrinketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthTrinketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.LabyrinthSecretsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthSecretsKey(int? key, out LabyrinthTrinketsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthSecretsKey(key, out var items))
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
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.LabyrinthSecretsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthSecretsKey(int? key, out IReadOnlyList<LabyrinthTrinketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthTrinketsDat>();
            return false;
        }

        if (byLabyrinthSecretsKey is null)
        {
            byLabyrinthSecretsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthSecretsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byLabyrinthSecretsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byLabyrinthSecretsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byLabyrinthSecretsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthTrinketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.byLabyrinthSecretsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthTrinketsDat>> GetManyToManyByLabyrinthSecretsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthTrinketsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthTrinketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthSecretsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthTrinketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.Buff_BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuff_BuffDefinitionsKey(int? key, out LabyrinthTrinketsDat? item)
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
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.Buff_BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuff_BuffDefinitionsKey(int? key, out IReadOnlyList<LabyrinthTrinketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthTrinketsDat>();
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
            items = Array.Empty<LabyrinthTrinketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.byBuff_BuffDefinitionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthTrinketsDat>> GetManyToManyByBuff_BuffDefinitionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthTrinketsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthTrinketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuff_BuffDefinitionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthTrinketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.Buff_StatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuff_StatValues(int? key, out LabyrinthTrinketsDat? item)
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
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.Buff_StatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuff_StatValues(int? key, out IReadOnlyList<LabyrinthTrinketsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthTrinketsDat>();
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
            items = Array.Empty<LabyrinthTrinketsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthTrinketsDat"/> with <see cref="LabyrinthTrinketsDat.byBuff_StatValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthTrinketsDat>> GetManyToManyByBuff_StatValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthTrinketsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthTrinketsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuff_StatValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthTrinketsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LabyrinthTrinketsDat[] Load()
    {
        const string filePath = "Data/LabyrinthTrinkets.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthTrinketsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading LabyrinthSecretsKey
            (var templabyrinthsecretskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsecretskeyLoading = templabyrinthsecretskeyLoading.AsReadOnly();

            // loading Buff_BuffDefinitionsKey
            (var buff_buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Buff_StatValues
            (var tempbuff_statvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var buff_statvaluesLoading = tempbuff_statvaluesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthTrinketsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                LabyrinthSecretsKey = labyrinthsecretskeyLoading,
                Buff_BuffDefinitionsKey = buff_buffdefinitionskeyLoading,
                Buff_StatValues = buff_statvaluesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
