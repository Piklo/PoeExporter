using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AlternateQualityCurrencyDecayFactorsDat"/> related data and helper methods.
/// </summary>
public sealed class AlternateQualityCurrencyDecayFactorsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AlternateQualityCurrencyDecayFactorsDat> Items { get; }

    private Dictionary<int, List<AlternateQualityCurrencyDecayFactorsDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<AlternateQualityCurrencyDecayFactorsDat>>? byFactor;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlternateQualityCurrencyDecayFactorsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AlternateQualityCurrencyDecayFactorsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityCurrencyDecayFactorsDat"/> with <see cref="AlternateQualityCurrencyDecayFactorsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out AlternateQualityCurrencyDecayFactorsDat? item)
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
    /// Tries to get <see cref="AlternateQualityCurrencyDecayFactorsDat"/> with <see cref="AlternateQualityCurrencyDecayFactorsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<AlternateQualityCurrencyDecayFactorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateQualityCurrencyDecayFactorsDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateQualityCurrencyDecayFactorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityCurrencyDecayFactorsDat"/> with <see cref="AlternateQualityCurrencyDecayFactorsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateQualityCurrencyDecayFactorsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateQualityCurrencyDecayFactorsDat>>();
        }

        var items = new List<ResultItem<int, AlternateQualityCurrencyDecayFactorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateQualityCurrencyDecayFactorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityCurrencyDecayFactorsDat"/> with <see cref="AlternateQualityCurrencyDecayFactorsDat.Factor"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFactor(int? key, out AlternateQualityCurrencyDecayFactorsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFactor(key, out var items))
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
    /// Tries to get <see cref="AlternateQualityCurrencyDecayFactorsDat"/> with <see cref="AlternateQualityCurrencyDecayFactorsDat.Factor"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFactor(int? key, out IReadOnlyList<AlternateQualityCurrencyDecayFactorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateQualityCurrencyDecayFactorsDat>();
            return false;
        }

        if (byFactor is null)
        {
            byFactor = new();
            foreach (var item in Items)
            {
                var itemKey = item.Factor;

                if (!byFactor.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFactor.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFactor.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateQualityCurrencyDecayFactorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityCurrencyDecayFactorsDat"/> with <see cref="AlternateQualityCurrencyDecayFactorsDat.byFactor"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateQualityCurrencyDecayFactorsDat>> GetManyToManyByFactor(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateQualityCurrencyDecayFactorsDat>>();
        }

        var items = new List<ResultItem<int, AlternateQualityCurrencyDecayFactorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFactor(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateQualityCurrencyDecayFactorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AlternateQualityCurrencyDecayFactorsDat[] Load()
    {
        const string filePath = "Data/AlternateQualityCurrencyDecayFactors.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AlternateQualityCurrencyDecayFactorsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Factor
            (var factorLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AlternateQualityCurrencyDecayFactorsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Factor = factorLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
