using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ProjectilesDat"/> related data and helper methods.
/// </summary>
public sealed class ProjectilesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ProjectilesDat> Items { get; }

    private Dictionary<string, List<ProjectilesDat>>? byId;
    private Dictionary<string, List<ProjectilesDat>>? byAOFiles;
    private Dictionary<string, List<ProjectilesDat>>? byLoopAnimationIds;
    private Dictionary<string, List<ProjectilesDat>>? byImpactAnimationIds;
    private Dictionary<int, List<ProjectilesDat>>? byProjectileSpeed;
    private Dictionary<bool, List<ProjectilesDat>>? byUnknown60;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown61;
    private Dictionary<bool, List<ProjectilesDat>>? byUnknown65;
    private Dictionary<bool, List<ProjectilesDat>>? byUnknown66;
    private Dictionary<string, List<ProjectilesDat>>? byInheritsFrom;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown75;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown79;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown95;
    private Dictionary<bool, List<ProjectilesDat>>? byUnknown99;
    private Dictionary<bool, List<ProjectilesDat>>? byUnknown100;
    private Dictionary<string, List<ProjectilesDat>>? byStuck_AOFile;
    private Dictionary<string, List<ProjectilesDat>>? byBounce_AOFile;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown125;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown129;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown133;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown137;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown141;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown157;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown173;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown177;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown181;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown185;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown189;
    private Dictionary<string, List<ProjectilesDat>>? byUnknown193;
    private Dictionary<bool, List<ProjectilesDat>>? byUnknown209;
    private Dictionary<int, List<ProjectilesDat>>? byUnknown210;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectilesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ProjectilesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ProjectilesDat? item)
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
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
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ProjectilesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ProjectilesDat>>();
        }

        var items = new List<ResultItem<string, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.AOFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFiles(string? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFiles(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.AOFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFiles(string? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byAOFiles is null)
        {
            byAOFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byAOFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAOFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAOFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byAOFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ProjectilesDat>> GetManyToManyByAOFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ProjectilesDat>>();
        }

        var items = new List<ResultItem<string, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.LoopAnimationIds"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLoopAnimationIds(string? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLoopAnimationIds(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.LoopAnimationIds"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLoopAnimationIds(string? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byLoopAnimationIds is null)
        {
            byLoopAnimationIds = new();
            foreach (var item in Items)
            {
                var itemKey = item.LoopAnimationIds;
                foreach (var listKey in itemKey)
                {
                    if (!byLoopAnimationIds.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byLoopAnimationIds.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byLoopAnimationIds.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byLoopAnimationIds"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ProjectilesDat>> GetManyToManyByLoopAnimationIds(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ProjectilesDat>>();
        }

        var items = new List<ResultItem<string, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLoopAnimationIds(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.ImpactAnimationIds"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImpactAnimationIds(string? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByImpactAnimationIds(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.ImpactAnimationIds"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImpactAnimationIds(string? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byImpactAnimationIds is null)
        {
            byImpactAnimationIds = new();
            foreach (var item in Items)
            {
                var itemKey = item.ImpactAnimationIds;
                foreach (var listKey in itemKey)
                {
                    if (!byImpactAnimationIds.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byImpactAnimationIds.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byImpactAnimationIds.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byImpactAnimationIds"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ProjectilesDat>> GetManyToManyByImpactAnimationIds(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ProjectilesDat>>();
        }

        var items = new List<ResultItem<string, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImpactAnimationIds(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.ProjectileSpeed"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProjectileSpeed(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProjectileSpeed(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.ProjectileSpeed"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProjectileSpeed(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byProjectileSpeed is null)
        {
            byProjectileSpeed = new();
            foreach (var item in Items)
            {
                var itemKey = item.ProjectileSpeed;

                if (!byProjectileSpeed.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byProjectileSpeed.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byProjectileSpeed.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byProjectileSpeed"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByProjectileSpeed(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProjectileSpeed(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(bool? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown60(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(bool? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown60 is null)
        {
            byUnknown60 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown60;

                if (!byUnknown60.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown60.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown60.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ProjectilesDat>> GetManyToManyByUnknown60(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(int? key, out ProjectilesDat? item)
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown61 is null)
        {
            byUnknown61 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown61;

                if (!byUnknown61.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown61.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown61.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown61(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown65(bool? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown65(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown65(bool? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown65 is null)
        {
            byUnknown65 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown65;

                if (!byUnknown65.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown65.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown65.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown65"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ProjectilesDat>> GetManyToManyByUnknown65(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown65(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown66(bool? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown66(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown66(bool? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown66 is null)
        {
            byUnknown66 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown66;

                if (!byUnknown66.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown66.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown66.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown66"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ProjectilesDat>> GetManyToManyByUnknown66(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown66(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.InheritsFrom"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInheritsFrom(string? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInheritsFrom(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.InheritsFrom"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInheritsFrom(string? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byInheritsFrom is null)
        {
            byInheritsFrom = new();
            foreach (var item in Items)
            {
                var itemKey = item.InheritsFrom;

                if (!byInheritsFrom.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInheritsFrom.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInheritsFrom.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byInheritsFrom"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ProjectilesDat>> GetManyToManyByInheritsFrom(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ProjectilesDat>>();
        }

        var items = new List<ResultItem<string, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInheritsFrom(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown75"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown75(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown75(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown75"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown75(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown75 is null)
        {
            byUnknown75 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown75;

                if (!byUnknown75.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown75.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown75.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown75"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown75(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown75(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown79(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown79(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown79(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown79 is null)
        {
            byUnknown79 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown79;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown79.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown79.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown79.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown79"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown79(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown79(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown95"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown95(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown95(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown95"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown95(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown95 is null)
        {
            byUnknown95 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown95;

                if (!byUnknown95.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown95.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown95.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown95"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown95(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown95(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown99(bool? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown99(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown99(bool? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown99 is null)
        {
            byUnknown99 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown99;

                if (!byUnknown99.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown99.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown99.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown99"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ProjectilesDat>> GetManyToManyByUnknown99(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown99(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(bool? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown100(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(bool? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown100 is null)
        {
            byUnknown100 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown100;

                if (!byUnknown100.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown100.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown100.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ProjectilesDat>> GetManyToManyByUnknown100(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Stuck_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStuck_AOFile(string? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStuck_AOFile(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Stuck_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStuck_AOFile(string? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byStuck_AOFile is null)
        {
            byStuck_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stuck_AOFile;
                foreach (var listKey in itemKey)
                {
                    if (!byStuck_AOFile.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStuck_AOFile.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStuck_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byStuck_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ProjectilesDat>> GetManyToManyByStuck_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ProjectilesDat>>();
        }

        var items = new List<ResultItem<string, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStuck_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Bounce_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBounce_AOFile(string? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBounce_AOFile(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Bounce_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBounce_AOFile(string? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byBounce_AOFile is null)
        {
            byBounce_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Bounce_AOFile;

                if (!byBounce_AOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBounce_AOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBounce_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byBounce_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ProjectilesDat>> GetManyToManyByBounce_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ProjectilesDat>>();
        }

        var items = new List<ResultItem<string, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBounce_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown125"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown125(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown125(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown125"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown125(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown125 is null)
        {
            byUnknown125 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown125;

                if (!byUnknown125.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown125.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown125.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown125"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown125(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown125(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown129"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown129(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown129(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown129"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown129(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown129 is null)
        {
            byUnknown129 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown129;

                if (!byUnknown129.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown129.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown129.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown129"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown129(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown129(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown133"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown133(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown133(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown133"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown133(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown133 is null)
        {
            byUnknown133 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown133;

                if (!byUnknown133.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown133.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown133.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown133"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown133(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown133(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown137"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown137(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown137(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown137"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown137(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown137 is null)
        {
            byUnknown137 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown137;

                if (!byUnknown137.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown137.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown137.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown137"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown137(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown137(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown141"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown141(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown141(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown141"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown141(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown141 is null)
        {
            byUnknown141 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown141;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown141.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown141.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown141.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown141"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown141(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown141(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown157"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown157(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown157(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown157"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown157(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown157 is null)
        {
            byUnknown157 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown157;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown157.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown157.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown157.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown157"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown157(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown157(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown173"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown173(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown173(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown173"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown173(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown173 is null)
        {
            byUnknown173 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown173;

                if (!byUnknown173.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown173.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown173.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown173"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown173(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown173(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown177"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown177(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown177(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown177"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown177(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown177 is null)
        {
            byUnknown177 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown177;

                if (!byUnknown177.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown177.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown177.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown177"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown177(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown177(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown181"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown181(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown181(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown181"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown181(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown181 is null)
        {
            byUnknown181 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown181;

                if (!byUnknown181.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown181.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown181.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown181"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown181(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown181(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown185"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown185(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown185(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown185"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown185(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown185 is null)
        {
            byUnknown185 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown185;

                if (!byUnknown185.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown185.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown185.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown185"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown185(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown185(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown189"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown189(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown189(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown189"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown189(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown189 is null)
        {
            byUnknown189 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown189;

                if (!byUnknown189.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown189.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown189.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown189"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown189(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown189(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown193"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown193(string? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown193(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown193"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown193(string? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown193 is null)
        {
            byUnknown193 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown193;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown193.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown193.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown193.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown193"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ProjectilesDat>> GetManyToManyByUnknown193(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ProjectilesDat>>();
        }

        var items = new List<ResultItem<string, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown193(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown209"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown209(bool? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown209(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown209"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown209(bool? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown209 is null)
        {
            byUnknown209 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown209;

                if (!byUnknown209.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown209.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown209.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown209"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ProjectilesDat>> GetManyToManyByUnknown209(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown209(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown210"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown210(int? key, out ProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown210(key, out var items))
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
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.Unknown210"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown210(int? key, out IReadOnlyList<ProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        if (byUnknown210 is null)
        {
            byUnknown210 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown210;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown210.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown210.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown210.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectilesDat"/> with <see cref="ProjectilesDat.byUnknown210"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectilesDat>> GetManyToManyByUnknown210(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectilesDat>>();
        }

        var items = new List<ResultItem<int, ProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown210(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ProjectilesDat[] Load()
    {
        const string filePath = "Data/Projectiles.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ProjectilesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AOFiles
            (var tempaofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofilesLoading = tempaofilesLoading.AsReadOnly();

            // loading LoopAnimationIds
            (var temploopanimationidsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var loopanimationidsLoading = temploopanimationidsLoading.AsReadOnly();

            // loading ImpactAnimationIds
            (var tempimpactanimationidsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var impactanimationidsLoading = tempimpactanimationidsLoading.AsReadOnly();

            // loading ProjectileSpeed
            (var projectilespeedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading InheritsFrom
            (var inheritsfromLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown75
            (var unknown75Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown95
            (var unknown95Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Stuck_AOFile
            (var tempstuck_aofileLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var stuck_aofileLoading = tempstuck_aofileLoading.AsReadOnly();

            // loading Bounce_AOFile
            (var bounce_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown125
            (var unknown125Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown129
            (var unknown129Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown133
            (var unknown133Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown137
            (var unknown137Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown157
            (var unknown157Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown173
            (var unknown173Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown177
            (var unknown177Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown181
            (var unknown181Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown185
            (var unknown185Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown189
            (var unknown189Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown193
            (var tempunknown193Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown193Loading = tempunknown193Loading.AsReadOnly();

            // loading Unknown209
            (var unknown209Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown210
            (var tempunknown210Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown210Loading = tempunknown210Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ProjectilesDat()
            {
                Id = idLoading,
                AOFiles = aofilesLoading,
                LoopAnimationIds = loopanimationidsLoading,
                ImpactAnimationIds = impactanimationidsLoading,
                ProjectileSpeed = projectilespeedLoading,
                Unknown60 = unknown60Loading,
                Unknown61 = unknown61Loading,
                Unknown65 = unknown65Loading,
                Unknown66 = unknown66Loading,
                InheritsFrom = inheritsfromLoading,
                Unknown75 = unknown75Loading,
                Unknown79 = unknown79Loading,
                Unknown95 = unknown95Loading,
                Unknown99 = unknown99Loading,
                Unknown100 = unknown100Loading,
                Stuck_AOFile = stuck_aofileLoading,
                Bounce_AOFile = bounce_aofileLoading,
                Unknown125 = unknown125Loading,
                Unknown129 = unknown129Loading,
                Unknown133 = unknown133Loading,
                Unknown137 = unknown137Loading,
                Unknown141 = unknown141Loading,
                Unknown157 = unknown157Loading,
                Unknown173 = unknown173Loading,
                Unknown177 = unknown177Loading,
                Unknown181 = unknown181Loading,
                Unknown185 = unknown185Loading,
                Unknown189 = unknown189Loading,
                Unknown193 = unknown193Loading,
                Unknown209 = unknown209Loading,
                Unknown210 = unknown210Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
