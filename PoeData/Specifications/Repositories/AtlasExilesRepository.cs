using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasExilesDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasExilesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasExilesDat> Items { get; }

    private Dictionary<string, List<AtlasExilesDat>>? byId;
    private Dictionary<int, List<AtlasExilesDat>>? byUnknown8;
    private Dictionary<int, List<AtlasExilesDat>>? byInfluencedItemIncrStat;
    private Dictionary<string, List<AtlasExilesDat>>? byMapIcon;
    private Dictionary<string, List<AtlasExilesDat>>? byMapIcon2;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasExilesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasExilesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AtlasExilesDat? item)
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
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AtlasExilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasExilesDat>();
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
            items = Array.Empty<AtlasExilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasExilesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasExilesDat>>();
        }

        var items = new List<ResultItem<string, AtlasExilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasExilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out AtlasExilesDat? item)
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
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<AtlasExilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasExilesDat>();
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
            items = Array.Empty<AtlasExilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasExilesDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasExilesDat>>();
        }

        var items = new List<ResultItem<int, AtlasExilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasExilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.InfluencedItemIncrStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInfluencedItemIncrStat(int? key, out AtlasExilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInfluencedItemIncrStat(key, out var items))
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
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.InfluencedItemIncrStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInfluencedItemIncrStat(int? key, out IReadOnlyList<AtlasExilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasExilesDat>();
            return false;
        }

        if (byInfluencedItemIncrStat is null)
        {
            byInfluencedItemIncrStat = new();
            foreach (var item in Items)
            {
                var itemKey = item.InfluencedItemIncrStat;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byInfluencedItemIncrStat.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byInfluencedItemIncrStat.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byInfluencedItemIncrStat.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasExilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.byInfluencedItemIncrStat"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasExilesDat>> GetManyToManyByInfluencedItemIncrStat(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasExilesDat>>();
        }

        var items = new List<ResultItem<int, AtlasExilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInfluencedItemIncrStat(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasExilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.MapIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapIcon(string? key, out AtlasExilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapIcon(key, out var items))
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
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.MapIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapIcon(string? key, out IReadOnlyList<AtlasExilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasExilesDat>();
            return false;
        }

        if (byMapIcon is null)
        {
            byMapIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapIcon;

                if (!byMapIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMapIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMapIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasExilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.byMapIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasExilesDat>> GetManyToManyByMapIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasExilesDat>>();
        }

        var items = new List<ResultItem<string, AtlasExilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasExilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.MapIcon2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapIcon2(string? key, out AtlasExilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapIcon2(key, out var items))
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
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.MapIcon2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapIcon2(string? key, out IReadOnlyList<AtlasExilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasExilesDat>();
            return false;
        }

        if (byMapIcon2 is null)
        {
            byMapIcon2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapIcon2;

                if (!byMapIcon2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMapIcon2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMapIcon2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasExilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExilesDat"/> with <see cref="AtlasExilesDat.byMapIcon2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasExilesDat>> GetManyToManyByMapIcon2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasExilesDat>>();
        }

        var items = new List<ResultItem<string, AtlasExilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapIcon2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasExilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasExilesDat[] Load()
    {
        const string filePath = "Data/AtlasExiles.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasExilesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading InfluencedItemIncrStat
            (var influenceditemincrstatLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MapIcon
            (var mapiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MapIcon2
            (var mapicon2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasExilesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                InfluencedItemIncrStat = influenceditemincrstatLoading,
                MapIcon = mapiconLoading,
                MapIcon2 = mapicon2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
