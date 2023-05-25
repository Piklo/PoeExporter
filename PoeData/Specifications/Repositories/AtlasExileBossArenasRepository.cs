using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasExileBossArenasDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasExileBossArenasRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasExileBossArenasDat> Items { get; }

    private Dictionary<int, List<AtlasExileBossArenasDat>>? byConqueror;
    private Dictionary<int, List<AtlasExileBossArenasDat>>? byWorldArea;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasExileBossArenasRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasExileBossArenasRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExileBossArenasDat"/> with <see cref="AtlasExileBossArenasDat.Conqueror"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByConqueror(int? key, out AtlasExileBossArenasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByConqueror(key, out var items))
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
    /// Tries to get <see cref="AtlasExileBossArenasDat"/> with <see cref="AtlasExileBossArenasDat.Conqueror"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByConqueror(int? key, out IReadOnlyList<AtlasExileBossArenasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasExileBossArenasDat>();
            return false;
        }

        if (byConqueror is null)
        {
            byConqueror = new();
            foreach (var item in Items)
            {
                var itemKey = item.Conqueror;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byConqueror.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byConqueror.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byConqueror.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasExileBossArenasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExileBossArenasDat"/> with <see cref="AtlasExileBossArenasDat.byConqueror"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasExileBossArenasDat>> GetManyToManyByConqueror(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasExileBossArenasDat>>();
        }

        var items = new List<ResultItem<int, AtlasExileBossArenasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByConqueror(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasExileBossArenasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExileBossArenasDat"/> with <see cref="AtlasExileBossArenasDat.WorldArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldArea(int? key, out AtlasExileBossArenasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldArea(key, out var items))
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
    /// Tries to get <see cref="AtlasExileBossArenasDat"/> with <see cref="AtlasExileBossArenasDat.WorldArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldArea(int? key, out IReadOnlyList<AtlasExileBossArenasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasExileBossArenasDat>();
            return false;
        }

        if (byWorldArea is null)
        {
            byWorldArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldArea;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldArea.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldArea.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasExileBossArenasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasExileBossArenasDat"/> with <see cref="AtlasExileBossArenasDat.byWorldArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasExileBossArenasDat>> GetManyToManyByWorldArea(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasExileBossArenasDat>>();
        }

        var items = new List<ResultItem<int, AtlasExileBossArenasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasExileBossArenasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasExileBossArenasDat[] Load()
    {
        const string filePath = "Data/AtlasExileBossArenas.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasExileBossArenasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Conqueror
            (var conquerorLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading WorldArea
            (var worldareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasExileBossArenasDat()
            {
                Conqueror = conquerorLoading,
                WorldArea = worldareaLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
