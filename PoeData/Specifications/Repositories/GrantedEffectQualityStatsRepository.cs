using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GrantedEffectQualityStatsDat"/> related data and helper methods.
/// </summary>
public sealed class GrantedEffectQualityStatsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GrantedEffectQualityStatsDat> Items { get; }

    private Dictionary<int, List<GrantedEffectQualityStatsDat>>? byGrantedEffectsKey;
    private Dictionary<int, List<GrantedEffectQualityStatsDat>>? bySetId;
    private Dictionary<int, List<GrantedEffectQualityStatsDat>>? byStatsKeys;
    private Dictionary<int, List<GrantedEffectQualityStatsDat>>? byStatsValuesPermille;
    private Dictionary<int, List<GrantedEffectQualityStatsDat>>? byWeight;
    private Dictionary<int, List<GrantedEffectQualityStatsDat>>? byUnknown56;
    private Dictionary<int, List<GrantedEffectQualityStatsDat>>? byUnknown72;

    /// <summary>
    /// Initializes a new instance of the <see cref="GrantedEffectQualityStatsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GrantedEffectQualityStatsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.GrantedEffectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffectsKey(int? key, out GrantedEffectQualityStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffectsKey(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.GrantedEffectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffectsKey(int? key, out IReadOnlyList<GrantedEffectQualityStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        if (byGrantedEffectsKey is null)
        {
            byGrantedEffectsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffectsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGrantedEffectsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGrantedEffectsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGrantedEffectsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.byGrantedEffectsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectQualityStatsDat>> GetManyToManyByGrantedEffectsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectQualityStatsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectQualityStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffectsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectQualityStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.SetId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySetId(int? key, out GrantedEffectQualityStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySetId(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.SetId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySetId(int? key, out IReadOnlyList<GrantedEffectQualityStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        if (bySetId is null)
        {
            bySetId = new();
            foreach (var item in Items)
            {
                var itemKey = item.SetId;

                if (!bySetId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySetId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySetId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.bySetId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectQualityStatsDat>> GetManyToManyBySetId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectQualityStatsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectQualityStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySetId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectQualityStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys(int? key, out GrantedEffectQualityStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKeys(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys(int? key, out IReadOnlyList<GrantedEffectQualityStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        if (byStatsKeys is null)
        {
            byStatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.byStatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectQualityStatsDat>> GetManyToManyByStatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectQualityStatsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectQualityStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectQualityStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.StatsValuesPermille"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsValuesPermille(int? key, out GrantedEffectQualityStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsValuesPermille(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.StatsValuesPermille"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsValuesPermille(int? key, out IReadOnlyList<GrantedEffectQualityStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        if (byStatsValuesPermille is null)
        {
            byStatsValuesPermille = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsValuesPermille;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsValuesPermille.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsValuesPermille.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsValuesPermille.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.byStatsValuesPermille"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectQualityStatsDat>> GetManyToManyByStatsValuesPermille(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectQualityStatsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectQualityStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsValuesPermille(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectQualityStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.Weight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeight(int? key, out GrantedEffectQualityStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeight(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.Weight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeight(int? key, out IReadOnlyList<GrantedEffectQualityStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        if (byWeight is null)
        {
            byWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.Weight;

                if (!byWeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.byWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectQualityStatsDat>> GetManyToManyByWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectQualityStatsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectQualityStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectQualityStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out GrantedEffectQualityStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<GrantedEffectQualityStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown56.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown56.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectQualityStatsDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectQualityStatsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectQualityStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectQualityStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out GrantedEffectQualityStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<GrantedEffectQualityStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown72.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown72.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectQualityStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectQualityStatsDat"/> with <see cref="GrantedEffectQualityStatsDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectQualityStatsDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectQualityStatsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectQualityStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectQualityStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GrantedEffectQualityStatsDat[] Load()
    {
        const string filePath = "Data/GrantedEffectQualityStats.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrantedEffectQualityStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading GrantedEffectsKey
            (var grantedeffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SetId
            (var setidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatsValuesPermille
            (var tempstatsvaluespermilleLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statsvaluespermilleLoading = tempstatsvaluespermilleLoading.AsReadOnly();

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading Unknown72
            (var tempunknown72Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown72Loading = tempunknown72Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectQualityStatsDat()
            {
                GrantedEffectsKey = grantedeffectskeyLoading,
                SetId = setidLoading,
                StatsKeys = statskeysLoading,
                StatsValuesPermille = statsvaluespermilleLoading,
                Weight = weightLoading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
