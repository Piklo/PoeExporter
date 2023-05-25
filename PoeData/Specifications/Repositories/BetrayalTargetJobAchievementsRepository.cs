﻿using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BetrayalTargetJobAchievementsDat"/> related data and helper methods.
/// </summary>
public sealed class BetrayalTargetJobAchievementsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BetrayalTargetJobAchievementsDat> Items { get; }

    private Dictionary<int, List<BetrayalTargetJobAchievementsDat>>? byBetrayalTargetsKey;
    private Dictionary<int, List<BetrayalTargetJobAchievementsDat>>? byBetrayalJobsKey;
    private Dictionary<int, List<BetrayalTargetJobAchievementsDat>>? byAchievementItemsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="BetrayalTargetJobAchievementsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BetrayalTargetJobAchievementsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetJobAchievementsDat"/> with <see cref="BetrayalTargetJobAchievementsDat.BetrayalTargetsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalTargetsKey(int? key, out BetrayalTargetJobAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalTargetsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetJobAchievementsDat"/> with <see cref="BetrayalTargetJobAchievementsDat.BetrayalTargetsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalTargetsKey(int? key, out IReadOnlyList<BetrayalTargetJobAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetJobAchievementsDat>();
            return false;
        }

        if (byBetrayalTargetsKey is null)
        {
            byBetrayalTargetsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalTargetsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBetrayalTargetsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBetrayalTargetsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalTargetsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetJobAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetJobAchievementsDat"/> with <see cref="BetrayalTargetJobAchievementsDat.byBetrayalTargetsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetJobAchievementsDat>> GetManyToManyByBetrayalTargetsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetJobAchievementsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetJobAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalTargetsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetJobAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetJobAchievementsDat"/> with <see cref="BetrayalTargetJobAchievementsDat.BetrayalJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalJobsKey(int? key, out BetrayalTargetJobAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalJobsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetJobAchievementsDat"/> with <see cref="BetrayalTargetJobAchievementsDat.BetrayalJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalJobsKey(int? key, out IReadOnlyList<BetrayalTargetJobAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetJobAchievementsDat>();
            return false;
        }

        if (byBetrayalJobsKey is null)
        {
            byBetrayalJobsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalJobsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBetrayalJobsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBetrayalJobsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalJobsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetJobAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetJobAchievementsDat"/> with <see cref="BetrayalTargetJobAchievementsDat.byBetrayalJobsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetJobAchievementsDat>> GetManyToManyByBetrayalJobsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetJobAchievementsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetJobAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalJobsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetJobAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetJobAchievementsDat"/> with <see cref="BetrayalTargetJobAchievementsDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKey(int? key, out BetrayalTargetJobAchievementsDat? item)
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
    /// Tries to get <see cref="BetrayalTargetJobAchievementsDat"/> with <see cref="BetrayalTargetJobAchievementsDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKey(int? key, out IReadOnlyList<BetrayalTargetJobAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetJobAchievementsDat>();
            return false;
        }

        if (byAchievementItemsKey is null)
        {
            byAchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetJobAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetJobAchievementsDat"/> with <see cref="BetrayalTargetJobAchievementsDat.byAchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetJobAchievementsDat>> GetManyToManyByAchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetJobAchievementsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetJobAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetJobAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BetrayalTargetJobAchievementsDat[] Load()
    {
        const string filePath = "Data/BetrayalTargetJobAchievements.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalTargetJobAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BetrayalTargetsKey
            (var betrayaltargetskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BetrayalJobsKey
            (var betrayaljobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AchievementItemsKey
            (var achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalTargetJobAchievementsDat()
            {
                BetrayalTargetsKey = betrayaltargetskeyLoading,
                BetrayalJobsKey = betrayaljobskeyLoading,
                AchievementItemsKey = achievementitemskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
