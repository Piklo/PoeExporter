using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapSeriesTiersDat"/> related data and helper methods.
/// </summary>
public sealed class MapSeriesTiersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapSeriesTiersDat> Items { get; }

    private Dictionary<int, List<MapSeriesTiersDat>>? byMapsKey;
    private Dictionary<int, List<MapSeriesTiersDat>>? byMapWorldsTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byBetrayalTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? bySynthesisTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byLegionTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byBlightTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byMetamorphosisTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byDeliriumTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byHarvestTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byHeistTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byRitualTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byExpeditionTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byScourgeTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byArchnemesisTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? bySentinelTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? byKalandraTier;
    private Dictionary<int, List<MapSeriesTiersDat>>? bySanctumTier;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapSeriesTiersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapSeriesTiersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.MapsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapsKey(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapsKey(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.MapsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapsKey(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byMapsKey is null)
        {
            byMapsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMapsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMapsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMapsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byMapsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByMapsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.MapWorldsTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapWorldsTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapWorldsTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.MapWorldsTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapWorldsTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byMapWorldsTier is null)
        {
            byMapWorldsTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapWorldsTier;

                if (!byMapWorldsTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMapWorldsTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMapWorldsTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byMapWorldsTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByMapWorldsTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapWorldsTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.BetrayalTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.BetrayalTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byBetrayalTier is null)
        {
            byBetrayalTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalTier;

                if (!byBetrayalTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBetrayalTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byBetrayalTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByBetrayalTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.SynthesisTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySynthesisTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySynthesisTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.SynthesisTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySynthesisTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (bySynthesisTier is null)
        {
            bySynthesisTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.SynthesisTier;

                if (!bySynthesisTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySynthesisTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySynthesisTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.bySynthesisTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyBySynthesisTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySynthesisTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.LegionTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLegionTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLegionTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.LegionTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLegionTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byLegionTier is null)
        {
            byLegionTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.LegionTier;

                if (!byLegionTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLegionTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLegionTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byLegionTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByLegionTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLegionTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.BlightTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBlightTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBlightTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.BlightTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBlightTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byBlightTier is null)
        {
            byBlightTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.BlightTier;

                if (!byBlightTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBlightTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBlightTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byBlightTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByBlightTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBlightTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.MetamorphosisTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMetamorphosisTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMetamorphosisTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.MetamorphosisTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMetamorphosisTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byMetamorphosisTier is null)
        {
            byMetamorphosisTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.MetamorphosisTier;

                if (!byMetamorphosisTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMetamorphosisTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMetamorphosisTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byMetamorphosisTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByMetamorphosisTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMetamorphosisTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.DeliriumTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDeliriumTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDeliriumTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.DeliriumTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDeliriumTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byDeliriumTier is null)
        {
            byDeliriumTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.DeliriumTier;

                if (!byDeliriumTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDeliriumTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDeliriumTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byDeliriumTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByDeliriumTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDeliriumTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.HarvestTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHarvestTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHarvestTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.HarvestTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHarvestTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byHarvestTier is null)
        {
            byHarvestTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.HarvestTier;

                if (!byHarvestTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHarvestTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHarvestTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byHarvestTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByHarvestTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHarvestTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.HeistTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.HeistTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byHeistTier is null)
        {
            byHeistTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistTier;

                if (!byHeistTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeistTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byHeistTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByHeistTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.RitualTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRitualTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRitualTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.RitualTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRitualTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byRitualTier is null)
        {
            byRitualTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.RitualTier;

                if (!byRitualTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRitualTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRitualTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byRitualTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByRitualTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRitualTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.ExpeditionTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExpeditionTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExpeditionTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.ExpeditionTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExpeditionTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byExpeditionTier is null)
        {
            byExpeditionTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.ExpeditionTier;

                if (!byExpeditionTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byExpeditionTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byExpeditionTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byExpeditionTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByExpeditionTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExpeditionTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.ScourgeTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScourgeTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScourgeTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.ScourgeTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScourgeTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byScourgeTier is null)
        {
            byScourgeTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScourgeTier;

                if (!byScourgeTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScourgeTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScourgeTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byScourgeTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByScourgeTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScourgeTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.ArchnemesisTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArchnemesisTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArchnemesisTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.ArchnemesisTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArchnemesisTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byArchnemesisTier is null)
        {
            byArchnemesisTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.ArchnemesisTier;

                if (!byArchnemesisTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArchnemesisTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArchnemesisTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byArchnemesisTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByArchnemesisTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArchnemesisTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.SentinelTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySentinelTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySentinelTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.SentinelTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySentinelTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (bySentinelTier is null)
        {
            bySentinelTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.SentinelTier;

                if (!bySentinelTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySentinelTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySentinelTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.bySentinelTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyBySentinelTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySentinelTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.KalandraTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKalandraTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKalandraTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.KalandraTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKalandraTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (byKalandraTier is null)
        {
            byKalandraTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.KalandraTier;

                if (!byKalandraTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byKalandraTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byKalandraTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.byKalandraTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyByKalandraTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKalandraTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.SanctumTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySanctumTier(int? key, out MapSeriesTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySanctumTier(key, out var items))
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
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.SanctumTier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySanctumTier(int? key, out IReadOnlyList<MapSeriesTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        if (bySanctumTier is null)
        {
            bySanctumTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.SanctumTier;

                if (!bySanctumTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySanctumTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySanctumTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapSeriesTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesTiersDat"/> with <see cref="MapSeriesTiersDat.bySanctumTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapSeriesTiersDat>> GetManyToManyBySanctumTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapSeriesTiersDat>>();
        }

        var items = new List<ResultItem<int, MapSeriesTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySanctumTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapSeriesTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapSeriesTiersDat[] Load()
    {
        const string filePath = "Data/MapSeriesTiers.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapSeriesTiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MapsKey
            (var mapskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MapWorldsTier
            (var mapworldstierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BetrayalTier
            (var betrayaltierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SynthesisTier
            (var synthesistierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LegionTier
            (var legiontierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BlightTier
            (var blighttierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MetamorphosisTier
            (var metamorphosistierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DeliriumTier
            (var deliriumtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HarvestTier
            (var harvesttierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HeistTier
            (var heisttierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RitualTier
            (var ritualtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ExpeditionTier
            (var expeditiontierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ScourgeTier
            (var scourgetierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ArchnemesisTier
            (var archnemesistierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SentinelTier
            (var sentineltierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading KalandraTier
            (var kalandratierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SanctumTier
            (var sanctumtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapSeriesTiersDat()
            {
                MapsKey = mapskeyLoading,
                MapWorldsTier = mapworldstierLoading,
                BetrayalTier = betrayaltierLoading,
                SynthesisTier = synthesistierLoading,
                LegionTier = legiontierLoading,
                BlightTier = blighttierLoading,
                MetamorphosisTier = metamorphosistierLoading,
                DeliriumTier = deliriumtierLoading,
                HarvestTier = harvesttierLoading,
                HeistTier = heisttierLoading,
                RitualTier = ritualtierLoading,
                ExpeditionTier = expeditiontierLoading,
                ScourgeTier = scourgetierLoading,
                ArchnemesisTier = archnemesistierLoading,
                SentinelTier = sentineltierLoading,
                KalandraTier = kalandratierLoading,
                SanctumTier = sanctumtierLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
