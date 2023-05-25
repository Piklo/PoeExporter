using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PassiveSkillMasteryEffectsDat"/> related data and helper methods.
/// </summary>
public sealed class PassiveSkillMasteryEffectsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PassiveSkillMasteryEffectsDat> Items { get; }

    private Dictionary<string, List<PassiveSkillMasteryEffectsDat>>? byId;
    private Dictionary<int, List<PassiveSkillMasteryEffectsDat>>? byHASH16;
    private Dictionary<int, List<PassiveSkillMasteryEffectsDat>>? byStats;
    private Dictionary<int, List<PassiveSkillMasteryEffectsDat>>? byStat1Value;
    private Dictionary<int, List<PassiveSkillMasteryEffectsDat>>? byStat2Value;
    private Dictionary<int, List<PassiveSkillMasteryEffectsDat>>? byStat3Value;
    private Dictionary<int, List<PassiveSkillMasteryEffectsDat>>? byAchievementItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassiveSkillMasteryEffectsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PassiveSkillMasteryEffectsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out PassiveSkillMasteryEffectsDat? item)
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
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<PassiveSkillMasteryEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
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
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillMasteryEffectsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillMasteryEffectsDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillMasteryEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillMasteryEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH16(int? key, out PassiveSkillMasteryEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHASH16(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH16(int? key, out IReadOnlyList<PassiveSkillMasteryEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        if (byHASH16 is null)
        {
            byHASH16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HASH16;

                if (!byHASH16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHASH16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHASH16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.byHASH16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillMasteryEffectsDat>> GetManyToManyByHASH16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillMasteryEffectsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillMasteryEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillMasteryEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.Stats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStats(int? key, out PassiveSkillMasteryEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStats(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.Stats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStats(int? key, out IReadOnlyList<PassiveSkillMasteryEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        if (byStats is null)
        {
            byStats = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stats;
                foreach (var listKey in itemKey)
                {
                    if (!byStats.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStats.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStats.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.byStats"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillMasteryEffectsDat>> GetManyToManyByStats(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillMasteryEffectsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillMasteryEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStats(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillMasteryEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.Stat1Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat1Value(int? key, out PassiveSkillMasteryEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat1Value(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.Stat1Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat1Value(int? key, out IReadOnlyList<PassiveSkillMasteryEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        if (byStat1Value is null)
        {
            byStat1Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat1Value;

                if (!byStat1Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat1Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat1Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.byStat1Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillMasteryEffectsDat>> GetManyToManyByStat1Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillMasteryEffectsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillMasteryEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat1Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillMasteryEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.Stat2Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat2Value(int? key, out PassiveSkillMasteryEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat2Value(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.Stat2Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat2Value(int? key, out IReadOnlyList<PassiveSkillMasteryEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        if (byStat2Value is null)
        {
            byStat2Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat2Value;

                if (!byStat2Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat2Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat2Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.byStat2Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillMasteryEffectsDat>> GetManyToManyByStat2Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillMasteryEffectsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillMasteryEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat2Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillMasteryEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.Stat3Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat3Value(int? key, out PassiveSkillMasteryEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat3Value(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.Stat3Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat3Value(int? key, out IReadOnlyList<PassiveSkillMasteryEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        if (byStat3Value is null)
        {
            byStat3Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat3Value;

                if (!byStat3Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat3Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat3Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.byStat3Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillMasteryEffectsDat>> GetManyToManyByStat3Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillMasteryEffectsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillMasteryEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat3Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillMasteryEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.AchievementItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItem(int? key, out PassiveSkillMasteryEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItem(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.AchievementItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItem(int? key, out IReadOnlyList<PassiveSkillMasteryEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        if (byAchievementItem is null)
        {
            byAchievementItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAchievementItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAchievementItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAchievementItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryEffectsDat"/> with <see cref="PassiveSkillMasteryEffectsDat.byAchievementItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillMasteryEffectsDat>> GetManyToManyByAchievementItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillMasteryEffectsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillMasteryEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillMasteryEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PassiveSkillMasteryEffectsDat[] Load()
    {
        const string filePath = "Data/PassiveSkillMasteryEffects.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillMasteryEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading Stat1Value
            (var stat1valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Value
            (var stat2valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat3Value
            (var stat3valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementItem
            (var achievementitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillMasteryEffectsDat()
            {
                Id = idLoading,
                HASH16 = hash16Loading,
                Stats = statsLoading,
                Stat1Value = stat1valueLoading,
                Stat2Value = stat2valueLoading,
                Stat3Value = stat3valueLoading,
                AchievementItem = achievementitemLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
