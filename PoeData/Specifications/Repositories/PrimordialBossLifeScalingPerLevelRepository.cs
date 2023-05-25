using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PrimordialBossLifeScalingPerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class PrimordialBossLifeScalingPerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PrimordialBossLifeScalingPerLevelDat> Items { get; }

    private Dictionary<int, List<PrimordialBossLifeScalingPerLevelDat>>? byAreaLevel;
    private Dictionary<int, List<PrimordialBossLifeScalingPerLevelDat>>? byScale;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimordialBossLifeScalingPerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PrimordialBossLifeScalingPerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PrimordialBossLifeScalingPerLevelDat"/> with <see cref="PrimordialBossLifeScalingPerLevelDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAreaLevel(int? key, out PrimordialBossLifeScalingPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAreaLevel(key, out var items))
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
    /// Tries to get <see cref="PrimordialBossLifeScalingPerLevelDat"/> with <see cref="PrimordialBossLifeScalingPerLevelDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAreaLevel(int? key, out IReadOnlyList<PrimordialBossLifeScalingPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PrimordialBossLifeScalingPerLevelDat>();
            return false;
        }

        if (byAreaLevel is null)
        {
            byAreaLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.AreaLevel;

                if (!byAreaLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAreaLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAreaLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PrimordialBossLifeScalingPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PrimordialBossLifeScalingPerLevelDat"/> with <see cref="PrimordialBossLifeScalingPerLevelDat.byAreaLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PrimordialBossLifeScalingPerLevelDat>> GetManyToManyByAreaLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PrimordialBossLifeScalingPerLevelDat>>();
        }

        var items = new List<ResultItem<int, PrimordialBossLifeScalingPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAreaLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PrimordialBossLifeScalingPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PrimordialBossLifeScalingPerLevelDat"/> with <see cref="PrimordialBossLifeScalingPerLevelDat.Scale"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScale(int? key, out PrimordialBossLifeScalingPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScale(key, out var items))
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
    /// Tries to get <see cref="PrimordialBossLifeScalingPerLevelDat"/> with <see cref="PrimordialBossLifeScalingPerLevelDat.Scale"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScale(int? key, out IReadOnlyList<PrimordialBossLifeScalingPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PrimordialBossLifeScalingPerLevelDat>();
            return false;
        }

        if (byScale is null)
        {
            byScale = new();
            foreach (var item in Items)
            {
                var itemKey = item.Scale;

                if (!byScale.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScale.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScale.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PrimordialBossLifeScalingPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PrimordialBossLifeScalingPerLevelDat"/> with <see cref="PrimordialBossLifeScalingPerLevelDat.byScale"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PrimordialBossLifeScalingPerLevelDat>> GetManyToManyByScale(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PrimordialBossLifeScalingPerLevelDat>>();
        }

        var items = new List<ResultItem<int, PrimordialBossLifeScalingPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScale(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PrimordialBossLifeScalingPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PrimordialBossLifeScalingPerLevelDat[] Load()
    {
        const string filePath = "Data/PrimordialBossLifeScalingPerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PrimordialBossLifeScalingPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Scale
            (var scaleLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PrimordialBossLifeScalingPerLevelDat()
            {
                AreaLevel = arealevelLoading,
                Scale = scaleLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
