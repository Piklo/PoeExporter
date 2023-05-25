using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GrantedEffectStatSetsDat"/> related data and helper methods.
/// </summary>
public sealed class GrantedEffectStatSetsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GrantedEffectStatSetsDat> Items { get; }

    private Dictionary<string, List<GrantedEffectStatSetsDat>>? byId;
    private Dictionary<int, List<GrantedEffectStatSetsDat>>? byImplicitStats;
    private Dictionary<int, List<GrantedEffectStatSetsDat>>? byConstantStats;
    private Dictionary<int, List<GrantedEffectStatSetsDat>>? byConstantStatsValues;
    private Dictionary<float, List<GrantedEffectStatSetsDat>>? byBaseEffectiveness;
    private Dictionary<float, List<GrantedEffectStatSetsDat>>? byIncrementalEffectiveness;
    private Dictionary<int, List<GrantedEffectStatSetsDat>>? byUnknown64;

    /// <summary>
    /// Initializes a new instance of the <see cref="GrantedEffectStatSetsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GrantedEffectStatSetsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out GrantedEffectStatSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyById(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<GrantedEffectStatSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        if (byId is null)
        {
            byId = new();
            foreach (var item in Items)
            {
                var itemKey = item.Id;

                if (!byId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byId.TryGetValue(key, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GrantedEffectStatSetsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GrantedEffectStatSetsDat>>();
        }

        var items = new List<ResultItem<string, GrantedEffectStatSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GrantedEffectStatSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.ImplicitStats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImplicitStats(int? key, out GrantedEffectStatSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByImplicitStats(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.ImplicitStats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImplicitStats(int? key, out IReadOnlyList<GrantedEffectStatSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        if (byImplicitStats is null)
        {
            byImplicitStats = new();
            foreach (var item in Items)
            {
                var itemKey = item.ImplicitStats;
                foreach (var listKey in itemKey)
                {
                    if (!byImplicitStats.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byImplicitStats.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byImplicitStats.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.byImplicitStats"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsDat>> GetManyToManyByImplicitStats(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImplicitStats(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.ConstantStats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByConstantStats(int? key, out GrantedEffectStatSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByConstantStats(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.ConstantStats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByConstantStats(int? key, out IReadOnlyList<GrantedEffectStatSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        if (byConstantStats is null)
        {
            byConstantStats = new();
            foreach (var item in Items)
            {
                var itemKey = item.ConstantStats;
                foreach (var listKey in itemKey)
                {
                    if (!byConstantStats.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byConstantStats.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byConstantStats.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.byConstantStats"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsDat>> GetManyToManyByConstantStats(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByConstantStats(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.ConstantStatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByConstantStatsValues(int? key, out GrantedEffectStatSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByConstantStatsValues(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.ConstantStatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByConstantStatsValues(int? key, out IReadOnlyList<GrantedEffectStatSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        if (byConstantStatsValues is null)
        {
            byConstantStatsValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.ConstantStatsValues;
                foreach (var listKey in itemKey)
                {
                    if (!byConstantStatsValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byConstantStatsValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byConstantStatsValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.byConstantStatsValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsDat>> GetManyToManyByConstantStatsValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByConstantStatsValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.BaseEffectiveness"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseEffectiveness(float? key, out GrantedEffectStatSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseEffectiveness(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.BaseEffectiveness"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseEffectiveness(float? key, out IReadOnlyList<GrantedEffectStatSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        if (byBaseEffectiveness is null)
        {
            byBaseEffectiveness = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseEffectiveness;

                if (!byBaseEffectiveness.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseEffectiveness.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseEffectiveness.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.byBaseEffectiveness"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, GrantedEffectStatSetsDat>> GetManyToManyByBaseEffectiveness(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, GrantedEffectStatSetsDat>>();
        }

        var items = new List<ResultItem<float, GrantedEffectStatSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseEffectiveness(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, GrantedEffectStatSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.IncrementalEffectiveness"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIncrementalEffectiveness(float? key, out GrantedEffectStatSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIncrementalEffectiveness(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.IncrementalEffectiveness"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIncrementalEffectiveness(float? key, out IReadOnlyList<GrantedEffectStatSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        if (byIncrementalEffectiveness is null)
        {
            byIncrementalEffectiveness = new();
            foreach (var item in Items)
            {
                var itemKey = item.IncrementalEffectiveness;

                if (!byIncrementalEffectiveness.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIncrementalEffectiveness.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIncrementalEffectiveness.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.byIncrementalEffectiveness"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, GrantedEffectStatSetsDat>> GetManyToManyByIncrementalEffectiveness(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, GrantedEffectStatSetsDat>>();
        }

        var items = new List<ResultItem<float, GrantedEffectStatSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIncrementalEffectiveness(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, GrantedEffectStatSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out GrantedEffectStatSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<GrantedEffectStatSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsDat"/> with <see cref="GrantedEffectStatSetsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GrantedEffectStatSetsDat[] Load()
    {
        const string filePath = "Data/GrantedEffectStatSets.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrantedEffectStatSetsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ImplicitStats
            (var tempimplicitstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var implicitstatsLoading = tempimplicitstatsLoading.AsReadOnly();

            // loading ConstantStats
            (var tempconstantstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var constantstatsLoading = tempconstantstatsLoading.AsReadOnly();

            // loading ConstantStatsValues
            (var tempconstantstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var constantstatsvaluesLoading = tempconstantstatsvaluesLoading.AsReadOnly();

            // loading BaseEffectiveness
            (var baseeffectivenessLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading IncrementalEffectiveness
            (var incrementaleffectivenessLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectStatSetsDat()
            {
                Id = idLoading,
                ImplicitStats = implicitstatsLoading,
                ConstantStats = constantstatsLoading,
                ConstantStatsValues = constantstatsvaluesLoading,
                BaseEffectiveness = baseeffectivenessLoading,
                IncrementalEffectiveness = incrementaleffectivenessLoading,
                Unknown64 = unknown64Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
