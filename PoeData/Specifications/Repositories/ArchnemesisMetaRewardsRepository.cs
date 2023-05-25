using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ArchnemesisMetaRewardsDat"/> related data and helper methods.
/// </summary>
public sealed class ArchnemesisMetaRewardsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ArchnemesisMetaRewardsDat> Items { get; }

    private Dictionary<string, List<ArchnemesisMetaRewardsDat>>? byId;
    private Dictionary<string, List<ArchnemesisMetaRewardsDat>>? byRewardText;
    private Dictionary<int, List<ArchnemesisMetaRewardsDat>>? byRewardGroup;
    private Dictionary<string, List<ArchnemesisMetaRewardsDat>>? byScriptArgument;
    private Dictionary<int, List<ArchnemesisMetaRewardsDat>>? byMinLevel;
    private Dictionary<int, List<ArchnemesisMetaRewardsDat>>? byMaxLevel;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchnemesisMetaRewardsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ArchnemesisMetaRewardsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ArchnemesisMetaRewardsDat? item)
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
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ArchnemesisMetaRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
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
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchnemesisMetaRewardsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchnemesisMetaRewardsDat>>();
        }

        var items = new List<ResultItem<string, ArchnemesisMetaRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchnemesisMetaRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.RewardText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewardText(string? key, out ArchnemesisMetaRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewardText(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.RewardText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewardText(string? key, out IReadOnlyList<ArchnemesisMetaRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
            return false;
        }

        if (byRewardText is null)
        {
            byRewardText = new();
            foreach (var item in Items)
            {
                var itemKey = item.RewardText;

                if (!byRewardText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRewardText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRewardText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.byRewardText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchnemesisMetaRewardsDat>> GetManyToManyByRewardText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchnemesisMetaRewardsDat>>();
        }

        var items = new List<ResultItem<string, ArchnemesisMetaRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewardText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchnemesisMetaRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.RewardGroup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewardGroup(int? key, out ArchnemesisMetaRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewardGroup(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.RewardGroup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewardGroup(int? key, out IReadOnlyList<ArchnemesisMetaRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
            return false;
        }

        if (byRewardGroup is null)
        {
            byRewardGroup = new();
            foreach (var item in Items)
            {
                var itemKey = item.RewardGroup;

                if (!byRewardGroup.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRewardGroup.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRewardGroup.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.byRewardGroup"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArchnemesisMetaRewardsDat>> GetManyToManyByRewardGroup(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArchnemesisMetaRewardsDat>>();
        }

        var items = new List<ResultItem<int, ArchnemesisMetaRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewardGroup(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArchnemesisMetaRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.ScriptArgument"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScriptArgument(string? key, out ArchnemesisMetaRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScriptArgument(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.ScriptArgument"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScriptArgument(string? key, out IReadOnlyList<ArchnemesisMetaRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
            return false;
        }

        if (byScriptArgument is null)
        {
            byScriptArgument = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScriptArgument;

                if (!byScriptArgument.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScriptArgument.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScriptArgument.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.byScriptArgument"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchnemesisMetaRewardsDat>> GetManyToManyByScriptArgument(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchnemesisMetaRewardsDat>>();
        }

        var items = new List<ResultItem<string, ArchnemesisMetaRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScriptArgument(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchnemesisMetaRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out ArchnemesisMetaRewardsDat? item)
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
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<ArchnemesisMetaRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
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
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArchnemesisMetaRewardsDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArchnemesisMetaRewardsDat>>();
        }

        var items = new List<ResultItem<int, ArchnemesisMetaRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArchnemesisMetaRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out ArchnemesisMetaRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxLevel(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<ArchnemesisMetaRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
            return false;
        }

        if (byMaxLevel is null)
        {
            byMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxLevel;

                if (!byMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchnemesisMetaRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisMetaRewardsDat"/> with <see cref="ArchnemesisMetaRewardsDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArchnemesisMetaRewardsDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArchnemesisMetaRewardsDat>>();
        }

        var items = new List<ResultItem<int, ArchnemesisMetaRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArchnemesisMetaRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ArchnemesisMetaRewardsDat[] Load()
    {
        const string filePath = "Data/ArchnemesisMetaRewards.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArchnemesisMetaRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RewardText
            (var rewardtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RewardGroup
            (var rewardgroupLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ScriptArgument
            (var scriptargumentLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArchnemesisMetaRewardsDat()
            {
                Id = idLoading,
                RewardText = rewardtextLoading,
                RewardGroup = rewardgroupLoading,
                ScriptArgument = scriptargumentLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
