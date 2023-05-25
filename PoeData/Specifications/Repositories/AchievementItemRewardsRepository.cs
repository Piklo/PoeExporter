using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AchievementItemRewardsDat"/> related data and helper methods.
/// </summary>
public sealed class AchievementItemRewardsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AchievementItemRewardsDat> Items { get; }

    private Dictionary<int, List<AchievementItemRewardsDat>>? byAchievementItemsKey;
    private Dictionary<int, List<AchievementItemRewardsDat>>? byBaseItemTypesKey;
    private Dictionary<string, List<AchievementItemRewardsDat>>? byMessage;
    private Dictionary<string, List<AchievementItemRewardsDat>>? byId;

    /// <summary>
    /// Initializes a new instance of the <see cref="AchievementItemRewardsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AchievementItemRewardsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKey(int? key, out AchievementItemRewardsDat? item)
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
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKey(int? key, out IReadOnlyList<AchievementItemRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemRewardsDat>();
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
            items = Array.Empty<AchievementItemRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.byAchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementItemRewardsDat>> GetManyToManyByAchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementItemRewardsDat>>();
        }

        var items = new List<ResultItem<int, AchievementItemRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementItemRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out AchievementItemRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<AchievementItemRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemRewardsDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementItemRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementItemRewardsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementItemRewardsDat>>();
        }

        var items = new List<ResultItem<int, AchievementItemRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementItemRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.Message"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMessage(string? key, out AchievementItemRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMessage(key, out var items))
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
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.Message"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMessage(string? key, out IReadOnlyList<AchievementItemRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemRewardsDat>();
            return false;
        }

        if (byMessage is null)
        {
            byMessage = new();
            foreach (var item in Items)
            {
                var itemKey = item.Message;

                if (!byMessage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMessage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMessage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AchievementItemRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.byMessage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementItemRewardsDat>> GetManyToManyByMessage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementItemRewardsDat>>();
        }

        var items = new List<ResultItem<string, AchievementItemRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMessage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementItemRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AchievementItemRewardsDat? item)
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
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AchievementItemRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemRewardsDat>();
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
            items = Array.Empty<AchievementItemRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemRewardsDat"/> with <see cref="AchievementItemRewardsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementItemRewardsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementItemRewardsDat>>();
        }

        var items = new List<ResultItem<string, AchievementItemRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementItemRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AchievementItemRewardsDat[] Load()
    {
        const string filePath = "Data/AchievementItemRewards.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AchievementItemRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading AchievementItemsKey
            (var achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Message
            (var messageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AchievementItemRewardsDat()
            {
                AchievementItemsKey = achievementitemskeyLoading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Message = messageLoading,
                Id = idLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
