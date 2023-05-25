using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CharacterStartStatesDat"/> related data and helper methods.
/// </summary>
public sealed class CharacterStartStatesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CharacterStartStatesDat> Items { get; }

    private Dictionary<string, List<CharacterStartStatesDat>>? byId;
    private Dictionary<string, List<CharacterStartStatesDat>>? byDescription;
    private Dictionary<int, List<CharacterStartStatesDat>>? byCharactersKey;
    private Dictionary<int, List<CharacterStartStatesDat>>? byLevel;
    private Dictionary<int, List<CharacterStartStatesDat>>? byPassiveSkillsKeys;
    private Dictionary<int, List<CharacterStartStatesDat>>? byCharacterStartStateSetKey;
    private Dictionary<int, List<CharacterStartStatesDat>>? byUnknown68;
    private Dictionary<int, List<CharacterStartStatesDat>>? byCharacterStartQuestStateKeys;
    private Dictionary<bool, List<CharacterStartStatesDat>>? byUnknown100;
    private Dictionary<string, List<CharacterStartStatesDat>>? byInfoText;
    private Dictionary<int, List<CharacterStartStatesDat>>? byUnknown109;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterStartStatesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CharacterStartStatesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CharacterStartStatesDat? item)
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
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
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
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterStartStatesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<string, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out CharacterStartStatesDat? item)
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
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
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
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterStartStatesDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<string, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharactersKey(int? key, out CharacterStartStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharactersKey(key, out var items))
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
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharactersKey(int? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        if (byCharactersKey is null)
        {
            byCharactersKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.CharactersKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCharactersKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCharactersKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCharactersKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byCharactersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartStatesDat>> GetManyToManyByCharactersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharactersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out CharacterStartStatesDat? item)
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
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
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
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartStatesDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.PassiveSkillsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveSkillsKeys(int? key, out CharacterStartStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveSkillsKeys(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.PassiveSkillsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveSkillsKeys(int? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        if (byPassiveSkillsKeys is null)
        {
            byPassiveSkillsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveSkillsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byPassiveSkillsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPassiveSkillsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPassiveSkillsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byPassiveSkillsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartStatesDat>> GetManyToManyByPassiveSkillsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveSkillsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.CharacterStartStateSetKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharacterStartStateSetKey(int? key, out CharacterStartStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharacterStartStateSetKey(key, out var items))
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
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.CharacterStartStateSetKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharacterStartStateSetKey(int? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        if (byCharacterStartStateSetKey is null)
        {
            byCharacterStartStateSetKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.CharacterStartStateSetKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCharacterStartStateSetKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCharacterStartStateSetKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCharacterStartStateSetKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byCharacterStartStateSetKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartStatesDat>> GetManyToManyByCharacterStartStateSetKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharacterStartStateSetKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(int? key, out CharacterStartStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown68(key, out var items))
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
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(int? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown68.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartStatesDat>> GetManyToManyByUnknown68(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.CharacterStartQuestStateKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharacterStartQuestStateKeys(int? key, out CharacterStartStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharacterStartQuestStateKeys(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.CharacterStartQuestStateKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharacterStartQuestStateKeys(int? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        if (byCharacterStartQuestStateKeys is null)
        {
            byCharacterStartQuestStateKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.CharacterStartQuestStateKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byCharacterStartQuestStateKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCharacterStartQuestStateKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCharacterStartQuestStateKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byCharacterStartQuestStateKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartStatesDat>> GetManyToManyByCharacterStartQuestStateKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharacterStartQuestStateKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(bool? key, out CharacterStartStatesDat? item)
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
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(bool? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
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
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CharacterStartStatesDat>> GetManyToManyByUnknown100(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<bool, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.InfoText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInfoText(string? key, out CharacterStartStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInfoText(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.InfoText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInfoText(string? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        if (byInfoText is null)
        {
            byInfoText = new();
            foreach (var item in Items)
            {
                var itemKey = item.InfoText;

                if (!byInfoText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInfoText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInfoText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byInfoText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterStartStatesDat>> GetManyToManyByInfoText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<string, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInfoText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown109(int? key, out CharacterStartStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown109(key, out var items))
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
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown109(int? key, out IReadOnlyList<CharacterStartStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        if (byUnknown109 is null)
        {
            byUnknown109 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown109;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown109.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown109.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown109.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartStatesDat"/> with <see cref="CharacterStartStatesDat.byUnknown109"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartStatesDat>> GetManyToManyByUnknown109(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartStatesDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown109(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CharacterStartStatesDat[] Load()
    {
        const string filePath = "Data/CharacterStartStates.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterStartStatesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKey
            (var characterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PassiveSkillsKeys
            (var temppassiveskillskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var passiveskillskeysLoading = temppassiveskillskeysLoading.AsReadOnly();

            // loading CharacterStartStateSetKey
            (var characterstartstatesetkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading CharacterStartQuestStateKeys
            (var tempcharacterstartqueststatekeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var characterstartqueststatekeysLoading = tempcharacterstartqueststatekeysLoading.AsReadOnly();

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading InfoText
            (var infotextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterStartStatesDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                CharactersKey = characterskeyLoading,
                Level = levelLoading,
                PassiveSkillsKeys = passiveskillskeysLoading,
                CharacterStartStateSetKey = characterstartstatesetkeyLoading,
                Unknown68 = unknown68Loading,
                CharacterStartQuestStateKeys = characterstartqueststatekeysLoading,
                Unknown100 = unknown100Loading,
                InfoText = infotextLoading,
                Unknown109 = unknown109Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
