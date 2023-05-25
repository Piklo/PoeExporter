using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="WeaponClassesDat"/> related data and helper methods.
/// </summary>
public sealed class WeaponClassesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<WeaponClassesDat> Items { get; }

    private Dictionary<int, List<WeaponClassesDat>>? byItemClass;
    private Dictionary<int, List<WeaponClassesDat>>? byUnknown16;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeaponClassesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal WeaponClassesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="WeaponClassesDat"/> with <see cref="WeaponClassesDat.ItemClass"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClass(int? key, out WeaponClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemClass(key, out var items))
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
    /// Tries to get <see cref="WeaponClassesDat"/> with <see cref="WeaponClassesDat.ItemClass"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClass(int? key, out IReadOnlyList<WeaponClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponClassesDat>();
            return false;
        }

        if (byItemClass is null)
        {
            byItemClass = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemClass;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemClass.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemClass.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemClass.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponClassesDat"/> with <see cref="WeaponClassesDat.byItemClass"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponClassesDat>> GetManyToManyByItemClass(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponClassesDat>>();
        }

        var items = new List<ResultItem<int, WeaponClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClass(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponClassesDat"/> with <see cref="WeaponClassesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out WeaponClassesDat? item)
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
    /// Tries to get <see cref="WeaponClassesDat"/> with <see cref="WeaponClassesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<WeaponClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponClassesDat>();
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
            items = Array.Empty<WeaponClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponClassesDat"/> with <see cref="WeaponClassesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponClassesDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponClassesDat>>();
        }

        var items = new List<ResultItem<int, WeaponClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private WeaponClassesDat[] Load()
    {
        const string filePath = "Data/WeaponClasses.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WeaponClassesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ItemClass
            (var itemclassLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WeaponClassesDat()
            {
                ItemClass = itemclassLoading,
                Unknown16 = unknown16Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
