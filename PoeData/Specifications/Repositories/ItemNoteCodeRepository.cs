using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ItemNoteCodeDat"/> related data and helper methods.
/// </summary>
public sealed class ItemNoteCodeRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ItemNoteCodeDat> Items { get; }

    private Dictionary<int, List<ItemNoteCodeDat>>? byBaseItem;
    private Dictionary<string, List<ItemNoteCodeDat>>? byCode;
    private Dictionary<int, List<ItemNoteCodeDat>>? byOrder1;
    private Dictionary<bool, List<ItemNoteCodeDat>>? byShow;
    private Dictionary<int, List<ItemNoteCodeDat>>? byOrder2;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemNoteCodeRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ItemNoteCodeRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.BaseItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItem(int? key, out ItemNoteCodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItem(key, out var items))
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
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.BaseItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItem(int? key, out IReadOnlyList<ItemNoteCodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemNoteCodeDat>();
            return false;
        }

        if (byBaseItem is null)
        {
            byBaseItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemNoteCodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.byBaseItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemNoteCodeDat>> GetManyToManyByBaseItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemNoteCodeDat>>();
        }

        var items = new List<ResultItem<int, ItemNoteCodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemNoteCodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.Code"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCode(string? key, out ItemNoteCodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCode(key, out var items))
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
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.Code"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCode(string? key, out IReadOnlyList<ItemNoteCodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemNoteCodeDat>();
            return false;
        }

        if (byCode is null)
        {
            byCode = new();
            foreach (var item in Items)
            {
                var itemKey = item.Code;

                if (!byCode.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCode.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCode.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemNoteCodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.byCode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemNoteCodeDat>> GetManyToManyByCode(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemNoteCodeDat>>();
        }

        var items = new List<ResultItem<string, ItemNoteCodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemNoteCodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.Order1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOrder1(int? key, out ItemNoteCodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOrder1(key, out var items))
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
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.Order1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOrder1(int? key, out IReadOnlyList<ItemNoteCodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemNoteCodeDat>();
            return false;
        }

        if (byOrder1 is null)
        {
            byOrder1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Order1;

                if (!byOrder1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOrder1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOrder1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemNoteCodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.byOrder1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemNoteCodeDat>> GetManyToManyByOrder1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemNoteCodeDat>>();
        }

        var items = new List<ResultItem<int, ItemNoteCodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOrder1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemNoteCodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.Show"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShow(bool? key, out ItemNoteCodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShow(key, out var items))
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
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.Show"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShow(bool? key, out IReadOnlyList<ItemNoteCodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemNoteCodeDat>();
            return false;
        }

        if (byShow is null)
        {
            byShow = new();
            foreach (var item in Items)
            {
                var itemKey = item.Show;

                if (!byShow.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShow.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShow.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemNoteCodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.byShow"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemNoteCodeDat>> GetManyToManyByShow(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemNoteCodeDat>>();
        }

        var items = new List<ResultItem<bool, ItemNoteCodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShow(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemNoteCodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.Order2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOrder2(int? key, out ItemNoteCodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOrder2(key, out var items))
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
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.Order2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOrder2(int? key, out IReadOnlyList<ItemNoteCodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemNoteCodeDat>();
            return false;
        }

        if (byOrder2 is null)
        {
            byOrder2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Order2;

                if (!byOrder2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOrder2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOrder2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemNoteCodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemNoteCodeDat"/> with <see cref="ItemNoteCodeDat.byOrder2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemNoteCodeDat>> GetManyToManyByOrder2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemNoteCodeDat>>();
        }

        var items = new List<ResultItem<int, ItemNoteCodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOrder2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemNoteCodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ItemNoteCodeDat[] Load()
    {
        const string filePath = "Data/ItemNoteCode.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemNoteCodeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItem
            (var baseitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Code
            (var codeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Order1
            (var order1Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Show
            (var showLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Order2
            (var order2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemNoteCodeDat()
            {
                BaseItem = baseitemLoading,
                Code = codeLoading,
                Order1 = order1Loading,
                Show = showLoading,
                Order2 = order2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
