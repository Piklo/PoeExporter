using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ActiveSkillsDat"/> related data and helper methods.
/// </summary>
public sealed class ActiveSkillsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ActiveSkillsDat> Items { get; }

    private Dictionary<string, List<ActiveSkillsDat>>? byId;
    private Dictionary<string, List<ActiveSkillsDat>>? byDisplayedName;
    private Dictionary<string, List<ActiveSkillsDat>>? byDescription;
    private Dictionary<string, List<ActiveSkillsDat>>? byUnknown24;
    private Dictionary<string, List<ActiveSkillsDat>>? byIcon_DDSFile;
    private Dictionary<int, List<ActiveSkillsDat>>? byActiveSkillTargetTypes;
    private Dictionary<int, List<ActiveSkillsDat>>? byActiveSkillTypes;
    private Dictionary<int, List<ActiveSkillsDat>>? byWeaponRestriction_ItemClassesKeys;
    private Dictionary<string, List<ActiveSkillsDat>>? byWebsiteDescription;
    private Dictionary<string, List<ActiveSkillsDat>>? byWebsiteImage;
    private Dictionary<bool, List<ActiveSkillsDat>>? byUnknown104;
    private Dictionary<string, List<ActiveSkillsDat>>? byUnknown105;
    private Dictionary<bool, List<ActiveSkillsDat>>? byUnknown113;
    private Dictionary<int, List<ActiveSkillsDat>>? bySkillTotemId;
    private Dictionary<bool, List<ActiveSkillsDat>>? byIsManuallyCasted;
    private Dictionary<int, List<ActiveSkillsDat>>? byInput_StatKeys;
    private Dictionary<int, List<ActiveSkillsDat>>? byOutput_StatKeys;
    private Dictionary<int, List<ActiveSkillsDat>>? byMinionActiveSkillTypes;
    private Dictionary<bool, List<ActiveSkillsDat>>? byUnknown167;
    private Dictionary<bool, List<ActiveSkillsDat>>? byUnknown168;
    private Dictionary<int, List<ActiveSkillsDat>>? byUnknown169;
    private Dictionary<int, List<ActiveSkillsDat>>? byUnknown185;
    private Dictionary<int, List<ActiveSkillsDat>>? byAlternateSkillTargetingBehavioursKey;
    private Dictionary<bool, List<ActiveSkillsDat>>? byUnknown205;
    private Dictionary<string, List<ActiveSkillsDat>>? byAIFile;
    private Dictionary<int, List<ActiveSkillsDat>>? byUnknown214;
    private Dictionary<bool, List<ActiveSkillsDat>>? byUnknown230;
    private Dictionary<bool, List<ActiveSkillsDat>>? byUnknown231;
    private Dictionary<bool, List<ActiveSkillsDat>>? byUnknown232;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActiveSkillsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ActiveSkillsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ActiveSkillsDat? item)
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
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
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ActiveSkillsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.DisplayedName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplayedName(string? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplayedName(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.DisplayedName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplayedName(string? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byDisplayedName is null)
        {
            byDisplayedName = new();
            foreach (var item in Items)
            {
                var itemKey = item.DisplayedName;

                if (!byDisplayedName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDisplayedName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplayedName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byDisplayedName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ActiveSkillsDat>> GetManyToManyByDisplayedName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplayedName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out ActiveSkillsDat? item)
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
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
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ActiveSkillsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(string? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown24(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(string? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;

                if (!byUnknown24.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ActiveSkillsDat>> GetManyToManyByUnknown24(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Icon_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon_DDSFile(string? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIcon_DDSFile(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Icon_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon_DDSFile(string? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byIcon_DDSFile is null)
        {
            byIcon_DDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Icon_DDSFile;

                if (!byIcon_DDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIcon_DDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIcon_DDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byIcon_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ActiveSkillsDat>> GetManyToManyByIcon_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.ActiveSkillTargetTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByActiveSkillTargetTypes(int? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByActiveSkillTargetTypes(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.ActiveSkillTargetTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByActiveSkillTargetTypes(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byActiveSkillTargetTypes is null)
        {
            byActiveSkillTargetTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.ActiveSkillTargetTypes;
                foreach (var listKey in itemKey)
                {
                    if (!byActiveSkillTargetTypes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byActiveSkillTargetTypes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byActiveSkillTargetTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byActiveSkillTargetTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyByActiveSkillTargetTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByActiveSkillTargetTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.ActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByActiveSkillTypes(int? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByActiveSkillTypes(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.ActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByActiveSkillTypes(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byActiveSkillTypes is null)
        {
            byActiveSkillTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.ActiveSkillTypes;
                foreach (var listKey in itemKey)
                {
                    if (!byActiveSkillTypes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byActiveSkillTypes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byActiveSkillTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byActiveSkillTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyByActiveSkillTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByActiveSkillTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.WeaponRestriction_ItemClassesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeaponRestriction_ItemClassesKeys(int? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeaponRestriction_ItemClassesKeys(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.WeaponRestriction_ItemClassesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeaponRestriction_ItemClassesKeys(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byWeaponRestriction_ItemClassesKeys is null)
        {
            byWeaponRestriction_ItemClassesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.WeaponRestriction_ItemClassesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byWeaponRestriction_ItemClassesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWeaponRestriction_ItemClassesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWeaponRestriction_ItemClassesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byWeaponRestriction_ItemClassesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyByWeaponRestriction_ItemClassesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeaponRestriction_ItemClassesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.WebsiteDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWebsiteDescription(string? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWebsiteDescription(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.WebsiteDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWebsiteDescription(string? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byWebsiteDescription is null)
        {
            byWebsiteDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.WebsiteDescription;

                if (!byWebsiteDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWebsiteDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWebsiteDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byWebsiteDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ActiveSkillsDat>> GetManyToManyByWebsiteDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWebsiteDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.WebsiteImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWebsiteImage(string? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWebsiteImage(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.WebsiteImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWebsiteImage(string? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byWebsiteImage is null)
        {
            byWebsiteImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.WebsiteImage;

                if (!byWebsiteImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWebsiteImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWebsiteImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byWebsiteImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ActiveSkillsDat>> GetManyToManyByWebsiteImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWebsiteImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(bool? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown104(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(bool? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;

                if (!byUnknown104.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown104.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ActiveSkillsDat>> GetManyToManyByUnknown104(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown105(string? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown105(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown105(string? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown105 is null)
        {
            byUnknown105 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown105;

                if (!byUnknown105.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown105.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown105.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown105"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ActiveSkillsDat>> GetManyToManyByUnknown105(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown105(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown113(bool? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown113(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown113(bool? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown113 is null)
        {
            byUnknown113 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown113;

                if (!byUnknown113.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown113.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown113.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown113"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ActiveSkillsDat>> GetManyToManyByUnknown113(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown113(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.SkillTotemId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillTotemId(int? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillTotemId(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.SkillTotemId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillTotemId(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (bySkillTotemId is null)
        {
            bySkillTotemId = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillTotemId;

                if (!bySkillTotemId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySkillTotemId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillTotemId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.bySkillTotemId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyBySkillTotemId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillTotemId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.IsManuallyCasted"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsManuallyCasted(bool? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsManuallyCasted(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.IsManuallyCasted"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsManuallyCasted(bool? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byIsManuallyCasted is null)
        {
            byIsManuallyCasted = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsManuallyCasted;

                if (!byIsManuallyCasted.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsManuallyCasted.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsManuallyCasted.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byIsManuallyCasted"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ActiveSkillsDat>> GetManyToManyByIsManuallyCasted(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsManuallyCasted(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Input_StatKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInput_StatKeys(int? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInput_StatKeys(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Input_StatKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInput_StatKeys(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byInput_StatKeys is null)
        {
            byInput_StatKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Input_StatKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byInput_StatKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byInput_StatKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byInput_StatKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byInput_StatKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyByInput_StatKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInput_StatKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Output_StatKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOutput_StatKeys(int? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOutput_StatKeys(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Output_StatKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOutput_StatKeys(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byOutput_StatKeys is null)
        {
            byOutput_StatKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Output_StatKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byOutput_StatKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byOutput_StatKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byOutput_StatKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byOutput_StatKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyByOutput_StatKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOutput_StatKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.MinionActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinionActiveSkillTypes(int? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinionActiveSkillTypes(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.MinionActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinionActiveSkillTypes(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byMinionActiveSkillTypes is null)
        {
            byMinionActiveSkillTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinionActiveSkillTypes;
                foreach (var listKey in itemKey)
                {
                    if (!byMinionActiveSkillTypes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMinionActiveSkillTypes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMinionActiveSkillTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byMinionActiveSkillTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyByMinionActiveSkillTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinionActiveSkillTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown167"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown167(bool? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown167(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown167"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown167(bool? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown167 is null)
        {
            byUnknown167 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown167;

                if (!byUnknown167.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown167.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown167.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown167"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ActiveSkillsDat>> GetManyToManyByUnknown167(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown167(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown168"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown168(bool? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown168(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown168"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown168(bool? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown168 is null)
        {
            byUnknown168 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown168;

                if (!byUnknown168.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown168.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown168.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown168"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ActiveSkillsDat>> GetManyToManyByUnknown168(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown168(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown169"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown169(int? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown169(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown169"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown169(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown169 is null)
        {
            byUnknown169 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown169;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown169.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown169.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown169.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown169"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyByUnknown169(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown169(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown185"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown185(int? key, out ActiveSkillsDat? item)
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown185"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown185(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
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
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown185"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyByUnknown185(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown185(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.AlternateSkillTargetingBehavioursKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAlternateSkillTargetingBehavioursKey(int? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAlternateSkillTargetingBehavioursKey(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.AlternateSkillTargetingBehavioursKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAlternateSkillTargetingBehavioursKey(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byAlternateSkillTargetingBehavioursKey is null)
        {
            byAlternateSkillTargetingBehavioursKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AlternateSkillTargetingBehavioursKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAlternateSkillTargetingBehavioursKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAlternateSkillTargetingBehavioursKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAlternateSkillTargetingBehavioursKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byAlternateSkillTargetingBehavioursKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyByAlternateSkillTargetingBehavioursKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAlternateSkillTargetingBehavioursKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown205"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown205(bool? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown205(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown205"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown205(bool? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown205 is null)
        {
            byUnknown205 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown205;

                if (!byUnknown205.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown205.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown205.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown205"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ActiveSkillsDat>> GetManyToManyByUnknown205(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown205(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.AIFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAIFile(string? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAIFile(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.AIFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAIFile(string? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byAIFile is null)
        {
            byAIFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.AIFile;

                if (!byAIFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAIFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAIFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byAIFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ActiveSkillsDat>> GetManyToManyByAIFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAIFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown214"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown214(int? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown214(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown214"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown214(int? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown214 is null)
        {
            byUnknown214 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown214;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown214.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown214.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown214.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown214"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ActiveSkillsDat>> GetManyToManyByUnknown214(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown214(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown230"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown230(bool? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown230(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown230"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown230(bool? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown230 is null)
        {
            byUnknown230 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown230;

                if (!byUnknown230.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown230.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown230.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown230"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ActiveSkillsDat>> GetManyToManyByUnknown230(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown230(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown231"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown231(bool? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown231(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown231"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown231(bool? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown231 is null)
        {
            byUnknown231 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown231;

                if (!byUnknown231.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown231.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown231.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown231"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ActiveSkillsDat>> GetManyToManyByUnknown231(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown231(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown232"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown232(bool? key, out ActiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown232(key, out var items))
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
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.Unknown232"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown232(bool? key, out IReadOnlyList<ActiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        if (byUnknown232 is null)
        {
            byUnknown232 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown232;

                if (!byUnknown232.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown232.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown232.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ActiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ActiveSkillsDat"/> with <see cref="ActiveSkillsDat.byUnknown232"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ActiveSkillsDat>> GetManyToManyByUnknown232(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ActiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, ActiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown232(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ActiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ActiveSkillsDat[] Load()
    {
        const string filePath = "Data/ActiveSkills.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ActiveSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DisplayedName
            (var displayednameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon_DDSFile
            (var icon_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ActiveSkillTargetTypes
            (var tempactiveskilltargettypesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var activeskilltargettypesLoading = tempactiveskilltargettypesLoading.AsReadOnly();

            // loading ActiveSkillTypes
            (var tempactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var activeskilltypesLoading = tempactiveskilltypesLoading.AsReadOnly();

            // loading WeaponRestriction_ItemClassesKeys
            (var tempweaponrestriction_itemclasseskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var weaponrestriction_itemclasseskeysLoading = tempweaponrestriction_itemclasseskeysLoading.AsReadOnly();

            // loading WebsiteDescription
            (var websitedescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WebsiteImage
            (var websiteimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SkillTotemId
            (var skilltotemidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsManuallyCasted
            (var ismanuallycastedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Input_StatKeys
            (var tempinput_statkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var input_statkeysLoading = tempinput_statkeysLoading.AsReadOnly();

            // loading Output_StatKeys
            (var tempoutput_statkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var output_statkeysLoading = tempoutput_statkeysLoading.AsReadOnly();

            // loading MinionActiveSkillTypes
            (var tempminionactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var minionactiveskilltypesLoading = tempminionactiveskilltypesLoading.AsReadOnly();

            // loading Unknown167
            (var unknown167Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown168
            (var unknown168Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown169
            (var tempunknown169Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown169Loading = tempunknown169Loading.AsReadOnly();

            // loading Unknown185
            (var unknown185Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AlternateSkillTargetingBehavioursKey
            (var alternateskilltargetingbehaviourskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown205
            (var unknown205Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AIFile
            (var aifileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown214
            (var tempunknown214Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown214Loading = tempunknown214Loading.AsReadOnly();

            // loading Unknown230
            (var unknown230Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown231
            (var unknown231Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown232
            (var unknown232Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ActiveSkillsDat()
            {
                Id = idLoading,
                DisplayedName = displayednameLoading,
                Description = descriptionLoading,
                Unknown24 = unknown24Loading,
                Icon_DDSFile = icon_ddsfileLoading,
                ActiveSkillTargetTypes = activeskilltargettypesLoading,
                ActiveSkillTypes = activeskilltypesLoading,
                WeaponRestriction_ItemClassesKeys = weaponrestriction_itemclasseskeysLoading,
                WebsiteDescription = websitedescriptionLoading,
                WebsiteImage = websiteimageLoading,
                Unknown104 = unknown104Loading,
                Unknown105 = unknown105Loading,
                Unknown113 = unknown113Loading,
                SkillTotemId = skilltotemidLoading,
                IsManuallyCasted = ismanuallycastedLoading,
                Input_StatKeys = input_statkeysLoading,
                Output_StatKeys = output_statkeysLoading,
                MinionActiveSkillTypes = minionactiveskilltypesLoading,
                Unknown167 = unknown167Loading,
                Unknown168 = unknown168Loading,
                Unknown169 = unknown169Loading,
                Unknown185 = unknown185Loading,
                AlternateSkillTargetingBehavioursKey = alternateskilltargetingbehaviourskeyLoading,
                Unknown205 = unknown205Loading,
                AIFile = aifileLoading,
                Unknown214 = unknown214Loading,
                Unknown230 = unknown230Loading,
                Unknown231 = unknown231Loading,
                Unknown232 = unknown232Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
