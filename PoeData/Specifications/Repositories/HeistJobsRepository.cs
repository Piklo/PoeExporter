using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistJobsDat"/> related data and helper methods.
/// </summary>
public sealed class HeistJobsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistJobsDat> Items { get; }

    private Dictionary<string, List<HeistJobsDat>>? byId;
    private Dictionary<string, List<HeistJobsDat>>? byName;
    private Dictionary<string, List<HeistJobsDat>>? byRequiredSkillIcon;
    private Dictionary<string, List<HeistJobsDat>>? bySkillIcon;
    private Dictionary<float, List<HeistJobsDat>>? byUnknown32;
    private Dictionary<int, List<HeistJobsDat>>? byUnknown36;
    private Dictionary<string, List<HeistJobsDat>>? byMapIcon;
    private Dictionary<int, List<HeistJobsDat>>? byLevel_StatsKey;
    private Dictionary<int, List<HeistJobsDat>>? byAlert_StatsKey;
    private Dictionary<int, List<HeistJobsDat>>? byAlarm_StatsKey;
    private Dictionary<int, List<HeistJobsDat>>? byCost_StatsKey;
    private Dictionary<int, List<HeistJobsDat>>? byExperienceGain_StatsKey;
    private Dictionary<string, List<HeistJobsDat>>? byConsoleBlueprintLegend;
    private Dictionary<string, List<HeistJobsDat>>? byDescription;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistJobsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistJobsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HeistJobsDat? item)
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
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
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistJobsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistJobsDat>>();
        }

        var items = new List<ResultItem<string, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out HeistJobsDat? item)
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
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
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistJobsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistJobsDat>>();
        }

        var items = new List<ResultItem<string, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.RequiredSkillIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRequiredSkillIcon(string? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRequiredSkillIcon(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.RequiredSkillIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRequiredSkillIcon(string? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byRequiredSkillIcon is null)
        {
            byRequiredSkillIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.RequiredSkillIcon;

                if (!byRequiredSkillIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRequiredSkillIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRequiredSkillIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byRequiredSkillIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistJobsDat>> GetManyToManyByRequiredSkillIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistJobsDat>>();
        }

        var items = new List<ResultItem<string, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRequiredSkillIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.SkillIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillIcon(string? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillIcon(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.SkillIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillIcon(string? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (bySkillIcon is null)
        {
            bySkillIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillIcon;

                if (!bySkillIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySkillIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.bySkillIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistJobsDat>> GetManyToManyBySkillIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistJobsDat>>();
        }

        var items = new List<ResultItem<string, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(float? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(float? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistJobsDat>> GetManyToManyByUnknown32(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistJobsDat>>();
        }

        var items = new List<ResultItem<float, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown36(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byUnknown36 is null)
        {
            byUnknown36 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown36;

                if (!byUnknown36.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown36.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown36.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.MapIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapIcon(string? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapIcon(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.MapIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapIcon(string? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byMapIcon is null)
        {
            byMapIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapIcon;

                if (!byMapIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMapIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMapIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byMapIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistJobsDat>> GetManyToManyByMapIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistJobsDat>>();
        }

        var items = new List<ResultItem<string, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Level_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel_StatsKey(int? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevel_StatsKey(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Level_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel_StatsKey(int? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byLevel_StatsKey is null)
        {
            byLevel_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Level_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byLevel_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byLevel_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byLevel_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byLevel_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsDat>> GetManyToManyByLevel_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Alert_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAlert_StatsKey(int? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAlert_StatsKey(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Alert_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAlert_StatsKey(int? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byAlert_StatsKey is null)
        {
            byAlert_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Alert_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAlert_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAlert_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAlert_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byAlert_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsDat>> GetManyToManyByAlert_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAlert_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Alarm_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAlarm_StatsKey(int? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAlarm_StatsKey(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Alarm_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAlarm_StatsKey(int? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byAlarm_StatsKey is null)
        {
            byAlarm_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Alarm_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAlarm_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAlarm_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAlarm_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byAlarm_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsDat>> GetManyToManyByAlarm_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAlarm_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Cost_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost_StatsKey(int? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost_StatsKey(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Cost_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost_StatsKey(int? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byCost_StatsKey is null)
        {
            byCost_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCost_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCost_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCost_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byCost_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsDat>> GetManyToManyByCost_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.ExperienceGain_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExperienceGain_StatsKey(int? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExperienceGain_StatsKey(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.ExperienceGain_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExperienceGain_StatsKey(int? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byExperienceGain_StatsKey is null)
        {
            byExperienceGain_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ExperienceGain_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byExperienceGain_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byExperienceGain_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byExperienceGain_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byExperienceGain_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistJobsDat>> GetManyToManyByExperienceGain_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistJobsDat>>();
        }

        var items = new List<ResultItem<int, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExperienceGain_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.ConsoleBlueprintLegend"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByConsoleBlueprintLegend(string? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByConsoleBlueprintLegend(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.ConsoleBlueprintLegend"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByConsoleBlueprintLegend(string? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byConsoleBlueprintLegend is null)
        {
            byConsoleBlueprintLegend = new();
            foreach (var item in Items)
            {
                var itemKey = item.ConsoleBlueprintLegend;

                if (!byConsoleBlueprintLegend.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byConsoleBlueprintLegend.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byConsoleBlueprintLegend.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byConsoleBlueprintLegend"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistJobsDat>> GetManyToManyByConsoleBlueprintLegend(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistJobsDat>>();
        }

        var items = new List<ResultItem<string, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByConsoleBlueprintLegend(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out HeistJobsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<HeistJobsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistJobsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistJobsDat"/> with <see cref="HeistJobsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistJobsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistJobsDat>>();
        }

        var items = new List<ResultItem<string, HeistJobsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistJobsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistJobsDat[] Load()
    {
        const string filePath = "Data/HeistJobs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistJobsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RequiredSkillIcon
            (var requiredskilliconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SkillIcon
            (var skilliconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MapIcon
            (var mapiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Level_StatsKey
            (var level_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Alert_StatsKey
            (var alert_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Alarm_StatsKey
            (var alarm_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Cost_StatsKey
            (var cost_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ExperienceGain_StatsKey
            (var experiencegain_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ConsoleBlueprintLegend
            (var consoleblueprintlegendLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistJobsDat()
            {
                Id = idLoading,
                Name = nameLoading,
                RequiredSkillIcon = requiredskilliconLoading,
                SkillIcon = skilliconLoading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                MapIcon = mapiconLoading,
                Level_StatsKey = level_statskeyLoading,
                Alert_StatsKey = alert_statskeyLoading,
                Alarm_StatsKey = alarm_statskeyLoading,
                Cost_StatsKey = cost_statskeyLoading,
                ExperienceGain_StatsKey = experiencegain_statskeyLoading,
                ConsoleBlueprintLegend = consoleblueprintlegendLoading,
                Description = descriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
