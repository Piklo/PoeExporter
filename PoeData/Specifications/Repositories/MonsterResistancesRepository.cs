using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterResistancesDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterResistancesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterResistancesDat> Items { get; }

    private Dictionary<string, List<MonsterResistancesDat>>? byId;
    private Dictionary<int, List<MonsterResistancesDat>>? byFireNormal;
    private Dictionary<int, List<MonsterResistancesDat>>? byColdNormal;
    private Dictionary<int, List<MonsterResistancesDat>>? byLightningNormal;
    private Dictionary<int, List<MonsterResistancesDat>>? byChaosNormal;
    private Dictionary<int, List<MonsterResistancesDat>>? byFireCruel;
    private Dictionary<int, List<MonsterResistancesDat>>? byColdCruel;
    private Dictionary<int, List<MonsterResistancesDat>>? byLightningCruel;
    private Dictionary<int, List<MonsterResistancesDat>>? byChaosCruel;
    private Dictionary<int, List<MonsterResistancesDat>>? byFireMerciless;
    private Dictionary<int, List<MonsterResistancesDat>>? byColdMerciless;
    private Dictionary<int, List<MonsterResistancesDat>>? byLightningMerciless;
    private Dictionary<int, List<MonsterResistancesDat>>? byChaosMerciless;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterResistancesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterResistancesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MonsterResistancesDat? item)
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
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
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterResistancesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<string, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.FireNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFireNormal(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFireNormal(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.FireNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFireNormal(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byFireNormal is null)
        {
            byFireNormal = new();
            foreach (var item in Items)
            {
                var itemKey = item.FireNormal;

                if (!byFireNormal.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFireNormal.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFireNormal.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byFireNormal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByFireNormal(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFireNormal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ColdNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByColdNormal(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByColdNormal(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ColdNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByColdNormal(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byColdNormal is null)
        {
            byColdNormal = new();
            foreach (var item in Items)
            {
                var itemKey = item.ColdNormal;

                if (!byColdNormal.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byColdNormal.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byColdNormal.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byColdNormal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByColdNormal(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByColdNormal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.LightningNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLightningNormal(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLightningNormal(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.LightningNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLightningNormal(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byLightningNormal is null)
        {
            byLightningNormal = new();
            foreach (var item in Items)
            {
                var itemKey = item.LightningNormal;

                if (!byLightningNormal.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLightningNormal.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLightningNormal.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byLightningNormal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByLightningNormal(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLightningNormal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ChaosNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChaosNormal(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChaosNormal(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ChaosNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChaosNormal(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byChaosNormal is null)
        {
            byChaosNormal = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChaosNormal;

                if (!byChaosNormal.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChaosNormal.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChaosNormal.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byChaosNormal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByChaosNormal(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChaosNormal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.FireCruel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFireCruel(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFireCruel(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.FireCruel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFireCruel(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byFireCruel is null)
        {
            byFireCruel = new();
            foreach (var item in Items)
            {
                var itemKey = item.FireCruel;

                if (!byFireCruel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFireCruel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFireCruel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byFireCruel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByFireCruel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFireCruel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ColdCruel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByColdCruel(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByColdCruel(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ColdCruel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByColdCruel(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byColdCruel is null)
        {
            byColdCruel = new();
            foreach (var item in Items)
            {
                var itemKey = item.ColdCruel;

                if (!byColdCruel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byColdCruel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byColdCruel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byColdCruel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByColdCruel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByColdCruel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.LightningCruel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLightningCruel(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLightningCruel(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.LightningCruel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLightningCruel(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byLightningCruel is null)
        {
            byLightningCruel = new();
            foreach (var item in Items)
            {
                var itemKey = item.LightningCruel;

                if (!byLightningCruel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLightningCruel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLightningCruel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byLightningCruel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByLightningCruel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLightningCruel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ChaosCruel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChaosCruel(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChaosCruel(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ChaosCruel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChaosCruel(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byChaosCruel is null)
        {
            byChaosCruel = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChaosCruel;

                if (!byChaosCruel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChaosCruel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChaosCruel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byChaosCruel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByChaosCruel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChaosCruel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.FireMerciless"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFireMerciless(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFireMerciless(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.FireMerciless"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFireMerciless(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byFireMerciless is null)
        {
            byFireMerciless = new();
            foreach (var item in Items)
            {
                var itemKey = item.FireMerciless;

                if (!byFireMerciless.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFireMerciless.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFireMerciless.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byFireMerciless"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByFireMerciless(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFireMerciless(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ColdMerciless"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByColdMerciless(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByColdMerciless(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ColdMerciless"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByColdMerciless(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byColdMerciless is null)
        {
            byColdMerciless = new();
            foreach (var item in Items)
            {
                var itemKey = item.ColdMerciless;

                if (!byColdMerciless.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byColdMerciless.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byColdMerciless.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byColdMerciless"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByColdMerciless(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByColdMerciless(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.LightningMerciless"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLightningMerciless(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLightningMerciless(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.LightningMerciless"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLightningMerciless(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byLightningMerciless is null)
        {
            byLightningMerciless = new();
            foreach (var item in Items)
            {
                var itemKey = item.LightningMerciless;

                if (!byLightningMerciless.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLightningMerciless.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLightningMerciless.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byLightningMerciless"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByLightningMerciless(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLightningMerciless(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ChaosMerciless"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChaosMerciless(int? key, out MonsterResistancesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChaosMerciless(key, out var items))
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
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.ChaosMerciless"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChaosMerciless(int? key, out IReadOnlyList<MonsterResistancesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        if (byChaosMerciless is null)
        {
            byChaosMerciless = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChaosMerciless;

                if (!byChaosMerciless.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChaosMerciless.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChaosMerciless.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterResistancesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterResistancesDat"/> with <see cref="MonsterResistancesDat.byChaosMerciless"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterResistancesDat>> GetManyToManyByChaosMerciless(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterResistancesDat>>();
        }

        var items = new List<ResultItem<int, MonsterResistancesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChaosMerciless(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterResistancesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterResistancesDat[] Load()
    {
        const string filePath = "Data/MonsterResistances.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterResistancesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FireNormal
            (var firenormalLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ColdNormal
            (var coldnormalLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LightningNormal
            (var lightningnormalLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ChaosNormal
            (var chaosnormalLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FireCruel
            (var firecruelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ColdCruel
            (var coldcruelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LightningCruel
            (var lightningcruelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ChaosCruel
            (var chaoscruelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FireMerciless
            (var firemercilessLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ColdMerciless
            (var coldmercilessLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LightningMerciless
            (var lightningmercilessLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ChaosMerciless
            (var chaosmercilessLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterResistancesDat()
            {
                Id = idLoading,
                FireNormal = firenormalLoading,
                ColdNormal = coldnormalLoading,
                LightningNormal = lightningnormalLoading,
                ChaosNormal = chaosnormalLoading,
                FireCruel = firecruelLoading,
                ColdCruel = coldcruelLoading,
                LightningCruel = lightningcruelLoading,
                ChaosCruel = chaoscruelLoading,
                FireMerciless = firemercilessLoading,
                ColdMerciless = coldmercilessLoading,
                LightningMerciless = lightningmercilessLoading,
                ChaosMerciless = chaosmercilessLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
