using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BetrayalTargetLifeScalingPerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class BetrayalTargetLifeScalingPerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BetrayalTargetLifeScalingPerLevelDat> Items { get; }

    private Dictionary<int, List<BetrayalTargetLifeScalingPerLevelDat>>? byLevel;
    private Dictionary<int, List<BetrayalTargetLifeScalingPerLevelDat>>? byMoreLife;

    /// <summary>
    /// Initializes a new instance of the <see cref="BetrayalTargetLifeScalingPerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BetrayalTargetLifeScalingPerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetLifeScalingPerLevelDat"/> with <see cref="BetrayalTargetLifeScalingPerLevelDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out BetrayalTargetLifeScalingPerLevelDat? item)
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
    /// Tries to get <see cref="BetrayalTargetLifeScalingPerLevelDat"/> with <see cref="BetrayalTargetLifeScalingPerLevelDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<BetrayalTargetLifeScalingPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetLifeScalingPerLevelDat>();
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
            items = Array.Empty<BetrayalTargetLifeScalingPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetLifeScalingPerLevelDat"/> with <see cref="BetrayalTargetLifeScalingPerLevelDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetLifeScalingPerLevelDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetLifeScalingPerLevelDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetLifeScalingPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetLifeScalingPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetLifeScalingPerLevelDat"/> with <see cref="BetrayalTargetLifeScalingPerLevelDat.MoreLife"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMoreLife(int? key, out BetrayalTargetLifeScalingPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMoreLife(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetLifeScalingPerLevelDat"/> with <see cref="BetrayalTargetLifeScalingPerLevelDat.MoreLife"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMoreLife(int? key, out IReadOnlyList<BetrayalTargetLifeScalingPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetLifeScalingPerLevelDat>();
            return false;
        }

        if (byMoreLife is null)
        {
            byMoreLife = new();
            foreach (var item in Items)
            {
                var itemKey = item.MoreLife;

                if (!byMoreLife.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMoreLife.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMoreLife.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetLifeScalingPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetLifeScalingPerLevelDat"/> with <see cref="BetrayalTargetLifeScalingPerLevelDat.byMoreLife"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetLifeScalingPerLevelDat>> GetManyToManyByMoreLife(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetLifeScalingPerLevelDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetLifeScalingPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMoreLife(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetLifeScalingPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BetrayalTargetLifeScalingPerLevelDat[] Load()
    {
        const string filePath = "Data/BetrayalTargetLifeScalingPerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalTargetLifeScalingPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MoreLife
            (var morelifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalTargetLifeScalingPerLevelDat()
            {
                Level = levelLoading,
                MoreLife = morelifeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
