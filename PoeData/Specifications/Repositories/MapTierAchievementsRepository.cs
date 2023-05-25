using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapTierAchievementsDat"/> related data and helper methods.
/// </summary>
public sealed class MapTierAchievementsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapTierAchievementsDat> Items { get; }

    private Dictionary<string, List<MapTierAchievementsDat>>? byId;
    private Dictionary<int, List<MapTierAchievementsDat>>? byAchievementItemsKey;
    private Dictionary<int, List<MapTierAchievementsDat>>? byMapTiers;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapTierAchievementsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapTierAchievementsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapTierAchievementsDat"/> with <see cref="MapTierAchievementsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MapTierAchievementsDat? item)
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
    /// Tries to get <see cref="MapTierAchievementsDat"/> with <see cref="MapTierAchievementsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MapTierAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapTierAchievementsDat>();
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
            items = Array.Empty<MapTierAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapTierAchievementsDat"/> with <see cref="MapTierAchievementsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapTierAchievementsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapTierAchievementsDat>>();
        }

        var items = new List<ResultItem<string, MapTierAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapTierAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapTierAchievementsDat"/> with <see cref="MapTierAchievementsDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKey(int? key, out MapTierAchievementsDat? item)
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
    /// Tries to get <see cref="MapTierAchievementsDat"/> with <see cref="MapTierAchievementsDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKey(int? key, out IReadOnlyList<MapTierAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapTierAchievementsDat>();
            return false;
        }

        if (byAchievementItemsKey is null)
        {
            byAchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapTierAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapTierAchievementsDat"/> with <see cref="MapTierAchievementsDat.byAchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapTierAchievementsDat>> GetManyToManyByAchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapTierAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MapTierAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapTierAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapTierAchievementsDat"/> with <see cref="MapTierAchievementsDat.MapTiers"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapTiers(int? key, out MapTierAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapTiers(key, out var items))
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
    /// Tries to get <see cref="MapTierAchievementsDat"/> with <see cref="MapTierAchievementsDat.MapTiers"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapTiers(int? key, out IReadOnlyList<MapTierAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapTierAchievementsDat>();
            return false;
        }

        if (byMapTiers is null)
        {
            byMapTiers = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapTiers;
                foreach (var listKey in itemKey)
                {
                    if (!byMapTiers.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMapTiers.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMapTiers.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapTierAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapTierAchievementsDat"/> with <see cref="MapTierAchievementsDat.byMapTiers"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapTierAchievementsDat>> GetManyToManyByMapTiers(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapTierAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MapTierAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapTiers(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapTierAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapTierAchievementsDat[] Load()
    {
        const string filePath = "Data/MapTierAchievements.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapTierAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKey
            (var tempachievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeyLoading = tempachievementitemskeyLoading.AsReadOnly();

            // loading MapTiers
            (var tempmaptiersLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var maptiersLoading = tempmaptiersLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapTierAchievementsDat()
            {
                Id = idLoading,
                AchievementItemsKey = achievementitemskeyLoading,
                MapTiers = maptiersLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
