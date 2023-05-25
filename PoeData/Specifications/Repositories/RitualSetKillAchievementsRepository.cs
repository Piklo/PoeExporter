using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="RitualSetKillAchievementsDat"/> related data and helper methods.
/// </summary>
public sealed class RitualSetKillAchievementsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<RitualSetKillAchievementsDat> Items { get; }

    private Dictionary<int, List<RitualSetKillAchievementsDat>>? byAchievement;
    private Dictionary<int, List<RitualSetKillAchievementsDat>>? byKillBosses;

    /// <summary>
    /// Initializes a new instance of the <see cref="RitualSetKillAchievementsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal RitualSetKillAchievementsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="RitualSetKillAchievementsDat"/> with <see cref="RitualSetKillAchievementsDat.Achievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievement(int? key, out RitualSetKillAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievement(key, out var items))
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
    /// Tries to get <see cref="RitualSetKillAchievementsDat"/> with <see cref="RitualSetKillAchievementsDat.Achievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievement(int? key, out IReadOnlyList<RitualSetKillAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualSetKillAchievementsDat>();
            return false;
        }

        if (byAchievement is null)
        {
            byAchievement = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievement;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAchievement.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAchievement.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAchievement.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualSetKillAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualSetKillAchievementsDat"/> with <see cref="RitualSetKillAchievementsDat.byAchievement"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualSetKillAchievementsDat>> GetManyToManyByAchievement(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualSetKillAchievementsDat>>();
        }

        var items = new List<ResultItem<int, RitualSetKillAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievement(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualSetKillAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualSetKillAchievementsDat"/> with <see cref="RitualSetKillAchievementsDat.KillBosses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKillBosses(int? key, out RitualSetKillAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKillBosses(key, out var items))
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
    /// Tries to get <see cref="RitualSetKillAchievementsDat"/> with <see cref="RitualSetKillAchievementsDat.KillBosses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKillBosses(int? key, out IReadOnlyList<RitualSetKillAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualSetKillAchievementsDat>();
            return false;
        }

        if (byKillBosses is null)
        {
            byKillBosses = new();
            foreach (var item in Items)
            {
                var itemKey = item.KillBosses;
                foreach (var listKey in itemKey)
                {
                    if (!byKillBosses.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byKillBosses.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byKillBosses.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualSetKillAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualSetKillAchievementsDat"/> with <see cref="RitualSetKillAchievementsDat.byKillBosses"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualSetKillAchievementsDat>> GetManyToManyByKillBosses(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualSetKillAchievementsDat>>();
        }

        var items = new List<ResultItem<int, RitualSetKillAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKillBosses(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualSetKillAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private RitualSetKillAchievementsDat[] Load()
    {
        const string filePath = "Data/RitualSetKillAchievements.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RitualSetKillAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Achievement
            (var achievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading KillBosses
            (var tempkillbossesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var killbossesLoading = tempkillbossesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RitualSetKillAchievementsDat()
            {
                Achievement = achievementLoading,
                KillBosses = killbossesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
