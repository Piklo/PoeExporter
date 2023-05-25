using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SkillMineVariationsDat"/> related data and helper methods.
/// </summary>
public sealed class SkillMineVariationsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SkillMineVariationsDat> Items { get; }

    private Dictionary<int, List<SkillMineVariationsDat>>? bySkillMinesKey;
    private Dictionary<int, List<SkillMineVariationsDat>>? byUnknown4;
    private Dictionary<int, List<SkillMineVariationsDat>>? byMiscObject;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkillMineVariationsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SkillMineVariationsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SkillMineVariationsDat"/> with <see cref="SkillMineVariationsDat.SkillMinesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillMinesKey(int? key, out SkillMineVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillMinesKey(key, out var items))
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
    /// Tries to get <see cref="SkillMineVariationsDat"/> with <see cref="SkillMineVariationsDat.SkillMinesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillMinesKey(int? key, out IReadOnlyList<SkillMineVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillMineVariationsDat>();
            return false;
        }

        if (bySkillMinesKey is null)
        {
            bySkillMinesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillMinesKey;

                if (!bySkillMinesKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySkillMinesKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillMinesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillMineVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillMineVariationsDat"/> with <see cref="SkillMineVariationsDat.bySkillMinesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillMineVariationsDat>> GetManyToManyBySkillMinesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillMineVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillMineVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillMinesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillMineVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillMineVariationsDat"/> with <see cref="SkillMineVariationsDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown4(int? key, out SkillMineVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown4(key, out var items))
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
    /// Tries to get <see cref="SkillMineVariationsDat"/> with <see cref="SkillMineVariationsDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown4(int? key, out IReadOnlyList<SkillMineVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillMineVariationsDat>();
            return false;
        }

        if (byUnknown4 is null)
        {
            byUnknown4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown4;

                if (!byUnknown4.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown4.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillMineVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillMineVariationsDat"/> with <see cref="SkillMineVariationsDat.byUnknown4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillMineVariationsDat>> GetManyToManyByUnknown4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillMineVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillMineVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillMineVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillMineVariationsDat"/> with <see cref="SkillMineVariationsDat.MiscObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscObject(int? key, out SkillMineVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscObject(key, out var items))
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
    /// Tries to get <see cref="SkillMineVariationsDat"/> with <see cref="SkillMineVariationsDat.MiscObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscObject(int? key, out IReadOnlyList<SkillMineVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillMineVariationsDat>();
            return false;
        }

        if (byMiscObject is null)
        {
            byMiscObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscObject;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscObject.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscObject.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscObject.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillMineVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillMineVariationsDat"/> with <see cref="SkillMineVariationsDat.byMiscObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillMineVariationsDat>> GetManyToManyByMiscObject(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillMineVariationsDat>>();
        }

        var items = new List<ResultItem<int, SkillMineVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillMineVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SkillMineVariationsDat[] Load()
    {
        const string filePath = "Data/SkillMineVariations.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillMineVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading SkillMinesKey
            (var skillmineskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscObject
            (var miscobjectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillMineVariationsDat()
            {
                SkillMinesKey = skillmineskeyLoading,
                Unknown4 = unknown4Loading,
                MiscObject = miscobjectLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
