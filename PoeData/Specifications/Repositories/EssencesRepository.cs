using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="EssencesDat"/> related data and helper methods.
/// </summary>
public sealed class EssencesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<EssencesDat> Items { get; }

    private Dictionary<int, List<EssencesDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<EssencesDat>>? byUnknown16;
    private Dictionary<int, List<EssencesDat>>? byUnknown32;
    private Dictionary<int, List<EssencesDat>>? byUnknown48;
    private Dictionary<int, List<EssencesDat>>? byUnknown64;
    private Dictionary<int, List<EssencesDat>>? byUnknown80;
    private Dictionary<int, List<EssencesDat>>? byUnknown96;
    private Dictionary<int, List<EssencesDat>>? byUnknown112;
    private Dictionary<int, List<EssencesDat>>? byUnknown128;
    private Dictionary<int, List<EssencesDat>>? byUnknown144;
    private Dictionary<int, List<EssencesDat>>? byUnknown160;
    private Dictionary<int, List<EssencesDat>>? byUnknown176;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Wand_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Bow_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Quiver_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Amulet_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Ring_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Belt_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Gloves_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Boots_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_BodyArmour_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Helmet_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Shield_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byUnknown368;
    private Dictionary<int, List<EssencesDat>>? byDropLevelMinimum;
    private Dictionary<int, List<EssencesDat>>? byDropLevelMaximum;
    private Dictionary<int, List<EssencesDat>>? byMonster_ModsKeys;
    private Dictionary<int, List<EssencesDat>>? byEssenceTypeKey;
    private Dictionary<int, List<EssencesDat>>? byLevel;
    private Dictionary<int, List<EssencesDat>>? byUnknown416;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Weapon_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_MeleeWeapon_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_OneHandWeapon_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_TwoHandWeapon_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_TwoHandMeleeWeapon_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Armour_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_RangedWeapon_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byHelmet_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byBodyArmour_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byBoots_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byGloves_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byBow_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byWand_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byStaff_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byTwoHandSword_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byTwoHandAxe_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byTwoHandMace_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byClaw_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDagger_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byOneHandSword_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byOneHandThrustingSword_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byOneHandAxe_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byOneHandMace_ModsKey;
    private Dictionary<int, List<EssencesDat>>? bySceptre_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Monster_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byItemLevelRestriction;
    private Dictionary<int, List<EssencesDat>>? byBelt_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byAmulet_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byRing_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Jewellery_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byShield_ModsKey;
    private Dictionary<int, List<EssencesDat>>? byDisplay_Items_ModsKey;
    private Dictionary<bool, List<EssencesDat>>? byIsScreamingEssence;
    private Dictionary<int, List<EssencesDat>>? byUnknown921;
    private Dictionary<int, List<EssencesDat>>? byUnknown937;
    private Dictionary<int, List<EssencesDat>>? byUnknown941;
    private Dictionary<int, List<EssencesDat>>? byUnknown945;

    /// <summary>
    /// Initializes a new instance of the <see cref="EssencesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal EssencesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown16.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out EssencesDat? item)
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown32.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out EssencesDat? item)
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown48.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown64.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown80(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown80 is null)
        {
            byUnknown80 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown80;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown80.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown80.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown80.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown80(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown96(int? key, out EssencesDat? item)
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown96(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown96 is null)
        {
            byUnknown96 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown96;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown96.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown96.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown96.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown96"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown96(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown96(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown112(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown112(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown112(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown112 is null)
        {
            byUnknown112 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown112;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown112.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown112.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown112.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown112"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown112(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown112(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown128"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown128(int? key, out EssencesDat? item)
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown128"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown128(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown128 is null)
        {
            byUnknown128 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown128;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown128.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown128.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown128.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown128"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown128(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown128(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown144(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown144(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown144(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown144 is null)
        {
            byUnknown144 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown144;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown144.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown144.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown144.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown144"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown144(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown144(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown160(int? key, out EssencesDat? item)
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown160(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown160 is null)
        {
            byUnknown160 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown160;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown160.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown160.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown160.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown160"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown160(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown160(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown176(int? key, out EssencesDat? item)
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown176(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown176 is null)
        {
            byUnknown176 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown176;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown176.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown176.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown176.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown176"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown176(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown176(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Wand_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Wand_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Wand_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Wand_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Wand_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Wand_ModsKey is null)
        {
            byDisplay_Wand_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Wand_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Wand_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Wand_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Wand_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Wand_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Wand_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Wand_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Bow_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Bow_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Bow_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Bow_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Bow_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Bow_ModsKey is null)
        {
            byDisplay_Bow_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Bow_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Bow_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Bow_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Bow_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Bow_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Bow_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Bow_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Quiver_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Quiver_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Quiver_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Quiver_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Quiver_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Quiver_ModsKey is null)
        {
            byDisplay_Quiver_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Quiver_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Quiver_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Quiver_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Quiver_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Quiver_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Quiver_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Quiver_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Amulet_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Amulet_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Amulet_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Amulet_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Amulet_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Amulet_ModsKey is null)
        {
            byDisplay_Amulet_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Amulet_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Amulet_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Amulet_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Amulet_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Amulet_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Amulet_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Amulet_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Ring_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Ring_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Ring_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Ring_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Ring_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Ring_ModsKey is null)
        {
            byDisplay_Ring_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Ring_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Ring_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Ring_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Ring_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Ring_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Ring_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Ring_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Belt_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Belt_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Belt_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Belt_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Belt_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Belt_ModsKey is null)
        {
            byDisplay_Belt_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Belt_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Belt_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Belt_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Belt_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Belt_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Belt_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Belt_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Gloves_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Gloves_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Gloves_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Gloves_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Gloves_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Gloves_ModsKey is null)
        {
            byDisplay_Gloves_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Gloves_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Gloves_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Gloves_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Gloves_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Gloves_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Gloves_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Gloves_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Boots_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Boots_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Boots_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Boots_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Boots_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Boots_ModsKey is null)
        {
            byDisplay_Boots_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Boots_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Boots_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Boots_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Boots_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Boots_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Boots_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Boots_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_BodyArmour_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_BodyArmour_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_BodyArmour_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_BodyArmour_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_BodyArmour_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_BodyArmour_ModsKey is null)
        {
            byDisplay_BodyArmour_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_BodyArmour_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_BodyArmour_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_BodyArmour_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_BodyArmour_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_BodyArmour_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_BodyArmour_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_BodyArmour_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Helmet_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Helmet_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Helmet_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Helmet_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Helmet_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Helmet_ModsKey is null)
        {
            byDisplay_Helmet_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Helmet_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Helmet_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Helmet_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Helmet_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Helmet_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Helmet_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Helmet_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Shield_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Shield_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Shield_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Shield_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Shield_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Shield_ModsKey is null)
        {
            byDisplay_Shield_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Shield_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Shield_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Shield_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Shield_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Shield_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Shield_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Shield_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown368"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown368(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown368(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown368"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown368(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown368 is null)
        {
            byUnknown368 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown368;

                if (!byUnknown368.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown368.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown368.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown368"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown368(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown368(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.DropLevelMinimum"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDropLevelMinimum(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDropLevelMinimum(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.DropLevelMinimum"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDropLevelMinimum(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDropLevelMinimum is null)
        {
            byDropLevelMinimum = new();
            foreach (var item in Items)
            {
                var itemKey = item.DropLevelMinimum;

                if (!byDropLevelMinimum.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDropLevelMinimum.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDropLevelMinimum.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDropLevelMinimum"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDropLevelMinimum(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDropLevelMinimum(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.DropLevelMaximum"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDropLevelMaximum(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDropLevelMaximum(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.DropLevelMaximum"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDropLevelMaximum(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDropLevelMaximum is null)
        {
            byDropLevelMaximum = new();
            foreach (var item in Items)
            {
                var itemKey = item.DropLevelMaximum;

                if (!byDropLevelMaximum.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDropLevelMaximum.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDropLevelMaximum.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDropLevelMaximum"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDropLevelMaximum(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDropLevelMaximum(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Monster_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonster_ModsKeys(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonster_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Monster_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonster_ModsKeys(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byMonster_ModsKeys is null)
        {
            byMonster_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Monster_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMonster_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonster_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonster_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byMonster_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByMonster_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonster_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.EssenceTypeKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEssenceTypeKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEssenceTypeKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.EssenceTypeKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEssenceTypeKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byEssenceTypeKey is null)
        {
            byEssenceTypeKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.EssenceTypeKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEssenceTypeKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEssenceTypeKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEssenceTypeKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byEssenceTypeKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByEssenceTypeKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEssenceTypeKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevel(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byLevel is null)
        {
            byLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.Level;

                if (!byLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown416"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown416(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown416(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown416"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown416(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown416 is null)
        {
            byUnknown416 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown416;

                if (!byUnknown416.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown416.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown416.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown416"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown416(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown416(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Weapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Weapon_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Weapon_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Weapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Weapon_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Weapon_ModsKey is null)
        {
            byDisplay_Weapon_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Weapon_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Weapon_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Weapon_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Weapon_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Weapon_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Weapon_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Weapon_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_MeleeWeapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_MeleeWeapon_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_MeleeWeapon_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_MeleeWeapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_MeleeWeapon_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_MeleeWeapon_ModsKey is null)
        {
            byDisplay_MeleeWeapon_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_MeleeWeapon_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_MeleeWeapon_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_MeleeWeapon_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_MeleeWeapon_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_MeleeWeapon_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_MeleeWeapon_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_MeleeWeapon_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_OneHandWeapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_OneHandWeapon_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_OneHandWeapon_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_OneHandWeapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_OneHandWeapon_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_OneHandWeapon_ModsKey is null)
        {
            byDisplay_OneHandWeapon_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_OneHandWeapon_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_OneHandWeapon_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_OneHandWeapon_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_OneHandWeapon_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_OneHandWeapon_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_OneHandWeapon_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_OneHandWeapon_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_TwoHandWeapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_TwoHandWeapon_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_TwoHandWeapon_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_TwoHandWeapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_TwoHandWeapon_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_TwoHandWeapon_ModsKey is null)
        {
            byDisplay_TwoHandWeapon_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_TwoHandWeapon_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_TwoHandWeapon_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_TwoHandWeapon_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_TwoHandWeapon_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_TwoHandWeapon_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_TwoHandWeapon_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_TwoHandWeapon_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_TwoHandMeleeWeapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_TwoHandMeleeWeapon_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_TwoHandMeleeWeapon_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_TwoHandMeleeWeapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_TwoHandMeleeWeapon_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_TwoHandMeleeWeapon_ModsKey is null)
        {
            byDisplay_TwoHandMeleeWeapon_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_TwoHandMeleeWeapon_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_TwoHandMeleeWeapon_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_TwoHandMeleeWeapon_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_TwoHandMeleeWeapon_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_TwoHandMeleeWeapon_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_TwoHandMeleeWeapon_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_TwoHandMeleeWeapon_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Armour_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Armour_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Armour_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Armour_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Armour_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Armour_ModsKey is null)
        {
            byDisplay_Armour_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Armour_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Armour_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Armour_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Armour_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Armour_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Armour_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Armour_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_RangedWeapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_RangedWeapon_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_RangedWeapon_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_RangedWeapon_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_RangedWeapon_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_RangedWeapon_ModsKey is null)
        {
            byDisplay_RangedWeapon_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_RangedWeapon_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_RangedWeapon_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_RangedWeapon_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_RangedWeapon_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_RangedWeapon_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_RangedWeapon_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_RangedWeapon_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Helmet_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHelmet_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHelmet_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Helmet_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHelmet_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byHelmet_ModsKey is null)
        {
            byHelmet_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Helmet_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHelmet_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHelmet_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHelmet_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byHelmet_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByHelmet_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHelmet_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.BodyArmour_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBodyArmour_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBodyArmour_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.BodyArmour_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBodyArmour_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byBodyArmour_ModsKey is null)
        {
            byBodyArmour_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BodyArmour_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBodyArmour_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBodyArmour_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBodyArmour_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byBodyArmour_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByBodyArmour_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBodyArmour_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Boots_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBoots_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBoots_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Boots_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBoots_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byBoots_ModsKey is null)
        {
            byBoots_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Boots_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBoots_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBoots_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBoots_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byBoots_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByBoots_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBoots_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Gloves_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGloves_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGloves_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Gloves_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGloves_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byGloves_ModsKey is null)
        {
            byGloves_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Gloves_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGloves_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGloves_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGloves_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byGloves_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByGloves_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGloves_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Bow_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBow_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBow_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Bow_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBow_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byBow_ModsKey is null)
        {
            byBow_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Bow_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBow_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBow_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBow_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byBow_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByBow_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBow_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Wand_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWand_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWand_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Wand_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWand_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byWand_ModsKey is null)
        {
            byWand_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Wand_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWand_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWand_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWand_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byWand_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByWand_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWand_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Staff_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStaff_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStaff_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Staff_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStaff_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byStaff_ModsKey is null)
        {
            byStaff_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Staff_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStaff_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStaff_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStaff_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byStaff_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByStaff_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStaff_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.TwoHandSword_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTwoHandSword_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTwoHandSword_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.TwoHandSword_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTwoHandSword_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byTwoHandSword_ModsKey is null)
        {
            byTwoHandSword_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.TwoHandSword_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTwoHandSword_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTwoHandSword_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTwoHandSword_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byTwoHandSword_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByTwoHandSword_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTwoHandSword_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.TwoHandAxe_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTwoHandAxe_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTwoHandAxe_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.TwoHandAxe_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTwoHandAxe_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byTwoHandAxe_ModsKey is null)
        {
            byTwoHandAxe_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.TwoHandAxe_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTwoHandAxe_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTwoHandAxe_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTwoHandAxe_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byTwoHandAxe_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByTwoHandAxe_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTwoHandAxe_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.TwoHandMace_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTwoHandMace_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTwoHandMace_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.TwoHandMace_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTwoHandMace_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byTwoHandMace_ModsKey is null)
        {
            byTwoHandMace_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.TwoHandMace_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTwoHandMace_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTwoHandMace_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTwoHandMace_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byTwoHandMace_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByTwoHandMace_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTwoHandMace_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Claw_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClaw_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClaw_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Claw_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClaw_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byClaw_ModsKey is null)
        {
            byClaw_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Claw_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byClaw_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byClaw_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byClaw_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byClaw_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByClaw_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClaw_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Dagger_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDagger_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDagger_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Dagger_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDagger_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDagger_ModsKey is null)
        {
            byDagger_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Dagger_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDagger_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDagger_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDagger_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDagger_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDagger_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDagger_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.OneHandSword_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOneHandSword_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOneHandSword_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.OneHandSword_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOneHandSword_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byOneHandSword_ModsKey is null)
        {
            byOneHandSword_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.OneHandSword_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byOneHandSword_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byOneHandSword_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byOneHandSword_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byOneHandSword_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByOneHandSword_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOneHandSword_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.OneHandThrustingSword_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOneHandThrustingSword_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOneHandThrustingSword_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.OneHandThrustingSword_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOneHandThrustingSword_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byOneHandThrustingSword_ModsKey is null)
        {
            byOneHandThrustingSword_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.OneHandThrustingSword_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byOneHandThrustingSword_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byOneHandThrustingSword_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byOneHandThrustingSword_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byOneHandThrustingSword_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByOneHandThrustingSword_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOneHandThrustingSword_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.OneHandAxe_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOneHandAxe_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOneHandAxe_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.OneHandAxe_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOneHandAxe_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byOneHandAxe_ModsKey is null)
        {
            byOneHandAxe_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.OneHandAxe_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byOneHandAxe_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byOneHandAxe_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byOneHandAxe_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byOneHandAxe_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByOneHandAxe_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOneHandAxe_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.OneHandMace_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOneHandMace_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOneHandMace_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.OneHandMace_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOneHandMace_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byOneHandMace_ModsKey is null)
        {
            byOneHandMace_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.OneHandMace_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byOneHandMace_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byOneHandMace_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byOneHandMace_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byOneHandMace_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByOneHandMace_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOneHandMace_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Sceptre_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySceptre_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySceptre_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Sceptre_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySceptre_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (bySceptre_ModsKey is null)
        {
            bySceptre_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Sceptre_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySceptre_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySceptre_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySceptre_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.bySceptre_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyBySceptre_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySceptre_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Monster_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Monster_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Monster_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Monster_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Monster_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Monster_ModsKey is null)
        {
            byDisplay_Monster_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Monster_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Monster_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Monster_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Monster_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Monster_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Monster_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Monster_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.ItemLevelRestriction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemLevelRestriction(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemLevelRestriction(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.ItemLevelRestriction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemLevelRestriction(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byItemLevelRestriction is null)
        {
            byItemLevelRestriction = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemLevelRestriction;

                if (!byItemLevelRestriction.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byItemLevelRestriction.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byItemLevelRestriction.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byItemLevelRestriction"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByItemLevelRestriction(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemLevelRestriction(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Belt_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBelt_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBelt_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Belt_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBelt_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byBelt_ModsKey is null)
        {
            byBelt_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Belt_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBelt_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBelt_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBelt_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byBelt_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByBelt_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBelt_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Amulet_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAmulet_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAmulet_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Amulet_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAmulet_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byAmulet_ModsKey is null)
        {
            byAmulet_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Amulet_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAmulet_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAmulet_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAmulet_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byAmulet_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByAmulet_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAmulet_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Ring_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRing_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRing_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Ring_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRing_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byRing_ModsKey is null)
        {
            byRing_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Ring_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRing_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRing_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRing_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byRing_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByRing_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRing_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Jewellery_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Jewellery_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Jewellery_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Jewellery_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Jewellery_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Jewellery_ModsKey is null)
        {
            byDisplay_Jewellery_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Jewellery_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Jewellery_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Jewellery_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Jewellery_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Jewellery_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Jewellery_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Jewellery_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Shield_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShield_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShield_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Shield_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShield_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byShield_ModsKey is null)
        {
            byShield_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Shield_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byShield_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byShield_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byShield_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byShield_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByShield_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShield_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Items_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplay_Items_ModsKey(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplay_Items_ModsKey(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Display_Items_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplay_Items_ModsKey(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byDisplay_Items_ModsKey is null)
        {
            byDisplay_Items_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Display_Items_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplay_Items_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplay_Items_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplay_Items_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byDisplay_Items_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByDisplay_Items_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplay_Items_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.IsScreamingEssence"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsScreamingEssence(bool? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsScreamingEssence(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.IsScreamingEssence"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsScreamingEssence(bool? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byIsScreamingEssence is null)
        {
            byIsScreamingEssence = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsScreamingEssence;

                if (!byIsScreamingEssence.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsScreamingEssence.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsScreamingEssence.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byIsScreamingEssence"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EssencesDat>> GetManyToManyByIsScreamingEssence(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EssencesDat>>();
        }

        var items = new List<ResultItem<bool, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsScreamingEssence(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown921"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown921(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown921(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown921"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown921(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown921 is null)
        {
            byUnknown921 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown921;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown921.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown921.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown921.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown921"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown921(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown921(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown937"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown937(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown937(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown937"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown937(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown937 is null)
        {
            byUnknown937 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown937;

                if (!byUnknown937.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown937.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown937.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown937"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown937(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown937(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown941"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown941(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown941(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown941"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown941(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown941 is null)
        {
            byUnknown941 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown941;

                if (!byUnknown941.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown941.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown941.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown941"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown941(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown941(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown945"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown945(int? key, out EssencesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown945(key, out var items))
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
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.Unknown945"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown945(int? key, out IReadOnlyList<EssencesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        if (byUnknown945 is null)
        {
            byUnknown945 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown945;

                if (!byUnknown945.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown945.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown945.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssencesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssencesDat"/> with <see cref="EssencesDat.byUnknown945"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssencesDat>> GetManyToManyByUnknown945(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssencesDat>>();
        }

        var items = new List<ResultItem<int, EssencesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown945(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssencesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private EssencesDat[] Load()
    {
        const string filePath = "Data/Essences.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EssencesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown128
            (var unknown128Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown160
            (var unknown160Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown176
            (var unknown176Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Wand_ModsKey
            (var display_wand_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Bow_ModsKey
            (var display_bow_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Quiver_ModsKey
            (var display_quiver_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Amulet_ModsKey
            (var display_amulet_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Ring_ModsKey
            (var display_ring_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Belt_ModsKey
            (var display_belt_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Gloves_ModsKey
            (var display_gloves_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Boots_ModsKey
            (var display_boots_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_BodyArmour_ModsKey
            (var display_bodyarmour_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Helmet_ModsKey
            (var display_helmet_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Shield_ModsKey
            (var display_shield_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown368
            (var unknown368Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DropLevelMinimum
            (var droplevelminimumLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DropLevelMaximum
            (var droplevelmaximumLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Monster_ModsKeys
            (var tempmonster_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monster_modskeysLoading = tempmonster_modskeysLoading.AsReadOnly();

            // loading EssenceTypeKey
            (var essencetypekeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown416
            (var unknown416Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Display_Weapon_ModsKey
            (var display_weapon_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_MeleeWeapon_ModsKey
            (var display_meleeweapon_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_OneHandWeapon_ModsKey
            (var display_onehandweapon_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_TwoHandWeapon_ModsKey
            (var display_twohandweapon_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_TwoHandMeleeWeapon_ModsKey
            (var display_twohandmeleeweapon_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Armour_ModsKey
            (var display_armour_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_RangedWeapon_ModsKey
            (var display_rangedweapon_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Helmet_ModsKey
            (var helmet_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BodyArmour_ModsKey
            (var bodyarmour_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Boots_ModsKey
            (var boots_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Gloves_ModsKey
            (var gloves_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Bow_ModsKey
            (var bow_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Wand_ModsKey
            (var wand_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Staff_ModsKey
            (var staff_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TwoHandSword_ModsKey
            (var twohandsword_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TwoHandAxe_ModsKey
            (var twohandaxe_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TwoHandMace_ModsKey
            (var twohandmace_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Claw_ModsKey
            (var claw_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Dagger_ModsKey
            (var dagger_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading OneHandSword_ModsKey
            (var onehandsword_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading OneHandThrustingSword_ModsKey
            (var onehandthrustingsword_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading OneHandAxe_ModsKey
            (var onehandaxe_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading OneHandMace_ModsKey
            (var onehandmace_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Sceptre_ModsKey
            (var sceptre_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Monster_ModsKey
            (var display_monster_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemLevelRestriction
            (var itemlevelrestrictionLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Belt_ModsKey
            (var belt_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Amulet_ModsKey
            (var amulet_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Ring_ModsKey
            (var ring_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Jewellery_ModsKey
            (var display_jewellery_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Shield_ModsKey
            (var shield_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Display_Items_ModsKey
            (var display_items_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsScreamingEssence
            (var isscreamingessenceLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown921
            (var tempunknown921Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown921Loading = tempunknown921Loading.AsReadOnly();

            // loading Unknown937
            (var unknown937Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown941
            (var unknown941Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown945
            (var unknown945Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EssencesDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Unknown80 = unknown80Loading,
                Unknown96 = unknown96Loading,
                Unknown112 = unknown112Loading,
                Unknown128 = unknown128Loading,
                Unknown144 = unknown144Loading,
                Unknown160 = unknown160Loading,
                Unknown176 = unknown176Loading,
                Display_Wand_ModsKey = display_wand_modskeyLoading,
                Display_Bow_ModsKey = display_bow_modskeyLoading,
                Display_Quiver_ModsKey = display_quiver_modskeyLoading,
                Display_Amulet_ModsKey = display_amulet_modskeyLoading,
                Display_Ring_ModsKey = display_ring_modskeyLoading,
                Display_Belt_ModsKey = display_belt_modskeyLoading,
                Display_Gloves_ModsKey = display_gloves_modskeyLoading,
                Display_Boots_ModsKey = display_boots_modskeyLoading,
                Display_BodyArmour_ModsKey = display_bodyarmour_modskeyLoading,
                Display_Helmet_ModsKey = display_helmet_modskeyLoading,
                Display_Shield_ModsKey = display_shield_modskeyLoading,
                Unknown368 = unknown368Loading,
                DropLevelMinimum = droplevelminimumLoading,
                DropLevelMaximum = droplevelmaximumLoading,
                Monster_ModsKeys = monster_modskeysLoading,
                EssenceTypeKey = essencetypekeyLoading,
                Level = levelLoading,
                Unknown416 = unknown416Loading,
                Display_Weapon_ModsKey = display_weapon_modskeyLoading,
                Display_MeleeWeapon_ModsKey = display_meleeweapon_modskeyLoading,
                Display_OneHandWeapon_ModsKey = display_onehandweapon_modskeyLoading,
                Display_TwoHandWeapon_ModsKey = display_twohandweapon_modskeyLoading,
                Display_TwoHandMeleeWeapon_ModsKey = display_twohandmeleeweapon_modskeyLoading,
                Display_Armour_ModsKey = display_armour_modskeyLoading,
                Display_RangedWeapon_ModsKey = display_rangedweapon_modskeyLoading,
                Helmet_ModsKey = helmet_modskeyLoading,
                BodyArmour_ModsKey = bodyarmour_modskeyLoading,
                Boots_ModsKey = boots_modskeyLoading,
                Gloves_ModsKey = gloves_modskeyLoading,
                Bow_ModsKey = bow_modskeyLoading,
                Wand_ModsKey = wand_modskeyLoading,
                Staff_ModsKey = staff_modskeyLoading,
                TwoHandSword_ModsKey = twohandsword_modskeyLoading,
                TwoHandAxe_ModsKey = twohandaxe_modskeyLoading,
                TwoHandMace_ModsKey = twohandmace_modskeyLoading,
                Claw_ModsKey = claw_modskeyLoading,
                Dagger_ModsKey = dagger_modskeyLoading,
                OneHandSword_ModsKey = onehandsword_modskeyLoading,
                OneHandThrustingSword_ModsKey = onehandthrustingsword_modskeyLoading,
                OneHandAxe_ModsKey = onehandaxe_modskeyLoading,
                OneHandMace_ModsKey = onehandmace_modskeyLoading,
                Sceptre_ModsKey = sceptre_modskeyLoading,
                Display_Monster_ModsKey = display_monster_modskeyLoading,
                ItemLevelRestriction = itemlevelrestrictionLoading,
                Belt_ModsKey = belt_modskeyLoading,
                Amulet_ModsKey = amulet_modskeyLoading,
                Ring_ModsKey = ring_modskeyLoading,
                Display_Jewellery_ModsKey = display_jewellery_modskeyLoading,
                Shield_ModsKey = shield_modskeyLoading,
                Display_Items_ModsKey = display_items_modskeyLoading,
                IsScreamingEssence = isscreamingessenceLoading,
                Unknown921 = unknown921Loading,
                Unknown937 = unknown937Loading,
                Unknown941 = unknown941Loading,
                Unknown945 = unknown945Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
