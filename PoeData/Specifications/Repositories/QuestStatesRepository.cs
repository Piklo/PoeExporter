using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="QuestStatesDat"/> related data and helper methods.
/// </summary>
public sealed class QuestStatesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<QuestStatesDat> Items { get; }

    private Dictionary<int, List<QuestStatesDat>>? byQuestKey;
    private Dictionary<int, List<QuestStatesDat>>? byUnknown16;
    private Dictionary<int, List<QuestStatesDat>>? byQuestStates;
    private Dictionary<int, List<QuestStatesDat>>? byUnknown36;
    private Dictionary<string, List<QuestStatesDat>>? byText;
    private Dictionary<bool, List<QuestStatesDat>>? byUnknown60;
    private Dictionary<string, List<QuestStatesDat>>? byMessage;
    private Dictionary<int, List<QuestStatesDat>>? byMapPinsKeys1;
    private Dictionary<int, List<QuestStatesDat>>? byUnknown85;
    private Dictionary<string, List<QuestStatesDat>>? byMapPinsTexts;
    private Dictionary<int, List<QuestStatesDat>>? byMapPinsKeys2;
    private Dictionary<int, List<QuestStatesDat>>? byUnknown121;
    private Dictionary<bool, List<QuestStatesDat>>? byUnknown137;
    private Dictionary<int, List<QuestStatesDat>>? byUnknown138;
    private Dictionary<int, List<QuestStatesDat>>? byUnknown154;
    private Dictionary<int, List<QuestStatesDat>>? byUnknown170;
    private Dictionary<int, List<QuestStatesDat>>? bySoundEffect;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestStatesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal QuestStatesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.QuestKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestKey(int? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestKey(key, out var items))
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
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.QuestKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestKey(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byQuestKey is null)
        {
            byQuestKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byQuestKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByQuestKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out QuestStatesDat? item)
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
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.QuestStates"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestStates(int? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestStates(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.QuestStates"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestStates(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byQuestStates is null)
        {
            byQuestStates = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestStates;
                foreach (var listKey in itemKey)
                {
                    if (!byQuestStates.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byQuestStates.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byQuestStates.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byQuestStates"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByQuestStates(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestStates(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out QuestStatesDat? item)
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
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byUnknown36 is null)
        {
            byUnknown36 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown36;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown36.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown36.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown36.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByText(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byText is null)
        {
            byText = new();
            foreach (var item in Items)
            {
                var itemKey = item.Text;

                if (!byText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, QuestStatesDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, QuestStatesDat>>();
        }

        var items = new List<ResultItem<string, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(bool? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown60(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(bool? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byUnknown60 is null)
        {
            byUnknown60 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown60;

                if (!byUnknown60.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown60.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown60.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, QuestStatesDat>> GetManyToManyByUnknown60(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, QuestStatesDat>>();
        }

        var items = new List<ResultItem<bool, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Message"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMessage(string? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMessage(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Message"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMessage(string? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byMessage is null)
        {
            byMessage = new();
            foreach (var item in Items)
            {
                var itemKey = item.Message;

                if (!byMessage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMessage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMessage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byMessage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, QuestStatesDat>> GetManyToManyByMessage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, QuestStatesDat>>();
        }

        var items = new List<ResultItem<string, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMessage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.MapPinsKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapPinsKeys1(int? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapPinsKeys1(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.MapPinsKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapPinsKeys1(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byMapPinsKeys1 is null)
        {
            byMapPinsKeys1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapPinsKeys1;
                foreach (var listKey in itemKey)
                {
                    if (!byMapPinsKeys1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMapPinsKeys1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMapPinsKeys1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byMapPinsKeys1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByMapPinsKeys1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapPinsKeys1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown85"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown85(int? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown85(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown85"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown85(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byUnknown85 is null)
        {
            byUnknown85 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown85;

                if (!byUnknown85.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown85.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown85.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byUnknown85"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByUnknown85(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown85(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.MapPinsTexts"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapPinsTexts(string? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapPinsTexts(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.MapPinsTexts"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapPinsTexts(string? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byMapPinsTexts is null)
        {
            byMapPinsTexts = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapPinsTexts;
                foreach (var listKey in itemKey)
                {
                    if (!byMapPinsTexts.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMapPinsTexts.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMapPinsTexts.TryGetValue(key, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byMapPinsTexts"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, QuestStatesDat>> GetManyToManyByMapPinsTexts(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, QuestStatesDat>>();
        }

        var items = new List<ResultItem<string, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapPinsTexts(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.MapPinsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapPinsKeys2(int? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapPinsKeys2(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.MapPinsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapPinsKeys2(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byMapPinsKeys2 is null)
        {
            byMapPinsKeys2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapPinsKeys2;
                foreach (var listKey in itemKey)
                {
                    if (!byMapPinsKeys2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMapPinsKeys2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMapPinsKeys2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byMapPinsKeys2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByMapPinsKeys2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapPinsKeys2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown121"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown121(int? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown121(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown121"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown121(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byUnknown121 is null)
        {
            byUnknown121 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown121;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown121.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown121.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown121.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byUnknown121"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByUnknown121(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown121(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown137"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown137(bool? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown137(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown137"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown137(bool? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byUnknown137 is null)
        {
            byUnknown137 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown137;

                if (!byUnknown137.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown137.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown137.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byUnknown137"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, QuestStatesDat>> GetManyToManyByUnknown137(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, QuestStatesDat>>();
        }

        var items = new List<ResultItem<bool, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown137(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown138"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown138(int? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown138(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown138"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown138(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byUnknown138 is null)
        {
            byUnknown138 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown138;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown138.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown138.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown138.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byUnknown138"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByUnknown138(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown138(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown154"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown154(int? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown154(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown154"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown154(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byUnknown154 is null)
        {
            byUnknown154 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown154;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown154.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown154.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown154.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byUnknown154"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByUnknown154(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown154(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown170"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown170(int? key, out QuestStatesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown170(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.Unknown170"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown170(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        if (byUnknown170 is null)
        {
            byUnknown170 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown170;

                if (!byUnknown170.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown170.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown170.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.byUnknown170"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyByUnknown170(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown170(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.SoundEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySoundEffect(int? key, out QuestStatesDat? item)
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
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.SoundEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySoundEffect(int? key, out IReadOnlyList<QuestStatesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStatesDat>();
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
            items = Array.Empty<QuestStatesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStatesDat"/> with <see cref="QuestStatesDat.bySoundEffect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStatesDat>> GetManyToManyBySoundEffect(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStatesDat>>();
        }

        var items = new List<ResultItem<int, QuestStatesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySoundEffect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStatesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private QuestStatesDat[] Load()
    {
        const string filePath = "Data/QuestStates.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestStatesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading QuestKey
            (var questkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading QuestStates
            (var tempqueststatesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var queststatesLoading = tempqueststatesLoading.AsReadOnly();

            // loading Unknown36
            (var tempunknown36Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown36Loading = tempunknown36Loading.AsReadOnly();

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Message
            (var messageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MapPinsKeys1
            (var tempmappinskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var mappinskeys1Loading = tempmappinskeys1Loading.AsReadOnly();

            // loading Unknown85
            (var unknown85Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MapPinsTexts
            (var tempmappinstextsLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var mappinstextsLoading = tempmappinstextsLoading.AsReadOnly();

            // loading MapPinsKeys2
            (var tempmappinskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var mappinskeys2Loading = tempmappinskeys2Loading.AsReadOnly();

            // loading Unknown121
            (var tempunknown121Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown121Loading = tempunknown121Loading.AsReadOnly();

            // loading Unknown137
            (var unknown137Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown138
            (var tempunknown138Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown138Loading = tempunknown138Loading.AsReadOnly();

            // loading Unknown154
            (var tempunknown154Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown154Loading = tempunknown154Loading.AsReadOnly();

            // loading Unknown170
            (var unknown170Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SoundEffect
            (var soundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestStatesDat()
            {
                QuestKey = questkeyLoading,
                Unknown16 = unknown16Loading,
                QuestStates = queststatesLoading,
                Unknown36 = unknown36Loading,
                Text = textLoading,
                Unknown60 = unknown60Loading,
                Message = messageLoading,
                MapPinsKeys1 = mappinskeys1Loading,
                Unknown85 = unknown85Loading,
                MapPinsTexts = mappinstextsLoading,
                MapPinsKeys2 = mappinskeys2Loading,
                Unknown121 = unknown121Loading,
                Unknown137 = unknown137Loading,
                Unknown138 = unknown138Loading,
                Unknown154 = unknown154Loading,
                Unknown170 = unknown170Loading,
                SoundEffect = soundeffectLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
