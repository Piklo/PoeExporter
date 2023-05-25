using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="FixedHideoutDoodadTypesDat"/> related data and helper methods.
/// </summary>
public sealed class FixedHideoutDoodadTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<FixedHideoutDoodadTypesDat> Items { get; }

    private Dictionary<string, List<FixedHideoutDoodadTypesDat>>? byId;
    private Dictionary<int, List<FixedHideoutDoodadTypesDat>>? byHideoutDoodadsKeys;
    private Dictionary<int, List<FixedHideoutDoodadTypesDat>>? byBaseTypeHideoutDoodadsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="FixedHideoutDoodadTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal FixedHideoutDoodadTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="FixedHideoutDoodadTypesDat"/> with <see cref="FixedHideoutDoodadTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out FixedHideoutDoodadTypesDat? item)
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
    /// Tries to get <see cref="FixedHideoutDoodadTypesDat"/> with <see cref="FixedHideoutDoodadTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<FixedHideoutDoodadTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FixedHideoutDoodadTypesDat>();
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
            items = Array.Empty<FixedHideoutDoodadTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FixedHideoutDoodadTypesDat"/> with <see cref="FixedHideoutDoodadTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, FixedHideoutDoodadTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, FixedHideoutDoodadTypesDat>>();
        }

        var items = new List<ResultItem<string, FixedHideoutDoodadTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, FixedHideoutDoodadTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FixedHideoutDoodadTypesDat"/> with <see cref="FixedHideoutDoodadTypesDat.HideoutDoodadsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHideoutDoodadsKeys(int? key, out FixedHideoutDoodadTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHideoutDoodadsKeys(key, out var items))
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
    /// Tries to get <see cref="FixedHideoutDoodadTypesDat"/> with <see cref="FixedHideoutDoodadTypesDat.HideoutDoodadsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHideoutDoodadsKeys(int? key, out IReadOnlyList<FixedHideoutDoodadTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FixedHideoutDoodadTypesDat>();
            return false;
        }

        if (byHideoutDoodadsKeys is null)
        {
            byHideoutDoodadsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.HideoutDoodadsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byHideoutDoodadsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byHideoutDoodadsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byHideoutDoodadsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FixedHideoutDoodadTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FixedHideoutDoodadTypesDat"/> with <see cref="FixedHideoutDoodadTypesDat.byHideoutDoodadsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FixedHideoutDoodadTypesDat>> GetManyToManyByHideoutDoodadsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FixedHideoutDoodadTypesDat>>();
        }

        var items = new List<ResultItem<int, FixedHideoutDoodadTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHideoutDoodadsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FixedHideoutDoodadTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FixedHideoutDoodadTypesDat"/> with <see cref="FixedHideoutDoodadTypesDat.BaseTypeHideoutDoodadsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseTypeHideoutDoodadsKey(int? key, out FixedHideoutDoodadTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseTypeHideoutDoodadsKey(key, out var items))
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
    /// Tries to get <see cref="FixedHideoutDoodadTypesDat"/> with <see cref="FixedHideoutDoodadTypesDat.BaseTypeHideoutDoodadsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseTypeHideoutDoodadsKey(int? key, out IReadOnlyList<FixedHideoutDoodadTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FixedHideoutDoodadTypesDat>();
            return false;
        }

        if (byBaseTypeHideoutDoodadsKey is null)
        {
            byBaseTypeHideoutDoodadsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseTypeHideoutDoodadsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseTypeHideoutDoodadsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseTypeHideoutDoodadsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseTypeHideoutDoodadsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FixedHideoutDoodadTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FixedHideoutDoodadTypesDat"/> with <see cref="FixedHideoutDoodadTypesDat.byBaseTypeHideoutDoodadsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FixedHideoutDoodadTypesDat>> GetManyToManyByBaseTypeHideoutDoodadsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FixedHideoutDoodadTypesDat>>();
        }

        var items = new List<ResultItem<int, FixedHideoutDoodadTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseTypeHideoutDoodadsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FixedHideoutDoodadTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private FixedHideoutDoodadTypesDat[] Load()
    {
        const string filePath = "Data/FixedHideoutDoodadTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new FixedHideoutDoodadTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HideoutDoodadsKeys
            (var temphideoutdoodadskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var hideoutdoodadskeysLoading = temphideoutdoodadskeysLoading.AsReadOnly();

            // loading BaseTypeHideoutDoodadsKey
            (var basetypehideoutdoodadskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new FixedHideoutDoodadTypesDat()
            {
                Id = idLoading,
                HideoutDoodadsKeys = hideoutdoodadskeysLoading,
                BaseTypeHideoutDoodadsKey = basetypehideoutdoodadskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
