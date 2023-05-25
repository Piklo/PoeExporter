using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HellscapeItemModificationTiersDat"/> related data and helper methods.
/// </summary>
public sealed class HellscapeItemModificationTiersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HellscapeItemModificationTiersDat> Items { get; }

    private Dictionary<int, List<HellscapeItemModificationTiersDat>>? byTier;
    private Dictionary<bool, List<HellscapeItemModificationTiersDat>>? byIsMap;
    private Dictionary<int, List<HellscapeItemModificationTiersDat>>? byUnknown5;
    private Dictionary<int, List<HellscapeItemModificationTiersDat>>? byMinItemLvl;

    /// <summary>
    /// Initializes a new instance of the <see cref="HellscapeItemModificationTiersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HellscapeItemModificationTiersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out HellscapeItemModificationTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier(key, out var items))
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
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<HellscapeItemModificationTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeItemModificationTiersDat>();
            return false;
        }

        if (byTier is null)
        {
            byTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier;

                if (!byTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeItemModificationTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeItemModificationTiersDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeItemModificationTiersDat>>();
        }

        var items = new List<ResultItem<int, HellscapeItemModificationTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeItemModificationTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.IsMap"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsMap(bool? key, out HellscapeItemModificationTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsMap(key, out var items))
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
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.IsMap"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsMap(bool? key, out IReadOnlyList<HellscapeItemModificationTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeItemModificationTiersDat>();
            return false;
        }

        if (byIsMap is null)
        {
            byIsMap = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsMap;

                if (!byIsMap.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsMap.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsMap.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeItemModificationTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.byIsMap"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HellscapeItemModificationTiersDat>> GetManyToManyByIsMap(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HellscapeItemModificationTiersDat>>();
        }

        var items = new List<ResultItem<bool, HellscapeItemModificationTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsMap(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HellscapeItemModificationTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.Unknown5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown5(int? key, out HellscapeItemModificationTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown5(key, out var items))
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
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.Unknown5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown5(int? key, out IReadOnlyList<HellscapeItemModificationTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeItemModificationTiersDat>();
            return false;
        }

        if (byUnknown5 is null)
        {
            byUnknown5 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown5;

                if (!byUnknown5.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown5.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown5.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeItemModificationTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.byUnknown5"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeItemModificationTiersDat>> GetManyToManyByUnknown5(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeItemModificationTiersDat>>();
        }

        var items = new List<ResultItem<int, HellscapeItemModificationTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown5(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeItemModificationTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.MinItemLvl"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinItemLvl(int? key, out HellscapeItemModificationTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinItemLvl(key, out var items))
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
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.MinItemLvl"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinItemLvl(int? key, out IReadOnlyList<HellscapeItemModificationTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeItemModificationTiersDat>();
            return false;
        }

        if (byMinItemLvl is null)
        {
            byMinItemLvl = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinItemLvl;

                if (!byMinItemLvl.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinItemLvl.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinItemLvl.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeItemModificationTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeItemModificationTiersDat"/> with <see cref="HellscapeItemModificationTiersDat.byMinItemLvl"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeItemModificationTiersDat>> GetManyToManyByMinItemLvl(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeItemModificationTiersDat>>();
        }

        var items = new List<ResultItem<int, HellscapeItemModificationTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinItemLvl(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeItemModificationTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HellscapeItemModificationTiersDat[] Load()
    {
        const string filePath = "Data/HellscapeItemModificationTiers.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeItemModificationTiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsMap
            (var ismapLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown5
            (var unknown5Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinItemLvl
            (var minitemlvlLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeItemModificationTiersDat()
            {
                Tier = tierLoading,
                IsMap = ismapLoading,
                Unknown5 = unknown5Loading,
                MinItemLvl = minitemlvlLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
