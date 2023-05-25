using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PassiveSkillMasteryGroupsDat"/> related data and helper methods.
/// </summary>
public sealed class PassiveSkillMasteryGroupsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PassiveSkillMasteryGroupsDat> Items { get; }

    private Dictionary<string, List<PassiveSkillMasteryGroupsDat>>? byId;
    private Dictionary<int, List<PassiveSkillMasteryGroupsDat>>? byMasteryEffects;
    private Dictionary<string, List<PassiveSkillMasteryGroupsDat>>? byInactiveIcon;
    private Dictionary<string, List<PassiveSkillMasteryGroupsDat>>? byActiveIcon;
    private Dictionary<string, List<PassiveSkillMasteryGroupsDat>>? byActiveEffectImage;
    private Dictionary<bool, List<PassiveSkillMasteryGroupsDat>>? byUnknown48;
    private Dictionary<int, List<PassiveSkillMasteryGroupsDat>>? bySoundEffect;
    private Dictionary<int, List<PassiveSkillMasteryGroupsDat>>? byMasteryCountStat;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassiveSkillMasteryGroupsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PassiveSkillMasteryGroupsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out PassiveSkillMasteryGroupsDat? item)
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
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<PassiveSkillMasteryGroupsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
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
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillMasteryGroupsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillMasteryGroupsDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillMasteryGroupsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillMasteryGroupsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.MasteryEffects"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMasteryEffects(int? key, out PassiveSkillMasteryGroupsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMasteryEffects(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.MasteryEffects"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMasteryEffects(int? key, out IReadOnlyList<PassiveSkillMasteryGroupsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        if (byMasteryEffects is null)
        {
            byMasteryEffects = new();
            foreach (var item in Items)
            {
                var itemKey = item.MasteryEffects;
                foreach (var listKey in itemKey)
                {
                    if (!byMasteryEffects.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMasteryEffects.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMasteryEffects.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.byMasteryEffects"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillMasteryGroupsDat>> GetManyToManyByMasteryEffects(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillMasteryGroupsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillMasteryGroupsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMasteryEffects(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillMasteryGroupsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.InactiveIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInactiveIcon(string? key, out PassiveSkillMasteryGroupsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInactiveIcon(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.InactiveIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInactiveIcon(string? key, out IReadOnlyList<PassiveSkillMasteryGroupsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        if (byInactiveIcon is null)
        {
            byInactiveIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.InactiveIcon;

                if (!byInactiveIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInactiveIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInactiveIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.byInactiveIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillMasteryGroupsDat>> GetManyToManyByInactiveIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillMasteryGroupsDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillMasteryGroupsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInactiveIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillMasteryGroupsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.ActiveIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByActiveIcon(string? key, out PassiveSkillMasteryGroupsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByActiveIcon(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.ActiveIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByActiveIcon(string? key, out IReadOnlyList<PassiveSkillMasteryGroupsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        if (byActiveIcon is null)
        {
            byActiveIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.ActiveIcon;

                if (!byActiveIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byActiveIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byActiveIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.byActiveIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillMasteryGroupsDat>> GetManyToManyByActiveIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillMasteryGroupsDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillMasteryGroupsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByActiveIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillMasteryGroupsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.ActiveEffectImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByActiveEffectImage(string? key, out PassiveSkillMasteryGroupsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByActiveEffectImage(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.ActiveEffectImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByActiveEffectImage(string? key, out IReadOnlyList<PassiveSkillMasteryGroupsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        if (byActiveEffectImage is null)
        {
            byActiveEffectImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.ActiveEffectImage;

                if (!byActiveEffectImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byActiveEffectImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byActiveEffectImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.byActiveEffectImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillMasteryGroupsDat>> GetManyToManyByActiveEffectImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillMasteryGroupsDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillMasteryGroupsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByActiveEffectImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillMasteryGroupsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(bool? key, out PassiveSkillMasteryGroupsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown48(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(bool? key, out IReadOnlyList<PassiveSkillMasteryGroupsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;

                if (!byUnknown48.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillMasteryGroupsDat>> GetManyToManyByUnknown48(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillMasteryGroupsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillMasteryGroupsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillMasteryGroupsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.SoundEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySoundEffect(int? key, out PassiveSkillMasteryGroupsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySoundEffect(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.SoundEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySoundEffect(int? key, out IReadOnlyList<PassiveSkillMasteryGroupsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        if (bySoundEffect is null)
        {
            bySoundEffect = new();
            foreach (var item in Items)
            {
                var itemKey = item.SoundEffect;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySoundEffect.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySoundEffect.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySoundEffect.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.bySoundEffect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillMasteryGroupsDat>> GetManyToManyBySoundEffect(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillMasteryGroupsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillMasteryGroupsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySoundEffect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillMasteryGroupsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.MasteryCountStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMasteryCountStat(int? key, out PassiveSkillMasteryGroupsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMasteryCountStat(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.MasteryCountStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMasteryCountStat(int? key, out IReadOnlyList<PassiveSkillMasteryGroupsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        if (byMasteryCountStat is null)
        {
            byMasteryCountStat = new();
            foreach (var item in Items)
            {
                var itemKey = item.MasteryCountStat;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMasteryCountStat.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMasteryCountStat.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMasteryCountStat.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillMasteryGroupsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillMasteryGroupsDat"/> with <see cref="PassiveSkillMasteryGroupsDat.byMasteryCountStat"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillMasteryGroupsDat>> GetManyToManyByMasteryCountStat(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillMasteryGroupsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillMasteryGroupsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMasteryCountStat(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillMasteryGroupsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PassiveSkillMasteryGroupsDat[] Load()
    {
        const string filePath = "Data/PassiveSkillMasteryGroups.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillMasteryGroupsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MasteryEffects
            (var tempmasteryeffectsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var masteryeffectsLoading = tempmasteryeffectsLoading.AsReadOnly();

            // loading InactiveIcon
            (var inactiveiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ActiveIcon
            (var activeiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ActiveEffectImage
            (var activeeffectimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SoundEffect
            (var soundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MasteryCountStat
            (var masterycountstatLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillMasteryGroupsDat()
            {
                Id = idLoading,
                MasteryEffects = masteryeffectsLoading,
                InactiveIcon = inactiveiconLoading,
                ActiveIcon = activeiconLoading,
                ActiveEffectImage = activeeffectimageLoading,
                Unknown48 = unknown48Loading,
                SoundEffect = soundeffectLoading,
                MasteryCountStat = masterycountstatLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
