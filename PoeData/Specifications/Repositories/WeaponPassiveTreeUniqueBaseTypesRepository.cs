using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="WeaponPassiveTreeUniqueBaseTypesDat"/> related data and helper methods.
/// </summary>
public sealed class WeaponPassiveTreeUniqueBaseTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<WeaponPassiveTreeUniqueBaseTypesDat> Items { get; }

    private Dictionary<int, List<WeaponPassiveTreeUniqueBaseTypesDat>>? byUniqueBase;
    private Dictionary<int, List<WeaponPassiveTreeUniqueBaseTypesDat>>? byUnknown16;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeaponPassiveTreeUniqueBaseTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal WeaponPassiveTreeUniqueBaseTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeUniqueBaseTypesDat"/> with <see cref="WeaponPassiveTreeUniqueBaseTypesDat.UniqueBase"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUniqueBase(int? key, out WeaponPassiveTreeUniqueBaseTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUniqueBase(key, out var items))
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
    /// Tries to get <see cref="WeaponPassiveTreeUniqueBaseTypesDat"/> with <see cref="WeaponPassiveTreeUniqueBaseTypesDat.UniqueBase"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUniqueBase(int? key, out IReadOnlyList<WeaponPassiveTreeUniqueBaseTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponPassiveTreeUniqueBaseTypesDat>();
            return false;
        }

        if (byUniqueBase is null)
        {
            byUniqueBase = new();
            foreach (var item in Items)
            {
                var itemKey = item.UniqueBase;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUniqueBase.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUniqueBase.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUniqueBase.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponPassiveTreeUniqueBaseTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeUniqueBaseTypesDat"/> with <see cref="WeaponPassiveTreeUniqueBaseTypesDat.byUniqueBase"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponPassiveTreeUniqueBaseTypesDat>> GetManyToManyByUniqueBase(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponPassiveTreeUniqueBaseTypesDat>>();
        }

        var items = new List<ResultItem<int, WeaponPassiveTreeUniqueBaseTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUniqueBase(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponPassiveTreeUniqueBaseTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeUniqueBaseTypesDat"/> with <see cref="WeaponPassiveTreeUniqueBaseTypesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out WeaponPassiveTreeUniqueBaseTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="WeaponPassiveTreeUniqueBaseTypesDat"/> with <see cref="WeaponPassiveTreeUniqueBaseTypesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<WeaponPassiveTreeUniqueBaseTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponPassiveTreeUniqueBaseTypesDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponPassiveTreeUniqueBaseTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeUniqueBaseTypesDat"/> with <see cref="WeaponPassiveTreeUniqueBaseTypesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponPassiveTreeUniqueBaseTypesDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponPassiveTreeUniqueBaseTypesDat>>();
        }

        var items = new List<ResultItem<int, WeaponPassiveTreeUniqueBaseTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponPassiveTreeUniqueBaseTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private WeaponPassiveTreeUniqueBaseTypesDat[] Load()
    {
        const string filePath = "Data/WeaponPassiveTreeUniqueBaseTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WeaponPassiveTreeUniqueBaseTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading UniqueBase
            (var uniquebaseLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WeaponPassiveTreeUniqueBaseTypesDat()
            {
                UniqueBase = uniquebaseLoading,
                Unknown16 = unknown16Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
