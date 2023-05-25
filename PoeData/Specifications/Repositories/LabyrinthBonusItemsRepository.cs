using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LabyrinthBonusItemsDat"/> related data and helper methods.
/// </summary>
public sealed class LabyrinthBonusItemsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LabyrinthBonusItemsDat> Items { get; }

    private Dictionary<int, List<LabyrinthBonusItemsDat>>? byBaseItemType;
    private Dictionary<int, List<LabyrinthBonusItemsDat>>? byAreaLevel;
    private Dictionary<string, List<LabyrinthBonusItemsDat>>? byLabyrinthName;

    /// <summary>
    /// Initializes a new instance of the <see cref="LabyrinthBonusItemsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LabyrinthBonusItemsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthBonusItemsDat"/> with <see cref="LabyrinthBonusItemsDat.BaseItemType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemType(int? key, out LabyrinthBonusItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemType(key, out var items))
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
    /// Tries to get <see cref="LabyrinthBonusItemsDat"/> with <see cref="LabyrinthBonusItemsDat.BaseItemType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemType(int? key, out IReadOnlyList<LabyrinthBonusItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthBonusItemsDat>();
            return false;
        }

        if (byBaseItemType is null)
        {
            byBaseItemType = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemType;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthBonusItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthBonusItemsDat"/> with <see cref="LabyrinthBonusItemsDat.byBaseItemType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthBonusItemsDat>> GetManyToManyByBaseItemType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthBonusItemsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthBonusItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthBonusItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthBonusItemsDat"/> with <see cref="LabyrinthBonusItemsDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAreaLevel(int? key, out LabyrinthBonusItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAreaLevel(key, out var items))
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
    /// Tries to get <see cref="LabyrinthBonusItemsDat"/> with <see cref="LabyrinthBonusItemsDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAreaLevel(int? key, out IReadOnlyList<LabyrinthBonusItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthBonusItemsDat>();
            return false;
        }

        if (byAreaLevel is null)
        {
            byAreaLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.AreaLevel;

                if (!byAreaLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAreaLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAreaLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthBonusItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthBonusItemsDat"/> with <see cref="LabyrinthBonusItemsDat.byAreaLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthBonusItemsDat>> GetManyToManyByAreaLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthBonusItemsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthBonusItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAreaLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthBonusItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthBonusItemsDat"/> with <see cref="LabyrinthBonusItemsDat.LabyrinthName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthName(string? key, out LabyrinthBonusItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthName(key, out var items))
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
    /// Tries to get <see cref="LabyrinthBonusItemsDat"/> with <see cref="LabyrinthBonusItemsDat.LabyrinthName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthName(string? key, out IReadOnlyList<LabyrinthBonusItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthBonusItemsDat>();
            return false;
        }

        if (byLabyrinthName is null)
        {
            byLabyrinthName = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthName;

                if (!byLabyrinthName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLabyrinthName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLabyrinthName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LabyrinthBonusItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthBonusItemsDat"/> with <see cref="LabyrinthBonusItemsDat.byLabyrinthName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthBonusItemsDat>> GetManyToManyByLabyrinthName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthBonusItemsDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthBonusItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthBonusItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LabyrinthBonusItemsDat[] Load()
    {
        const string filePath = "Data/LabyrinthBonusItems.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthBonusItemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LabyrinthName
            (var labyrinthnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthBonusItemsDat()
            {
                BaseItemType = baseitemtypeLoading,
                AreaLevel = arealevelLoading,
                LabyrinthName = labyrinthnameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
