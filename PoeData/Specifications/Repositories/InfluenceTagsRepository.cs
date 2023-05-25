using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="InfluenceTagsDat"/> related data and helper methods.
/// </summary>
public sealed class InfluenceTagsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<InfluenceTagsDat> Items { get; }

    private Dictionary<int, List<InfluenceTagsDat>>? byItemClass;
    private Dictionary<int, List<InfluenceTagsDat>>? byInfluence;
    private Dictionary<int, List<InfluenceTagsDat>>? byTag;

    /// <summary>
    /// Initializes a new instance of the <see cref="InfluenceTagsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal InfluenceTagsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceTagsDat"/> with <see cref="InfluenceTagsDat.ItemClass"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClass(int? key, out InfluenceTagsDat? item)
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
    /// Tries to get <see cref="InfluenceTagsDat"/> with <see cref="InfluenceTagsDat.ItemClass"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClass(int? key, out IReadOnlyList<InfluenceTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<InfluenceTagsDat>();
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
            items = Array.Empty<InfluenceTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceTagsDat"/> with <see cref="InfluenceTagsDat.byItemClass"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, InfluenceTagsDat>> GetManyToManyByItemClass(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, InfluenceTagsDat>>();
        }

        var items = new List<ResultItem<int, InfluenceTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClass(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, InfluenceTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceTagsDat"/> with <see cref="InfluenceTagsDat.Influence"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInfluence(int? key, out InfluenceTagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInfluence(key, out var items))
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
    /// Tries to get <see cref="InfluenceTagsDat"/> with <see cref="InfluenceTagsDat.Influence"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInfluence(int? key, out IReadOnlyList<InfluenceTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<InfluenceTagsDat>();
            return false;
        }

        if (byInfluence is null)
        {
            byInfluence = new();
            foreach (var item in Items)
            {
                var itemKey = item.Influence;

                if (!byInfluence.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInfluence.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInfluence.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<InfluenceTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceTagsDat"/> with <see cref="InfluenceTagsDat.byInfluence"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, InfluenceTagsDat>> GetManyToManyByInfluence(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, InfluenceTagsDat>>();
        }

        var items = new List<ResultItem<int, InfluenceTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInfluence(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, InfluenceTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceTagsDat"/> with <see cref="InfluenceTagsDat.Tag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTag(int? key, out InfluenceTagsDat? item)
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
    /// Tries to get <see cref="InfluenceTagsDat"/> with <see cref="InfluenceTagsDat.Tag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTag(int? key, out IReadOnlyList<InfluenceTagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<InfluenceTagsDat>();
            return false;
        }

        if (byTag is null)
        {
            byTag = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tag;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTag.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTag.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTag.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<InfluenceTagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceTagsDat"/> with <see cref="InfluenceTagsDat.byTag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, InfluenceTagsDat>> GetManyToManyByTag(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, InfluenceTagsDat>>();
        }

        var items = new List<ResultItem<int, InfluenceTagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, InfluenceTagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private InfluenceTagsDat[] Load()
    {
        const string filePath = "Data/InfluenceTags.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new InfluenceTagsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ItemClass
            (var itemclassLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Influence
            (var influenceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tag
            (var tagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new InfluenceTagsDat()
            {
                ItemClass = itemclassLoading,
                Influence = influenceLoading,
                Tag = tagLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
