using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SessionQuestFlagsDat"/> related data and helper methods.
/// </summary>
public sealed class SessionQuestFlagsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SessionQuestFlagsDat> Items { get; }

    private Dictionary<int, List<SessionQuestFlagsDat>>? byQuestFlag;

    /// <summary>
    /// Initializes a new instance of the <see cref="SessionQuestFlagsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SessionQuestFlagsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SessionQuestFlagsDat"/> with <see cref="SessionQuestFlagsDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestFlag(int? key, out SessionQuestFlagsDat? item)
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
    /// Tries to get <see cref="SessionQuestFlagsDat"/> with <see cref="SessionQuestFlagsDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestFlag(int? key, out IReadOnlyList<SessionQuestFlagsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SessionQuestFlagsDat>();
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
            items = Array.Empty<SessionQuestFlagsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SessionQuestFlagsDat"/> with <see cref="SessionQuestFlagsDat.byQuestFlag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SessionQuestFlagsDat>> GetManyToManyByQuestFlag(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SessionQuestFlagsDat>>();
        }

        var items = new List<ResultItem<int, SessionQuestFlagsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestFlag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SessionQuestFlagsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SessionQuestFlagsDat[] Load()
    {
        const string filePath = "Data/SessionQuestFlags.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SessionQuestFlagsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SessionQuestFlagsDat()
            {
                QuestFlag = questflagLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
