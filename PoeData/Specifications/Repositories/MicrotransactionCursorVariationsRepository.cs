using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MicrotransactionCursorVariationsDat"/> related data and helper methods.
/// </summary>
public sealed class MicrotransactionCursorVariationsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MicrotransactionCursorVariationsDat> Items { get; }

    private Dictionary<int, List<MicrotransactionCursorVariationsDat>>? byUnknown0;
    private Dictionary<int, List<MicrotransactionCursorVariationsDat>>? byUnknown16;
    private Dictionary<int, List<MicrotransactionCursorVariationsDat>>? byUnknown32;

    /// <summary>
    /// Initializes a new instance of the <see cref="MicrotransactionCursorVariationsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MicrotransactionCursorVariationsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCursorVariationsDat"/> with <see cref="MicrotransactionCursorVariationsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out MicrotransactionCursorVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown0(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionCursorVariationsDat"/> with <see cref="MicrotransactionCursorVariationsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<MicrotransactionCursorVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionCursorVariationsDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionCursorVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCursorVariationsDat"/> with <see cref="MicrotransactionCursorVariationsDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionCursorVariationsDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionCursorVariationsDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionCursorVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionCursorVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCursorVariationsDat"/> with <see cref="MicrotransactionCursorVariationsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out MicrotransactionCursorVariationsDat? item)
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
    /// Tries to get <see cref="MicrotransactionCursorVariationsDat"/> with <see cref="MicrotransactionCursorVariationsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<MicrotransactionCursorVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionCursorVariationsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown16.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionCursorVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCursorVariationsDat"/> with <see cref="MicrotransactionCursorVariationsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionCursorVariationsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionCursorVariationsDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionCursorVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionCursorVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCursorVariationsDat"/> with <see cref="MicrotransactionCursorVariationsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out MicrotransactionCursorVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionCursorVariationsDat"/> with <see cref="MicrotransactionCursorVariationsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<MicrotransactionCursorVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionCursorVariationsDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionCursorVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCursorVariationsDat"/> with <see cref="MicrotransactionCursorVariationsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionCursorVariationsDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionCursorVariationsDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionCursorVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionCursorVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MicrotransactionCursorVariationsDat[] Load()
    {
        const string filePath = "Data/MicrotransactionCursorVariations.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionCursorVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionCursorVariationsDat()
            {
                Unknown0 = unknown0Loading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
