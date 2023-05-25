using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BetrayalTraitorRewardsDat"/> related data and helper methods.
/// </summary>
public sealed class BetrayalTraitorRewardsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BetrayalTraitorRewardsDat> Items { get; }

    private Dictionary<int, List<BetrayalTraitorRewardsDat>>? byBetrayalJobsKey;
    private Dictionary<int, List<BetrayalTraitorRewardsDat>>? byBetrayalTargetsKey;
    private Dictionary<int, List<BetrayalTraitorRewardsDat>>? byBetrayalRanksKey;
    private Dictionary<string, List<BetrayalTraitorRewardsDat>>? byDescription;

    /// <summary>
    /// Initializes a new instance of the <see cref="BetrayalTraitorRewardsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BetrayalTraitorRewardsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.BetrayalJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalJobsKey(int? key, out BetrayalTraitorRewardsDat? item)
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
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.BetrayalJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalJobsKey(int? key, out IReadOnlyList<BetrayalTraitorRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTraitorRewardsDat>();
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
            items = Array.Empty<BetrayalTraitorRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.byBetrayalJobsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTraitorRewardsDat>> GetManyToManyByBetrayalJobsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTraitorRewardsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTraitorRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalJobsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTraitorRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.BetrayalTargetsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalTargetsKey(int? key, out BetrayalTraitorRewardsDat? item)
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
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.BetrayalTargetsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalTargetsKey(int? key, out IReadOnlyList<BetrayalTraitorRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTraitorRewardsDat>();
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
            items = Array.Empty<BetrayalTraitorRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.byBetrayalTargetsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTraitorRewardsDat>> GetManyToManyByBetrayalTargetsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTraitorRewardsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTraitorRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalTargetsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTraitorRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.BetrayalRanksKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalRanksKey(int? key, out BetrayalTraitorRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalRanksKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.BetrayalRanksKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalRanksKey(int? key, out IReadOnlyList<BetrayalTraitorRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTraitorRewardsDat>();
            return false;
        }

        if (byBetrayalRanksKey is null)
        {
            byBetrayalRanksKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalRanksKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBetrayalRanksKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBetrayalRanksKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalRanksKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTraitorRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.byBetrayalRanksKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTraitorRewardsDat>> GetManyToManyByBetrayalRanksKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTraitorRewardsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTraitorRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalRanksKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTraitorRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out BetrayalTraitorRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<BetrayalTraitorRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTraitorRewardsDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BetrayalTraitorRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTraitorRewardsDat"/> with <see cref="BetrayalTraitorRewardsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalTraitorRewardsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalTraitorRewardsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalTraitorRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalTraitorRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BetrayalTraitorRewardsDat[] Load()
    {
        const string filePath = "Data/BetrayalTraitorRewards.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalTraitorRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BetrayalJobsKey
            (var betrayaljobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BetrayalTargetsKey
            (var betrayaltargetskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BetrayalRanksKey
            (var betrayalrankskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalTraitorRewardsDat()
            {
                BetrayalJobsKey = betrayaljobskeyLoading,
                BetrayalTargetsKey = betrayaltargetskeyLoading,
                BetrayalRanksKey = betrayalrankskeyLoading,
                Description = descriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
