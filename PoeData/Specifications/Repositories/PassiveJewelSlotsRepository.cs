using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PassiveJewelSlotsDat"/> related data and helper methods.
/// </summary>
public sealed class PassiveJewelSlotsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PassiveJewelSlotsDat> Items { get; }

    private Dictionary<int, List<PassiveJewelSlotsDat>>? bySlot;
    private Dictionary<int, List<PassiveJewelSlotsDat>>? byClusterJewelSize;
    private Dictionary<int, List<PassiveJewelSlotsDat>>? byUnknown32;
    private Dictionary<int, List<PassiveJewelSlotsDat>>? byReplacesSlot;
    private Dictionary<int, List<PassiveJewelSlotsDat>>? byProxySlot;
    private Dictionary<int, List<PassiveJewelSlotsDat>>? byStartIndices;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassiveJewelSlotsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PassiveJewelSlotsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.Slot"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySlot(int? key, out PassiveJewelSlotsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySlot(key, out var items))
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
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.Slot"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySlot(int? key, out IReadOnlyList<PassiveJewelSlotsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        if (bySlot is null)
        {
            bySlot = new();
            foreach (var item in Items)
            {
                var itemKey = item.Slot;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySlot.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySlot.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySlot.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.bySlot"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveJewelSlotsDat>> GetManyToManyBySlot(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveJewelSlotsDat>>();
        }

        var items = new List<ResultItem<int, PassiveJewelSlotsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySlot(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveJewelSlotsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.ClusterJewelSize"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClusterJewelSize(int? key, out PassiveJewelSlotsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClusterJewelSize(key, out var items))
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
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.ClusterJewelSize"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClusterJewelSize(int? key, out IReadOnlyList<PassiveJewelSlotsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        if (byClusterJewelSize is null)
        {
            byClusterJewelSize = new();
            foreach (var item in Items)
            {
                var itemKey = item.ClusterJewelSize;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byClusterJewelSize.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byClusterJewelSize.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byClusterJewelSize.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.byClusterJewelSize"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveJewelSlotsDat>> GetManyToManyByClusterJewelSize(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveJewelSlotsDat>>();
        }

        var items = new List<ResultItem<int, PassiveJewelSlotsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClusterJewelSize(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveJewelSlotsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out PassiveJewelSlotsDat? item)
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
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<PassiveJewelSlotsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
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
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveJewelSlotsDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveJewelSlotsDat>>();
        }

        var items = new List<ResultItem<int, PassiveJewelSlotsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveJewelSlotsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.ReplacesSlot"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByReplacesSlot(int? key, out PassiveJewelSlotsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByReplacesSlot(key, out var items))
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
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.ReplacesSlot"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByReplacesSlot(int? key, out IReadOnlyList<PassiveJewelSlotsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        if (byReplacesSlot is null)
        {
            byReplacesSlot = new();
            foreach (var item in Items)
            {
                var itemKey = item.ReplacesSlot;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byReplacesSlot.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byReplacesSlot.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byReplacesSlot.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.byReplacesSlot"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveJewelSlotsDat>> GetManyToManyByReplacesSlot(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveJewelSlotsDat>>();
        }

        var items = new List<ResultItem<int, PassiveJewelSlotsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByReplacesSlot(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveJewelSlotsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.ProxySlot"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProxySlot(int? key, out PassiveJewelSlotsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProxySlot(key, out var items))
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
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.ProxySlot"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProxySlot(int? key, out IReadOnlyList<PassiveJewelSlotsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        if (byProxySlot is null)
        {
            byProxySlot = new();
            foreach (var item in Items)
            {
                var itemKey = item.ProxySlot;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byProxySlot.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byProxySlot.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byProxySlot.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.byProxySlot"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveJewelSlotsDat>> GetManyToManyByProxySlot(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveJewelSlotsDat>>();
        }

        var items = new List<ResultItem<int, PassiveJewelSlotsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProxySlot(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveJewelSlotsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.StartIndices"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStartIndices(int? key, out PassiveJewelSlotsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStartIndices(key, out var items))
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
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.StartIndices"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStartIndices(int? key, out IReadOnlyList<PassiveJewelSlotsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        if (byStartIndices is null)
        {
            byStartIndices = new();
            foreach (var item in Items)
            {
                var itemKey = item.StartIndices;
                foreach (var listKey in itemKey)
                {
                    if (!byStartIndices.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStartIndices.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStartIndices.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveJewelSlotsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelSlotsDat"/> with <see cref="PassiveJewelSlotsDat.byStartIndices"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveJewelSlotsDat>> GetManyToManyByStartIndices(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveJewelSlotsDat>>();
        }

        var items = new List<ResultItem<int, PassiveJewelSlotsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStartIndices(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveJewelSlotsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PassiveJewelSlotsDat[] Load()
    {
        const string filePath = "Data/PassiveJewelSlots.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveJewelSlotsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Slot
            (var slotLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ClusterJewelSize
            (var clusterjewelsizeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ReplacesSlot
            (var replacesslotLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading ProxySlot
            (var proxyslotLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StartIndices
            (var tempstartindicesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var startindicesLoading = tempstartindicesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveJewelSlotsDat()
            {
                Slot = slotLoading,
                ClusterJewelSize = clusterjewelsizeLoading,
                Unknown32 = unknown32Loading,
                ReplacesSlot = replacesslotLoading,
                ProxySlot = proxyslotLoading,
                StartIndices = startindicesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
