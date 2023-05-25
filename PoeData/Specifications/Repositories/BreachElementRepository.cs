using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BreachElementDat"/> related data and helper methods.
/// </summary>
public sealed class BreachElementRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BreachElementDat> Items { get; }

    private Dictionary<string, List<BreachElementDat>>? byElement;
    private Dictionary<int, List<BreachElementDat>>? byUnknown8;
    private Dictionary<int, List<BreachElementDat>>? byBaseBreachstone;
    private Dictionary<int, List<BreachElementDat>>? byBossMapMod;
    private Dictionary<int, List<BreachElementDat>>? byDuplicateBoss;

    /// <summary>
    /// Initializes a new instance of the <see cref="BreachElementRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BreachElementRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.Element"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByElement(string? key, out BreachElementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByElement(key, out var items))
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
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.Element"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByElement(string? key, out IReadOnlyList<BreachElementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachElementDat>();
            return false;
        }

        if (byElement is null)
        {
            byElement = new();
            foreach (var item in Items)
            {
                var itemKey = item.Element;

                if (!byElement.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byElement.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byElement.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BreachElementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.byElement"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BreachElementDat>> GetManyToManyByElement(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BreachElementDat>>();
        }

        var items = new List<ResultItem<string, BreachElementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByElement(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BreachElementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out BreachElementDat? item)
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
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<BreachElementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachElementDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown8.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BreachElementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachElementDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachElementDat>>();
        }

        var items = new List<ResultItem<int, BreachElementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachElementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.BaseBreachstone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseBreachstone(int? key, out BreachElementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseBreachstone(key, out var items))
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
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.BaseBreachstone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseBreachstone(int? key, out IReadOnlyList<BreachElementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachElementDat>();
            return false;
        }

        if (byBaseBreachstone is null)
        {
            byBaseBreachstone = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseBreachstone;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseBreachstone.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseBreachstone.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseBreachstone.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BreachElementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.byBaseBreachstone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachElementDat>> GetManyToManyByBaseBreachstone(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachElementDat>>();
        }

        var items = new List<ResultItem<int, BreachElementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseBreachstone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachElementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.BossMapMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBossMapMod(int? key, out BreachElementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBossMapMod(key, out var items))
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
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.BossMapMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBossMapMod(int? key, out IReadOnlyList<BreachElementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachElementDat>();
            return false;
        }

        if (byBossMapMod is null)
        {
            byBossMapMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.BossMapMod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBossMapMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBossMapMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBossMapMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BreachElementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.byBossMapMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachElementDat>> GetManyToManyByBossMapMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachElementDat>>();
        }

        var items = new List<ResultItem<int, BreachElementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBossMapMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachElementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.DuplicateBoss"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDuplicateBoss(int? key, out BreachElementDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDuplicateBoss(key, out var items))
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
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.DuplicateBoss"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDuplicateBoss(int? key, out IReadOnlyList<BreachElementDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachElementDat>();
            return false;
        }

        if (byDuplicateBoss is null)
        {
            byDuplicateBoss = new();
            foreach (var item in Items)
            {
                var itemKey = item.DuplicateBoss;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDuplicateBoss.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDuplicateBoss.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDuplicateBoss.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BreachElementDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachElementDat"/> with <see cref="BreachElementDat.byDuplicateBoss"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachElementDat>> GetManyToManyByDuplicateBoss(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachElementDat>>();
        }

        var items = new List<ResultItem<int, BreachElementDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDuplicateBoss(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachElementDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BreachElementDat[] Load()
    {
        const string filePath = "Data/BreachElement.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BreachElementDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Element
            (var elementLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BaseBreachstone
            (var basebreachstoneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BossMapMod
            (var bossmapmodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DuplicateBoss
            (var duplicatebossLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BreachElementDat()
            {
                Element = elementLoading,
                Unknown8 = unknown8Loading,
                BaseBreachstone = basebreachstoneLoading,
                BossMapMod = bossmapmodLoading,
                DuplicateBoss = duplicatebossLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
