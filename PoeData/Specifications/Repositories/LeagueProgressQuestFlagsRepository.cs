using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LeagueProgressQuestFlagsDat"/> related data and helper methods.
/// </summary>
public sealed class LeagueProgressQuestFlagsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LeagueProgressQuestFlagsDat> Items { get; }

    private Dictionary<int, List<LeagueProgressQuestFlagsDat>>? byQuestFlag;
    private Dictionary<int, List<LeagueProgressQuestFlagsDat>>? byCompletionString;
    private Dictionary<string, List<LeagueProgressQuestFlagsDat>>? byBoss;
    private Dictionary<bool, List<LeagueProgressQuestFlagsDat>>? byUnknown40;

    /// <summary>
    /// Initializes a new instance of the <see cref="LeagueProgressQuestFlagsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LeagueProgressQuestFlagsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestFlag(int? key, out LeagueProgressQuestFlagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestFlag(key, out var items))
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
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestFlag(int? key, out IReadOnlyList<LeagueProgressQuestFlagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueProgressQuestFlagsDat>();
            return false;
        }

        if (byQuestFlag is null)
        {
            byQuestFlag = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestFlag;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestFlag.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestFlag.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestFlag.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LeagueProgressQuestFlagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.byQuestFlag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LeagueProgressQuestFlagsDat>> GetManyToManyByQuestFlag(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LeagueProgressQuestFlagsDat>>();
        }

        var items = new List<ResultItem<int, LeagueProgressQuestFlagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestFlag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LeagueProgressQuestFlagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.CompletionString"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCompletionString(int? key, out LeagueProgressQuestFlagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCompletionString(key, out var items))
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
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.CompletionString"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCompletionString(int? key, out IReadOnlyList<LeagueProgressQuestFlagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueProgressQuestFlagsDat>();
            return false;
        }

        if (byCompletionString is null)
        {
            byCompletionString = new();
            foreach (var item in Items)
            {
                var itemKey = item.CompletionString;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCompletionString.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCompletionString.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCompletionString.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LeagueProgressQuestFlagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.byCompletionString"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LeagueProgressQuestFlagsDat>> GetManyToManyByCompletionString(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LeagueProgressQuestFlagsDat>>();
        }

        var items = new List<ResultItem<int, LeagueProgressQuestFlagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCompletionString(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LeagueProgressQuestFlagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.Boss"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBoss(string? key, out LeagueProgressQuestFlagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBoss(key, out var items))
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
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.Boss"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBoss(string? key, out IReadOnlyList<LeagueProgressQuestFlagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueProgressQuestFlagsDat>();
            return false;
        }

        if (byBoss is null)
        {
            byBoss = new();
            foreach (var item in Items)
            {
                var itemKey = item.Boss;

                if (!byBoss.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBoss.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBoss.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LeagueProgressQuestFlagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.byBoss"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueProgressQuestFlagsDat>> GetManyToManyByBoss(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueProgressQuestFlagsDat>>();
        }

        var items = new List<ResultItem<string, LeagueProgressQuestFlagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBoss(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueProgressQuestFlagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(bool? key, out LeagueProgressQuestFlagsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(bool? key, out IReadOnlyList<LeagueProgressQuestFlagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueProgressQuestFlagsDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;

                if (!byUnknown40.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LeagueProgressQuestFlagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueProgressQuestFlagsDat"/> with <see cref="LeagueProgressQuestFlagsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LeagueProgressQuestFlagsDat>> GetManyToManyByUnknown40(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LeagueProgressQuestFlagsDat>>();
        }

        var items = new List<ResultItem<bool, LeagueProgressQuestFlagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LeagueProgressQuestFlagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LeagueProgressQuestFlagsDat[] Load()
    {
        const string filePath = "Data/LeagueProgressQuestFlags.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LeagueProgressQuestFlagsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading CompletionString
            (var completionstringLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Boss
            (var bossLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LeagueProgressQuestFlagsDat()
            {
                QuestFlag = questflagLoading,
                CompletionString = completionstringLoading,
                Boss = bossLoading,
                Unknown40 = unknown40Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
