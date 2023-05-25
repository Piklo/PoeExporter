using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ComponentAttributeRequirementsDat"/> related data and helper methods.
/// </summary>
public sealed class ComponentAttributeRequirementsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ComponentAttributeRequirementsDat> Items { get; }

    private Dictionary<string, List<ComponentAttributeRequirementsDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<ComponentAttributeRequirementsDat>>? byReqStr;
    private Dictionary<int, List<ComponentAttributeRequirementsDat>>? byReqDex;
    private Dictionary<int, List<ComponentAttributeRequirementsDat>>? byReqInt;

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentAttributeRequirementsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ComponentAttributeRequirementsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(string? key, out ComponentAttributeRequirementsDat? item)
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
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(string? key, out IReadOnlyList<ComponentAttributeRequirementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ComponentAttributeRequirementsDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;

                if (!byBaseItemTypesKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ComponentAttributeRequirementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ComponentAttributeRequirementsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ComponentAttributeRequirementsDat>>();
        }

        var items = new List<ResultItem<string, ComponentAttributeRequirementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ComponentAttributeRequirementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.ReqStr"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByReqStr(int? key, out ComponentAttributeRequirementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByReqStr(key, out var items))
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
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.ReqStr"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByReqStr(int? key, out IReadOnlyList<ComponentAttributeRequirementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ComponentAttributeRequirementsDat>();
            return false;
        }

        if (byReqStr is null)
        {
            byReqStr = new();
            foreach (var item in Items)
            {
                var itemKey = item.ReqStr;

                if (!byReqStr.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byReqStr.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byReqStr.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ComponentAttributeRequirementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.byReqStr"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ComponentAttributeRequirementsDat>> GetManyToManyByReqStr(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ComponentAttributeRequirementsDat>>();
        }

        var items = new List<ResultItem<int, ComponentAttributeRequirementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByReqStr(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ComponentAttributeRequirementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.ReqDex"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByReqDex(int? key, out ComponentAttributeRequirementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByReqDex(key, out var items))
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
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.ReqDex"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByReqDex(int? key, out IReadOnlyList<ComponentAttributeRequirementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ComponentAttributeRequirementsDat>();
            return false;
        }

        if (byReqDex is null)
        {
            byReqDex = new();
            foreach (var item in Items)
            {
                var itemKey = item.ReqDex;

                if (!byReqDex.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byReqDex.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byReqDex.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ComponentAttributeRequirementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.byReqDex"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ComponentAttributeRequirementsDat>> GetManyToManyByReqDex(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ComponentAttributeRequirementsDat>>();
        }

        var items = new List<ResultItem<int, ComponentAttributeRequirementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByReqDex(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ComponentAttributeRequirementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.ReqInt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByReqInt(int? key, out ComponentAttributeRequirementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByReqInt(key, out var items))
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
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.ReqInt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByReqInt(int? key, out IReadOnlyList<ComponentAttributeRequirementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ComponentAttributeRequirementsDat>();
            return false;
        }

        if (byReqInt is null)
        {
            byReqInt = new();
            foreach (var item in Items)
            {
                var itemKey = item.ReqInt;

                if (!byReqInt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byReqInt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byReqInt.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ComponentAttributeRequirementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentAttributeRequirementsDat"/> with <see cref="ComponentAttributeRequirementsDat.byReqInt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ComponentAttributeRequirementsDat>> GetManyToManyByReqInt(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ComponentAttributeRequirementsDat>>();
        }

        var items = new List<ResultItem<int, ComponentAttributeRequirementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByReqInt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ComponentAttributeRequirementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ComponentAttributeRequirementsDat[] Load()
    {
        const string filePath = "Data/ComponentAttributeRequirements.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ComponentAttributeRequirementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ReqStr
            (var reqstrLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ReqDex
            (var reqdexLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ReqInt
            (var reqintLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ComponentAttributeRequirementsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                ReqStr = reqstrLoading,
                ReqDex = reqdexLoading,
                ReqInt = reqintLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
