using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BlightTowerAurasDat"/> related data and helper methods.
/// </summary>
public sealed class BlightTowerAurasRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BlightTowerAurasDat> Items { get; }

    private Dictionary<int, List<BlightTowerAurasDat>>? byId;
    private Dictionary<int, List<BlightTowerAurasDat>>? byBuffDefinitionsKey;
    private Dictionary<int, List<BlightTowerAurasDat>>? byUnknown20;
    private Dictionary<int, List<BlightTowerAurasDat>>? byMiscAnimatedKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightTowerAurasRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BlightTowerAurasRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(int? key, out BlightTowerAurasDat? item)
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
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(int? key, out IReadOnlyList<BlightTowerAurasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowerAurasDat>();
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

        if (!byId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightTowerAurasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowerAurasDat>> GetManyToManyById(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowerAurasDat>>();
        }

        var items = new List<ResultItem<int, BlightTowerAurasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowerAurasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffDefinitionsKey(int? key, out BlightTowerAurasDat? item)
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
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffDefinitionsKey(int? key, out IReadOnlyList<BlightTowerAurasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowerAurasDat>();
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
            items = Array.Empty<BlightTowerAurasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.byBuffDefinitionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowerAurasDat>> GetManyToManyByBuffDefinitionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowerAurasDat>>();
        }

        var items = new List<ResultItem<int, BlightTowerAurasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffDefinitionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowerAurasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out BlightTowerAurasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown20(key, out var items))
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
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<BlightTowerAurasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowerAurasDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;

                if (!byUnknown20.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightTowerAurasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowerAurasDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowerAurasDat>>();
        }

        var items = new List<ResultItem<int, BlightTowerAurasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowerAurasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.MiscAnimatedKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey(int? key, out BlightTowerAurasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey(key, out var items))
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
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.MiscAnimatedKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey(int? key, out IReadOnlyList<BlightTowerAurasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightTowerAurasDat>();
            return false;
        }

        if (byMiscAnimatedKey is null)
        {
            byMiscAnimatedKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightTowerAurasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightTowerAurasDat"/> with <see cref="BlightTowerAurasDat.byMiscAnimatedKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightTowerAurasDat>> GetManyToManyByMiscAnimatedKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightTowerAurasDat>>();
        }

        var items = new List<ResultItem<int, BlightTowerAurasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightTowerAurasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BlightTowerAurasDat[] Load()
    {
        const string filePath = "Data/BlightTowerAuras.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightTowerAurasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscAnimatedKey
            (var miscanimatedkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightTowerAurasDat()
            {
                Id = idLoading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                Unknown20 = unknown20Loading,
                MiscAnimatedKey = miscanimatedkeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
