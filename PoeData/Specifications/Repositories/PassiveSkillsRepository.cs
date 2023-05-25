using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PassiveSkillsDat"/> related data and helper methods.
/// </summary>
public sealed class PassiveSkillsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PassiveSkillsDat> Items { get; }

    private Dictionary<string, List<PassiveSkillsDat>>? byId;
    private Dictionary<string, List<PassiveSkillsDat>>? byIcon_DDSFile;
    private Dictionary<int, List<PassiveSkillsDat>>? byStats;
    private Dictionary<int, List<PassiveSkillsDat>>? byStat1Value;
    private Dictionary<int, List<PassiveSkillsDat>>? byStat2Value;
    private Dictionary<int, List<PassiveSkillsDat>>? byStat3Value;
    private Dictionary<int, List<PassiveSkillsDat>>? byStat4Value;
    private Dictionary<int, List<PassiveSkillsDat>>? byPassiveSkillGraphId;
    private Dictionary<string, List<PassiveSkillsDat>>? byName;
    private Dictionary<int, List<PassiveSkillsDat>>? byCharacters;
    private Dictionary<bool, List<PassiveSkillsDat>>? byIsKeystone;
    private Dictionary<bool, List<PassiveSkillsDat>>? byIsNotable;
    private Dictionary<string, List<PassiveSkillsDat>>? byFlavourText;
    private Dictionary<bool, List<PassiveSkillsDat>>? byIsJustIcon;
    private Dictionary<int, List<PassiveSkillsDat>>? byAchievementItem;
    private Dictionary<bool, List<PassiveSkillsDat>>? byIsJewelSocket;
    private Dictionary<int, List<PassiveSkillsDat>>? byAscendancyKey;
    private Dictionary<bool, List<PassiveSkillsDat>>? byIsAscendancyStartingNode;
    private Dictionary<int, List<PassiveSkillsDat>>? byReminderStrings;
    private Dictionary<int, List<PassiveSkillsDat>>? bySkillPointsGranted;
    private Dictionary<bool, List<PassiveSkillsDat>>? byIsMultipleChoice;
    private Dictionary<bool, List<PassiveSkillsDat>>? byIsMultipleChoiceOption;
    private Dictionary<int, List<PassiveSkillsDat>>? byStat5Value;
    private Dictionary<int, List<PassiveSkillsDat>>? byPassiveSkillBuffs;
    private Dictionary<int, List<PassiveSkillsDat>>? byGrantedEffectsPerLevel;
    private Dictionary<bool, List<PassiveSkillsDat>>? byIsAnointmentOnly;
    private Dictionary<int, List<PassiveSkillsDat>>? byUnknown180;
    private Dictionary<bool, List<PassiveSkillsDat>>? byIsExpansion;
    private Dictionary<bool, List<PassiveSkillsDat>>? byIsProxyPassive;
    private Dictionary<int, List<PassiveSkillsDat>>? bySkillType;
    private Dictionary<int, List<PassiveSkillsDat>>? byMasteryGroup;
    private Dictionary<int, List<PassiveSkillsDat>>? byUnknown206;
    private Dictionary<int, List<PassiveSkillsDat>>? bySoundEffect;
    private Dictionary<string, List<PassiveSkillsDat>>? byUnknown238;
    private Dictionary<int, List<PassiveSkillsDat>>? byUnknown246;
    private Dictionary<int, List<PassiveSkillsDat>>? byUnknown250;
    private Dictionary<int, List<PassiveSkillsDat>>? byUnknown254;
    private Dictionary<int, List<PassiveSkillsDat>>? byUnknown258;
    private Dictionary<int, List<PassiveSkillsDat>>? byUnknown262;
    private Dictionary<bool, List<PassiveSkillsDat>>? byUnknown266;
    private Dictionary<int, List<PassiveSkillsDat>>? byUnknown267;
    private Dictionary<int, List<PassiveSkillsDat>>? byUnknown283;
    private Dictionary<int, List<PassiveSkillsDat>>? byUnknown287;
    private Dictionary<bool, List<PassiveSkillsDat>>? byUnknown303;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassiveSkillsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PassiveSkillsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out PassiveSkillsDat? item)
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
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
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Icon_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon_DDSFile(string? key, out PassiveSkillsDat? item)
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Icon_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon_DDSFile(string? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
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
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIcon_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillsDat>> GetManyToManyByIcon_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStats(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStats(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStats(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byStats is null)
        {
            byStats = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stats;
                foreach (var listKey in itemKey)
                {
                    if (!byStats.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStats.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStats.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byStats"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByStats(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStats(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stat1Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat1Value(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat1Value(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stat1Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat1Value(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byStat1Value is null)
        {
            byStat1Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat1Value;

                if (!byStat1Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat1Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat1Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byStat1Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByStat1Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat1Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stat2Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat2Value(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat2Value(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stat2Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat2Value(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byStat2Value is null)
        {
            byStat2Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat2Value;

                if (!byStat2Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat2Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat2Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byStat2Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByStat2Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat2Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stat3Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat3Value(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat3Value(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stat3Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat3Value(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byStat3Value is null)
        {
            byStat3Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat3Value;

                if (!byStat3Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat3Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat3Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byStat3Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByStat3Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat3Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stat4Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat4Value(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat4Value(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stat4Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat4Value(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byStat4Value is null)
        {
            byStat4Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat4Value;

                if (!byStat4Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat4Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat4Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byStat4Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByStat4Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat4Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.PassiveSkillGraphId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveSkillGraphId(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveSkillGraphId(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.PassiveSkillGraphId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveSkillGraphId(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byPassiveSkillGraphId is null)
        {
            byPassiveSkillGraphId = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveSkillGraphId;

                if (!byPassiveSkillGraphId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPassiveSkillGraphId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveSkillGraphId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byPassiveSkillGraphId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByPassiveSkillGraphId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveSkillGraphId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out PassiveSkillsDat? item)
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
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
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Characters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharacters(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharacters(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Characters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharacters(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byCharacters is null)
        {
            byCharacters = new();
            foreach (var item in Items)
            {
                var itemKey = item.Characters;
                foreach (var listKey in itemKey)
                {
                    if (!byCharacters.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCharacters.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCharacters.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byCharacters"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByCharacters(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharacters(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsKeystone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsKeystone(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsKeystone(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsKeystone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsKeystone(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byIsKeystone is null)
        {
            byIsKeystone = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsKeystone;

                if (!byIsKeystone.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsKeystone.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsKeystone.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIsKeystone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByIsKeystone(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsKeystone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsNotable"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsNotable(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsNotable(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsNotable"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsNotable(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byIsNotable is null)
        {
            byIsNotable = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsNotable;

                if (!byIsNotable.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsNotable.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsNotable.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIsNotable"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByIsNotable(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsNotable(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourText(string? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlavourText(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourText(string? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byFlavourText is null)
        {
            byFlavourText = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlavourText;

                if (!byFlavourText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFlavourText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFlavourText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byFlavourText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillsDat>> GetManyToManyByFlavourText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsJustIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsJustIcon(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsJustIcon(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsJustIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsJustIcon(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byIsJustIcon is null)
        {
            byIsJustIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsJustIcon;

                if (!byIsJustIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsJustIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsJustIcon.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIsJustIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByIsJustIcon(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsJustIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.AchievementItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItem(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItem(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.AchievementItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItem(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byAchievementItem is null)
        {
            byAchievementItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAchievementItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAchievementItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAchievementItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byAchievementItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByAchievementItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsJewelSocket"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsJewelSocket(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsJewelSocket(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsJewelSocket"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsJewelSocket(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byIsJewelSocket is null)
        {
            byIsJewelSocket = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsJewelSocket;

                if (!byIsJewelSocket.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsJewelSocket.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsJewelSocket.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIsJewelSocket"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByIsJewelSocket(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsJewelSocket(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.AscendancyKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAscendancyKey(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAscendancyKey(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.AscendancyKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAscendancyKey(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byAscendancyKey is null)
        {
            byAscendancyKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AscendancyKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAscendancyKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAscendancyKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAscendancyKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byAscendancyKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByAscendancyKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAscendancyKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsAscendancyStartingNode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsAscendancyStartingNode(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsAscendancyStartingNode(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsAscendancyStartingNode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsAscendancyStartingNode(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byIsAscendancyStartingNode is null)
        {
            byIsAscendancyStartingNode = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsAscendancyStartingNode;

                if (!byIsAscendancyStartingNode.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsAscendancyStartingNode.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsAscendancyStartingNode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIsAscendancyStartingNode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByIsAscendancyStartingNode(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsAscendancyStartingNode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.ReminderStrings"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByReminderStrings(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByReminderStrings(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.ReminderStrings"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByReminderStrings(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byReminderStrings is null)
        {
            byReminderStrings = new();
            foreach (var item in Items)
            {
                var itemKey = item.ReminderStrings;
                foreach (var listKey in itemKey)
                {
                    if (!byReminderStrings.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byReminderStrings.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byReminderStrings.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byReminderStrings"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByReminderStrings(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByReminderStrings(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.SkillPointsGranted"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillPointsGranted(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillPointsGranted(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.SkillPointsGranted"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillPointsGranted(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (bySkillPointsGranted is null)
        {
            bySkillPointsGranted = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillPointsGranted;

                if (!bySkillPointsGranted.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySkillPointsGranted.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillPointsGranted.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.bySkillPointsGranted"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyBySkillPointsGranted(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillPointsGranted(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsMultipleChoice"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsMultipleChoice(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsMultipleChoice(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsMultipleChoice"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsMultipleChoice(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byIsMultipleChoice is null)
        {
            byIsMultipleChoice = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsMultipleChoice;

                if (!byIsMultipleChoice.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsMultipleChoice.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsMultipleChoice.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIsMultipleChoice"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByIsMultipleChoice(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsMultipleChoice(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsMultipleChoiceOption"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsMultipleChoiceOption(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsMultipleChoiceOption(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsMultipleChoiceOption"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsMultipleChoiceOption(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byIsMultipleChoiceOption is null)
        {
            byIsMultipleChoiceOption = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsMultipleChoiceOption;

                if (!byIsMultipleChoiceOption.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsMultipleChoiceOption.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsMultipleChoiceOption.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIsMultipleChoiceOption"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByIsMultipleChoiceOption(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsMultipleChoiceOption(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stat5Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat5Value(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat5Value(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Stat5Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat5Value(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byStat5Value is null)
        {
            byStat5Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat5Value;

                if (!byStat5Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat5Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat5Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byStat5Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByStat5Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat5Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.PassiveSkillBuffs"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveSkillBuffs(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveSkillBuffs(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.PassiveSkillBuffs"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveSkillBuffs(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byPassiveSkillBuffs is null)
        {
            byPassiveSkillBuffs = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveSkillBuffs;
                foreach (var listKey in itemKey)
                {
                    if (!byPassiveSkillBuffs.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPassiveSkillBuffs.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPassiveSkillBuffs.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byPassiveSkillBuffs"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByPassiveSkillBuffs(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveSkillBuffs(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.GrantedEffectsPerLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffectsPerLevel(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffectsPerLevel(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.GrantedEffectsPerLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffectsPerLevel(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byGrantedEffectsPerLevel is null)
        {
            byGrantedEffectsPerLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffectsPerLevel;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGrantedEffectsPerLevel.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGrantedEffectsPerLevel.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGrantedEffectsPerLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byGrantedEffectsPerLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByGrantedEffectsPerLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffectsPerLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsAnointmentOnly"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsAnointmentOnly(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsAnointmentOnly(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsAnointmentOnly"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsAnointmentOnly(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byIsAnointmentOnly is null)
        {
            byIsAnointmentOnly = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsAnointmentOnly;

                if (!byIsAnointmentOnly.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsAnointmentOnly.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsAnointmentOnly.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIsAnointmentOnly"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByIsAnointmentOnly(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsAnointmentOnly(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown180"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown180(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown180(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown180"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown180(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown180 is null)
        {
            byUnknown180 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown180;

                if (!byUnknown180.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown180.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown180.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown180"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByUnknown180(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown180(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsExpansion"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsExpansion(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsExpansion(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsExpansion"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsExpansion(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byIsExpansion is null)
        {
            byIsExpansion = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsExpansion;

                if (!byIsExpansion.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsExpansion.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsExpansion.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIsExpansion"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByIsExpansion(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsExpansion(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsProxyPassive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsProxyPassive(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsProxyPassive(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.IsProxyPassive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsProxyPassive(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byIsProxyPassive is null)
        {
            byIsProxyPassive = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsProxyPassive;

                if (!byIsProxyPassive.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsProxyPassive.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsProxyPassive.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byIsProxyPassive"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByIsProxyPassive(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsProxyPassive(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.SkillType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillType(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillType(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.SkillType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillType(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (bySkillType is null)
        {
            bySkillType = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillType;

                if (!bySkillType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySkillType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.bySkillType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyBySkillType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.MasteryGroup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMasteryGroup(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMasteryGroup(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.MasteryGroup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMasteryGroup(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byMasteryGroup is null)
        {
            byMasteryGroup = new();
            foreach (var item in Items)
            {
                var itemKey = item.MasteryGroup;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMasteryGroup.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMasteryGroup.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMasteryGroup.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byMasteryGroup"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByMasteryGroup(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMasteryGroup(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown206"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown206(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown206(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown206"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown206(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown206 is null)
        {
            byUnknown206 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown206;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown206.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown206.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown206.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown206"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByUnknown206(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown206(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.SoundEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySoundEffect(int? key, out PassiveSkillsDat? item)
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.SoundEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySoundEffect(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
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
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.bySoundEffect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyBySoundEffect(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySoundEffect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown238"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown238(string? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown238(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown238"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown238(string? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown238 is null)
        {
            byUnknown238 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown238;

                if (!byUnknown238.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown238.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown238.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown238"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillsDat>> GetManyToManyByUnknown238(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown238(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown246"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown246(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown246(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown246"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown246(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown246 is null)
        {
            byUnknown246 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown246;

                if (!byUnknown246.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown246.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown246.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown246"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByUnknown246(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown246(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown250"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown250(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown250(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown250"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown250(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown250 is null)
        {
            byUnknown250 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown250;

                if (!byUnknown250.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown250.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown250.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown250"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByUnknown250(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown250(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown254"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown254(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown254(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown254"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown254(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown254 is null)
        {
            byUnknown254 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown254;

                if (!byUnknown254.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown254.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown254.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown254"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByUnknown254(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown254(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown258"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown258(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown258(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown258"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown258(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown258 is null)
        {
            byUnknown258 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown258;

                if (!byUnknown258.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown258.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown258.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown258"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByUnknown258(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown258(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown262"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown262(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown262(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown262"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown262(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown262 is null)
        {
            byUnknown262 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown262;

                if (!byUnknown262.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown262.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown262.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown262"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByUnknown262(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown262(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown266"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown266(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown266(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown266"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown266(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown266 is null)
        {
            byUnknown266 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown266;

                if (!byUnknown266.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown266.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown266.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown266"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByUnknown266(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown266(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown267"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown267(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown267(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown267"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown267(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown267 is null)
        {
            byUnknown267 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown267;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown267.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown267.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown267.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown267"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByUnknown267(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown267(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown283"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown283(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown283(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown283"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown283(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown283 is null)
        {
            byUnknown283 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown283;

                if (!byUnknown283.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown283.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown283.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown283"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByUnknown283(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown283(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown287"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown287(int? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown287(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown287"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown287(int? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown287 is null)
        {
            byUnknown287 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown287;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown287.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown287.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown287.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown287"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillsDat>> GetManyToManyByUnknown287(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown287(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown303"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown303(bool? key, out PassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown303(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.Unknown303"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown303(bool? key, out IReadOnlyList<PassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        if (byUnknown303 is null)
        {
            byUnknown303 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown303;

                if (!byUnknown303.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown303.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown303.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillsDat"/> with <see cref="PassiveSkillsDat.byUnknown303"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillsDat>> GetManyToManyByUnknown303(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillsDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown303(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PassiveSkillsDat[] Load()
    {
        const string filePath = "Data/PassiveSkills.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon_DDSFile
            (var icon_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading Stat1Value
            (var stat1valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Value
            (var stat2valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat3Value
            (var stat3valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat4Value
            (var stat4valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PassiveSkillGraphId
            (var passiveskillgraphidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Characters
            (var tempcharactersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var charactersLoading = tempcharactersLoading.AsReadOnly();

            // loading IsKeystone
            (var iskeystoneLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsNotable
            (var isnotableLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsJustIcon
            (var isjusticonLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AchievementItem
            (var achievementitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsJewelSocket
            (var isjewelsocketLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AscendancyKey
            (var ascendancykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsAscendancyStartingNode
            (var isascendancystartingnodeLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ReminderStrings
            (var tempreminderstringsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var reminderstringsLoading = tempreminderstringsLoading.AsReadOnly();

            // loading SkillPointsGranted
            (var skillpointsgrantedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsMultipleChoice
            (var ismultiplechoiceLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsMultipleChoiceOption
            (var ismultiplechoiceoptionLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Stat5Value
            (var stat5valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PassiveSkillBuffs
            (var temppassiveskillbuffsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var passiveskillbuffsLoading = temppassiveskillbuffsLoading.AsReadOnly();

            // loading GrantedEffectsPerLevel
            (var grantedeffectsperlevelLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsAnointmentOnly
            (var isanointmentonlyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown180
            (var unknown180Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsExpansion
            (var isexpansionLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsProxyPassive
            (var isproxypassiveLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SkillType
            (var skilltypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MasteryGroup
            (var masterygroupLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown206
            (var unknown206Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SoundEffect
            (var soundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown238
            (var unknown238Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown246
            (var unknown246Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown250
            (var unknown250Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown254
            (var unknown254Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown258
            (var unknown258Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown262
            (var unknown262Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown266
            (var unknown266Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown267
            (var tempunknown267Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown267Loading = tempunknown267Loading.AsReadOnly();

            // loading Unknown283
            (var unknown283Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown287
            (var tempunknown287Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown287Loading = tempunknown287Loading.AsReadOnly();

            // loading Unknown303
            (var unknown303Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillsDat()
            {
                Id = idLoading,
                Icon_DDSFile = icon_ddsfileLoading,
                Stats = statsLoading,
                Stat1Value = stat1valueLoading,
                Stat2Value = stat2valueLoading,
                Stat3Value = stat3valueLoading,
                Stat4Value = stat4valueLoading,
                PassiveSkillGraphId = passiveskillgraphidLoading,
                Name = nameLoading,
                Characters = charactersLoading,
                IsKeystone = iskeystoneLoading,
                IsNotable = isnotableLoading,
                FlavourText = flavourtextLoading,
                IsJustIcon = isjusticonLoading,
                AchievementItem = achievementitemLoading,
                IsJewelSocket = isjewelsocketLoading,
                AscendancyKey = ascendancykeyLoading,
                IsAscendancyStartingNode = isascendancystartingnodeLoading,
                ReminderStrings = reminderstringsLoading,
                SkillPointsGranted = skillpointsgrantedLoading,
                IsMultipleChoice = ismultiplechoiceLoading,
                IsMultipleChoiceOption = ismultiplechoiceoptionLoading,
                Stat5Value = stat5valueLoading,
                PassiveSkillBuffs = passiveskillbuffsLoading,
                GrantedEffectsPerLevel = grantedeffectsperlevelLoading,
                IsAnointmentOnly = isanointmentonlyLoading,
                Unknown180 = unknown180Loading,
                IsExpansion = isexpansionLoading,
                IsProxyPassive = isproxypassiveLoading,
                SkillType = skilltypeLoading,
                MasteryGroup = masterygroupLoading,
                Unknown206 = unknown206Loading,
                SoundEffect = soundeffectLoading,
                Unknown238 = unknown238Loading,
                Unknown246 = unknown246Loading,
                Unknown250 = unknown250Loading,
                Unknown254 = unknown254Loading,
                Unknown258 = unknown258Loading,
                Unknown262 = unknown262Loading,
                Unknown266 = unknown266Loading,
                Unknown267 = unknown267Loading,
                Unknown283 = unknown283Loading,
                Unknown287 = unknown287Loading,
                Unknown303 = unknown303Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
