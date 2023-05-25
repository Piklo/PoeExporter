using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GrantedEffectsDat"/> related data and helper methods.
/// </summary>
public sealed class GrantedEffectsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GrantedEffectsDat> Items { get; }

    private Dictionary<string, List<GrantedEffectsDat>>? byId;
    private Dictionary<bool, List<GrantedEffectsDat>>? byIsSupport;
    private Dictionary<int, List<GrantedEffectsDat>>? byAllowedActiveSkillTypes;
    private Dictionary<string, List<GrantedEffectsDat>>? bySupportGemLetter;
    private Dictionary<int, List<GrantedEffectsDat>>? byAttribute;
    private Dictionary<int, List<GrantedEffectsDat>>? byAddedActiveSkillTypes;
    private Dictionary<int, List<GrantedEffectsDat>>? byExcludedActiveSkillTypes;
    private Dictionary<bool, List<GrantedEffectsDat>>? bySupportsGemsOnly;
    private Dictionary<int, List<GrantedEffectsDat>>? byUnknown70;
    private Dictionary<int, List<GrantedEffectsDat>>? byUnknown74;
    private Dictionary<bool, List<GrantedEffectsDat>>? byCannotBeSupported;
    private Dictionary<int, List<GrantedEffectsDat>>? byUnknown91;
    private Dictionary<int, List<GrantedEffectsDat>>? byCastTime;
    private Dictionary<int, List<GrantedEffectsDat>>? byActiveSkill;
    private Dictionary<bool, List<GrantedEffectsDat>>? byIgnoreMinionTypes;
    private Dictionary<bool, List<GrantedEffectsDat>>? byUnknown116;
    private Dictionary<int, List<GrantedEffectsDat>>? byAddedMinionActiveSkillTypes;
    private Dictionary<int, List<GrantedEffectsDat>>? byAnimation;
    private Dictionary<int, List<GrantedEffectsDat>>? byMultiPartAchievement;
    private Dictionary<bool, List<GrantedEffectsDat>>? byUnknown165;
    private Dictionary<int, List<GrantedEffectsDat>>? bySupportWeaponRestrictions;
    private Dictionary<int, List<GrantedEffectsDat>>? byRegularVariant;
    private Dictionary<int, List<GrantedEffectsDat>>? byUnknown190;
    private Dictionary<int, List<GrantedEffectsDat>>? byUnknown194;
    private Dictionary<int, List<GrantedEffectsDat>>? byUnknown198;
    private Dictionary<bool, List<GrantedEffectsDat>>? byUnknown202;
    private Dictionary<int, List<GrantedEffectsDat>>? byStatSet;
    private Dictionary<int, List<GrantedEffectsDat>>? byUnknown219;

    /// <summary>
    /// Initializes a new instance of the <see cref="GrantedEffectsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GrantedEffectsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out GrantedEffectsDat? item)
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
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
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GrantedEffectsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<string, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.IsSupport"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsSupport(bool? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsSupport(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.IsSupport"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsSupport(bool? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byIsSupport is null)
        {
            byIsSupport = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsSupport;

                if (!byIsSupport.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsSupport.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsSupport.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byIsSupport"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GrantedEffectsDat>> GetManyToManyByIsSupport(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsSupport(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.AllowedActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAllowedActiveSkillTypes(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAllowedActiveSkillTypes(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.AllowedActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAllowedActiveSkillTypes(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byAllowedActiveSkillTypes is null)
        {
            byAllowedActiveSkillTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.AllowedActiveSkillTypes;
                foreach (var listKey in itemKey)
                {
                    if (!byAllowedActiveSkillTypes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAllowedActiveSkillTypes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAllowedActiveSkillTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byAllowedActiveSkillTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByAllowedActiveSkillTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAllowedActiveSkillTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.SupportGemLetter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySupportGemLetter(string? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySupportGemLetter(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.SupportGemLetter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySupportGemLetter(string? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (bySupportGemLetter is null)
        {
            bySupportGemLetter = new();
            foreach (var item in Items)
            {
                var itemKey = item.SupportGemLetter;

                if (!bySupportGemLetter.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySupportGemLetter.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySupportGemLetter.TryGetValue(key, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.bySupportGemLetter"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GrantedEffectsDat>> GetManyToManyBySupportGemLetter(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<string, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySupportGemLetter(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Attribute"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAttribute(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAttribute(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Attribute"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAttribute(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byAttribute is null)
        {
            byAttribute = new();
            foreach (var item in Items)
            {
                var itemKey = item.Attribute;

                if (!byAttribute.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAttribute.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAttribute.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byAttribute"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByAttribute(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAttribute(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.AddedActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAddedActiveSkillTypes(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAddedActiveSkillTypes(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.AddedActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAddedActiveSkillTypes(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byAddedActiveSkillTypes is null)
        {
            byAddedActiveSkillTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.AddedActiveSkillTypes;
                foreach (var listKey in itemKey)
                {
                    if (!byAddedActiveSkillTypes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAddedActiveSkillTypes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAddedActiveSkillTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byAddedActiveSkillTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByAddedActiveSkillTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAddedActiveSkillTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.ExcludedActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExcludedActiveSkillTypes(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExcludedActiveSkillTypes(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.ExcludedActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExcludedActiveSkillTypes(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byExcludedActiveSkillTypes is null)
        {
            byExcludedActiveSkillTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.ExcludedActiveSkillTypes;
                foreach (var listKey in itemKey)
                {
                    if (!byExcludedActiveSkillTypes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byExcludedActiveSkillTypes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byExcludedActiveSkillTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byExcludedActiveSkillTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByExcludedActiveSkillTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExcludedActiveSkillTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.SupportsGemsOnly"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySupportsGemsOnly(bool? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySupportsGemsOnly(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.SupportsGemsOnly"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySupportsGemsOnly(bool? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (bySupportsGemsOnly is null)
        {
            bySupportsGemsOnly = new();
            foreach (var item in Items)
            {
                var itemKey = item.SupportsGemsOnly;

                if (!bySupportsGemsOnly.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySupportsGemsOnly.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySupportsGemsOnly.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.bySupportsGemsOnly"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GrantedEffectsDat>> GetManyToManyBySupportsGemsOnly(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySupportsGemsOnly(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown70(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown70(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown70(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byUnknown70 is null)
        {
            byUnknown70 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown70;

                if (!byUnknown70.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown70.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown70.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byUnknown70"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByUnknown70(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown70(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown74(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown74(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown74(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byUnknown74 is null)
        {
            byUnknown74 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown74;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown74.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown74.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown74.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byUnknown74"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByUnknown74(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown74(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.CannotBeSupported"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCannotBeSupported(bool? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCannotBeSupported(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.CannotBeSupported"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCannotBeSupported(bool? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byCannotBeSupported is null)
        {
            byCannotBeSupported = new();
            foreach (var item in Items)
            {
                var itemKey = item.CannotBeSupported;

                if (!byCannotBeSupported.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCannotBeSupported.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCannotBeSupported.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byCannotBeSupported"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GrantedEffectsDat>> GetManyToManyByCannotBeSupported(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCannotBeSupported(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown91"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown91(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown91(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown91"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown91(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byUnknown91 is null)
        {
            byUnknown91 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown91;

                if (!byUnknown91.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown91.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown91.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byUnknown91"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByUnknown91(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown91(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.CastTime"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCastTime(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCastTime(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.CastTime"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCastTime(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byCastTime is null)
        {
            byCastTime = new();
            foreach (var item in Items)
            {
                var itemKey = item.CastTime;

                if (!byCastTime.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCastTime.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCastTime.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byCastTime"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByCastTime(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCastTime(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.ActiveSkill"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByActiveSkill(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByActiveSkill(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.ActiveSkill"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByActiveSkill(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byActiveSkill is null)
        {
            byActiveSkill = new();
            foreach (var item in Items)
            {
                var itemKey = item.ActiveSkill;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byActiveSkill.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byActiveSkill.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byActiveSkill.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byActiveSkill"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByActiveSkill(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByActiveSkill(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.IgnoreMinionTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIgnoreMinionTypes(bool? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIgnoreMinionTypes(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.IgnoreMinionTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIgnoreMinionTypes(bool? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byIgnoreMinionTypes is null)
        {
            byIgnoreMinionTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.IgnoreMinionTypes;

                if (!byIgnoreMinionTypes.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIgnoreMinionTypes.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIgnoreMinionTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byIgnoreMinionTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GrantedEffectsDat>> GetManyToManyByIgnoreMinionTypes(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIgnoreMinionTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown116(bool? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown116(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown116(bool? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byUnknown116 is null)
        {
            byUnknown116 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown116;

                if (!byUnknown116.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown116.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown116.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byUnknown116"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GrantedEffectsDat>> GetManyToManyByUnknown116(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown116(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.AddedMinionActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAddedMinionActiveSkillTypes(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAddedMinionActiveSkillTypes(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.AddedMinionActiveSkillTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAddedMinionActiveSkillTypes(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byAddedMinionActiveSkillTypes is null)
        {
            byAddedMinionActiveSkillTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.AddedMinionActiveSkillTypes;
                foreach (var listKey in itemKey)
                {
                    if (!byAddedMinionActiveSkillTypes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAddedMinionActiveSkillTypes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAddedMinionActiveSkillTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byAddedMinionActiveSkillTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByAddedMinionActiveSkillTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAddedMinionActiveSkillTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Animation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAnimation(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAnimation(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Animation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAnimation(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byAnimation is null)
        {
            byAnimation = new();
            foreach (var item in Items)
            {
                var itemKey = item.Animation;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAnimation.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAnimation.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAnimation.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byAnimation"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByAnimation(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAnimation(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.MultiPartAchievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMultiPartAchievement(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMultiPartAchievement(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.MultiPartAchievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMultiPartAchievement(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byMultiPartAchievement is null)
        {
            byMultiPartAchievement = new();
            foreach (var item in Items)
            {
                var itemKey = item.MultiPartAchievement;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMultiPartAchievement.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMultiPartAchievement.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMultiPartAchievement.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byMultiPartAchievement"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByMultiPartAchievement(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMultiPartAchievement(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown165"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown165(bool? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown165(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown165"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown165(bool? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byUnknown165 is null)
        {
            byUnknown165 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown165;

                if (!byUnknown165.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown165.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown165.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byUnknown165"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GrantedEffectsDat>> GetManyToManyByUnknown165(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown165(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.SupportWeaponRestrictions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySupportWeaponRestrictions(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySupportWeaponRestrictions(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.SupportWeaponRestrictions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySupportWeaponRestrictions(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (bySupportWeaponRestrictions is null)
        {
            bySupportWeaponRestrictions = new();
            foreach (var item in Items)
            {
                var itemKey = item.SupportWeaponRestrictions;
                foreach (var listKey in itemKey)
                {
                    if (!bySupportWeaponRestrictions.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySupportWeaponRestrictions.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySupportWeaponRestrictions.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.bySupportWeaponRestrictions"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyBySupportWeaponRestrictions(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySupportWeaponRestrictions(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.RegularVariant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRegularVariant(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRegularVariant(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.RegularVariant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRegularVariant(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byRegularVariant is null)
        {
            byRegularVariant = new();
            foreach (var item in Items)
            {
                var itemKey = item.RegularVariant;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRegularVariant.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRegularVariant.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRegularVariant.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byRegularVariant"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByRegularVariant(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRegularVariant(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown190"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown190(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown190(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown190"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown190(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byUnknown190 is null)
        {
            byUnknown190 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown190;

                if (!byUnknown190.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown190.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown190.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byUnknown190"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByUnknown190(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown190(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown194"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown194(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown194(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown194"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown194(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byUnknown194 is null)
        {
            byUnknown194 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown194;

                if (!byUnknown194.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown194.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown194.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byUnknown194"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByUnknown194(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown194(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown198"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown198(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown198(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown198"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown198(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byUnknown198 is null)
        {
            byUnknown198 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown198;

                if (!byUnknown198.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown198.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown198.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byUnknown198"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByUnknown198(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown198(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown202"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown202(bool? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown202(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown202"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown202(bool? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byUnknown202 is null)
        {
            byUnknown202 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown202;

                if (!byUnknown202.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown202.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown202.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byUnknown202"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GrantedEffectsDat>> GetManyToManyByUnknown202(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown202(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.StatSet"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatSet(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatSet(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.StatSet"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatSet(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byStatSet is null)
        {
            byStatSet = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatSet;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatSet.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatSet.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatSet.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byStatSet"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByStatSet(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatSet(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown219"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown219(int? key, out GrantedEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown219(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.Unknown219"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown219(int? key, out IReadOnlyList<GrantedEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        if (byUnknown219 is null)
        {
            byUnknown219 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown219;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown219.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown219.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown219.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsDat"/> with <see cref="GrantedEffectsDat.byUnknown219"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsDat>> GetManyToManyByUnknown219(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown219(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GrantedEffectsDat[] Load()
    {
        const string filePath = "Data/GrantedEffects.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrantedEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsSupport
            (var issupportLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AllowedActiveSkillTypes
            (var tempallowedactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var allowedactiveskilltypesLoading = tempallowedactiveskilltypesLoading.AsReadOnly();

            // loading SupportGemLetter
            (var supportgemletterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Attribute
            (var attributeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AddedActiveSkillTypes
            (var tempaddedactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var addedactiveskilltypesLoading = tempaddedactiveskilltypesLoading.AsReadOnly();

            // loading ExcludedActiveSkillTypes
            (var tempexcludedactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var excludedactiveskilltypesLoading = tempexcludedactiveskilltypesLoading.AsReadOnly();

            // loading SupportsGemsOnly
            (var supportsgemsonlyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown74
            (var tempunknown74Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown74Loading = tempunknown74Loading.AsReadOnly();

            // loading CannotBeSupported
            (var cannotbesupportedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CastTime
            (var casttimeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ActiveSkill
            (var activeskillLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IgnoreMinionTypes
            (var ignoreminiontypesLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AddedMinionActiveSkillTypes
            (var tempaddedminionactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var addedminionactiveskilltypesLoading = tempaddedminionactiveskilltypesLoading.AsReadOnly();

            // loading Animation
            (var animationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MultiPartAchievement
            (var multipartachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown165
            (var unknown165Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SupportWeaponRestrictions
            (var tempsupportweaponrestrictionsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var supportweaponrestrictionsLoading = tempsupportweaponrestrictionsLoading.AsReadOnly();

            // loading RegularVariant
            (var regularvariantLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown190
            (var unknown190Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown194
            (var unknown194Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown198
            (var unknown198Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown202
            (var unknown202Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading StatSet
            (var statsetLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown219
            (var tempunknown219Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown219Loading = tempunknown219Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectsDat()
            {
                Id = idLoading,
                IsSupport = issupportLoading,
                AllowedActiveSkillTypes = allowedactiveskilltypesLoading,
                SupportGemLetter = supportgemletterLoading,
                Attribute = attributeLoading,
                AddedActiveSkillTypes = addedactiveskilltypesLoading,
                ExcludedActiveSkillTypes = excludedactiveskilltypesLoading,
                SupportsGemsOnly = supportsgemsonlyLoading,
                Unknown70 = unknown70Loading,
                Unknown74 = unknown74Loading,
                CannotBeSupported = cannotbesupportedLoading,
                Unknown91 = unknown91Loading,
                CastTime = casttimeLoading,
                ActiveSkill = activeskillLoading,
                IgnoreMinionTypes = ignoreminiontypesLoading,
                Unknown116 = unknown116Loading,
                AddedMinionActiveSkillTypes = addedminionactiveskilltypesLoading,
                Animation = animationLoading,
                MultiPartAchievement = multipartachievementLoading,
                Unknown165 = unknown165Loading,
                SupportWeaponRestrictions = supportweaponrestrictionsLoading,
                RegularVariant = regularvariantLoading,
                Unknown190 = unknown190Loading,
                Unknown194 = unknown194Loading,
                Unknown198 = unknown198Loading,
                Unknown202 = unknown202Loading,
                StatSet = statsetLoading,
                Unknown219 = unknown219Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
