using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="QuestDat"/> related data and helper methods.
/// </summary>
public sealed class QuestRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<QuestDat> Items { get; }

    private Dictionary<string, List<QuestDat>>? byId;
    private Dictionary<int, List<QuestDat>>? byAct;
    private Dictionary<string, List<QuestDat>>? byName;
    private Dictionary<string, List<QuestDat>>? byIcon_DDSFile;
    private Dictionary<int, List<QuestDat>>? byQuestId;
    private Dictionary<bool, List<QuestDat>>? byUnknown32;
    private Dictionary<int, List<QuestDat>>? byType;
    private Dictionary<int, List<QuestDat>>? byUnknown49;
    private Dictionary<int, List<QuestDat>>? byUnknown65;
    private Dictionary<int, List<QuestDat>>? byTrackerGroup;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal QuestRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out QuestDat? item)
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
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<QuestDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestDat>();
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
            items = Array.Empty<QuestDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, QuestDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, QuestDat>>();
        }

        var items = new List<ResultItem<string, QuestDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, QuestDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Act"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAct(int? key, out QuestDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAct(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Act"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAct(int? key, out IReadOnlyList<QuestDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        if (byAct is null)
        {
            byAct = new();
            foreach (var item in Items)
            {
                var itemKey = item.Act;

                if (!byAct.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAct.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAct.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.byAct"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestDat>> GetManyToManyByAct(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestDat>>();
        }

        var items = new List<ResultItem<int, QuestDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAct(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out QuestDat? item)
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
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<QuestDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestDat>();
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
            items = Array.Empty<QuestDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, QuestDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, QuestDat>>();
        }

        var items = new List<ResultItem<string, QuestDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, QuestDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Icon_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon_DDSFile(string? key, out QuestDat? item)
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
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Icon_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon_DDSFile(string? key, out IReadOnlyList<QuestDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestDat>();
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
            items = Array.Empty<QuestDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.byIcon_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, QuestDat>> GetManyToManyByIcon_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, QuestDat>>();
        }

        var items = new List<ResultItem<string, QuestDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, QuestDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.QuestId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestId(int? key, out QuestDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestId(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.QuestId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestId(int? key, out IReadOnlyList<QuestDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        if (byQuestId is null)
        {
            byQuestId = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestId;

                if (!byQuestId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byQuestId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.byQuestId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestDat>> GetManyToManyByQuestId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestDat>>();
        }

        var items = new List<ResultItem<int, QuestDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(bool? key, out QuestDat? item)
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
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(bool? key, out IReadOnlyList<QuestDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, QuestDat>> GetManyToManyByUnknown32(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, QuestDat>>();
        }

        var items = new List<ResultItem<bool, QuestDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, QuestDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByType(int? key, out QuestDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByType(key, out var items))
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
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByType(int? key, out IReadOnlyList<QuestDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        if (byType is null)
        {
            byType = new();
            foreach (var item in Items)
            {
                var itemKey = item.Type;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.byType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestDat>> GetManyToManyByType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestDat>>();
        }

        var items = new List<ResultItem<int, QuestDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown49(int? key, out QuestDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown49(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown49(int? key, out IReadOnlyList<QuestDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        if (byUnknown49 is null)
        {
            byUnknown49 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown49;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown49.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown49.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown49.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.byUnknown49"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestDat>> GetManyToManyByUnknown49(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestDat>>();
        }

        var items = new List<ResultItem<int, QuestDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown49(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown65(int? key, out QuestDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown65(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown65(int? key, out IReadOnlyList<QuestDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        if (byUnknown65 is null)
        {
            byUnknown65 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown65;

                if (!byUnknown65.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown65.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown65.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.byUnknown65"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestDat>> GetManyToManyByUnknown65(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestDat>>();
        }

        var items = new List<ResultItem<int, QuestDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown65(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.TrackerGroup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTrackerGroup(int? key, out QuestDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTrackerGroup(key, out var items))
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
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.TrackerGroup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTrackerGroup(int? key, out IReadOnlyList<QuestDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        if (byTrackerGroup is null)
        {
            byTrackerGroup = new();
            foreach (var item in Items)
            {
                var itemKey = item.TrackerGroup;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTrackerGroup.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTrackerGroup.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTrackerGroup.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestDat"/> with <see cref="QuestDat.byTrackerGroup"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestDat>> GetManyToManyByTrackerGroup(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestDat>>();
        }

        var items = new List<ResultItem<int, QuestDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTrackerGroup(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private QuestDat[] Load()
    {
        const string filePath = "Data/Quest.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Act
            (var actLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon_DDSFile
            (var icon_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading QuestId
            (var questidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown49
            (var tempunknown49Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown49Loading = tempunknown49Loading.AsReadOnly();

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TrackerGroup
            (var trackergroupLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestDat()
            {
                Id = idLoading,
                Act = actLoading,
                Name = nameLoading,
                Icon_DDSFile = icon_ddsfileLoading,
                QuestId = questidLoading,
                Unknown32 = unknown32Loading,
                Type = typeLoading,
                Unknown49 = unknown49Loading,
                Unknown65 = unknown65Loading,
                TrackerGroup = trackergroupLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
