using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MetamorphosisScalingDat"/> related data and helper methods.
/// </summary>
public sealed class MetamorphosisScalingRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MetamorphosisScalingDat> Items { get; }

    private Dictionary<int, List<MetamorphosisScalingDat>>? byLevel;
    private Dictionary<float, List<MetamorphosisScalingDat>>? byStatValueMultiplier;
    private Dictionary<int, List<MetamorphosisScalingDat>>? byScalingStats;
    private Dictionary<int, List<MetamorphosisScalingDat>>? byScalingValues;
    private Dictionary<int, List<MetamorphosisScalingDat>>? byScalingValues2;
    private Dictionary<int, List<MetamorphosisScalingDat>>? byScalingValuesHardmode;
    private Dictionary<int, List<MetamorphosisScalingDat>>? byScalingValuesHardmode2;

    /// <summary>
    /// Initializes a new instance of the <see cref="MetamorphosisScalingRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MetamorphosisScalingRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out MetamorphosisScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevel(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<MetamorphosisScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        if (byLevel is null)
        {
            byLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.Level;

                if (!byLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisScalingDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisScalingDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.StatValueMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatValueMultiplier(float? key, out MetamorphosisScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatValueMultiplier(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.StatValueMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatValueMultiplier(float? key, out IReadOnlyList<MetamorphosisScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        if (byStatValueMultiplier is null)
        {
            byStatValueMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatValueMultiplier;

                if (!byStatValueMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStatValueMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStatValueMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.byStatValueMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, MetamorphosisScalingDat>> GetManyToManyByStatValueMultiplier(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, MetamorphosisScalingDat>>();
        }

        var items = new List<ResultItem<float, MetamorphosisScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatValueMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, MetamorphosisScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.ScalingStats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScalingStats(int? key, out MetamorphosisScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScalingStats(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.ScalingStats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScalingStats(int? key, out IReadOnlyList<MetamorphosisScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        if (byScalingStats is null)
        {
            byScalingStats = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScalingStats;
                foreach (var listKey in itemKey)
                {
                    if (!byScalingStats.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byScalingStats.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byScalingStats.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.byScalingStats"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisScalingDat>> GetManyToManyByScalingStats(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisScalingDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScalingStats(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.ScalingValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScalingValues(int? key, out MetamorphosisScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScalingValues(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.ScalingValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScalingValues(int? key, out IReadOnlyList<MetamorphosisScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        if (byScalingValues is null)
        {
            byScalingValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScalingValues;
                foreach (var listKey in itemKey)
                {
                    if (!byScalingValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byScalingValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byScalingValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.byScalingValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisScalingDat>> GetManyToManyByScalingValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisScalingDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScalingValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.ScalingValues2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScalingValues2(int? key, out MetamorphosisScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScalingValues2(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.ScalingValues2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScalingValues2(int? key, out IReadOnlyList<MetamorphosisScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        if (byScalingValues2 is null)
        {
            byScalingValues2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScalingValues2;
                foreach (var listKey in itemKey)
                {
                    if (!byScalingValues2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byScalingValues2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byScalingValues2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.byScalingValues2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisScalingDat>> GetManyToManyByScalingValues2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisScalingDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScalingValues2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.ScalingValuesHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScalingValuesHardmode(int? key, out MetamorphosisScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScalingValuesHardmode(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.ScalingValuesHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScalingValuesHardmode(int? key, out IReadOnlyList<MetamorphosisScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        if (byScalingValuesHardmode is null)
        {
            byScalingValuesHardmode = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScalingValuesHardmode;
                foreach (var listKey in itemKey)
                {
                    if (!byScalingValuesHardmode.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byScalingValuesHardmode.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byScalingValuesHardmode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.byScalingValuesHardmode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisScalingDat>> GetManyToManyByScalingValuesHardmode(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisScalingDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScalingValuesHardmode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.ScalingValuesHardmode2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScalingValuesHardmode2(int? key, out MetamorphosisScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScalingValuesHardmode2(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.ScalingValuesHardmode2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScalingValuesHardmode2(int? key, out IReadOnlyList<MetamorphosisScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        if (byScalingValuesHardmode2 is null)
        {
            byScalingValuesHardmode2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScalingValuesHardmode2;
                foreach (var listKey in itemKey)
                {
                    if (!byScalingValuesHardmode2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byScalingValuesHardmode2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byScalingValuesHardmode2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisScalingDat"/> with <see cref="MetamorphosisScalingDat.byScalingValuesHardmode2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisScalingDat>> GetManyToManyByScalingValuesHardmode2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisScalingDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScalingValuesHardmode2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MetamorphosisScalingDat[] Load()
    {
        const string filePath = "Data/MetamorphosisScaling.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MetamorphosisScalingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatValueMultiplier
            (var statvaluemultiplierLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading ScalingStats
            (var tempscalingstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var scalingstatsLoading = tempscalingstatsLoading.AsReadOnly();

            // loading ScalingValues
            (var tempscalingvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var scalingvaluesLoading = tempscalingvaluesLoading.AsReadOnly();

            // loading ScalingValues2
            (var tempscalingvalues2Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var scalingvalues2Loading = tempscalingvalues2Loading.AsReadOnly();

            // loading ScalingValuesHardmode
            (var tempscalingvalueshardmodeLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var scalingvalueshardmodeLoading = tempscalingvalueshardmodeLoading.AsReadOnly();

            // loading ScalingValuesHardmode2
            (var tempscalingvalueshardmode2Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var scalingvalueshardmode2Loading = tempscalingvalueshardmode2Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MetamorphosisScalingDat()
            {
                Level = levelLoading,
                StatValueMultiplier = statvaluemultiplierLoading,
                ScalingStats = scalingstatsLoading,
                ScalingValues = scalingvaluesLoading,
                ScalingValues2 = scalingvalues2Loading,
                ScalingValuesHardmode = scalingvalueshardmodeLoading,
                ScalingValuesHardmode2 = scalingvalueshardmode2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
