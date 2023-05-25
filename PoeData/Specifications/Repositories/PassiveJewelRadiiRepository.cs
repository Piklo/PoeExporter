using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PassiveJewelRadiiDat"/> related data and helper methods.
/// </summary>
public sealed class PassiveJewelRadiiRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PassiveJewelRadiiDat> Items { get; }

    private Dictionary<string, List<PassiveJewelRadiiDat>>? byID;
    private Dictionary<int, List<PassiveJewelRadiiDat>>? byRingOuterRadius;
    private Dictionary<int, List<PassiveJewelRadiiDat>>? byRingInnerRadius;
    private Dictionary<int, List<PassiveJewelRadiiDat>>? byRadius;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassiveJewelRadiiRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PassiveJewelRadiiRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.ID"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByID(string? key, out PassiveJewelRadiiDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByID(key, out var items))
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
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.ID"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByID(string? key, out IReadOnlyList<PassiveJewelRadiiDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveJewelRadiiDat>();
            return false;
        }

        if (byID is null)
        {
            byID = new();
            foreach (var item in Items)
            {
                var itemKey = item.ID;

                if (!byID.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byID.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byID.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveJewelRadiiDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.byID"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveJewelRadiiDat>> GetManyToManyByID(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveJewelRadiiDat>>();
        }

        var items = new List<ResultItem<string, PassiveJewelRadiiDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByID(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveJewelRadiiDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.RingOuterRadius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRingOuterRadius(int? key, out PassiveJewelRadiiDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRingOuterRadius(key, out var items))
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
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.RingOuterRadius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRingOuterRadius(int? key, out IReadOnlyList<PassiveJewelRadiiDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveJewelRadiiDat>();
            return false;
        }

        if (byRingOuterRadius is null)
        {
            byRingOuterRadius = new();
            foreach (var item in Items)
            {
                var itemKey = item.RingOuterRadius;

                if (!byRingOuterRadius.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRingOuterRadius.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRingOuterRadius.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveJewelRadiiDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.byRingOuterRadius"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveJewelRadiiDat>> GetManyToManyByRingOuterRadius(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveJewelRadiiDat>>();
        }

        var items = new List<ResultItem<int, PassiveJewelRadiiDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRingOuterRadius(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveJewelRadiiDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.RingInnerRadius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRingInnerRadius(int? key, out PassiveJewelRadiiDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRingInnerRadius(key, out var items))
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
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.RingInnerRadius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRingInnerRadius(int? key, out IReadOnlyList<PassiveJewelRadiiDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveJewelRadiiDat>();
            return false;
        }

        if (byRingInnerRadius is null)
        {
            byRingInnerRadius = new();
            foreach (var item in Items)
            {
                var itemKey = item.RingInnerRadius;

                if (!byRingInnerRadius.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRingInnerRadius.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRingInnerRadius.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveJewelRadiiDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.byRingInnerRadius"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveJewelRadiiDat>> GetManyToManyByRingInnerRadius(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveJewelRadiiDat>>();
        }

        var items = new List<ResultItem<int, PassiveJewelRadiiDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRingInnerRadius(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveJewelRadiiDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.Radius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRadius(int? key, out PassiveJewelRadiiDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRadius(key, out var items))
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
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.Radius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRadius(int? key, out IReadOnlyList<PassiveJewelRadiiDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveJewelRadiiDat>();
            return false;
        }

        if (byRadius is null)
        {
            byRadius = new();
            foreach (var item in Items)
            {
                var itemKey = item.Radius;

                if (!byRadius.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRadius.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRadius.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveJewelRadiiDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveJewelRadiiDat"/> with <see cref="PassiveJewelRadiiDat.byRadius"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveJewelRadiiDat>> GetManyToManyByRadius(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveJewelRadiiDat>>();
        }

        var items = new List<ResultItem<int, PassiveJewelRadiiDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRadius(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveJewelRadiiDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PassiveJewelRadiiDat[] Load()
    {
        const string filePath = "Data/PassiveJewelRadii.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveJewelRadiiDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ID
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RingOuterRadius
            (var ringouterradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RingInnerRadius
            (var ringinnerradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Radius
            (var radiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveJewelRadiiDat()
            {
                ID = idLoading,
                RingOuterRadius = ringouterradiusLoading,
                RingInnerRadius = ringinnerradiusLoading,
                Radius = radiusLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
