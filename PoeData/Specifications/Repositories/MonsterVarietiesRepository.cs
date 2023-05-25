using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterVarietiesDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterVarietiesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterVarietiesDat> Items { get; }

    private Dictionary<string, List<MonsterVarietiesDat>>? byId;
    private Dictionary<int, List<MonsterVarietiesDat>>? byMonsterTypesKey;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown24;
    private Dictionary<int, List<MonsterVarietiesDat>>? byObjectSize;
    private Dictionary<int, List<MonsterVarietiesDat>>? byMinimumAttackDistance;
    private Dictionary<int, List<MonsterVarietiesDat>>? byMaximumAttackDistance;
    private Dictionary<string, List<MonsterVarietiesDat>>? byACTFiles;
    private Dictionary<string, List<MonsterVarietiesDat>>? byAOFiles;
    private Dictionary<string, List<MonsterVarietiesDat>>? byBaseMonsterTypeIndex;
    private Dictionary<int, List<MonsterVarietiesDat>>? byModsKeys;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown96;
    private Dictionary<string, List<MonsterVarietiesDat>>? byUnknown100;
    private Dictionary<string, List<MonsterVarietiesDat>>? byUnknown108;
    private Dictionary<int, List<MonsterVarietiesDat>>? byModelSizeMultiplier;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown120;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown124;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown128;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown132;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown136;
    private Dictionary<int, List<MonsterVarietiesDat>>? byTagsKeys;
    private Dictionary<int, List<MonsterVarietiesDat>>? byExperienceMultiplier;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown160;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown176;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown180;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown184;
    private Dictionary<int, List<MonsterVarietiesDat>>? byCriticalStrikeChance;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown192;
    private Dictionary<int, List<MonsterVarietiesDat>>? byGrantedEffectsKeys;
    private Dictionary<string, List<MonsterVarietiesDat>>? byAISFile;
    private Dictionary<int, List<MonsterVarietiesDat>>? byModsKeys2;
    private Dictionary<string, List<MonsterVarietiesDat>>? byStance;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown244;
    private Dictionary<string, List<MonsterVarietiesDat>>? byName;
    private Dictionary<int, List<MonsterVarietiesDat>>? byDamageMultiplier;
    private Dictionary<int, List<MonsterVarietiesDat>>? byLifeMultiplier;
    private Dictionary<int, List<MonsterVarietiesDat>>? byAttackSpeed;
    private Dictionary<int, List<MonsterVarietiesDat>>? byWeapon1_ItemVisualIdentityKeys;
    private Dictionary<int, List<MonsterVarietiesDat>>? byWeapon2_ItemVisualIdentityKeys;
    private Dictionary<int, List<MonsterVarietiesDat>>? byBack_ItemVisualIdentityKey;
    private Dictionary<int, List<MonsterVarietiesDat>>? byMainHand_ItemClassesKey;
    private Dictionary<int, List<MonsterVarietiesDat>>? byOffHand_ItemClassesKey;
    private Dictionary<int, List<MonsterVarietiesDat>>? byHelmet_ItemVisualIdentityKey;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown376;
    private Dictionary<int, List<MonsterVarietiesDat>>? byKillSpecificMonsterCount_AchievementItemsKeys;
    private Dictionary<int, List<MonsterVarietiesDat>>? bySpecial_ModsKeys;
    private Dictionary<int, List<MonsterVarietiesDat>>? byKillRare_AchievementItemsKeys;
    private Dictionary<bool, List<MonsterVarietiesDat>>? byUnknown428;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown429;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown433;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown437;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown441;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown445;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown449;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown453;
    private Dictionary<bool, List<MonsterVarietiesDat>>? byUnknown457;
    private Dictionary<string, List<MonsterVarietiesDat>>? byUnknown458;
    private Dictionary<int, List<MonsterVarietiesDat>>? byKillWhileOnslaughtIsActive_AchievementItemsKey;
    private Dictionary<int, List<MonsterVarietiesDat>>? byMonsterSegmentsKey;
    private Dictionary<int, List<MonsterVarietiesDat>>? byMonsterArmoursKey;
    private Dictionary<int, List<MonsterVarietiesDat>>? byKillWhileTalismanIsActive_AchievementItemsKey;
    private Dictionary<int, List<MonsterVarietiesDat>>? byPart1_ModsKeys;
    private Dictionary<int, List<MonsterVarietiesDat>>? byPart2_ModsKeys;
    private Dictionary<int, List<MonsterVarietiesDat>>? byEndgame_ModsKeys;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown578;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown594;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown598;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown602;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown618;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown634;
    private Dictionary<string, List<MonsterVarietiesDat>>? bySinkAnimation_AOFile;
    private Dictionary<bool, List<MonsterVarietiesDat>>? byUnknown646;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown647;
    private Dictionary<bool, List<MonsterVarietiesDat>>? byUnknown663;
    private Dictionary<bool, List<MonsterVarietiesDat>>? byUnknown664;
    private Dictionary<bool, List<MonsterVarietiesDat>>? byUnknown665;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown666;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown670;
    private Dictionary<float, List<MonsterVarietiesDat>>? byUnknown674;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown678;
    private Dictionary<string, List<MonsterVarietiesDat>>? byEPKFile;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown690;
    private Dictionary<int, List<MonsterVarietiesDat>>? byMonsterConditionalEffectPacksKey;
    private Dictionary<bool, List<MonsterVarietiesDat>>? byUnknown710;
    private Dictionary<bool, List<MonsterVarietiesDat>>? byUnknown711;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown712;
    private Dictionary<bool, List<MonsterVarietiesDat>>? byUnknown716;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown717;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown721;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown725;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown729;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown733;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown737;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown741;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown757;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown761;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown765;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown769;
    private Dictionary<int, List<MonsterVarietiesDat>>? byUnknown773;
    private Dictionary<bool, List<MonsterVarietiesDat>>? byUnknown777;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterVarietiesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterVarietiesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MonsterVarietiesDat? item)
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
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
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MonsterTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterTypesKey(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterTypesKey(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MonsterTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterTypesKey(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byMonsterTypesKey is null)
        {
            byMonsterTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byMonsterTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByMonsterTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out MonsterVarietiesDat? item)
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
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

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ObjectSize"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByObjectSize(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByObjectSize(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ObjectSize"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByObjectSize(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byObjectSize is null)
        {
            byObjectSize = new();
            foreach (var item in Items)
            {
                var itemKey = item.ObjectSize;

                if (!byObjectSize.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byObjectSize.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byObjectSize.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byObjectSize"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByObjectSize(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByObjectSize(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MinimumAttackDistance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinimumAttackDistance(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinimumAttackDistance(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MinimumAttackDistance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinimumAttackDistance(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byMinimumAttackDistance is null)
        {
            byMinimumAttackDistance = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinimumAttackDistance;

                if (!byMinimumAttackDistance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinimumAttackDistance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinimumAttackDistance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byMinimumAttackDistance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByMinimumAttackDistance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinimumAttackDistance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MaximumAttackDistance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaximumAttackDistance(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaximumAttackDistance(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MaximumAttackDistance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaximumAttackDistance(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byMaximumAttackDistance is null)
        {
            byMaximumAttackDistance = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaximumAttackDistance;

                if (!byMaximumAttackDistance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaximumAttackDistance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaximumAttackDistance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byMaximumAttackDistance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByMaximumAttackDistance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaximumAttackDistance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ACTFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByACTFiles(string? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByACTFiles(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ACTFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByACTFiles(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byACTFiles is null)
        {
            byACTFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.ACTFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byACTFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byACTFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byACTFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byACTFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyByACTFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByACTFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.AOFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFiles(string? key, out MonsterVarietiesDat? item)
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.AOFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFiles(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
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
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byAOFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyByAOFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.BaseMonsterTypeIndex"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseMonsterTypeIndex(string? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseMonsterTypeIndex(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.BaseMonsterTypeIndex"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseMonsterTypeIndex(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byBaseMonsterTypeIndex is null)
        {
            byBaseMonsterTypeIndex = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseMonsterTypeIndex;

                if (!byBaseMonsterTypeIndex.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseMonsterTypeIndex.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseMonsterTypeIndex.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byBaseMonsterTypeIndex"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyByBaseMonsterTypeIndex(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseMonsterTypeIndex(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byModsKeys is null)
        {
            byModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown96(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown96(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown96(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown96 is null)
        {
            byUnknown96 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown96;

                if (!byUnknown96.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown96.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown96.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown96"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown96(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown96(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(string? key, out MonsterVarietiesDat? item)
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
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

        if (!byUnknown100.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyByUnknown100(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(string? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown108(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown108 is null)
        {
            byUnknown108 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown108;

                if (!byUnknown108.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown108.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown108.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyByUnknown108(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ModelSizeMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModelSizeMultiplier(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModelSizeMultiplier(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ModelSizeMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModelSizeMultiplier(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byModelSizeMultiplier is null)
        {
            byModelSizeMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModelSizeMultiplier;

                if (!byModelSizeMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byModelSizeMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byModelSizeMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byModelSizeMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByModelSizeMultiplier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModelSizeMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown120(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown120(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown120(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown120 is null)
        {
            byUnknown120 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown120;

                if (!byUnknown120.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown120.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown120.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown120"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown120(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown120(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown124(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown124(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown124(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown124 is null)
        {
            byUnknown124 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown124;

                if (!byUnknown124.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown124.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown124.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown124"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown124(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown124(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown128"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown128(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown128(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown128"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown128(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown128 is null)
        {
            byUnknown128 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown128;

                if (!byUnknown128.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown128.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown128.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown128"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown128(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown128(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown132"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown132(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown132(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown132"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown132(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown132 is null)
        {
            byUnknown132 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown132;

                if (!byUnknown132.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown132.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown132.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown132"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown132(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown132(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown136"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown136(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown136(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown136"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown136(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown136 is null)
        {
            byUnknown136 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown136;

                if (!byUnknown136.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown136.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown136.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown136"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown136(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown136(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTagsKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTagsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTagsKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byTagsKeys is null)
        {
            byTagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byTagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byTagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByTagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ExperienceMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExperienceMultiplier(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExperienceMultiplier(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ExperienceMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExperienceMultiplier(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byExperienceMultiplier is null)
        {
            byExperienceMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.ExperienceMultiplier;

                if (!byExperienceMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byExperienceMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byExperienceMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byExperienceMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByExperienceMultiplier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExperienceMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown160(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown160(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown160(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown160 is null)
        {
            byUnknown160 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown160;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown160.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown160.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown160.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown160"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown160(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown160(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown176(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown176(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown176(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown176 is null)
        {
            byUnknown176 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown176;

                if (!byUnknown176.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown176.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown176.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown176"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown176(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown176(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown180"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown180(int? key, out MonsterVarietiesDat? item)
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown180"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown180(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
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
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown180"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown180(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown180(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown184"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown184(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown184(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown184"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown184(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown184 is null)
        {
            byUnknown184 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown184;

                if (!byUnknown184.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown184.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown184.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown184"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown184(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown184(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.CriticalStrikeChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCriticalStrikeChance(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCriticalStrikeChance(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.CriticalStrikeChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCriticalStrikeChance(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byCriticalStrikeChance is null)
        {
            byCriticalStrikeChance = new();
            foreach (var item in Items)
            {
                var itemKey = item.CriticalStrikeChance;

                if (!byCriticalStrikeChance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCriticalStrikeChance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCriticalStrikeChance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byCriticalStrikeChance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByCriticalStrikeChance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCriticalStrikeChance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown192"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown192(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown192(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown192"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown192(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown192 is null)
        {
            byUnknown192 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown192;

                if (!byUnknown192.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown192.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown192.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown192"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown192(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown192(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.GrantedEffectsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffectsKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffectsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.GrantedEffectsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffectsKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byGrantedEffectsKeys is null)
        {
            byGrantedEffectsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffectsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byGrantedEffectsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byGrantedEffectsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byGrantedEffectsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byGrantedEffectsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByGrantedEffectsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffectsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.AISFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAISFile(string? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAISFile(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.AISFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAISFile(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byAISFile is null)
        {
            byAISFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.AISFile;

                if (!byAISFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAISFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAISFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byAISFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyByAISFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAISFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ModsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKeys2(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKeys2(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.ModsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKeys2(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byModsKeys2 is null)
        {
            byModsKeys2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKeys2;
                foreach (var listKey in itemKey)
                {
                    if (!byModsKeys2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModsKeys2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModsKeys2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byModsKeys2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByModsKeys2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKeys2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Stance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStance(string? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStance(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Stance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStance(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byStance is null)
        {
            byStance = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stance;

                if (!byStance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStance.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byStance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyByStance(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown244"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown244(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown244(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown244"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown244(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown244 is null)
        {
            byUnknown244 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown244;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown244.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown244.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown244.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown244"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown244(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown244(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out MonsterVarietiesDat? item)
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
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
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.DamageMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDamageMultiplier(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDamageMultiplier(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.DamageMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDamageMultiplier(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byDamageMultiplier is null)
        {
            byDamageMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.DamageMultiplier;

                if (!byDamageMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDamageMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDamageMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byDamageMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByDamageMultiplier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDamageMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.LifeMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLifeMultiplier(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLifeMultiplier(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.LifeMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLifeMultiplier(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byLifeMultiplier is null)
        {
            byLifeMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.LifeMultiplier;

                if (!byLifeMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLifeMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLifeMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byLifeMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByLifeMultiplier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLifeMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.AttackSpeed"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAttackSpeed(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAttackSpeed(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.AttackSpeed"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAttackSpeed(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byAttackSpeed is null)
        {
            byAttackSpeed = new();
            foreach (var item in Items)
            {
                var itemKey = item.AttackSpeed;

                if (!byAttackSpeed.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAttackSpeed.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAttackSpeed.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byAttackSpeed"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByAttackSpeed(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAttackSpeed(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Weapon1_ItemVisualIdentityKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeapon1_ItemVisualIdentityKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeapon1_ItemVisualIdentityKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Weapon1_ItemVisualIdentityKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeapon1_ItemVisualIdentityKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byWeapon1_ItemVisualIdentityKeys is null)
        {
            byWeapon1_ItemVisualIdentityKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Weapon1_ItemVisualIdentityKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byWeapon1_ItemVisualIdentityKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWeapon1_ItemVisualIdentityKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWeapon1_ItemVisualIdentityKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byWeapon1_ItemVisualIdentityKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByWeapon1_ItemVisualIdentityKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeapon1_ItemVisualIdentityKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Weapon2_ItemVisualIdentityKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeapon2_ItemVisualIdentityKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeapon2_ItemVisualIdentityKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Weapon2_ItemVisualIdentityKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeapon2_ItemVisualIdentityKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byWeapon2_ItemVisualIdentityKeys is null)
        {
            byWeapon2_ItemVisualIdentityKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Weapon2_ItemVisualIdentityKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byWeapon2_ItemVisualIdentityKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWeapon2_ItemVisualIdentityKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWeapon2_ItemVisualIdentityKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byWeapon2_ItemVisualIdentityKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByWeapon2_ItemVisualIdentityKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeapon2_ItemVisualIdentityKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Back_ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBack_ItemVisualIdentityKey(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBack_ItemVisualIdentityKey(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Back_ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBack_ItemVisualIdentityKey(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byBack_ItemVisualIdentityKey is null)
        {
            byBack_ItemVisualIdentityKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Back_ItemVisualIdentityKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBack_ItemVisualIdentityKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBack_ItemVisualIdentityKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBack_ItemVisualIdentityKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byBack_ItemVisualIdentityKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByBack_ItemVisualIdentityKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBack_ItemVisualIdentityKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MainHand_ItemClassesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMainHand_ItemClassesKey(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMainHand_ItemClassesKey(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MainHand_ItemClassesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMainHand_ItemClassesKey(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byMainHand_ItemClassesKey is null)
        {
            byMainHand_ItemClassesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MainHand_ItemClassesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMainHand_ItemClassesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMainHand_ItemClassesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMainHand_ItemClassesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byMainHand_ItemClassesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByMainHand_ItemClassesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMainHand_ItemClassesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.OffHand_ItemClassesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOffHand_ItemClassesKey(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOffHand_ItemClassesKey(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.OffHand_ItemClassesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOffHand_ItemClassesKey(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byOffHand_ItemClassesKey is null)
        {
            byOffHand_ItemClassesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.OffHand_ItemClassesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byOffHand_ItemClassesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byOffHand_ItemClassesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byOffHand_ItemClassesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byOffHand_ItemClassesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByOffHand_ItemClassesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOffHand_ItemClassesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Helmet_ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHelmet_ItemVisualIdentityKey(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHelmet_ItemVisualIdentityKey(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Helmet_ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHelmet_ItemVisualIdentityKey(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byHelmet_ItemVisualIdentityKey is null)
        {
            byHelmet_ItemVisualIdentityKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Helmet_ItemVisualIdentityKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHelmet_ItemVisualIdentityKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHelmet_ItemVisualIdentityKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHelmet_ItemVisualIdentityKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byHelmet_ItemVisualIdentityKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByHelmet_ItemVisualIdentityKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHelmet_ItemVisualIdentityKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown376"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown376(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown376(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown376"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown376(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown376 is null)
        {
            byUnknown376 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown376;

                if (!byUnknown376.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown376.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown376.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown376"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown376(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown376(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.KillSpecificMonsterCount_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKillSpecificMonsterCount_AchievementItemsKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKillSpecificMonsterCount_AchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.KillSpecificMonsterCount_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKillSpecificMonsterCount_AchievementItemsKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byKillSpecificMonsterCount_AchievementItemsKeys is null)
        {
            byKillSpecificMonsterCount_AchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.KillSpecificMonsterCount_AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byKillSpecificMonsterCount_AchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byKillSpecificMonsterCount_AchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byKillSpecificMonsterCount_AchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byKillSpecificMonsterCount_AchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByKillSpecificMonsterCount_AchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKillSpecificMonsterCount_AchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Special_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpecial_ModsKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpecial_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Special_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpecial_ModsKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (bySpecial_ModsKeys is null)
        {
            bySpecial_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Special_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!bySpecial_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpecial_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpecial_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.bySpecial_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyBySpecial_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpecial_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.KillRare_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKillRare_AchievementItemsKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKillRare_AchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.KillRare_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKillRare_AchievementItemsKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byKillRare_AchievementItemsKeys is null)
        {
            byKillRare_AchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.KillRare_AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byKillRare_AchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byKillRare_AchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byKillRare_AchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byKillRare_AchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByKillRare_AchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKillRare_AchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown428"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown428(bool? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown428(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown428"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown428(bool? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown428 is null)
        {
            byUnknown428 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown428;

                if (!byUnknown428.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown428.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown428.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown428"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterVarietiesDat>> GetManyToManyByUnknown428(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown428(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown429"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown429(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown429(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown429"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown429(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown429 is null)
        {
            byUnknown429 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown429;

                if (!byUnknown429.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown429.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown429.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown429"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown429(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown429(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown433"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown433(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown433(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown433"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown433(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown433 is null)
        {
            byUnknown433 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown433;

                if (!byUnknown433.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown433.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown433.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown433"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown433(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown433(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown437"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown437(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown437(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown437"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown437(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown437 is null)
        {
            byUnknown437 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown437;

                if (!byUnknown437.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown437.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown437.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown437"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown437(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown437(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown441"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown441(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown441(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown441"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown441(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown441 is null)
        {
            byUnknown441 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown441;

                if (!byUnknown441.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown441.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown441.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown441"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown441(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown441(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown445"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown445(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown445(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown445"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown445(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown445 is null)
        {
            byUnknown445 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown445;

                if (!byUnknown445.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown445.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown445.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown445"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown445(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown445(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown449"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown449(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown449(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown449"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown449(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown449 is null)
        {
            byUnknown449 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown449;

                if (!byUnknown449.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown449.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown449.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown449"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown449(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown449(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown453"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown453(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown453(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown453"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown453(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown453 is null)
        {
            byUnknown453 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown453;

                if (!byUnknown453.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown453.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown453.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown453"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown453(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown453(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown457"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown457(bool? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown457(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown457"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown457(bool? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown457 is null)
        {
            byUnknown457 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown457;

                if (!byUnknown457.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown457.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown457.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown457"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterVarietiesDat>> GetManyToManyByUnknown457(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown457(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown458"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown458(string? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown458(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown458"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown458(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown458 is null)
        {
            byUnknown458 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown458;

                if (!byUnknown458.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown458.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown458.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown458"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyByUnknown458(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown458(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.KillWhileOnslaughtIsActive_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKillWhileOnslaughtIsActive_AchievementItemsKey(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKillWhileOnslaughtIsActive_AchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.KillWhileOnslaughtIsActive_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKillWhileOnslaughtIsActive_AchievementItemsKey(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byKillWhileOnslaughtIsActive_AchievementItemsKey is null)
        {
            byKillWhileOnslaughtIsActive_AchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.KillWhileOnslaughtIsActive_AchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byKillWhileOnslaughtIsActive_AchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byKillWhileOnslaughtIsActive_AchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byKillWhileOnslaughtIsActive_AchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byKillWhileOnslaughtIsActive_AchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByKillWhileOnslaughtIsActive_AchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKillWhileOnslaughtIsActive_AchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MonsterSegmentsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterSegmentsKey(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterSegmentsKey(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MonsterSegmentsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterSegmentsKey(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byMonsterSegmentsKey is null)
        {
            byMonsterSegmentsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterSegmentsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterSegmentsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterSegmentsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterSegmentsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byMonsterSegmentsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByMonsterSegmentsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterSegmentsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MonsterArmoursKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterArmoursKey(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterArmoursKey(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MonsterArmoursKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterArmoursKey(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byMonsterArmoursKey is null)
        {
            byMonsterArmoursKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterArmoursKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterArmoursKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterArmoursKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterArmoursKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byMonsterArmoursKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByMonsterArmoursKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterArmoursKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.KillWhileTalismanIsActive_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKillWhileTalismanIsActive_AchievementItemsKey(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKillWhileTalismanIsActive_AchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.KillWhileTalismanIsActive_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKillWhileTalismanIsActive_AchievementItemsKey(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byKillWhileTalismanIsActive_AchievementItemsKey is null)
        {
            byKillWhileTalismanIsActive_AchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.KillWhileTalismanIsActive_AchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byKillWhileTalismanIsActive_AchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byKillWhileTalismanIsActive_AchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byKillWhileTalismanIsActive_AchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byKillWhileTalismanIsActive_AchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByKillWhileTalismanIsActive_AchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKillWhileTalismanIsActive_AchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Part1_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPart1_ModsKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPart1_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Part1_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPart1_ModsKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byPart1_ModsKeys is null)
        {
            byPart1_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Part1_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byPart1_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPart1_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPart1_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byPart1_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByPart1_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPart1_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Part2_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPart2_ModsKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPart2_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Part2_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPart2_ModsKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byPart2_ModsKeys is null)
        {
            byPart2_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Part2_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byPart2_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPart2_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPart2_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byPart2_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByPart2_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPart2_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Endgame_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEndgame_ModsKeys(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEndgame_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Endgame_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEndgame_ModsKeys(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byEndgame_ModsKeys is null)
        {
            byEndgame_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Endgame_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byEndgame_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEndgame_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEndgame_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byEndgame_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByEndgame_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEndgame_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown578"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown578(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown578(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown578"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown578(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown578 is null)
        {
            byUnknown578 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown578;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown578.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown578.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown578.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown578"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown578(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown578(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown594"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown594(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown594(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown594"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown594(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown594 is null)
        {
            byUnknown594 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown594;

                if (!byUnknown594.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown594.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown594.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown594"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown594(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown594(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown598"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown598(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown598(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown598"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown598(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown598 is null)
        {
            byUnknown598 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown598;

                if (!byUnknown598.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown598.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown598.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown598"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown598(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown598(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown602"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown602(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown602(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown602"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown602(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown602 is null)
        {
            byUnknown602 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown602;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown602.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown602.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown602.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown602"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown602(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown602(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown618"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown618(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown618(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown618"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown618(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown618 is null)
        {
            byUnknown618 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown618;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown618.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown618.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown618.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown618"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown618(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown618(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown634"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown634(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown634(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown634"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown634(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown634 is null)
        {
            byUnknown634 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown634;

                if (!byUnknown634.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown634.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown634.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown634"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown634(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown634(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.SinkAnimation_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySinkAnimation_AOFile(string? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySinkAnimation_AOFile(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.SinkAnimation_AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySinkAnimation_AOFile(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (bySinkAnimation_AOFile is null)
        {
            bySinkAnimation_AOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.SinkAnimation_AOFile;

                if (!bySinkAnimation_AOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySinkAnimation_AOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySinkAnimation_AOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.bySinkAnimation_AOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyBySinkAnimation_AOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySinkAnimation_AOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown646"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown646(bool? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown646(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown646"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown646(bool? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown646 is null)
        {
            byUnknown646 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown646;

                if (!byUnknown646.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown646.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown646.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown646"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterVarietiesDat>> GetManyToManyByUnknown646(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown646(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown647"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown647(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown647(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown647"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown647(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown647 is null)
        {
            byUnknown647 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown647;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown647.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown647.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown647.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown647"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown647(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown647(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown663"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown663(bool? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown663(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown663"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown663(bool? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown663 is null)
        {
            byUnknown663 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown663;

                if (!byUnknown663.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown663.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown663.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown663"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterVarietiesDat>> GetManyToManyByUnknown663(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown663(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown664"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown664(bool? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown664(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown664"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown664(bool? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown664 is null)
        {
            byUnknown664 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown664;

                if (!byUnknown664.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown664.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown664.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown664"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterVarietiesDat>> GetManyToManyByUnknown664(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown664(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown665"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown665(bool? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown665(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown665"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown665(bool? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown665 is null)
        {
            byUnknown665 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown665;

                if (!byUnknown665.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown665.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown665.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown665"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterVarietiesDat>> GetManyToManyByUnknown665(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown665(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown666"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown666(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown666(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown666"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown666(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown666 is null)
        {
            byUnknown666 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown666;

                if (!byUnknown666.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown666.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown666.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown666"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown666(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown666(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown670"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown670(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown670(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown670"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown670(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown670 is null)
        {
            byUnknown670 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown670;

                if (!byUnknown670.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown670.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown670.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown670"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown670(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown670(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown674"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown674(float? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown674(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown674"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown674(float? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown674 is null)
        {
            byUnknown674 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown674;

                if (!byUnknown674.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown674.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown674.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown674"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, MonsterVarietiesDat>> GetManyToManyByUnknown674(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<float, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown674(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown678"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown678(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown678(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown678"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown678(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown678 is null)
        {
            byUnknown678 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown678;

                if (!byUnknown678.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown678.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown678.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown678"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown678(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown678(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEPKFile(string? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEPKFile(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEPKFile(string? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byEPKFile is null)
        {
            byEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.EPKFile;

                if (!byEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterVarietiesDat>> GetManyToManyByEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<string, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown690"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown690(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown690(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown690"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown690(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown690 is null)
        {
            byUnknown690 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown690;

                if (!byUnknown690.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown690.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown690.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown690"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown690(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown690(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MonsterConditionalEffectPacksKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterConditionalEffectPacksKey(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterConditionalEffectPacksKey(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.MonsterConditionalEffectPacksKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterConditionalEffectPacksKey(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byMonsterConditionalEffectPacksKey is null)
        {
            byMonsterConditionalEffectPacksKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterConditionalEffectPacksKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterConditionalEffectPacksKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterConditionalEffectPacksKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterConditionalEffectPacksKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byMonsterConditionalEffectPacksKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByMonsterConditionalEffectPacksKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterConditionalEffectPacksKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown710"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown710(bool? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown710(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown710"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown710(bool? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown710 is null)
        {
            byUnknown710 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown710;

                if (!byUnknown710.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown710.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown710.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown710"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterVarietiesDat>> GetManyToManyByUnknown710(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown710(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown711"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown711(bool? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown711(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown711"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown711(bool? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown711 is null)
        {
            byUnknown711 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown711;

                if (!byUnknown711.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown711.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown711.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown711"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterVarietiesDat>> GetManyToManyByUnknown711(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown711(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown712"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown712(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown712(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown712"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown712(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown712 is null)
        {
            byUnknown712 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown712;

                if (!byUnknown712.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown712.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown712.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown712"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown712(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown712(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown716"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown716(bool? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown716(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown716"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown716(bool? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown716 is null)
        {
            byUnknown716 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown716;

                if (!byUnknown716.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown716.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown716.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown716"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterVarietiesDat>> GetManyToManyByUnknown716(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown716(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown717"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown717(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown717(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown717"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown717(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown717 is null)
        {
            byUnknown717 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown717;

                if (!byUnknown717.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown717.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown717.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown717"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown717(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown717(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown721"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown721(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown721(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown721"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown721(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown721 is null)
        {
            byUnknown721 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown721;

                if (!byUnknown721.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown721.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown721.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown721"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown721(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown721(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown725"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown725(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown725(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown725"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown725(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown725 is null)
        {
            byUnknown725 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown725;

                if (!byUnknown725.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown725.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown725.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown725"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown725(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown725(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown729"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown729(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown729(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown729"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown729(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown729 is null)
        {
            byUnknown729 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown729;

                if (!byUnknown729.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown729.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown729.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown729"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown729(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown729(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown733"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown733(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown733(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown733"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown733(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown733 is null)
        {
            byUnknown733 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown733;

                if (!byUnknown733.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown733.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown733.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown733"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown733(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown733(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown737"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown737(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown737(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown737"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown737(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown737 is null)
        {
            byUnknown737 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown737;

                if (!byUnknown737.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown737.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown737.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown737"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown737(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown737(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown741"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown741(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown741(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown741"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown741(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown741 is null)
        {
            byUnknown741 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown741;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown741.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown741.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown741.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown741"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown741(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown741(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown757"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown757(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown757(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown757"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown757(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown757 is null)
        {
            byUnknown757 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown757;

                if (!byUnknown757.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown757.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown757.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown757"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown757(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown757(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown761"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown761(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown761(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown761"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown761(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown761 is null)
        {
            byUnknown761 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown761;

                if (!byUnknown761.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown761.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown761.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown761"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown761(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown761(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown765"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown765(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown765(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown765"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown765(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown765 is null)
        {
            byUnknown765 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown765;

                if (!byUnknown765.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown765.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown765.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown765"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown765(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown765(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown769"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown769(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown769(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown769"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown769(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown769 is null)
        {
            byUnknown769 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown769;

                if (!byUnknown769.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown769.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown769.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown769"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown769(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown769(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown773"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown773(int? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown773(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown773"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown773(int? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown773 is null)
        {
            byUnknown773 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown773;

                if (!byUnknown773.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown773.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown773.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown773"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterVarietiesDat>> GetManyToManyByUnknown773(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown773(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown777"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown777(bool? key, out MonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown777(key, out var items))
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
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.Unknown777"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown777(bool? key, out IReadOnlyList<MonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        if (byUnknown777 is null)
        {
            byUnknown777 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown777;

                if (!byUnknown777.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown777.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown777.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterVarietiesDat"/> with <see cref="MonsterVarietiesDat.byUnknown777"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterVarietiesDat>> GetManyToManyByUnknown777(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown777(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterVarietiesDat[] Load()
    {
        const string filePath = "Data/MonsterVarieties.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterVarietiesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterTypesKey
            (var monstertypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ObjectSize
            (var objectsizeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinimumAttackDistance
            (var minimumattackdistanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaximumAttackDistance
            (var maximumattackdistanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ACTFiles
            (var tempactfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var actfilesLoading = tempactfilesLoading.AsReadOnly();

            // loading AOFiles
            (var tempaofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofilesLoading = tempaofilesLoading.AsReadOnly();

            // loading BaseMonsterTypeIndex
            (var basemonstertypeindexLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModelSizeMultiplier
            (var modelsizemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown128
            (var unknown128Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown132
            (var unknown132Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TagsKeys
            (var temptagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagskeysLoading = temptagskeysLoading.AsReadOnly();

            // loading ExperienceMultiplier
            (var experiencemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown160
            (var tempunknown160Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown160Loading = tempunknown160Loading.AsReadOnly();

            // loading Unknown176
            (var unknown176Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown180
            (var unknown180Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown184
            (var unknown184Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CriticalStrikeChance
            (var criticalstrikechanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown192
            (var unknown192Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading GrantedEffectsKeys
            (var tempgrantedeffectskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var grantedeffectskeysLoading = tempgrantedeffectskeysLoading.AsReadOnly();

            // loading AISFile
            (var aisfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKeys2
            (var tempmodskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeys2Loading = tempmodskeys2Loading.AsReadOnly();

            // loading Stance
            (var stanceLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown244
            (var unknown244Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DamageMultiplier
            (var damagemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifeMultiplier
            (var lifemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AttackSpeed
            (var attackspeedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Weapon1_ItemVisualIdentityKeys
            (var tempweapon1_itemvisualidentitykeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var weapon1_itemvisualidentitykeysLoading = tempweapon1_itemvisualidentitykeysLoading.AsReadOnly();

            // loading Weapon2_ItemVisualIdentityKeys
            (var tempweapon2_itemvisualidentitykeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var weapon2_itemvisualidentitykeysLoading = tempweapon2_itemvisualidentitykeysLoading.AsReadOnly();

            // loading Back_ItemVisualIdentityKey
            (var back_itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MainHand_ItemClassesKey
            (var mainhand_itemclasseskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading OffHand_ItemClassesKey
            (var offhand_itemclasseskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Helmet_ItemVisualIdentityKey
            (var helmet_itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown376
            (var unknown376Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading KillSpecificMonsterCount_AchievementItemsKeys
            (var tempkillspecificmonstercount_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var killspecificmonstercount_achievementitemskeysLoading = tempkillspecificmonstercount_achievementitemskeysLoading.AsReadOnly();

            // loading Special_ModsKeys
            (var tempspecial_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var special_modskeysLoading = tempspecial_modskeysLoading.AsReadOnly();

            // loading KillRare_AchievementItemsKeys
            (var tempkillrare_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var killrare_achievementitemskeysLoading = tempkillrare_achievementitemskeysLoading.AsReadOnly();

            // loading Unknown428
            (var unknown428Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown429
            (var unknown429Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown433
            (var unknown433Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown437
            (var unknown437Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown441
            (var unknown441Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown445
            (var unknown445Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown449
            (var unknown449Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown453
            (var unknown453Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown457
            (var unknown457Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown458
            (var unknown458Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading KillWhileOnslaughtIsActive_AchievementItemsKey
            (var killwhileonslaughtisactive_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MonsterSegmentsKey
            (var monstersegmentskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MonsterArmoursKey
            (var monsterarmourskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading KillWhileTalismanIsActive_AchievementItemsKey
            (var killwhiletalismanisactive_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Part1_ModsKeys
            (var temppart1_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var part1_modskeysLoading = temppart1_modskeysLoading.AsReadOnly();

            // loading Part2_ModsKeys
            (var temppart2_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var part2_modskeysLoading = temppart2_modskeysLoading.AsReadOnly();

            // loading Endgame_ModsKeys
            (var tempendgame_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var endgame_modskeysLoading = tempendgame_modskeysLoading.AsReadOnly();

            // loading Unknown578
            (var unknown578Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown594
            (var unknown594Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown598
            (var unknown598Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown602
            (var tempunknown602Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown602Loading = tempunknown602Loading.AsReadOnly();

            // loading Unknown618
            (var tempunknown618Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown618Loading = tempunknown618Loading.AsReadOnly();

            // loading Unknown634
            (var unknown634Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SinkAnimation_AOFile
            (var sinkanimation_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown646
            (var unknown646Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown647
            (var tempunknown647Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown647Loading = tempunknown647Loading.AsReadOnly();

            // loading Unknown663
            (var unknown663Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown664
            (var unknown664Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown665
            (var unknown665Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown666
            (var unknown666Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown670
            (var unknown670Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown674
            (var unknown674Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown678
            (var unknown678Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EPKFile
            (var epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown690
            (var unknown690Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterConditionalEffectPacksKey
            (var monsterconditionaleffectpackskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown710
            (var unknown710Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown711
            (var unknown711Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown712
            (var unknown712Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown716
            (var unknown716Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown717
            (var unknown717Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown721
            (var unknown721Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown725
            (var unknown725Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown729
            (var unknown729Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown733
            (var unknown733Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown737
            (var unknown737Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown741
            (var tempunknown741Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown741Loading = tempunknown741Loading.AsReadOnly();

            // loading Unknown757
            (var unknown757Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown761
            (var unknown761Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown765
            (var unknown765Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown769
            (var unknown769Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown773
            (var unknown773Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown777
            (var unknown777Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterVarietiesDat()
            {
                Id = idLoading,
                MonsterTypesKey = monstertypeskeyLoading,
                Unknown24 = unknown24Loading,
                ObjectSize = objectsizeLoading,
                MinimumAttackDistance = minimumattackdistanceLoading,
                MaximumAttackDistance = maximumattackdistanceLoading,
                ACTFiles = actfilesLoading,
                AOFiles = aofilesLoading,
                BaseMonsterTypeIndex = basemonstertypeindexLoading,
                ModsKeys = modskeysLoading,
                Unknown96 = unknown96Loading,
                Unknown100 = unknown100Loading,
                Unknown108 = unknown108Loading,
                ModelSizeMultiplier = modelsizemultiplierLoading,
                Unknown120 = unknown120Loading,
                Unknown124 = unknown124Loading,
                Unknown128 = unknown128Loading,
                Unknown132 = unknown132Loading,
                Unknown136 = unknown136Loading,
                TagsKeys = tagskeysLoading,
                ExperienceMultiplier = experiencemultiplierLoading,
                Unknown160 = unknown160Loading,
                Unknown176 = unknown176Loading,
                Unknown180 = unknown180Loading,
                Unknown184 = unknown184Loading,
                CriticalStrikeChance = criticalstrikechanceLoading,
                Unknown192 = unknown192Loading,
                GrantedEffectsKeys = grantedeffectskeysLoading,
                AISFile = aisfileLoading,
                ModsKeys2 = modskeys2Loading,
                Stance = stanceLoading,
                Unknown244 = unknown244Loading,
                Name = nameLoading,
                DamageMultiplier = damagemultiplierLoading,
                LifeMultiplier = lifemultiplierLoading,
                AttackSpeed = attackspeedLoading,
                Weapon1_ItemVisualIdentityKeys = weapon1_itemvisualidentitykeysLoading,
                Weapon2_ItemVisualIdentityKeys = weapon2_itemvisualidentitykeysLoading,
                Back_ItemVisualIdentityKey = back_itemvisualidentitykeyLoading,
                MainHand_ItemClassesKey = mainhand_itemclasseskeyLoading,
                OffHand_ItemClassesKey = offhand_itemclasseskeyLoading,
                Helmet_ItemVisualIdentityKey = helmet_itemvisualidentitykeyLoading,
                Unknown376 = unknown376Loading,
                KillSpecificMonsterCount_AchievementItemsKeys = killspecificmonstercount_achievementitemskeysLoading,
                Special_ModsKeys = special_modskeysLoading,
                KillRare_AchievementItemsKeys = killrare_achievementitemskeysLoading,
                Unknown428 = unknown428Loading,
                Unknown429 = unknown429Loading,
                Unknown433 = unknown433Loading,
                Unknown437 = unknown437Loading,
                Unknown441 = unknown441Loading,
                Unknown445 = unknown445Loading,
                Unknown449 = unknown449Loading,
                Unknown453 = unknown453Loading,
                Unknown457 = unknown457Loading,
                Unknown458 = unknown458Loading,
                KillWhileOnslaughtIsActive_AchievementItemsKey = killwhileonslaughtisactive_achievementitemskeyLoading,
                MonsterSegmentsKey = monstersegmentskeyLoading,
                MonsterArmoursKey = monsterarmourskeyLoading,
                KillWhileTalismanIsActive_AchievementItemsKey = killwhiletalismanisactive_achievementitemskeyLoading,
                Part1_ModsKeys = part1_modskeysLoading,
                Part2_ModsKeys = part2_modskeysLoading,
                Endgame_ModsKeys = endgame_modskeysLoading,
                Unknown578 = unknown578Loading,
                Unknown594 = unknown594Loading,
                Unknown598 = unknown598Loading,
                Unknown602 = unknown602Loading,
                Unknown618 = unknown618Loading,
                Unknown634 = unknown634Loading,
                SinkAnimation_AOFile = sinkanimation_aofileLoading,
                Unknown646 = unknown646Loading,
                Unknown647 = unknown647Loading,
                Unknown663 = unknown663Loading,
                Unknown664 = unknown664Loading,
                Unknown665 = unknown665Loading,
                Unknown666 = unknown666Loading,
                Unknown670 = unknown670Loading,
                Unknown674 = unknown674Loading,
                Unknown678 = unknown678Loading,
                EPKFile = epkfileLoading,
                Unknown690 = unknown690Loading,
                MonsterConditionalEffectPacksKey = monsterconditionaleffectpackskeyLoading,
                Unknown710 = unknown710Loading,
                Unknown711 = unknown711Loading,
                Unknown712 = unknown712Loading,
                Unknown716 = unknown716Loading,
                Unknown717 = unknown717Loading,
                Unknown721 = unknown721Loading,
                Unknown725 = unknown725Loading,
                Unknown729 = unknown729Loading,
                Unknown733 = unknown733Loading,
                Unknown737 = unknown737Loading,
                Unknown741 = unknown741Loading,
                Unknown757 = unknown757Loading,
                Unknown761 = unknown761Loading,
                Unknown765 = unknown765Loading,
                Unknown769 = unknown769Loading,
                Unknown773 = unknown773Loading,
                Unknown777 = unknown777Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
