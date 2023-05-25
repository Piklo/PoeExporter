using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ItemExperiencePerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class ItemExperiencePerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ItemExperiencePerLevelDat> Items { get; }

    private Dictionary<int, List<ItemExperiencePerLevelDat>>? byItemExperienceType;
    private Dictionary<int, List<ItemExperiencePerLevelDat>>? byItemCurrentLevel;
    private Dictionary<int, List<ItemExperiencePerLevelDat>>? byExperience;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemExperiencePerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ItemExperiencePerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ItemExperiencePerLevelDat"/> with <see cref="ItemExperiencePerLevelDat.ItemExperienceType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemExperienceType(int? key, out ItemExperiencePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemExperienceType(key, out var items))
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
    /// Tries to get <see cref="ItemExperiencePerLevelDat"/> with <see cref="ItemExperiencePerLevelDat.ItemExperienceType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemExperienceType(int? key, out IReadOnlyList<ItemExperiencePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemExperiencePerLevelDat>();
            return false;
        }

        if (byItemExperienceType is null)
        {
            byItemExperienceType = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemExperienceType;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemExperienceType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemExperienceType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemExperienceType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemExperiencePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemExperiencePerLevelDat"/> with <see cref="ItemExperiencePerLevelDat.byItemExperienceType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemExperiencePerLevelDat>> GetManyToManyByItemExperienceType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemExperiencePerLevelDat>>();
        }

        var items = new List<ResultItem<int, ItemExperiencePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemExperienceType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemExperiencePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemExperiencePerLevelDat"/> with <see cref="ItemExperiencePerLevelDat.ItemCurrentLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemCurrentLevel(int? key, out ItemExperiencePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemCurrentLevel(key, out var items))
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
    /// Tries to get <see cref="ItemExperiencePerLevelDat"/> with <see cref="ItemExperiencePerLevelDat.ItemCurrentLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemCurrentLevel(int? key, out IReadOnlyList<ItemExperiencePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemExperiencePerLevelDat>();
            return false;
        }

        if (byItemCurrentLevel is null)
        {
            byItemCurrentLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemCurrentLevel;

                if (!byItemCurrentLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byItemCurrentLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byItemCurrentLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemExperiencePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemExperiencePerLevelDat"/> with <see cref="ItemExperiencePerLevelDat.byItemCurrentLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemExperiencePerLevelDat>> GetManyToManyByItemCurrentLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemExperiencePerLevelDat>>();
        }

        var items = new List<ResultItem<int, ItemExperiencePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemCurrentLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemExperiencePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemExperiencePerLevelDat"/> with <see cref="ItemExperiencePerLevelDat.Experience"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExperience(int? key, out ItemExperiencePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExperience(key, out var items))
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
    /// Tries to get <see cref="ItemExperiencePerLevelDat"/> with <see cref="ItemExperiencePerLevelDat.Experience"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExperience(int? key, out IReadOnlyList<ItemExperiencePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemExperiencePerLevelDat>();
            return false;
        }

        if (byExperience is null)
        {
            byExperience = new();
            foreach (var item in Items)
            {
                var itemKey = item.Experience;

                if (!byExperience.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byExperience.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byExperience.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemExperiencePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemExperiencePerLevelDat"/> with <see cref="ItemExperiencePerLevelDat.byExperience"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemExperiencePerLevelDat>> GetManyToManyByExperience(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemExperiencePerLevelDat>>();
        }

        var items = new List<ResultItem<int, ItemExperiencePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExperience(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemExperiencePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ItemExperiencePerLevelDat[] Load()
    {
        const string filePath = "Data/ItemExperiencePerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemExperiencePerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ItemExperienceType
            (var itemexperiencetypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemCurrentLevel
            (var itemcurrentlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Experience
            (var experienceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemExperiencePerLevelDat()
            {
                ItemExperienceType = itemexperiencetypeLoading,
                ItemCurrentLevel = itemcurrentlevelLoading,
                Experience = experienceLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
