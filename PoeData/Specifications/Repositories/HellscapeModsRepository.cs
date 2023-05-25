using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HellscapeModsDat"/> related data and helper methods.
/// </summary>
public sealed class HellscapeModsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HellscapeModsDat> Items { get; }

    private Dictionary<int, List<HellscapeModsDat>>? byMod;
    private Dictionary<int, List<HellscapeModsDat>>? byTiersWhitelist;
    private Dictionary<int, List<HellscapeModsDat>>? byTransformAchievement;
    private Dictionary<int, List<HellscapeModsDat>>? byModFamilies;

    /// <summary>
    /// Initializes a new instance of the <see cref="HellscapeModsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HellscapeModsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.Mod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMod(int? key, out HellscapeModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMod(key, out var items))
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
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.Mod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMod(int? key, out IReadOnlyList<HellscapeModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModsDat>();
            return false;
        }

        if (byMod is null)
        {
            byMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.byMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModsDat>> GetManyToManyByMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModsDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.TiersWhitelist"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTiersWhitelist(int? key, out HellscapeModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTiersWhitelist(key, out var items))
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
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.TiersWhitelist"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTiersWhitelist(int? key, out IReadOnlyList<HellscapeModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModsDat>();
            return false;
        }

        if (byTiersWhitelist is null)
        {
            byTiersWhitelist = new();
            foreach (var item in Items)
            {
                var itemKey = item.TiersWhitelist;
                foreach (var listKey in itemKey)
                {
                    if (!byTiersWhitelist.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTiersWhitelist.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTiersWhitelist.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.byTiersWhitelist"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModsDat>> GetManyToManyByTiersWhitelist(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModsDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTiersWhitelist(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.TransformAchievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTransformAchievement(int? key, out HellscapeModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTransformAchievement(key, out var items))
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
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.TransformAchievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTransformAchievement(int? key, out IReadOnlyList<HellscapeModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModsDat>();
            return false;
        }

        if (byTransformAchievement is null)
        {
            byTransformAchievement = new();
            foreach (var item in Items)
            {
                var itemKey = item.TransformAchievement;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTransformAchievement.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTransformAchievement.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTransformAchievement.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.byTransformAchievement"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModsDat>> GetManyToManyByTransformAchievement(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModsDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTransformAchievement(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.ModFamilies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModFamilies(int? key, out HellscapeModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModFamilies(key, out var items))
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
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.ModFamilies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModFamilies(int? key, out IReadOnlyList<HellscapeModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModsDat>();
            return false;
        }

        if (byModFamilies is null)
        {
            byModFamilies = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModFamilies;
                foreach (var listKey in itemKey)
                {
                    if (!byModFamilies.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModFamilies.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModFamilies.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModsDat"/> with <see cref="HellscapeModsDat.byModFamilies"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModsDat>> GetManyToManyByModFamilies(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModsDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModFamilies(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HellscapeModsDat[] Load()
    {
        const string filePath = "Data/HellscapeMods.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Mod
            (var modLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TiersWhitelist
            (var temptierswhitelistLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var tierswhitelistLoading = temptierswhitelistLoading.AsReadOnly();

            // loading TransformAchievement
            (var transformachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ModFamilies
            (var tempmodfamiliesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modfamiliesLoading = tempmodfamiliesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeModsDat()
            {
                Mod = modLoading,
                TiersWhitelist = tierswhitelistLoading,
                TransformAchievement = transformachievementLoading,
                ModFamilies = modfamiliesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
