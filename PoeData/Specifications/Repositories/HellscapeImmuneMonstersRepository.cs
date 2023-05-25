using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HellscapeImmuneMonstersDat"/> related data and helper methods.
/// </summary>
public sealed class HellscapeImmuneMonstersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HellscapeImmuneMonstersDat> Items { get; }

    private Dictionary<int, List<HellscapeImmuneMonstersDat>>? byMonster;

    /// <summary>
    /// Initializes a new instance of the <see cref="HellscapeImmuneMonstersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HellscapeImmuneMonstersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeImmuneMonstersDat"/> with <see cref="HellscapeImmuneMonstersDat.Monster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonster(int? key, out HellscapeImmuneMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonster(key, out var items))
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
    /// Tries to get <see cref="HellscapeImmuneMonstersDat"/> with <see cref="HellscapeImmuneMonstersDat.Monster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonster(int? key, out IReadOnlyList<HellscapeImmuneMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeImmuneMonstersDat>();
            return false;
        }

        if (byMonster is null)
        {
            byMonster = new();
            foreach (var item in Items)
            {
                var itemKey = item.Monster;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonster.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonster.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonster.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeImmuneMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeImmuneMonstersDat"/> with <see cref="HellscapeImmuneMonstersDat.byMonster"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeImmuneMonstersDat>> GetManyToManyByMonster(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeImmuneMonstersDat>>();
        }

        var items = new List<ResultItem<int, HellscapeImmuneMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonster(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeImmuneMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HellscapeImmuneMonstersDat[] Load()
    {
        const string filePath = "Data/HellscapeImmuneMonsters.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeImmuneMonstersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Monster
            (var monsterLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeImmuneMonstersDat()
            {
                Monster = monsterLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
