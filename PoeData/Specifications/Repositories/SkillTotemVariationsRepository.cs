using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SkillTotemVariationsDat"/> related data and helper methods.
/// </summary>
public sealed class SkillTotemVariationsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SkillTotemVariationsDat> Items { get; }

    private Dictionary<int, List<SkillTotemVariationsDat>>? bySkillTotemsKey;
    private Dictionary<int, List<SkillTotemVariationsDat>>? byTotemSkinId;
    private Dictionary<int, List<SkillTotemVariationsDat>>? byMonsterVarietiesKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkillTotemVariationsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SkillTotemVariationsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SkillTotemVariationsDat"/> with <see cref="SkillTotemVariationsDat.SkillTotemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillTotemsKey(int? key, out SkillTotemVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillTotemsKey(key, out var items))
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
    /// Tries to get <see cref="SkillTotemVariationsDat"/> with <see cref="SkillTotemVariationsDat.SkillTotemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillTotemsKey(int? key, out IReadOnlyList<SkillTotemVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillTotemVariationsDat>();
            return false;
        }

        if (bySkillTotemsKey is null)
        {
            bySkillTotemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillTotemsKey;

                if (!bySkillTotemsKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySkillTotemsKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillTotemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillTotemVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillTotemVariationsDat"/> with <see cref="SkillTotemVariationsDat.bySkillTotemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillTotemVariationsDat>> GetManyToManyBySkillTotemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillTotemVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillTotemVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillTotemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillTotemVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillTotemVariationsDat"/> with <see cref="SkillTotemVariationsDat.TotemSkinId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTotemSkinId(int? key, out SkillTotemVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTotemSkinId(key, out var items))
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
    /// Tries to get <see cref="SkillTotemVariationsDat"/> with <see cref="SkillTotemVariationsDat.TotemSkinId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTotemSkinId(int? key, out IReadOnlyList<SkillTotemVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillTotemVariationsDat>();
            return false;
        }

        if (byTotemSkinId is null)
        {
            byTotemSkinId = new();
            foreach (var item in Items)
            {
                var itemKey = item.TotemSkinId;

                if (!byTotemSkinId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTotemSkinId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTotemSkinId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillTotemVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillTotemVariationsDat"/> with <see cref="SkillTotemVariationsDat.byTotemSkinId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillTotemVariationsDat>> GetManyToManyByTotemSkinId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillTotemVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillTotemVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTotemSkinId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillTotemVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillTotemVariationsDat"/> with <see cref="SkillTotemVariationsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out SkillTotemVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="SkillTotemVariationsDat"/> with <see cref="SkillTotemVariationsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<SkillTotemVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillTotemVariationsDat>();
            return false;
        }

        if (byMonsterVarietiesKey is null)
        {
            byMonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillTotemVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillTotemVariationsDat"/> with <see cref="SkillTotemVariationsDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillTotemVariationsDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillTotemVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillTotemVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillTotemVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SkillTotemVariationsDat[] Load()
    {
        const string filePath = "Data/SkillTotemVariations.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillTotemVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading SkillTotemsKey
            (var skilltotemskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TotemSkinId
            (var totemskinidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillTotemVariationsDat()
            {
                SkillTotemsKey = skilltotemskeyLoading,
                TotemSkinId = totemskinidLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
