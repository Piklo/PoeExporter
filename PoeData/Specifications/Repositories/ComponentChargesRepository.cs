using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ComponentChargesDat"/> related data and helper methods.
/// </summary>
public sealed class ComponentChargesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ComponentChargesDat> Items { get; }

    private Dictionary<string, List<ComponentChargesDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<ComponentChargesDat>>? byMaxCharges;
    private Dictionary<int, List<ComponentChargesDat>>? byPerCharge;
    private Dictionary<int, List<ComponentChargesDat>>? byMaxCharges2;
    private Dictionary<int, List<ComponentChargesDat>>? byPerCharge2;

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentChargesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ComponentChargesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(string? key, out ComponentChargesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(string? key, out IReadOnlyList<ComponentChargesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ComponentChargesDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;

                if (!byBaseItemTypesKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ComponentChargesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ComponentChargesDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ComponentChargesDat>>();
        }

        var items = new List<ResultItem<string, ComponentChargesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ComponentChargesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.MaxCharges"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxCharges(int? key, out ComponentChargesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxCharges(key, out var items))
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
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.MaxCharges"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxCharges(int? key, out IReadOnlyList<ComponentChargesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ComponentChargesDat>();
            return false;
        }

        if (byMaxCharges is null)
        {
            byMaxCharges = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxCharges;

                if (!byMaxCharges.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxCharges.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxCharges.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ComponentChargesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.byMaxCharges"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ComponentChargesDat>> GetManyToManyByMaxCharges(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ComponentChargesDat>>();
        }

        var items = new List<ResultItem<int, ComponentChargesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxCharges(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ComponentChargesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.PerCharge"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPerCharge(int? key, out ComponentChargesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPerCharge(key, out var items))
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
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.PerCharge"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPerCharge(int? key, out IReadOnlyList<ComponentChargesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ComponentChargesDat>();
            return false;
        }

        if (byPerCharge is null)
        {
            byPerCharge = new();
            foreach (var item in Items)
            {
                var itemKey = item.PerCharge;

                if (!byPerCharge.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPerCharge.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPerCharge.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ComponentChargesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.byPerCharge"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ComponentChargesDat>> GetManyToManyByPerCharge(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ComponentChargesDat>>();
        }

        var items = new List<ResultItem<int, ComponentChargesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPerCharge(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ComponentChargesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.MaxCharges2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxCharges2(int? key, out ComponentChargesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxCharges2(key, out var items))
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
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.MaxCharges2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxCharges2(int? key, out IReadOnlyList<ComponentChargesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ComponentChargesDat>();
            return false;
        }

        if (byMaxCharges2 is null)
        {
            byMaxCharges2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxCharges2;

                if (!byMaxCharges2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxCharges2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxCharges2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ComponentChargesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.byMaxCharges2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ComponentChargesDat>> GetManyToManyByMaxCharges2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ComponentChargesDat>>();
        }

        var items = new List<ResultItem<int, ComponentChargesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxCharges2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ComponentChargesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.PerCharge2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPerCharge2(int? key, out ComponentChargesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPerCharge2(key, out var items))
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
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.PerCharge2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPerCharge2(int? key, out IReadOnlyList<ComponentChargesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ComponentChargesDat>();
            return false;
        }

        if (byPerCharge2 is null)
        {
            byPerCharge2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.PerCharge2;

                if (!byPerCharge2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPerCharge2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPerCharge2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ComponentChargesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ComponentChargesDat"/> with <see cref="ComponentChargesDat.byPerCharge2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ComponentChargesDat>> GetManyToManyByPerCharge2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ComponentChargesDat>>();
        }

        var items = new List<ResultItem<int, ComponentChargesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPerCharge2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ComponentChargesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ComponentChargesDat[] Load()
    {
        const string filePath = "Data/ComponentCharges.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ComponentChargesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MaxCharges
            (var maxchargesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PerCharge
            (var perchargeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxCharges2
            (var maxcharges2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PerCharge2
            (var percharge2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ComponentChargesDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                MaxCharges = maxchargesLoading,
                PerCharge = perchargeLoading,
                MaxCharges2 = maxcharges2Loading,
                PerCharge2 = percharge2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
