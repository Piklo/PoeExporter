using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="InfluenceModUpgradesDat"/> related data and helper methods.
/// </summary>
public sealed class InfluenceModUpgradesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<InfluenceModUpgradesDat> Items { get; }

    private Dictionary<int, List<InfluenceModUpgradesDat>>? byInfluenceMod;
    private Dictionary<int, List<InfluenceModUpgradesDat>>? byUpgradedMod;
    private Dictionary<bool, List<InfluenceModUpgradesDat>>? byUnknown32;

    /// <summary>
    /// Initializes a new instance of the <see cref="InfluenceModUpgradesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal InfluenceModUpgradesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceModUpgradesDat"/> with <see cref="InfluenceModUpgradesDat.InfluenceMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInfluenceMod(int? key, out InfluenceModUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInfluenceMod(key, out var items))
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
    /// Tries to get <see cref="InfluenceModUpgradesDat"/> with <see cref="InfluenceModUpgradesDat.InfluenceMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInfluenceMod(int? key, out IReadOnlyList<InfluenceModUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<InfluenceModUpgradesDat>();
            return false;
        }

        if (byInfluenceMod is null)
        {
            byInfluenceMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.InfluenceMod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byInfluenceMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byInfluenceMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byInfluenceMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<InfluenceModUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceModUpgradesDat"/> with <see cref="InfluenceModUpgradesDat.byInfluenceMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, InfluenceModUpgradesDat>> GetManyToManyByInfluenceMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, InfluenceModUpgradesDat>>();
        }

        var items = new List<ResultItem<int, InfluenceModUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInfluenceMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, InfluenceModUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceModUpgradesDat"/> with <see cref="InfluenceModUpgradesDat.UpgradedMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUpgradedMod(int? key, out InfluenceModUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUpgradedMod(key, out var items))
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
    /// Tries to get <see cref="InfluenceModUpgradesDat"/> with <see cref="InfluenceModUpgradesDat.UpgradedMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUpgradedMod(int? key, out IReadOnlyList<InfluenceModUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<InfluenceModUpgradesDat>();
            return false;
        }

        if (byUpgradedMod is null)
        {
            byUpgradedMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.UpgradedMod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUpgradedMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUpgradedMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUpgradedMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<InfluenceModUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceModUpgradesDat"/> with <see cref="InfluenceModUpgradesDat.byUpgradedMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, InfluenceModUpgradesDat>> GetManyToManyByUpgradedMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, InfluenceModUpgradesDat>>();
        }

        var items = new List<ResultItem<int, InfluenceModUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUpgradedMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, InfluenceModUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceModUpgradesDat"/> with <see cref="InfluenceModUpgradesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(bool? key, out InfluenceModUpgradesDat? item)
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
    /// Tries to get <see cref="InfluenceModUpgradesDat"/> with <see cref="InfluenceModUpgradesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(bool? key, out IReadOnlyList<InfluenceModUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<InfluenceModUpgradesDat>();
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
            items = Array.Empty<InfluenceModUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="InfluenceModUpgradesDat"/> with <see cref="InfluenceModUpgradesDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, InfluenceModUpgradesDat>> GetManyToManyByUnknown32(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, InfluenceModUpgradesDat>>();
        }

        var items = new List<ResultItem<bool, InfluenceModUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, InfluenceModUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private InfluenceModUpgradesDat[] Load()
    {
        const string filePath = "Data/InfluenceModUpgrades.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new InfluenceModUpgradesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading InfluenceMod
            (var influencemodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading UpgradedMod
            (var upgradedmodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new InfluenceModUpgradesDat()
            {
                InfluenceMod = influencemodLoading,
                UpgradedMod = upgradedmodLoading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
