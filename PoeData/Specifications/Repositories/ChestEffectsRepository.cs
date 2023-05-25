using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ChestEffectsDat"/> related data and helper methods.
/// </summary>
public sealed class ChestEffectsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ChestEffectsDat> Items { get; }

    private Dictionary<string, List<ChestEffectsDat>>? byId;
    private Dictionary<string, List<ChestEffectsDat>>? byNormal_EPKFile;
    private Dictionary<string, List<ChestEffectsDat>>? byNormal_Closed_AOFile;
    private Dictionary<string, List<ChestEffectsDat>>? byNormal_Open_AOFile;
    private Dictionary<string, List<ChestEffectsDat>>? byMagic_EPKFile;
    private Dictionary<string, List<ChestEffectsDat>>? byUnique_EPKFile;
    private Dictionary<string, List<ChestEffectsDat>>? byRare_EPKFile;
    private Dictionary<string, List<ChestEffectsDat>>? byMagic_Closed_AOFile;
    private Dictionary<string, List<ChestEffectsDat>>? byUnique_Closed_AOFile;
    private Dictionary<string, List<ChestEffectsDat>>? byRare_Closed_AOFile;
    private Dictionary<string, List<ChestEffectsDat>>? byMagic_Open_AOFile;
    private Dictionary<string, List<ChestEffectsDat>>? byUnique_Open_AOFile;
    private Dictionary<string, List<ChestEffectsDat>>? byRare_Open_AOFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChestEffectsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ChestEffectsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ChestEffectsDat? item)
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
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
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Normal_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNormal_EPKFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNormal_EPKFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Normal_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNormal_EPKFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byNormal_EPKFile is null)
        {
            byNormal_EPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Normal_EPKFile;

                if (!byNormal_EPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNormal_EPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNormal_EPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byNormal_EPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByNormal_EPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNormal_EPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Normal_Closed_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNormal_Closed_AOFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNormal_Closed_AOFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Normal_Closed_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNormal_Closed_AOFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byNormal_Closed_AOFile is null)
        {
            byNormal_Closed_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Normal_Closed_AOFile;

                if (!byNormal_Closed_AOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNormal_Closed_AOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNormal_Closed_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byNormal_Closed_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByNormal_Closed_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNormal_Closed_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Normal_Open_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNormal_Open_AOFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNormal_Open_AOFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Normal_Open_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNormal_Open_AOFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byNormal_Open_AOFile is null)
        {
            byNormal_Open_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Normal_Open_AOFile;

                if (!byNormal_Open_AOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNormal_Open_AOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNormal_Open_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byNormal_Open_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByNormal_Open_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNormal_Open_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Magic_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMagic_EPKFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMagic_EPKFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Magic_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMagic_EPKFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byMagic_EPKFile is null)
        {
            byMagic_EPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Magic_EPKFile;

                if (!byMagic_EPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMagic_EPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMagic_EPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byMagic_EPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByMagic_EPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMagic_EPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Unique_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnique_EPKFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnique_EPKFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Unique_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnique_EPKFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byUnique_EPKFile is null)
        {
            byUnique_EPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unique_EPKFile;

                if (!byUnique_EPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnique_EPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnique_EPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byUnique_EPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByUnique_EPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnique_EPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Rare_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRare_EPKFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRare_EPKFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Rare_EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRare_EPKFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byRare_EPKFile is null)
        {
            byRare_EPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Rare_EPKFile;

                if (!byRare_EPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRare_EPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRare_EPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byRare_EPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByRare_EPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRare_EPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Magic_Closed_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMagic_Closed_AOFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMagic_Closed_AOFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Magic_Closed_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMagic_Closed_AOFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byMagic_Closed_AOFile is null)
        {
            byMagic_Closed_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Magic_Closed_AOFile;

                if (!byMagic_Closed_AOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMagic_Closed_AOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMagic_Closed_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byMagic_Closed_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByMagic_Closed_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMagic_Closed_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Unique_Closed_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnique_Closed_AOFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnique_Closed_AOFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Unique_Closed_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnique_Closed_AOFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byUnique_Closed_AOFile is null)
        {
            byUnique_Closed_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unique_Closed_AOFile;

                if (!byUnique_Closed_AOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnique_Closed_AOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnique_Closed_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byUnique_Closed_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByUnique_Closed_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnique_Closed_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Rare_Closed_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRare_Closed_AOFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRare_Closed_AOFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Rare_Closed_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRare_Closed_AOFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byRare_Closed_AOFile is null)
        {
            byRare_Closed_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Rare_Closed_AOFile;

                if (!byRare_Closed_AOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRare_Closed_AOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRare_Closed_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byRare_Closed_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByRare_Closed_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRare_Closed_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Magic_Open_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMagic_Open_AOFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMagic_Open_AOFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Magic_Open_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMagic_Open_AOFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byMagic_Open_AOFile is null)
        {
            byMagic_Open_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Magic_Open_AOFile;

                if (!byMagic_Open_AOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMagic_Open_AOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMagic_Open_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byMagic_Open_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByMagic_Open_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMagic_Open_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Unique_Open_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnique_Open_AOFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnique_Open_AOFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Unique_Open_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnique_Open_AOFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byUnique_Open_AOFile is null)
        {
            byUnique_Open_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unique_Open_AOFile;

                if (!byUnique_Open_AOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnique_Open_AOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnique_Open_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byUnique_Open_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByUnique_Open_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnique_Open_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Rare_Open_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRare_Open_AOFile(string? key, out ChestEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRare_Open_AOFile(key, out var items))
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
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.Rare_Open_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRare_Open_AOFile(string? key, out IReadOnlyList<ChestEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        if (byRare_Open_AOFile is null)
        {
            byRare_Open_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Rare_Open_AOFile;

                if (!byRare_Open_AOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRare_Open_AOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRare_Open_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChestEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChestEffectsDat"/> with <see cref="ChestEffectsDat.byRare_Open_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChestEffectsDat>> GetManyToManyByRare_Open_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChestEffectsDat>>();
        }

        var items = new List<ResultItem<string, ChestEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRare_Open_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChestEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ChestEffectsDat[] Load()
    {
        const string filePath = "Data/ChestEffects.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ChestEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Normal_EPKFile
            (var normal_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Normal_Closed_AOFile
            (var normal_closed_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Normal_Open_AOFile
            (var normal_open_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Magic_EPKFile
            (var magic_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unique_EPKFile
            (var unique_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Rare_EPKFile
            (var rare_epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Magic_Closed_AOFile
            (var magic_closed_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unique_Closed_AOFile
            (var unique_closed_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Rare_Closed_AOFile
            (var rare_closed_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Magic_Open_AOFile
            (var magic_open_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unique_Open_AOFile
            (var unique_open_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Rare_Open_AOFile
            (var rare_open_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ChestEffectsDat()
            {
                Id = idLoading,
                Normal_EPKFile = normal_epkfileLoading,
                Normal_Closed_AOFile = normal_closed_aofileLoading,
                Normal_Open_AOFile = normal_open_aofileLoading,
                Magic_EPKFile = magic_epkfileLoading,
                Unique_EPKFile = unique_epkfileLoading,
                Rare_EPKFile = rare_epkfileLoading,
                Magic_Closed_AOFile = magic_closed_aofileLoading,
                Unique_Closed_AOFile = unique_closed_aofileLoading,
                Rare_Closed_AOFile = rare_closed_aofileLoading,
                Magic_Open_AOFile = magic_open_aofileLoading,
                Unique_Open_AOFile = unique_open_aofileLoading,
                Rare_Open_AOFile = rare_open_aofileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
