using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PassiveTreeExpansionSpecialSkillsDat"/> related data and helper methods.
/// </summary>
public sealed class PassiveTreeExpansionSpecialSkillsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PassiveTreeExpansionSpecialSkillsDat> Items { get; }

    private Dictionary<int, List<PassiveTreeExpansionSpecialSkillsDat>>? byPassiveSkillsKey;
    private Dictionary<int, List<PassiveTreeExpansionSpecialSkillsDat>>? byStatsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassiveTreeExpansionSpecialSkillsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PassiveTreeExpansionSpecialSkillsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSpecialSkillsDat"/> with <see cref="PassiveTreeExpansionSpecialSkillsDat.PassiveSkillsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveSkillsKey(int? key, out PassiveTreeExpansionSpecialSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveSkillsKey(key, out var items))
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
    /// Tries to get <see cref="PassiveTreeExpansionSpecialSkillsDat"/> with <see cref="PassiveTreeExpansionSpecialSkillsDat.PassiveSkillsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveSkillsKey(int? key, out IReadOnlyList<PassiveTreeExpansionSpecialSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionSpecialSkillsDat>();
            return false;
        }

        if (byPassiveSkillsKey is null)
        {
            byPassiveSkillsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveSkillsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPassiveSkillsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPassiveSkillsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveSkillsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionSpecialSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSpecialSkillsDat"/> with <see cref="PassiveTreeExpansionSpecialSkillsDat.byPassiveSkillsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionSpecialSkillsDat>> GetManyToManyByPassiveSkillsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionSpecialSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionSpecialSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveSkillsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionSpecialSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSpecialSkillsDat"/> with <see cref="PassiveTreeExpansionSpecialSkillsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey(int? key, out PassiveTreeExpansionSpecialSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey(key, out var items))
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
    /// Tries to get <see cref="PassiveTreeExpansionSpecialSkillsDat"/> with <see cref="PassiveTreeExpansionSpecialSkillsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey(int? key, out IReadOnlyList<PassiveTreeExpansionSpecialSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveTreeExpansionSpecialSkillsDat>();
            return false;
        }

        if (byStatsKey is null)
        {
            byStatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveTreeExpansionSpecialSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveTreeExpansionSpecialSkillsDat"/> with <see cref="PassiveTreeExpansionSpecialSkillsDat.byStatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveTreeExpansionSpecialSkillsDat>> GetManyToManyByStatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveTreeExpansionSpecialSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveTreeExpansionSpecialSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveTreeExpansionSpecialSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PassiveTreeExpansionSpecialSkillsDat[] Load()
    {
        const string filePath = "Data/PassiveTreeExpansionSpecialSkills.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveTreeExpansionSpecialSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading PassiveSkillsKey
            (var passiveskillskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveTreeExpansionSpecialSkillsDat()
            {
                PassiveSkillsKey = passiveskillskeyLoading,
                StatsKey = statskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
