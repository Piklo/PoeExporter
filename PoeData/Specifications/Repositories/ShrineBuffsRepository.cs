using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ShrineBuffsDat"/> related data and helper methods.
/// </summary>
public sealed class ShrineBuffsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ShrineBuffsDat> Items { get; }

    private Dictionary<string, List<ShrineBuffsDat>>? byId;
    private Dictionary<int, List<ShrineBuffsDat>>? byBuffStatValues;
    private Dictionary<int, List<ShrineBuffsDat>>? byBuffDefinitionsKey;
    private Dictionary<int, List<ShrineBuffsDat>>? byBuffVisual;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShrineBuffsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ShrineBuffsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ShrineBuffsDat? item)
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
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ShrineBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrineBuffsDat>();
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
            items = Array.Empty<ShrineBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShrineBuffsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShrineBuffsDat>>();
        }

        var items = new List<ResultItem<string, ShrineBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShrineBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.BuffStatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffStatValues(int? key, out ShrineBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffStatValues(key, out var items))
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
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.BuffStatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffStatValues(int? key, out IReadOnlyList<ShrineBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrineBuffsDat>();
            return false;
        }

        if (byBuffStatValues is null)
        {
            byBuffStatValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffStatValues;
                foreach (var listKey in itemKey)
                {
                    if (!byBuffStatValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBuffStatValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBuffStatValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrineBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.byBuffStatValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrineBuffsDat>> GetManyToManyByBuffStatValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrineBuffsDat>>();
        }

        var items = new List<ResultItem<int, ShrineBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffStatValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrineBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffDefinitionsKey(int? key, out ShrineBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffDefinitionsKey(key, out var items))
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
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffDefinitionsKey(int? key, out IReadOnlyList<ShrineBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrineBuffsDat>();
            return false;
        }

        if (byBuffDefinitionsKey is null)
        {
            byBuffDefinitionsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffDefinitionsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffDefinitionsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffDefinitionsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffDefinitionsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrineBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.byBuffDefinitionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrineBuffsDat>> GetManyToManyByBuffDefinitionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrineBuffsDat>>();
        }

        var items = new List<ResultItem<int, ShrineBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffDefinitionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrineBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.BuffVisual"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisual(int? key, out ShrineBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffVisual(key, out var items))
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
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.BuffVisual"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisual(int? key, out IReadOnlyList<ShrineBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrineBuffsDat>();
            return false;
        }

        if (byBuffVisual is null)
        {
            byBuffVisual = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffVisual;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffVisual.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffVisual.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffVisual.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrineBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineBuffsDat"/> with <see cref="ShrineBuffsDat.byBuffVisual"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrineBuffsDat>> GetManyToManyByBuffVisual(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrineBuffsDat>>();
        }

        var items = new List<ResultItem<int, ShrineBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisual(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrineBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ShrineBuffsDat[] Load()
    {
        const string filePath = "Data/ShrineBuffs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShrineBuffsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffStatValues
            (var tempbuffstatvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var buffstatvaluesLoading = tempbuffstatvaluesLoading.AsReadOnly();

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BuffVisual
            (var buffvisualLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShrineBuffsDat()
            {
                Id = idLoading,
                BuffStatValues = buffstatvaluesLoading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                BuffVisual = buffvisualLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
