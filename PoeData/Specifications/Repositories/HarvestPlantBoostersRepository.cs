using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestPlantBoostersDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestPlantBoostersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestPlantBoostersDat> Items { get; }

    private Dictionary<int, List<HarvestPlantBoostersDat>>? byHarvestObjectsKey;
    private Dictionary<int, List<HarvestPlantBoostersDat>>? byRadius;
    private Dictionary<int, List<HarvestPlantBoostersDat>>? byUnknown20;
    private Dictionary<int, List<HarvestPlantBoostersDat>>? byLifeforce;
    private Dictionary<int, List<HarvestPlantBoostersDat>>? byAdditionalCraftingOptionsChance;
    private Dictionary<int, List<HarvestPlantBoostersDat>>? byRareExtraChances;
    private Dictionary<int, List<HarvestPlantBoostersDat>>? byHarvestPlantBoosterFamilies;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestPlantBoostersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestPlantBoostersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.HarvestObjectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHarvestObjectsKey(int? key, out HarvestPlantBoostersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHarvestObjectsKey(key, out var items))
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
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.HarvestObjectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHarvestObjectsKey(int? key, out IReadOnlyList<HarvestPlantBoostersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        if (byHarvestObjectsKey is null)
        {
            byHarvestObjectsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HarvestObjectsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHarvestObjectsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHarvestObjectsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHarvestObjectsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.byHarvestObjectsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestPlantBoostersDat>> GetManyToManyByHarvestObjectsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestPlantBoostersDat>>();
        }

        var items = new List<ResultItem<int, HarvestPlantBoostersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHarvestObjectsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestPlantBoostersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.Radius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRadius(int? key, out HarvestPlantBoostersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRadius(key, out var items))
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
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.Radius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRadius(int? key, out IReadOnlyList<HarvestPlantBoostersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        if (byRadius is null)
        {
            byRadius = new();
            foreach (var item in Items)
            {
                var itemKey = item.Radius;

                if (!byRadius.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRadius.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRadius.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.byRadius"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestPlantBoostersDat>> GetManyToManyByRadius(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestPlantBoostersDat>>();
        }

        var items = new List<ResultItem<int, HarvestPlantBoostersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRadius(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestPlantBoostersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out HarvestPlantBoostersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown20(key, out var items))
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
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<HarvestPlantBoostersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown20.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestPlantBoostersDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestPlantBoostersDat>>();
        }

        var items = new List<ResultItem<int, HarvestPlantBoostersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestPlantBoostersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.Lifeforce"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLifeforce(int? key, out HarvestPlantBoostersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLifeforce(key, out var items))
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
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.Lifeforce"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLifeforce(int? key, out IReadOnlyList<HarvestPlantBoostersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        if (byLifeforce is null)
        {
            byLifeforce = new();
            foreach (var item in Items)
            {
                var itemKey = item.Lifeforce;

                if (!byLifeforce.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLifeforce.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLifeforce.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.byLifeforce"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestPlantBoostersDat>> GetManyToManyByLifeforce(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestPlantBoostersDat>>();
        }

        var items = new List<ResultItem<int, HarvestPlantBoostersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLifeforce(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestPlantBoostersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.AdditionalCraftingOptionsChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAdditionalCraftingOptionsChance(int? key, out HarvestPlantBoostersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAdditionalCraftingOptionsChance(key, out var items))
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
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.AdditionalCraftingOptionsChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAdditionalCraftingOptionsChance(int? key, out IReadOnlyList<HarvestPlantBoostersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        if (byAdditionalCraftingOptionsChance is null)
        {
            byAdditionalCraftingOptionsChance = new();
            foreach (var item in Items)
            {
                var itemKey = item.AdditionalCraftingOptionsChance;

                if (!byAdditionalCraftingOptionsChance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAdditionalCraftingOptionsChance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAdditionalCraftingOptionsChance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.byAdditionalCraftingOptionsChance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestPlantBoostersDat>> GetManyToManyByAdditionalCraftingOptionsChance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestPlantBoostersDat>>();
        }

        var items = new List<ResultItem<int, HarvestPlantBoostersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAdditionalCraftingOptionsChance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestPlantBoostersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.RareExtraChances"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRareExtraChances(int? key, out HarvestPlantBoostersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRareExtraChances(key, out var items))
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
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.RareExtraChances"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRareExtraChances(int? key, out IReadOnlyList<HarvestPlantBoostersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        if (byRareExtraChances is null)
        {
            byRareExtraChances = new();
            foreach (var item in Items)
            {
                var itemKey = item.RareExtraChances;

                if (!byRareExtraChances.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRareExtraChances.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRareExtraChances.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.byRareExtraChances"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestPlantBoostersDat>> GetManyToManyByRareExtraChances(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestPlantBoostersDat>>();
        }

        var items = new List<ResultItem<int, HarvestPlantBoostersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRareExtraChances(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestPlantBoostersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.HarvestPlantBoosterFamilies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHarvestPlantBoosterFamilies(int? key, out HarvestPlantBoostersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHarvestPlantBoosterFamilies(key, out var items))
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
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.HarvestPlantBoosterFamilies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHarvestPlantBoosterFamilies(int? key, out IReadOnlyList<HarvestPlantBoostersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        if (byHarvestPlantBoosterFamilies is null)
        {
            byHarvestPlantBoosterFamilies = new();
            foreach (var item in Items)
            {
                var itemKey = item.HarvestPlantBoosterFamilies;

                if (!byHarvestPlantBoosterFamilies.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHarvestPlantBoosterFamilies.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHarvestPlantBoosterFamilies.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestPlantBoostersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestPlantBoostersDat"/> with <see cref="HarvestPlantBoostersDat.byHarvestPlantBoosterFamilies"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestPlantBoostersDat>> GetManyToManyByHarvestPlantBoosterFamilies(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestPlantBoostersDat>>();
        }

        var items = new List<ResultItem<int, HarvestPlantBoostersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHarvestPlantBoosterFamilies(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestPlantBoostersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestPlantBoostersDat[] Load()
    {
        const string filePath = "Data/HarvestPlantBoosters.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestPlantBoostersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HarvestObjectsKey
            (var harvestobjectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Radius
            (var radiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Lifeforce
            (var lifeforceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AdditionalCraftingOptionsChance
            (var additionalcraftingoptionschanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RareExtraChances
            (var rareextrachancesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HarvestPlantBoosterFamilies
            (var harvestplantboosterfamiliesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestPlantBoostersDat()
            {
                HarvestObjectsKey = harvestobjectskeyLoading,
                Radius = radiusLoading,
                Unknown20 = unknown20Loading,
                Lifeforce = lifeforceLoading,
                AdditionalCraftingOptionsChance = additionalcraftingoptionschanceLoading,
                RareExtraChances = rareextrachancesLoading,
                HarvestPlantBoosterFamilies = harvestplantboosterfamiliesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
