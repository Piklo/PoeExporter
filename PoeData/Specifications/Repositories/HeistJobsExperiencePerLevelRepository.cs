using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistJobsExperiencePerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class HeistJobsExperiencePerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistJobsExperiencePerLevelDat> Items { get; }

    private Dictionary<int, List<HeistJobsExperiencePerLevelDat>>? byHeistJobsKey;
    private Dictionary<int, List<HeistJobsExperiencePerLevelDat>>? byTier;
    private Dictionary<int, List<HeistJobsExperiencePerLevelDat>>? byExperience;
    private Dictionary<int, List<HeistJobsExperiencePerLevelDat>>? byMinLevel;
    private Dictionary<int, List<HeistJobsExperiencePerLevelDat>>? byAchievementItemsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistJobsExperiencePerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistJobsExperiencePerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistJobsKey(int? key, out HeistJobsExperiencePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistJobsKey(key, out var items))
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
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistJobsKey(int? key, out IReadOnlyList<HeistJobsExperiencePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsExperiencePerLevelDat>();
            return false;
        }

        if (byHeistJobsKey is null)
        {
            byHeistJobsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistJobsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistJobsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistJobsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistJobsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsExperiencePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.byHeistJobsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsExperiencePerLevelDat>> GetManyToManyByHeistJobsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsExperiencePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsExperiencePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistJobsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsExperiencePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out HeistJobsExperiencePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier(key, out var items))
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
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<HeistJobsExperiencePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsExperiencePerLevelDat>();
            return false;
        }

        if (byTier is null)
        {
            byTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier;

                if (!byTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsExperiencePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsExperiencePerLevelDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsExperiencePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsExperiencePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsExperiencePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.Experience"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExperience(int? key, out HeistJobsExperiencePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExperience(key, out var items))
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
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.Experience"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExperience(int? key, out IReadOnlyList<HeistJobsExperiencePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsExperiencePerLevelDat>();
            return false;
        }

        if (byExperience is null)
        {
            byExperience = new();
            foreach (var item in Items)
            {
                var itemKey = item.Experience;

                if (!byExperience.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byExperience.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byExperience.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsExperiencePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.byExperience"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsExperiencePerLevelDat>> GetManyToManyByExperience(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsExperiencePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsExperiencePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExperience(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsExperiencePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out HeistJobsExperiencePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLevel(key, out var items))
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
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<HeistJobsExperiencePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsExperiencePerLevelDat>();
            return false;
        }

        if (byMinLevel is null)
        {
            byMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLevel;

                if (!byMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsExperiencePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsExperiencePerLevelDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsExperiencePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsExperiencePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsExperiencePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKey(int? key, out HeistJobsExperiencePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKey(int? key, out IReadOnlyList<HeistJobsExperiencePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsExperiencePerLevelDat>();
            return false;
        }

        if (byAchievementItemsKey is null)
        {
            byAchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsExperiencePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsExperiencePerLevelDat"/> with <see cref="HeistJobsExperiencePerLevelDat.byAchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsExperiencePerLevelDat>> GetManyToManyByAchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsExperiencePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsExperiencePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsExperiencePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistJobsExperiencePerLevelDat[] Load()
    {
        const string filePath = "Data/HeistJobsExperiencePerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistJobsExperiencePerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HeistJobsKey
            (var heistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Experience
            (var experienceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementItemsKey
            (var tempachievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeyLoading = tempachievementitemskeyLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistJobsExperiencePerLevelDat()
            {
                HeistJobsKey = heistjobskeyLoading,
                Tier = tierLoading,
                Experience = experienceLoading,
                MinLevel = minlevelLoading,
                AchievementItemsKey = achievementitemskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
