using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ActiveSkillTypeDat"/> related data and helper methods.
/// </summary>
public sealed class ActiveSkillTypeRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ActiveSkillTypeDat> Items { get; }

    private Dictionary<string, List<ActiveSkillTypeDat>>? byId;
    private Dictionary<int, List<ActiveSkillTypeDat>>? byFlagStat;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActiveSkillTypeRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ActiveSkillTypeRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillTypeDat"/> with <see cref="ActiveSkillTypeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ActiveSkillTypeDat? item)
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
    /// Tries to get <see cref="ActiveSkillTypeDat"/> with <see cref="ActiveSkillTypeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ActiveSkillTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillTypeDat>();
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
            items = Array.Empty<ActiveSkillTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillTypeDat"/> with <see cref="ActiveSkillTypeDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ActiveSkillTypeDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ActiveSkillTypeDat>>();
        }

        var items = new List<ResultItem<string, ActiveSkillTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ActiveSkillTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillTypeDat"/> with <see cref="ActiveSkillTypeDat.FlagStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlagStat(int? key, out ActiveSkillTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlagStat(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillTypeDat"/> with <see cref="ActiveSkillTypeDat.FlagStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlagStat(int? key, out IReadOnlyList<ActiveSkillTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillTypeDat>();
            return false;
        }

        if (byFlagStat is null)
        {
            byFlagStat = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlagStat;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFlagStat.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFlagStat.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFlagStat.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillTypeDat"/> with <see cref="ActiveSkillTypeDat.byFlagStat"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillTypeDat>> GetManyToManyByFlagStat(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillTypeDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlagStat(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ActiveSkillTypeDat[] Load()
    {
        const string filePath = "Data/ActiveSkillType.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ActiveSkillTypeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlagStat
            (var flagstatLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ActiveSkillTypeDat()
            {
                Id = idLoading,
                FlagStat = flagstatLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
