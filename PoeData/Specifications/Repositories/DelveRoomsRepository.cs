using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DelveRoomsDat"/> related data and helper methods.
/// </summary>
public sealed class DelveRoomsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DelveRoomsDat> Items { get; }

    private Dictionary<int, List<DelveRoomsDat>>? byDelveBiomesKey;
    private Dictionary<int, List<DelveRoomsDat>>? byDelveFeaturesKey;
    private Dictionary<string, List<DelveRoomsDat>>? byARMFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveRoomsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DelveRoomsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DelveRoomsDat"/> with <see cref="DelveRoomsDat.DelveBiomesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDelveBiomesKey(int? key, out DelveRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDelveBiomesKey(key, out var items))
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
    /// Tries to get <see cref="DelveRoomsDat"/> with <see cref="DelveRoomsDat.DelveBiomesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDelveBiomesKey(int? key, out IReadOnlyList<DelveRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveRoomsDat>();
            return false;
        }

        if (byDelveBiomesKey is null)
        {
            byDelveBiomesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.DelveBiomesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDelveBiomesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDelveBiomesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDelveBiomesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveRoomsDat"/> with <see cref="DelveRoomsDat.byDelveBiomesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveRoomsDat>> GetManyToManyByDelveBiomesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveRoomsDat>>();
        }

        var items = new List<ResultItem<int, DelveRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDelveBiomesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveRoomsDat"/> with <see cref="DelveRoomsDat.DelveFeaturesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDelveFeaturesKey(int? key, out DelveRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDelveFeaturesKey(key, out var items))
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
    /// Tries to get <see cref="DelveRoomsDat"/> with <see cref="DelveRoomsDat.DelveFeaturesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDelveFeaturesKey(int? key, out IReadOnlyList<DelveRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveRoomsDat>();
            return false;
        }

        if (byDelveFeaturesKey is null)
        {
            byDelveFeaturesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.DelveFeaturesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDelveFeaturesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDelveFeaturesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDelveFeaturesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveRoomsDat"/> with <see cref="DelveRoomsDat.byDelveFeaturesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveRoomsDat>> GetManyToManyByDelveFeaturesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveRoomsDat>>();
        }

        var items = new List<ResultItem<int, DelveRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDelveFeaturesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveRoomsDat"/> with <see cref="DelveRoomsDat.ARMFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByARMFile(string? key, out DelveRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByARMFile(key, out var items))
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
    /// Tries to get <see cref="DelveRoomsDat"/> with <see cref="DelveRoomsDat.ARMFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByARMFile(string? key, out IReadOnlyList<DelveRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveRoomsDat>();
            return false;
        }

        if (byARMFile is null)
        {
            byARMFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.ARMFile;

                if (!byARMFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byARMFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byARMFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<DelveRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveRoomsDat"/> with <see cref="DelveRoomsDat.byARMFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DelveRoomsDat>> GetManyToManyByARMFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DelveRoomsDat>>();
        }

        var items = new List<ResultItem<string, DelveRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByARMFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DelveRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DelveRoomsDat[] Load()
    {
        const string filePath = "Data/DelveRooms.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveRoomsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading DelveBiomesKey
            (var delvebiomeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DelveFeaturesKey
            (var delvefeatureskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ARMFile
            (var armfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveRoomsDat()
            {
                DelveBiomesKey = delvebiomeskeyLoading,
                DelveFeaturesKey = delvefeatureskeyLoading,
                ARMFile = armfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
