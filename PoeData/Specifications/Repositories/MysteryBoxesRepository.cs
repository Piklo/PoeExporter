using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MysteryBoxesDat"/> related data and helper methods.
/// </summary>
public sealed class MysteryBoxesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MysteryBoxesDat> Items { get; }

    private Dictionary<int, List<MysteryBoxesDat>>? byBaseItemTypesKey;
    private Dictionary<string, List<MysteryBoxesDat>>? byBK2File;
    private Dictionary<string, List<MysteryBoxesDat>>? byBoxId;
    private Dictionary<string, List<MysteryBoxesDat>>? byBundleId;

    /// <summary>
    /// Initializes a new instance of the <see cref="MysteryBoxesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MysteryBoxesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out MysteryBoxesDat? item)
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
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<MysteryBoxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MysteryBoxesDat>();
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
            items = Array.Empty<MysteryBoxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MysteryBoxesDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MysteryBoxesDat>>();
        }

        var items = new List<ResultItem<int, MysteryBoxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MysteryBoxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.BK2File"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBK2File(string? key, out MysteryBoxesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBK2File(key, out var items))
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
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.BK2File"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBK2File(string? key, out IReadOnlyList<MysteryBoxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MysteryBoxesDat>();
            return false;
        }

        if (byBK2File is null)
        {
            byBK2File = new();
            foreach (var item in Items)
            {
                var itemKey = item.BK2File;

                if (!byBK2File.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBK2File.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBK2File.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MysteryBoxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.byBK2File"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MysteryBoxesDat>> GetManyToManyByBK2File(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MysteryBoxesDat>>();
        }

        var items = new List<ResultItem<string, MysteryBoxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBK2File(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MysteryBoxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.BoxId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBoxId(string? key, out MysteryBoxesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBoxId(key, out var items))
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
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.BoxId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBoxId(string? key, out IReadOnlyList<MysteryBoxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MysteryBoxesDat>();
            return false;
        }

        if (byBoxId is null)
        {
            byBoxId = new();
            foreach (var item in Items)
            {
                var itemKey = item.BoxId;

                if (!byBoxId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBoxId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBoxId.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MysteryBoxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.byBoxId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MysteryBoxesDat>> GetManyToManyByBoxId(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MysteryBoxesDat>>();
        }

        var items = new List<ResultItem<string, MysteryBoxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBoxId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MysteryBoxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.BundleId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBundleId(string? key, out MysteryBoxesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBundleId(key, out var items))
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
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.BundleId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBundleId(string? key, out IReadOnlyList<MysteryBoxesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MysteryBoxesDat>();
            return false;
        }

        if (byBundleId is null)
        {
            byBundleId = new();
            foreach (var item in Items)
            {
                var itemKey = item.BundleId;

                if (!byBundleId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBundleId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBundleId.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MysteryBoxesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MysteryBoxesDat"/> with <see cref="MysteryBoxesDat.byBundleId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MysteryBoxesDat>> GetManyToManyByBundleId(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MysteryBoxesDat>>();
        }

        var items = new List<ResultItem<string, MysteryBoxesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBundleId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MysteryBoxesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MysteryBoxesDat[] Load()
    {
        const string filePath = "Data/MysteryBoxes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MysteryBoxesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BK2File
            (var bk2fileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BoxId
            (var boxidLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BundleId
            (var bundleidLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MysteryBoxesDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                BK2File = bk2fileLoading,
                BoxId = boxidLoading,
                BundleId = bundleidLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
