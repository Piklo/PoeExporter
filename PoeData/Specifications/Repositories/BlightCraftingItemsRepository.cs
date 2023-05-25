using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BlightCraftingItemsDat"/> related data and helper methods.
/// </summary>
public sealed class BlightCraftingItemsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BlightCraftingItemsDat> Items { get; }

    private Dictionary<int, List<BlightCraftingItemsDat>>? byOil;
    private Dictionary<int, List<BlightCraftingItemsDat>>? byTier;
    private Dictionary<int, List<BlightCraftingItemsDat>>? byAchievements;
    private Dictionary<int, List<BlightCraftingItemsDat>>? byUseType;
    private Dictionary<string, List<BlightCraftingItemsDat>>? byNameShort;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightCraftingItemsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BlightCraftingItemsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.Oil"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOil(int? key, out BlightCraftingItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOil(key, out var items))
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
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.Oil"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOil(int? key, out IReadOnlyList<BlightCraftingItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingItemsDat>();
            return false;
        }

        if (byOil is null)
        {
            byOil = new();
            foreach (var item in Items)
            {
                var itemKey = item.Oil;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byOil.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byOil.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byOil.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightCraftingItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.byOil"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightCraftingItemsDat>> GetManyToManyByOil(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightCraftingItemsDat>>();
        }

        var items = new List<ResultItem<int, BlightCraftingItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOil(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightCraftingItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out BlightCraftingItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier(key, out var items))
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
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<BlightCraftingItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingItemsDat>();
            return false;
        }

        if (byTier is null)
        {
            byTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier;

                if (!byTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightCraftingItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightCraftingItemsDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightCraftingItemsDat>>();
        }

        var items = new List<ResultItem<int, BlightCraftingItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightCraftingItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements(int? key, out BlightCraftingItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievements(key, out var items))
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
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements(int? key, out IReadOnlyList<BlightCraftingItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingItemsDat>();
            return false;
        }

        if (byAchievements is null)
        {
            byAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievements;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightCraftingItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.byAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightCraftingItemsDat>> GetManyToManyByAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightCraftingItemsDat>>();
        }

        var items = new List<ResultItem<int, BlightCraftingItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightCraftingItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.UseType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUseType(int? key, out BlightCraftingItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUseType(key, out var items))
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
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.UseType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUseType(int? key, out IReadOnlyList<BlightCraftingItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingItemsDat>();
            return false;
        }

        if (byUseType is null)
        {
            byUseType = new();
            foreach (var item in Items)
            {
                var itemKey = item.UseType;

                if (!byUseType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUseType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUseType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightCraftingItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.byUseType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightCraftingItemsDat>> GetManyToManyByUseType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightCraftingItemsDat>>();
        }

        var items = new List<ResultItem<int, BlightCraftingItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUseType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightCraftingItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.NameShort"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNameShort(string? key, out BlightCraftingItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNameShort(key, out var items))
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
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.NameShort"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNameShort(string? key, out IReadOnlyList<BlightCraftingItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingItemsDat>();
            return false;
        }

        if (byNameShort is null)
        {
            byNameShort = new();
            foreach (var item in Items)
            {
                var itemKey = item.NameShort;

                if (!byNameShort.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNameShort.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNameShort.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BlightCraftingItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingItemsDat"/> with <see cref="BlightCraftingItemsDat.byNameShort"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightCraftingItemsDat>> GetManyToManyByNameShort(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightCraftingItemsDat>>();
        }

        var items = new List<ResultItem<string, BlightCraftingItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNameShort(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightCraftingItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BlightCraftingItemsDat[] Load()
    {
        const string filePath = "Data/BlightCraftingItems.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightCraftingItemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Oil
            (var oilLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            // loading UseType
            (var usetypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading NameShort
            (var nameshortLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightCraftingItemsDat()
            {
                Oil = oilLoading,
                Tier = tierLoading,
                Achievements = achievementsLoading,
                UseType = usetypeLoading,
                NameShort = nameshortLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
