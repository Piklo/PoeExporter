using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MetamorphosisRewardTypesDat"/> related data and helper methods.
/// </summary>
public sealed class MetamorphosisRewardTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MetamorphosisRewardTypesDat> Items { get; }

    private Dictionary<string, List<MetamorphosisRewardTypesDat>>? byId;
    private Dictionary<string, List<MetamorphosisRewardTypesDat>>? byArt;
    private Dictionary<string, List<MetamorphosisRewardTypesDat>>? byName;
    private Dictionary<int, List<MetamorphosisRewardTypesDat>>? byAchievementItemsKeys;

    /// <summary>
    /// Initializes a new instance of the <see cref="MetamorphosisRewardTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MetamorphosisRewardTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MetamorphosisRewardTypesDat? item)
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
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MetamorphosisRewardTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisRewardTypesDat>();
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
            items = Array.Empty<MetamorphosisRewardTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisRewardTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisRewardTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisRewardTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisRewardTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArt(string? key, out MetamorphosisRewardTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArt(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArt(string? key, out IReadOnlyList<MetamorphosisRewardTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisRewardTypesDat>();
            return false;
        }

        if (byArt is null)
        {
            byArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.Art;

                if (!byArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArt.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MetamorphosisRewardTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.byArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisRewardTypesDat>> GetManyToManyByArt(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisRewardTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisRewardTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisRewardTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out MetamorphosisRewardTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByName(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<MetamorphosisRewardTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisRewardTypesDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;

                if (!byName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MetamorphosisRewardTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisRewardTypesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisRewardTypesDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisRewardTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisRewardTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys(int? key, out MetamorphosisRewardTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys(int? key, out IReadOnlyList<MetamorphosisRewardTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisRewardTypesDat>();
            return false;
        }

        if (byAchievementItemsKeys is null)
        {
            byAchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisRewardTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypesDat"/> with <see cref="MetamorphosisRewardTypesDat.byAchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisRewardTypesDat>> GetManyToManyByAchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisRewardTypesDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisRewardTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisRewardTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MetamorphosisRewardTypesDat[] Load()
    {
        const string filePath = "Data/MetamorphosisRewardTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MetamorphosisRewardTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Art
            (var artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MetamorphosisRewardTypesDat()
            {
                Id = idLoading,
                Art = artLoading,
                Name = nameLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
