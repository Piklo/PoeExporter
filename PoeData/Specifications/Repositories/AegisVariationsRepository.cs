using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AegisVariationsDat"/> related data and helper methods.
/// </summary>
public sealed class AegisVariationsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AegisVariationsDat> Items { get; }

    private Dictionary<string, List<AegisVariationsDat>>? byName;
    private Dictionary<bool, List<AegisVariationsDat>>? byDefendsAgainstPhysical;
    private Dictionary<bool, List<AegisVariationsDat>>? byDefendsAgainstFire;
    private Dictionary<bool, List<AegisVariationsDat>>? byDefendsAgainstCold;
    private Dictionary<bool, List<AegisVariationsDat>>? byDefendsAgainstLightning;
    private Dictionary<bool, List<AegisVariationsDat>>? byDefendsAgainstChaos;
    private Dictionary<int, List<AegisVariationsDat>>? byUnknown13;
    private Dictionary<int, List<AegisVariationsDat>>? byUnknown29;
    private Dictionary<int, List<AegisVariationsDat>>? byUnknown45;
    private Dictionary<int, List<AegisVariationsDat>>? byUnknown61;

    /// <summary>
    /// Initializes a new instance of the <see cref="AegisVariationsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AegisVariationsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out AegisVariationsDat? item)
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
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<AegisVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AegisVariationsDat>();
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
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AegisVariationsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AegisVariationsDat>>();
        }

        var items = new List<ResultItem<string, AegisVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AegisVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.DefendsAgainstPhysical"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDefendsAgainstPhysical(bool? key, out AegisVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDefendsAgainstPhysical(key, out var items))
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
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.DefendsAgainstPhysical"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDefendsAgainstPhysical(bool? key, out IReadOnlyList<AegisVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        if (byDefendsAgainstPhysical is null)
        {
            byDefendsAgainstPhysical = new();
            foreach (var item in Items)
            {
                var itemKey = item.DefendsAgainstPhysical;

                if (!byDefendsAgainstPhysical.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDefendsAgainstPhysical.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDefendsAgainstPhysical.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.byDefendsAgainstPhysical"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AegisVariationsDat>> GetManyToManyByDefendsAgainstPhysical(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AegisVariationsDat>>();
        }

        var items = new List<ResultItem<bool, AegisVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDefendsAgainstPhysical(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AegisVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.DefendsAgainstFire"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDefendsAgainstFire(bool? key, out AegisVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDefendsAgainstFire(key, out var items))
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
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.DefendsAgainstFire"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDefendsAgainstFire(bool? key, out IReadOnlyList<AegisVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        if (byDefendsAgainstFire is null)
        {
            byDefendsAgainstFire = new();
            foreach (var item in Items)
            {
                var itemKey = item.DefendsAgainstFire;

                if (!byDefendsAgainstFire.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDefendsAgainstFire.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDefendsAgainstFire.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.byDefendsAgainstFire"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AegisVariationsDat>> GetManyToManyByDefendsAgainstFire(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AegisVariationsDat>>();
        }

        var items = new List<ResultItem<bool, AegisVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDefendsAgainstFire(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AegisVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.DefendsAgainstCold"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDefendsAgainstCold(bool? key, out AegisVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDefendsAgainstCold(key, out var items))
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
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.DefendsAgainstCold"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDefendsAgainstCold(bool? key, out IReadOnlyList<AegisVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        if (byDefendsAgainstCold is null)
        {
            byDefendsAgainstCold = new();
            foreach (var item in Items)
            {
                var itemKey = item.DefendsAgainstCold;

                if (!byDefendsAgainstCold.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDefendsAgainstCold.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDefendsAgainstCold.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.byDefendsAgainstCold"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AegisVariationsDat>> GetManyToManyByDefendsAgainstCold(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AegisVariationsDat>>();
        }

        var items = new List<ResultItem<bool, AegisVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDefendsAgainstCold(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AegisVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.DefendsAgainstLightning"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDefendsAgainstLightning(bool? key, out AegisVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDefendsAgainstLightning(key, out var items))
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
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.DefendsAgainstLightning"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDefendsAgainstLightning(bool? key, out IReadOnlyList<AegisVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        if (byDefendsAgainstLightning is null)
        {
            byDefendsAgainstLightning = new();
            foreach (var item in Items)
            {
                var itemKey = item.DefendsAgainstLightning;

                if (!byDefendsAgainstLightning.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDefendsAgainstLightning.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDefendsAgainstLightning.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.byDefendsAgainstLightning"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AegisVariationsDat>> GetManyToManyByDefendsAgainstLightning(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AegisVariationsDat>>();
        }

        var items = new List<ResultItem<bool, AegisVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDefendsAgainstLightning(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AegisVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.DefendsAgainstChaos"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDefendsAgainstChaos(bool? key, out AegisVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDefendsAgainstChaos(key, out var items))
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
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.DefendsAgainstChaos"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDefendsAgainstChaos(bool? key, out IReadOnlyList<AegisVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        if (byDefendsAgainstChaos is null)
        {
            byDefendsAgainstChaos = new();
            foreach (var item in Items)
            {
                var itemKey = item.DefendsAgainstChaos;

                if (!byDefendsAgainstChaos.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDefendsAgainstChaos.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDefendsAgainstChaos.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.byDefendsAgainstChaos"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AegisVariationsDat>> GetManyToManyByDefendsAgainstChaos(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AegisVariationsDat>>();
        }

        var items = new List<ResultItem<bool, AegisVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDefendsAgainstChaos(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AegisVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.Unknown13"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown13(int? key, out AegisVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown13(key, out var items))
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
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.Unknown13"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown13(int? key, out IReadOnlyList<AegisVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        if (byUnknown13 is null)
        {
            byUnknown13 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown13;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown13.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown13.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown13.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.byUnknown13"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AegisVariationsDat>> GetManyToManyByUnknown13(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AegisVariationsDat>>();
        }

        var items = new List<ResultItem<int, AegisVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown13(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AegisVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown29(int? key, out AegisVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown29(key, out var items))
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
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown29(int? key, out IReadOnlyList<AegisVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        if (byUnknown29 is null)
        {
            byUnknown29 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown29;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown29.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown29.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown29.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.byUnknown29"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AegisVariationsDat>> GetManyToManyByUnknown29(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AegisVariationsDat>>();
        }

        var items = new List<ResultItem<int, AegisVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown29(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AegisVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown45(int? key, out AegisVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown45(key, out var items))
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
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown45(int? key, out IReadOnlyList<AegisVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        if (byUnknown45 is null)
        {
            byUnknown45 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown45;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown45.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown45.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown45.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.byUnknown45"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AegisVariationsDat>> GetManyToManyByUnknown45(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AegisVariationsDat>>();
        }

        var items = new List<ResultItem<int, AegisVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown45(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AegisVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(int? key, out AegisVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown61(key, out var items))
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
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(int? key, out IReadOnlyList<AegisVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        if (byUnknown61 is null)
        {
            byUnknown61 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown61;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown61.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown61.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown61.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AegisVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AegisVariationsDat"/> with <see cref="AegisVariationsDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AegisVariationsDat>> GetManyToManyByUnknown61(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AegisVariationsDat>>();
        }

        var items = new List<ResultItem<int, AegisVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AegisVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AegisVariationsDat[] Load()
    {
        const string filePath = "Data/AegisVariations.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AegisVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DefendsAgainstPhysical
            (var defendsagainstphysicalLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DefendsAgainstFire
            (var defendsagainstfireLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DefendsAgainstCold
            (var defendsagainstcoldLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DefendsAgainstLightning
            (var defendsagainstlightningLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DefendsAgainstChaos
            (var defendsagainstchaosLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown13
            (var unknown13Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AegisVariationsDat()
            {
                Name = nameLoading,
                DefendsAgainstPhysical = defendsagainstphysicalLoading,
                DefendsAgainstFire = defendsagainstfireLoading,
                DefendsAgainstCold = defendsagainstcoldLoading,
                DefendsAgainstLightning = defendsagainstlightningLoading,
                DefendsAgainstChaos = defendsagainstchaosLoading,
                Unknown13 = unknown13Loading,
                Unknown29 = unknown29Loading,
                Unknown45 = unknown45Loading,
                Unknown61 = unknown61Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
