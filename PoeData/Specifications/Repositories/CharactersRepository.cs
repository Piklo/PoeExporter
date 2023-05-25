using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CharactersDat"/> related data and helper methods.
/// </summary>
public sealed class CharactersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CharactersDat> Items { get; }

    private Dictionary<string, List<CharactersDat>>? byId;
    private Dictionary<string, List<CharactersDat>>? byName;
    private Dictionary<string, List<CharactersDat>>? byAOFile;
    private Dictionary<string, List<CharactersDat>>? byACTFile;
    private Dictionary<int, List<CharactersDat>>? byBaseMaxLife;
    private Dictionary<int, List<CharactersDat>>? byBaseMaxMana;
    private Dictionary<int, List<CharactersDat>>? byWeaponSpeed;
    private Dictionary<int, List<CharactersDat>>? byMinDamage;
    private Dictionary<int, List<CharactersDat>>? byMaxDamage;
    private Dictionary<int, List<CharactersDat>>? byMaxAttackDistance;
    private Dictionary<string, List<CharactersDat>>? byIcon;
    private Dictionary<int, List<CharactersDat>>? byIntegerId;
    private Dictionary<int, List<CharactersDat>>? byBaseStrength;
    private Dictionary<int, List<CharactersDat>>? byBaseDexterity;
    private Dictionary<int, List<CharactersDat>>? byBaseIntelligence;
    private Dictionary<int, List<CharactersDat>>? byUnknown80;
    private Dictionary<string, List<CharactersDat>>? byDescription;
    private Dictionary<int, List<CharactersDat>>? byStartSkillGem;
    private Dictionary<int, List<CharactersDat>>? byUnknown120;
    private Dictionary<int, List<CharactersDat>>? byUnknown136;
    private Dictionary<int, List<CharactersDat>>? byUnknown140;
    private Dictionary<int, List<CharactersDat>>? byCharacterSize;
    private Dictionary<string, List<CharactersDat>>? byIntroSoundFile;
    private Dictionary<int, List<CharactersDat>>? byStartWeapons;
    private Dictionary<string, List<CharactersDat>>? byGender;
    private Dictionary<string, List<CharactersDat>>? byTraitDescription;
    private Dictionary<int, List<CharactersDat>>? byUnknown188;
    private Dictionary<int, List<CharactersDat>>? byUnknown204;
    private Dictionary<int, List<CharactersDat>>? byUnknown220;
    private Dictionary<int, List<CharactersDat>>? byUnknown236;
    private Dictionary<int, List<CharactersDat>>? byUnknown252;
    private Dictionary<int, List<CharactersDat>>? byUnknown256;
    private Dictionary<string, List<CharactersDat>>? byPassiveTreeImage;
    private Dictionary<int, List<CharactersDat>>? byUnknown280;
    private Dictionary<int, List<CharactersDat>>? byUnknown284;
    private Dictionary<string, List<CharactersDat>>? byTencentVideo;
    private Dictionary<string, List<CharactersDat>>? byAttrsAsId;
    private Dictionary<string, List<CharactersDat>>? byLoginScreen;
    private Dictionary<string, List<CharactersDat>>? byPlayerCritter;
    private Dictionary<string, List<CharactersDat>>? byPlayerEffect;
    private Dictionary<string, List<CharactersDat>>? byAfterImage;
    private Dictionary<int, List<CharactersDat>>? byMirage;
    private Dictionary<int, List<CharactersDat>>? byCloneImmobile;
    private Dictionary<int, List<CharactersDat>>? byReplicateClone;
    private Dictionary<int, List<CharactersDat>>? byLightningClone;
    private Dictionary<float, List<CharactersDat>>? byUnknown400;
    private Dictionary<float, List<CharactersDat>>? byUnknown404;
    private Dictionary<string, List<CharactersDat>>? bySkillTreeBackground;
    private Dictionary<int, List<CharactersDat>>? byClone;
    private Dictionary<int, List<CharactersDat>>? byDouble;
    private Dictionary<int, List<CharactersDat>>? byMirageWarrior;
    private Dictionary<int, List<CharactersDat>>? byDoubleTwo;
    private Dictionary<int, List<CharactersDat>>? byDarkExile;
    private Dictionary<string, List<CharactersDat>>? byAttr;
    private Dictionary<string, List<CharactersDat>>? byAttrLowercase;
    private Dictionary<string, List<CharactersDat>>? byScript;
    private Dictionary<int, List<CharactersDat>>? byUnknown520;
    private Dictionary<int, List<CharactersDat>>? byUnknown536;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharactersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CharactersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CharactersDat? item)
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
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
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out CharactersDat? item)
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
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
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFile(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFile(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFile(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byAOFile is null)
        {
            byAOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFile;

                if (!byAOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byAOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByAOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.ACTFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByACTFile(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByACTFile(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.ACTFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByACTFile(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byACTFile is null)
        {
            byACTFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.ACTFile;

                if (!byACTFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byACTFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byACTFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byACTFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByACTFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByACTFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.BaseMaxLife"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseMaxLife(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseMaxLife(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.BaseMaxLife"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseMaxLife(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byBaseMaxLife is null)
        {
            byBaseMaxLife = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseMaxLife;

                if (!byBaseMaxLife.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseMaxLife.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseMaxLife.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byBaseMaxLife"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByBaseMaxLife(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseMaxLife(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.BaseMaxMana"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseMaxMana(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseMaxMana(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.BaseMaxMana"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseMaxMana(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byBaseMaxMana is null)
        {
            byBaseMaxMana = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseMaxMana;

                if (!byBaseMaxMana.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseMaxMana.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseMaxMana.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byBaseMaxMana"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByBaseMaxMana(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseMaxMana(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.WeaponSpeed"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeaponSpeed(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeaponSpeed(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.WeaponSpeed"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeaponSpeed(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byWeaponSpeed is null)
        {
            byWeaponSpeed = new();
            foreach (var item in Items)
            {
                var itemKey = item.WeaponSpeed;

                if (!byWeaponSpeed.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWeaponSpeed.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWeaponSpeed.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byWeaponSpeed"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByWeaponSpeed(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeaponSpeed(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.MinDamage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinDamage(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinDamage(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.MinDamage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinDamage(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byMinDamage is null)
        {
            byMinDamage = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinDamage;

                if (!byMinDamage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinDamage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinDamage.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byMinDamage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByMinDamage(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinDamage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.MaxDamage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxDamage(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxDamage(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.MaxDamage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxDamage(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byMaxDamage is null)
        {
            byMaxDamage = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxDamage;

                if (!byMaxDamage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxDamage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxDamage.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byMaxDamage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByMaxDamage(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxDamage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.MaxAttackDistance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxAttackDistance(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxAttackDistance(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.MaxAttackDistance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxAttackDistance(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byMaxAttackDistance is null)
        {
            byMaxAttackDistance = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxAttackDistance;

                if (!byMaxAttackDistance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxAttackDistance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxAttackDistance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byMaxAttackDistance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByMaxAttackDistance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxAttackDistance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIcon(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byIcon is null)
        {
            byIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.Icon;

                if (!byIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.IntegerId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIntegerId(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIntegerId(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.IntegerId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIntegerId(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byIntegerId is null)
        {
            byIntegerId = new();
            foreach (var item in Items)
            {
                var itemKey = item.IntegerId;

                if (!byIntegerId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIntegerId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIntegerId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byIntegerId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByIntegerId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIntegerId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.BaseStrength"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseStrength(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseStrength(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.BaseStrength"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseStrength(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byBaseStrength is null)
        {
            byBaseStrength = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseStrength;

                if (!byBaseStrength.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseStrength.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseStrength.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byBaseStrength"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByBaseStrength(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseStrength(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.BaseDexterity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseDexterity(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseDexterity(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.BaseDexterity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseDexterity(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byBaseDexterity is null)
        {
            byBaseDexterity = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseDexterity;

                if (!byBaseDexterity.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseDexterity.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseDexterity.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byBaseDexterity"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByBaseDexterity(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseDexterity(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.BaseIntelligence"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseIntelligence(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseIntelligence(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.BaseIntelligence"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseIntelligence(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byBaseIntelligence is null)
        {
            byBaseIntelligence = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseIntelligence;

                if (!byBaseIntelligence.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseIntelligence.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseIntelligence.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byBaseIntelligence"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByBaseIntelligence(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseIntelligence(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(int? key, out CharactersDat? item)
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown80 is null)
        {
            byUnknown80 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown80;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown80.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown80.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown80.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown80(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out CharactersDat? item)
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
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
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.StartSkillGem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStartSkillGem(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStartSkillGem(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.StartSkillGem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStartSkillGem(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byStartSkillGem is null)
        {
            byStartSkillGem = new();
            foreach (var item in Items)
            {
                var itemKey = item.StartSkillGem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStartSkillGem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStartSkillGem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStartSkillGem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byStartSkillGem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByStartSkillGem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStartSkillGem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown120(int? key, out CharactersDat? item)
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown120(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown120 is null)
        {
            byUnknown120 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown120;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown120.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown120.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown120.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown120"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown120(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown120(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown136"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown136(int? key, out CharactersDat? item)
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown136"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown136(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
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
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown136"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown136(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown136(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown140"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown140(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown140(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown140"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown140(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown140 is null)
        {
            byUnknown140 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown140;

                if (!byUnknown140.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown140.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown140.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown140"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown140(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown140(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.CharacterSize"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharacterSize(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharacterSize(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.CharacterSize"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharacterSize(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byCharacterSize is null)
        {
            byCharacterSize = new();
            foreach (var item in Items)
            {
                var itemKey = item.CharacterSize;

                if (!byCharacterSize.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCharacterSize.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCharacterSize.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byCharacterSize"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByCharacterSize(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharacterSize(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.IntroSoundFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIntroSoundFile(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIntroSoundFile(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.IntroSoundFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIntroSoundFile(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byIntroSoundFile is null)
        {
            byIntroSoundFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.IntroSoundFile;

                if (!byIntroSoundFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIntroSoundFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIntroSoundFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byIntroSoundFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByIntroSoundFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIntroSoundFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.StartWeapons"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStartWeapons(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStartWeapons(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.StartWeapons"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStartWeapons(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byStartWeapons is null)
        {
            byStartWeapons = new();
            foreach (var item in Items)
            {
                var itemKey = item.StartWeapons;
                foreach (var listKey in itemKey)
                {
                    if (!byStartWeapons.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStartWeapons.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStartWeapons.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byStartWeapons"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByStartWeapons(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStartWeapons(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Gender"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGender(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGender(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Gender"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGender(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byGender is null)
        {
            byGender = new();
            foreach (var item in Items)
            {
                var itemKey = item.Gender;

                if (!byGender.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGender.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGender.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byGender"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByGender(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGender(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.TraitDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTraitDescription(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTraitDescription(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.TraitDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTraitDescription(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byTraitDescription is null)
        {
            byTraitDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.TraitDescription;

                if (!byTraitDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTraitDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTraitDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byTraitDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByTraitDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTraitDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown188"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown188(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown188(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown188"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown188(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown188 is null)
        {
            byUnknown188 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown188;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown188.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown188.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown188.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown188"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown188(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown188(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown204"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown204(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown204(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown204"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown204(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown204 is null)
        {
            byUnknown204 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown204;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown204.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown204.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown204.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown204"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown204(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown204(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown220"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown220(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown220(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown220"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown220(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown220 is null)
        {
            byUnknown220 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown220;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown220.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown220.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown220.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown220"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown220(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown220(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown236"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown236(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown236(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown236"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown236(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown236 is null)
        {
            byUnknown236 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown236;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown236.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown236.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown236.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown236"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown236(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown236(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown252"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown252(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown252(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown252"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown252(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown252 is null)
        {
            byUnknown252 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown252;

                if (!byUnknown252.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown252.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown252.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown252"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown252(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown252(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown256"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown256(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown256(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown256"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown256(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown256 is null)
        {
            byUnknown256 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown256;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown256.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown256.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown256.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown256"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown256(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown256(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.PassiveTreeImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveTreeImage(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveTreeImage(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.PassiveTreeImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveTreeImage(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byPassiveTreeImage is null)
        {
            byPassiveTreeImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveTreeImage;

                if (!byPassiveTreeImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPassiveTreeImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveTreeImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byPassiveTreeImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByPassiveTreeImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveTreeImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown280"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown280(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown280(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown280"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown280(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown280 is null)
        {
            byUnknown280 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown280;

                if (!byUnknown280.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown280.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown280.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown280"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown280(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown280(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown284"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown284(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown284(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown284"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown284(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown284 is null)
        {
            byUnknown284 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown284;

                if (!byUnknown284.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown284.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown284.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown284"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown284(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown284(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.TencentVideo"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTencentVideo(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTencentVideo(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.TencentVideo"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTencentVideo(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byTencentVideo is null)
        {
            byTencentVideo = new();
            foreach (var item in Items)
            {
                var itemKey = item.TencentVideo;

                if (!byTencentVideo.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTencentVideo.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTencentVideo.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byTencentVideo"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByTencentVideo(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTencentVideo(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.AttrsAsId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAttrsAsId(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAttrsAsId(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.AttrsAsId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAttrsAsId(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byAttrsAsId is null)
        {
            byAttrsAsId = new();
            foreach (var item in Items)
            {
                var itemKey = item.AttrsAsId;

                if (!byAttrsAsId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAttrsAsId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAttrsAsId.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byAttrsAsId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByAttrsAsId(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAttrsAsId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.LoginScreen"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLoginScreen(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLoginScreen(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.LoginScreen"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLoginScreen(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byLoginScreen is null)
        {
            byLoginScreen = new();
            foreach (var item in Items)
            {
                var itemKey = item.LoginScreen;

                if (!byLoginScreen.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLoginScreen.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLoginScreen.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byLoginScreen"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByLoginScreen(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLoginScreen(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.PlayerCritter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPlayerCritter(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPlayerCritter(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.PlayerCritter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPlayerCritter(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byPlayerCritter is null)
        {
            byPlayerCritter = new();
            foreach (var item in Items)
            {
                var itemKey = item.PlayerCritter;

                if (!byPlayerCritter.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPlayerCritter.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPlayerCritter.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byPlayerCritter"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByPlayerCritter(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPlayerCritter(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.PlayerEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPlayerEffect(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPlayerEffect(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.PlayerEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPlayerEffect(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byPlayerEffect is null)
        {
            byPlayerEffect = new();
            foreach (var item in Items)
            {
                var itemKey = item.PlayerEffect;

                if (!byPlayerEffect.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPlayerEffect.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPlayerEffect.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byPlayerEffect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByPlayerEffect(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPlayerEffect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.AfterImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAfterImage(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAfterImage(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.AfterImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAfterImage(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byAfterImage is null)
        {
            byAfterImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.AfterImage;

                if (!byAfterImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAfterImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAfterImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byAfterImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByAfterImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAfterImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Mirage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMirage(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMirage(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Mirage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMirage(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byMirage is null)
        {
            byMirage = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mirage;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMirage.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMirage.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMirage.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byMirage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByMirage(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMirage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.CloneImmobile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCloneImmobile(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCloneImmobile(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.CloneImmobile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCloneImmobile(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byCloneImmobile is null)
        {
            byCloneImmobile = new();
            foreach (var item in Items)
            {
                var itemKey = item.CloneImmobile;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCloneImmobile.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCloneImmobile.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCloneImmobile.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byCloneImmobile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByCloneImmobile(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCloneImmobile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.ReplicateClone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByReplicateClone(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByReplicateClone(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.ReplicateClone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByReplicateClone(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byReplicateClone is null)
        {
            byReplicateClone = new();
            foreach (var item in Items)
            {
                var itemKey = item.ReplicateClone;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byReplicateClone.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byReplicateClone.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byReplicateClone.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byReplicateClone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByReplicateClone(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByReplicateClone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.LightningClone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLightningClone(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLightningClone(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.LightningClone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLightningClone(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byLightningClone is null)
        {
            byLightningClone = new();
            foreach (var item in Items)
            {
                var itemKey = item.LightningClone;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byLightningClone.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byLightningClone.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byLightningClone.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byLightningClone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByLightningClone(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLightningClone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown400"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown400(float? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown400(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown400"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown400(float? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown400 is null)
        {
            byUnknown400 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown400;

                if (!byUnknown400.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown400.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown400.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown400"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, CharactersDat>> GetManyToManyByUnknown400(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, CharactersDat>>();
        }

        var items = new List<ResultItem<float, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown400(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown404"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown404(float? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown404(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown404"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown404(float? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown404 is null)
        {
            byUnknown404 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown404;

                if (!byUnknown404.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown404.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown404.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown404"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, CharactersDat>> GetManyToManyByUnknown404(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, CharactersDat>>();
        }

        var items = new List<ResultItem<float, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown404(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.SkillTreeBackground"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillTreeBackground(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillTreeBackground(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.SkillTreeBackground"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillTreeBackground(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (bySkillTreeBackground is null)
        {
            bySkillTreeBackground = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillTreeBackground;

                if (!bySkillTreeBackground.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySkillTreeBackground.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillTreeBackground.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.bySkillTreeBackground"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyBySkillTreeBackground(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillTreeBackground(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Clone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClone(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClone(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Clone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClone(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byClone is null)
        {
            byClone = new();
            foreach (var item in Items)
            {
                var itemKey = item.Clone;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byClone.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byClone.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byClone.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byClone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByClone(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Double"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDouble(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDouble(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Double"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDouble(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byDouble is null)
        {
            byDouble = new();
            foreach (var item in Items)
            {
                var itemKey = item.Double;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDouble.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDouble.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDouble.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byDouble"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByDouble(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDouble(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.MirageWarrior"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMirageWarrior(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMirageWarrior(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.MirageWarrior"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMirageWarrior(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byMirageWarrior is null)
        {
            byMirageWarrior = new();
            foreach (var item in Items)
            {
                var itemKey = item.MirageWarrior;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMirageWarrior.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMirageWarrior.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMirageWarrior.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byMirageWarrior"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByMirageWarrior(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMirageWarrior(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.DoubleTwo"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDoubleTwo(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDoubleTwo(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.DoubleTwo"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDoubleTwo(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byDoubleTwo is null)
        {
            byDoubleTwo = new();
            foreach (var item in Items)
            {
                var itemKey = item.DoubleTwo;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDoubleTwo.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDoubleTwo.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDoubleTwo.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byDoubleTwo"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByDoubleTwo(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDoubleTwo(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.DarkExile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDarkExile(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDarkExile(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.DarkExile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDarkExile(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byDarkExile is null)
        {
            byDarkExile = new();
            foreach (var item in Items)
            {
                var itemKey = item.DarkExile;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDarkExile.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDarkExile.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDarkExile.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byDarkExile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByDarkExile(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDarkExile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Attr"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAttr(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAttr(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Attr"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAttr(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byAttr is null)
        {
            byAttr = new();
            foreach (var item in Items)
            {
                var itemKey = item.Attr;

                if (!byAttr.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAttr.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAttr.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byAttr"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByAttr(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAttr(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.AttrLowercase"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAttrLowercase(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAttrLowercase(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.AttrLowercase"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAttrLowercase(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byAttrLowercase is null)
        {
            byAttrLowercase = new();
            foreach (var item in Items)
            {
                var itemKey = item.AttrLowercase;

                if (!byAttrLowercase.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAttrLowercase.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAttrLowercase.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byAttrLowercase"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByAttrLowercase(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAttrLowercase(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Script"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScript(string? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScript(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Script"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScript(string? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byScript is null)
        {
            byScript = new();
            foreach (var item in Items)
            {
                var itemKey = item.Script;

                if (!byScript.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScript.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScript.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byScript"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharactersDat>> GetManyToManyByScript(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharactersDat>>();
        }

        var items = new List<ResultItem<string, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScript(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown520"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown520(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown520(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown520"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown520(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown520 is null)
        {
            byUnknown520 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown520;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown520.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown520.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown520.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown520"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown520(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown520(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown536"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown536(int? key, out CharactersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown536(key, out var items))
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
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.Unknown536"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown536(int? key, out IReadOnlyList<CharactersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        if (byUnknown536 is null)
        {
            byUnknown536 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown536;

                if (!byUnknown536.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown536.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown536.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharactersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharactersDat"/> with <see cref="CharactersDat.byUnknown536"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharactersDat>> GetManyToManyByUnknown536(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharactersDat>>();
        }

        var items = new List<ResultItem<int, CharactersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown536(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharactersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CharactersDat[] Load()
    {
        const string filePath = "Data/Characters.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharactersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ACTFile
            (var actfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseMaxLife
            (var basemaxlifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseMaxMana
            (var basemaxmanaLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WeaponSpeed
            (var weaponspeedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinDamage
            (var mindamageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxDamage
            (var maxdamageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxAttackDistance
            (var maxattackdistanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IntegerId
            (var integeridLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseStrength
            (var basestrengthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseDexterity
            (var basedexterityLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseIntelligence
            (var baseintelligenceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var tempunknown80Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown80Loading = tempunknown80Loading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StartSkillGem
            (var startskillgemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown140
            (var unknown140Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CharacterSize
            (var charactersizeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IntroSoundFile
            (var introsoundfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StartWeapons
            (var tempstartweaponsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var startweaponsLoading = tempstartweaponsLoading.AsReadOnly();

            // loading Gender
            (var genderLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TraitDescription
            (var traitdescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown188
            (var unknown188Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown204
            (var unknown204Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown220
            (var unknown220Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown236
            (var unknown236Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown252
            (var unknown252Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown256
            (var tempunknown256Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown256Loading = tempunknown256Loading.AsReadOnly();

            // loading PassiveTreeImage
            (var passivetreeimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown280
            (var unknown280Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown284
            (var unknown284Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TencentVideo
            (var tencentvideoLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AttrsAsId
            (var attrsasidLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading LoginScreen
            (var loginscreenLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PlayerCritter
            (var playercritterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PlayerEffect
            (var playereffectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AfterImage
            (var afterimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Mirage
            (var mirageLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading CloneImmobile
            (var cloneimmobileLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ReplicateClone
            (var replicatecloneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading LightningClone
            (var lightningcloneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown400
            (var unknown400Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown404
            (var unknown404Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading SkillTreeBackground
            (var skilltreebackgroundLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Clone
            (var cloneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Double
            (var doubleLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MirageWarrior
            (var miragewarriorLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DoubleTwo
            (var doubletwoLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DarkExile
            (var darkexileLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Attr
            (var attrLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AttrLowercase
            (var attrlowercaseLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Script
            (var scriptLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown520
            (var unknown520Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown536
            (var unknown536Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharactersDat()
            {
                Id = idLoading,
                Name = nameLoading,
                AOFile = aofileLoading,
                ACTFile = actfileLoading,
                BaseMaxLife = basemaxlifeLoading,
                BaseMaxMana = basemaxmanaLoading,
                WeaponSpeed = weaponspeedLoading,
                MinDamage = mindamageLoading,
                MaxDamage = maxdamageLoading,
                MaxAttackDistance = maxattackdistanceLoading,
                Icon = iconLoading,
                IntegerId = integeridLoading,
                BaseStrength = basestrengthLoading,
                BaseDexterity = basedexterityLoading,
                BaseIntelligence = baseintelligenceLoading,
                Unknown80 = unknown80Loading,
                Description = descriptionLoading,
                StartSkillGem = startskillgemLoading,
                Unknown120 = unknown120Loading,
                Unknown136 = unknown136Loading,
                Unknown140 = unknown140Loading,
                CharacterSize = charactersizeLoading,
                IntroSoundFile = introsoundfileLoading,
                StartWeapons = startweaponsLoading,
                Gender = genderLoading,
                TraitDescription = traitdescriptionLoading,
                Unknown188 = unknown188Loading,
                Unknown204 = unknown204Loading,
                Unknown220 = unknown220Loading,
                Unknown236 = unknown236Loading,
                Unknown252 = unknown252Loading,
                Unknown256 = unknown256Loading,
                PassiveTreeImage = passivetreeimageLoading,
                Unknown280 = unknown280Loading,
                Unknown284 = unknown284Loading,
                TencentVideo = tencentvideoLoading,
                AttrsAsId = attrsasidLoading,
                LoginScreen = loginscreenLoading,
                PlayerCritter = playercritterLoading,
                PlayerEffect = playereffectLoading,
                AfterImage = afterimageLoading,
                Mirage = mirageLoading,
                CloneImmobile = cloneimmobileLoading,
                ReplicateClone = replicatecloneLoading,
                LightningClone = lightningcloneLoading,
                Unknown400 = unknown400Loading,
                Unknown404 = unknown404Loading,
                SkillTreeBackground = skilltreebackgroundLoading,
                Clone = cloneLoading,
                Double = doubleLoading,
                MirageWarrior = miragewarriorLoading,
                DoubleTwo = doubletwoLoading,
                DarkExile = darkexileLoading,
                Attr = attrLoading,
                AttrLowercase = attrlowercaseLoading,
                Script = scriptLoading,
                Unknown520 = unknown520Loading,
                Unknown536 = unknown536Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
