using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BuffVisualSetEntriesDat"/> related data and helper methods.
/// </summary>
public sealed class BuffVisualSetEntriesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BuffVisualSetEntriesDat> Items { get; }

    private Dictionary<string, List<BuffVisualSetEntriesDat>>? byId;
    private Dictionary<int, List<BuffVisualSetEntriesDat>>? byUnknown8;
    private Dictionary<int, List<BuffVisualSetEntriesDat>>? byBuffVisual;
    private Dictionary<int, List<BuffVisualSetEntriesDat>>? byUnknown28;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuffVisualSetEntriesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BuffVisualSetEntriesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BuffVisualSetEntriesDat? item)
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
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BuffVisualSetEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualSetEntriesDat>();
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
            items = Array.Empty<BuffVisualSetEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualSetEntriesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualSetEntriesDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualSetEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualSetEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out BuffVisualSetEntriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<BuffVisualSetEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualSetEntriesDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;

                if (!byUnknown8.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualSetEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualSetEntriesDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualSetEntriesDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualSetEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualSetEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.BuffVisual"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisual(int? key, out BuffVisualSetEntriesDat? item)
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
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.BuffVisual"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisual(int? key, out IReadOnlyList<BuffVisualSetEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualSetEntriesDat>();
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
            items = Array.Empty<BuffVisualSetEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.byBuffVisual"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualSetEntriesDat>> GetManyToManyByBuffVisual(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualSetEntriesDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualSetEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisual(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualSetEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out BuffVisualSetEntriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<BuffVisualSetEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualSetEntriesDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualSetEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualSetEntriesDat"/> with <see cref="BuffVisualSetEntriesDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualSetEntriesDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualSetEntriesDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualSetEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualSetEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BuffVisualSetEntriesDat[] Load()
    {
        const string filePath = "Data/BuffVisualSetEntries.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BuffVisualSetEntriesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffVisual
            (var buffvisualLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BuffVisualSetEntriesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                BuffVisual = buffvisualLoading,
                Unknown28 = unknown28Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
