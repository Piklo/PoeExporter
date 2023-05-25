using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="EventSeasonRewardsDat"/> related data and helper methods.
/// </summary>
public sealed class EventSeasonRewardsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<EventSeasonRewardsDat> Items { get; }

    private Dictionary<int, List<EventSeasonRewardsDat>>? byEventSeasonKey;
    private Dictionary<int, List<EventSeasonRewardsDat>>? byPoint;
    private Dictionary<string, List<EventSeasonRewardsDat>>? byRewardCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventSeasonRewardsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal EventSeasonRewardsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="EventSeasonRewardsDat"/> with <see cref="EventSeasonRewardsDat.EventSeasonKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEventSeasonKey(int? key, out EventSeasonRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEventSeasonKey(key, out var items))
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
    /// Tries to get <see cref="EventSeasonRewardsDat"/> with <see cref="EventSeasonRewardsDat.EventSeasonKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEventSeasonKey(int? key, out IReadOnlyList<EventSeasonRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EventSeasonRewardsDat>();
            return false;
        }

        if (byEventSeasonKey is null)
        {
            byEventSeasonKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.EventSeasonKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEventSeasonKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEventSeasonKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEventSeasonKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EventSeasonRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EventSeasonRewardsDat"/> with <see cref="EventSeasonRewardsDat.byEventSeasonKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EventSeasonRewardsDat>> GetManyToManyByEventSeasonKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EventSeasonRewardsDat>>();
        }

        var items = new List<ResultItem<int, EventSeasonRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEventSeasonKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EventSeasonRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EventSeasonRewardsDat"/> with <see cref="EventSeasonRewardsDat.Point"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPoint(int? key, out EventSeasonRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPoint(key, out var items))
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
    /// Tries to get <see cref="EventSeasonRewardsDat"/> with <see cref="EventSeasonRewardsDat.Point"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPoint(int? key, out IReadOnlyList<EventSeasonRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EventSeasonRewardsDat>();
            return false;
        }

        if (byPoint is null)
        {
            byPoint = new();
            foreach (var item in Items)
            {
                var itemKey = item.Point;

                if (!byPoint.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPoint.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPoint.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EventSeasonRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EventSeasonRewardsDat"/> with <see cref="EventSeasonRewardsDat.byPoint"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EventSeasonRewardsDat>> GetManyToManyByPoint(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EventSeasonRewardsDat>>();
        }

        var items = new List<ResultItem<int, EventSeasonRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPoint(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EventSeasonRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EventSeasonRewardsDat"/> with <see cref="EventSeasonRewardsDat.RewardCommand"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewardCommand(string? key, out EventSeasonRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewardCommand(key, out var items))
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
    /// Tries to get <see cref="EventSeasonRewardsDat"/> with <see cref="EventSeasonRewardsDat.RewardCommand"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewardCommand(string? key, out IReadOnlyList<EventSeasonRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EventSeasonRewardsDat>();
            return false;
        }

        if (byRewardCommand is null)
        {
            byRewardCommand = new();
            foreach (var item in Items)
            {
                var itemKey = item.RewardCommand;

                if (!byRewardCommand.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRewardCommand.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRewardCommand.TryGetValue(key, out var temp))
        {
            items = Array.Empty<EventSeasonRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EventSeasonRewardsDat"/> with <see cref="EventSeasonRewardsDat.byRewardCommand"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EventSeasonRewardsDat>> GetManyToManyByRewardCommand(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EventSeasonRewardsDat>>();
        }

        var items = new List<ResultItem<string, EventSeasonRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewardCommand(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EventSeasonRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private EventSeasonRewardsDat[] Load()
    {
        const string filePath = "Data/EventSeasonRewards.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EventSeasonRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading EventSeasonKey
            (var eventseasonkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Point
            (var pointLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RewardCommand
            (var rewardcommandLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EventSeasonRewardsDat()
            {
                EventSeasonKey = eventseasonkeyLoading,
                Point = pointLoading,
                RewardCommand = rewardcommandLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
