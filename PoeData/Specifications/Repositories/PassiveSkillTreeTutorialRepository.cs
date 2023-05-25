using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PassiveSkillTreeTutorialDat"/> related data and helper methods.
/// </summary>
public sealed class PassiveSkillTreeTutorialRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PassiveSkillTreeTutorialDat> Items { get; }

    private Dictionary<string, List<PassiveSkillTreeTutorialDat>>? byId;
    private Dictionary<int, List<PassiveSkillTreeTutorialDat>>? byCharactersKey;
    private Dictionary<int, List<PassiveSkillTreeTutorialDat>>? byUnknown24;
    private Dictionary<string, List<PassiveSkillTreeTutorialDat>>? byChoiceA_Description;
    private Dictionary<string, List<PassiveSkillTreeTutorialDat>>? byChoiceB_Description;
    private Dictionary<int, List<PassiveSkillTreeTutorialDat>>? byUnknown56;
    private Dictionary<string, List<PassiveSkillTreeTutorialDat>>? byChoiceA_PassiveTreeURL;
    private Dictionary<string, List<PassiveSkillTreeTutorialDat>>? byChoiceB_PassiveTreeURL;
    private Dictionary<int, List<PassiveSkillTreeTutorialDat>>? byUnknown88;
    private Dictionary<int, List<PassiveSkillTreeTutorialDat>>? byUnknown104;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassiveSkillTreeTutorialRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PassiveSkillTreeTutorialRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out PassiveSkillTreeTutorialDat? item)
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
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<PassiveSkillTreeTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
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
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeTutorialDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeTutorialDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharactersKey(int? key, out PassiveSkillTreeTutorialDat? item)
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
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharactersKey(int? key, out IReadOnlyList<PassiveSkillTreeTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
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
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.byCharactersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillTreeTutorialDat>> GetManyToManyByCharactersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillTreeTutorialDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillTreeTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharactersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillTreeTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out PassiveSkillTreeTutorialDat? item)
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
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<PassiveSkillTreeTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown24.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillTreeTutorialDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillTreeTutorialDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillTreeTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillTreeTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.ChoiceA_Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChoiceA_Description(string? key, out PassiveSkillTreeTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChoiceA_Description(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.ChoiceA_Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChoiceA_Description(string? key, out IReadOnlyList<PassiveSkillTreeTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        if (byChoiceA_Description is null)
        {
            byChoiceA_Description = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChoiceA_Description;

                if (!byChoiceA_Description.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChoiceA_Description.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChoiceA_Description.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.byChoiceA_Description"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeTutorialDat>> GetManyToManyByChoiceA_Description(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeTutorialDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChoiceA_Description(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.ChoiceB_Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChoiceB_Description(string? key, out PassiveSkillTreeTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChoiceB_Description(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.ChoiceB_Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChoiceB_Description(string? key, out IReadOnlyList<PassiveSkillTreeTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        if (byChoiceB_Description is null)
        {
            byChoiceB_Description = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChoiceB_Description;

                if (!byChoiceB_Description.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChoiceB_Description.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChoiceB_Description.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.byChoiceB_Description"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeTutorialDat>> GetManyToManyByChoiceB_Description(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeTutorialDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChoiceB_Description(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out PassiveSkillTreeTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<PassiveSkillTreeTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown56.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillTreeTutorialDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillTreeTutorialDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillTreeTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillTreeTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.ChoiceA_PassiveTreeURL"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChoiceA_PassiveTreeURL(string? key, out PassiveSkillTreeTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChoiceA_PassiveTreeURL(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.ChoiceA_PassiveTreeURL"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChoiceA_PassiveTreeURL(string? key, out IReadOnlyList<PassiveSkillTreeTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        if (byChoiceA_PassiveTreeURL is null)
        {
            byChoiceA_PassiveTreeURL = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChoiceA_PassiveTreeURL;

                if (!byChoiceA_PassiveTreeURL.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChoiceA_PassiveTreeURL.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChoiceA_PassiveTreeURL.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.byChoiceA_PassiveTreeURL"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeTutorialDat>> GetManyToManyByChoiceA_PassiveTreeURL(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeTutorialDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChoiceA_PassiveTreeURL(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.ChoiceB_PassiveTreeURL"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChoiceB_PassiveTreeURL(string? key, out PassiveSkillTreeTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChoiceB_PassiveTreeURL(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.ChoiceB_PassiveTreeURL"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChoiceB_PassiveTreeURL(string? key, out IReadOnlyList<PassiveSkillTreeTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        if (byChoiceB_PassiveTreeURL is null)
        {
            byChoiceB_PassiveTreeURL = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChoiceB_PassiveTreeURL;

                if (!byChoiceB_PassiveTreeURL.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChoiceB_PassiveTreeURL.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChoiceB_PassiveTreeURL.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.byChoiceB_PassiveTreeURL"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeTutorialDat>> GetManyToManyByChoiceB_PassiveTreeURL(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeTutorialDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChoiceB_PassiveTreeURL(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out PassiveSkillTreeTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown88(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<PassiveSkillTreeTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown88.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillTreeTutorialDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillTreeTutorialDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillTreeTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillTreeTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(int? key, out PassiveSkillTreeTutorialDat? item)
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
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(int? key, out IReadOnlyList<PassiveSkillTreeTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown104.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown104.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeTutorialDat"/> with <see cref="PassiveSkillTreeTutorialDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PassiveSkillTreeTutorialDat>> GetManyToManyByUnknown104(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PassiveSkillTreeTutorialDat>>();
        }

        var items = new List<ResultItem<int, PassiveSkillTreeTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PassiveSkillTreeTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PassiveSkillTreeTutorialDat[] Load()
    {
        const string filePath = "Data/PassiveSkillTreeTutorial.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillTreeTutorialDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKey
            (var characterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ChoiceA_Description
            (var choicea_descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ChoiceB_Description
            (var choiceb_descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ChoiceA_PassiveTreeURL
            (var choicea_passivetreeurlLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ChoiceB_PassiveTreeURL
            (var choiceb_passivetreeurlLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillTreeTutorialDat()
            {
                Id = idLoading,
                CharactersKey = characterskeyLoading,
                Unknown24 = unknown24Loading,
                ChoiceA_Description = choicea_descriptionLoading,
                ChoiceB_Description = choiceb_descriptionLoading,
                Unknown56 = unknown56Loading,
                ChoiceA_PassiveTreeURL = choicea_passivetreeurlLoading,
                ChoiceB_PassiveTreeURL = choiceb_passivetreeurlLoading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
