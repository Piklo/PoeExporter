using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GemTagsDat"/> related data and helper methods.
/// </summary>
public sealed class GemTagsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GemTagsDat> Items { get; }

    private Dictionary<string, List<GemTagsDat>>? byId;
    private Dictionary<string, List<GemTagsDat>>? byTag;
    private Dictionary<int, List<GemTagsDat>>? byStat1;
    private Dictionary<int, List<GemTagsDat>>? byStat2;
    private Dictionary<int, List<GemTagsDat>>? byStat3;
    private Dictionary<int, List<GemTagsDat>>? byStat4;

    /// <summary>
    /// Initializes a new instance of the <see cref="GemTagsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GemTagsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out GemTagsDat? item)
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
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<GemTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GemTagsDat>();
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
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GemTagsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GemTagsDat>>();
        }

        var items = new List<ResultItem<string, GemTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GemTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Tag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTag(string? key, out GemTagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTag(key, out var items))
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
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Tag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTag(string? key, out IReadOnlyList<GemTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        if (byTag is null)
        {
            byTag = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tag;

                if (!byTag.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTag.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTag.TryGetValue(key, out var temp))
        {
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.byTag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GemTagsDat>> GetManyToManyByTag(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GemTagsDat>>();
        }

        var items = new List<ResultItem<string, GemTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GemTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Stat1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat1(int? key, out GemTagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat1(key, out var items))
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
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Stat1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat1(int? key, out IReadOnlyList<GemTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        if (byStat1 is null)
        {
            byStat1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStat1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStat1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStat1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.byStat1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GemTagsDat>> GetManyToManyByStat1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GemTagsDat>>();
        }

        var items = new List<ResultItem<int, GemTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GemTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Stat2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat2(int? key, out GemTagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat2(key, out var items))
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
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Stat2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat2(int? key, out IReadOnlyList<GemTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        if (byStat2 is null)
        {
            byStat2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStat2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStat2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStat2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.byStat2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GemTagsDat>> GetManyToManyByStat2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GemTagsDat>>();
        }

        var items = new List<ResultItem<int, GemTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GemTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Stat3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat3(int? key, out GemTagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat3(key, out var items))
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
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Stat3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat3(int? key, out IReadOnlyList<GemTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        if (byStat3 is null)
        {
            byStat3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStat3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStat3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStat3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.byStat3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GemTagsDat>> GetManyToManyByStat3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GemTagsDat>>();
        }

        var items = new List<ResultItem<int, GemTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GemTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Stat4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat4(int? key, out GemTagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat4(key, out var items))
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
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.Stat4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat4(int? key, out IReadOnlyList<GemTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        if (byStat4 is null)
        {
            byStat4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat4;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStat4.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStat4.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStat4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GemTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GemTagsDat"/> with <see cref="GemTagsDat.byStat4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GemTagsDat>> GetManyToManyByStat4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GemTagsDat>>();
        }

        var items = new List<ResultItem<int, GemTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GemTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GemTagsDat[] Load()
    {
        const string filePath = "Data/GemTags.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GemTagsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tag
            (var tagLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Stat1
            (var stat1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Stat2
            (var stat2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Stat3
            (var stat3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Stat4
            (var stat4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GemTagsDat()
            {
                Id = idLoading,
                Tag = tagLoading,
                Stat1 = stat1Loading,
                Stat2 = stat2Loading,
                Stat3 = stat3Loading,
                Stat4 = stat4Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
