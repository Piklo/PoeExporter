using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MasterHideoutLevelsDat"/> related data and helper methods.
/// </summary>
public sealed class MasterHideoutLevelsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MasterHideoutLevelsDat> Items { get; }

    private Dictionary<int, List<MasterHideoutLevelsDat>>? byNPCMasterKey;
    private Dictionary<int, List<MasterHideoutLevelsDat>>? byLevel;
    private Dictionary<int, List<MasterHideoutLevelsDat>>? byMissionsRequired;

    /// <summary>
    /// Initializes a new instance of the <see cref="MasterHideoutLevelsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MasterHideoutLevelsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MasterHideoutLevelsDat"/> with <see cref="MasterHideoutLevelsDat.NPCMasterKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCMasterKey(int? key, out MasterHideoutLevelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCMasterKey(key, out var items))
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
    /// Tries to get <see cref="MasterHideoutLevelsDat"/> with <see cref="MasterHideoutLevelsDat.NPCMasterKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCMasterKey(int? key, out IReadOnlyList<MasterHideoutLevelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MasterHideoutLevelsDat>();
            return false;
        }

        if (byNPCMasterKey is null)
        {
            byNPCMasterKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCMasterKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCMasterKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCMasterKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCMasterKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MasterHideoutLevelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MasterHideoutLevelsDat"/> with <see cref="MasterHideoutLevelsDat.byNPCMasterKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MasterHideoutLevelsDat>> GetManyToManyByNPCMasterKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MasterHideoutLevelsDat>>();
        }

        var items = new List<ResultItem<int, MasterHideoutLevelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCMasterKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MasterHideoutLevelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MasterHideoutLevelsDat"/> with <see cref="MasterHideoutLevelsDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out MasterHideoutLevelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevel(key, out var items))
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
    /// Tries to get <see cref="MasterHideoutLevelsDat"/> with <see cref="MasterHideoutLevelsDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<MasterHideoutLevelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MasterHideoutLevelsDat>();
            return false;
        }

        if (byLevel is null)
        {
            byLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.Level;

                if (!byLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MasterHideoutLevelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MasterHideoutLevelsDat"/> with <see cref="MasterHideoutLevelsDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MasterHideoutLevelsDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MasterHideoutLevelsDat>>();
        }

        var items = new List<ResultItem<int, MasterHideoutLevelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MasterHideoutLevelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MasterHideoutLevelsDat"/> with <see cref="MasterHideoutLevelsDat.MissionsRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMissionsRequired(int? key, out MasterHideoutLevelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMissionsRequired(key, out var items))
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
    /// Tries to get <see cref="MasterHideoutLevelsDat"/> with <see cref="MasterHideoutLevelsDat.MissionsRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMissionsRequired(int? key, out IReadOnlyList<MasterHideoutLevelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MasterHideoutLevelsDat>();
            return false;
        }

        if (byMissionsRequired is null)
        {
            byMissionsRequired = new();
            foreach (var item in Items)
            {
                var itemKey = item.MissionsRequired;

                if (!byMissionsRequired.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMissionsRequired.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMissionsRequired.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MasterHideoutLevelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MasterHideoutLevelsDat"/> with <see cref="MasterHideoutLevelsDat.byMissionsRequired"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MasterHideoutLevelsDat>> GetManyToManyByMissionsRequired(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MasterHideoutLevelsDat>>();
        }

        var items = new List<ResultItem<int, MasterHideoutLevelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMissionsRequired(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MasterHideoutLevelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MasterHideoutLevelsDat[] Load()
    {
        const string filePath = "Data/MasterHideoutLevels.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MasterHideoutLevelsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading NPCMasterKey
            (var npcmasterkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MissionsRequired
            (var missionsrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MasterHideoutLevelsDat()
            {
                NPCMasterKey = npcmasterkeyLoading,
                Level = levelLoading,
                MissionsRequired = missionsrequiredLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
