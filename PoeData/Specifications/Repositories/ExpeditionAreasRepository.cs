using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ExpeditionAreasDat"/> related data and helper methods.
/// </summary>
public sealed class ExpeditionAreasRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ExpeditionAreasDat> Items { get; }

    private Dictionary<int, List<ExpeditionAreasDat>>? byArea;
    private Dictionary<int, List<ExpeditionAreasDat>>? byPosX;
    private Dictionary<int, List<ExpeditionAreasDat>>? byPosY;
    private Dictionary<int, List<ExpeditionAreasDat>>? byTags;
    private Dictionary<int, List<ExpeditionAreasDat>>? byUnknown40;
    private Dictionary<bool, List<ExpeditionAreasDat>>? byUnknown56;
    private Dictionary<int, List<ExpeditionAreasDat>>? byTextAudio;
    private Dictionary<int, List<ExpeditionAreasDat>>? byCompletionAchievements;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpeditionAreasRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ExpeditionAreasRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.Area"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArea(int? key, out ExpeditionAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArea(key, out var items))
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
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.Area"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArea(int? key, out IReadOnlyList<ExpeditionAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        if (byArea is null)
        {
            byArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.Area;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byArea.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byArea.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.byArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionAreasDat>> GetManyToManyByArea(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionAreasDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.PosX"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPosX(int? key, out ExpeditionAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPosX(key, out var items))
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
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.PosX"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPosX(int? key, out IReadOnlyList<ExpeditionAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        if (byPosX is null)
        {
            byPosX = new();
            foreach (var item in Items)
            {
                var itemKey = item.PosX;

                if (!byPosX.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPosX.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPosX.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.byPosX"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionAreasDat>> GetManyToManyByPosX(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionAreasDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPosX(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.PosY"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPosY(int? key, out ExpeditionAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPosY(key, out var items))
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
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.PosY"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPosY(int? key, out IReadOnlyList<ExpeditionAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        if (byPosY is null)
        {
            byPosY = new();
            foreach (var item in Items)
            {
                var itemKey = item.PosY;

                if (!byPosY.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPosY.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPosY.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.byPosY"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionAreasDat>> GetManyToManyByPosY(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionAreasDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPosY(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.Tags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTags(int? key, out ExpeditionAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTags(key, out var items))
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
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.Tags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTags(int? key, out IReadOnlyList<ExpeditionAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        if (byTags is null)
        {
            byTags = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tags;
                foreach (var listKey in itemKey)
                {
                    if (!byTags.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTags.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTags.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.byTags"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionAreasDat>> GetManyToManyByTags(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionAreasDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTags(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out ExpeditionAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<ExpeditionAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown40.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown40.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionAreasDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionAreasDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(bool? key, out ExpeditionAreasDat? item)
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
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(bool? key, out IReadOnlyList<ExpeditionAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;

                if (!byUnknown56.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ExpeditionAreasDat>> GetManyToManyByUnknown56(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ExpeditionAreasDat>>();
        }

        var items = new List<ResultItem<bool, ExpeditionAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ExpeditionAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudio(int? key, out ExpeditionAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTextAudio(key, out var items))
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
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudio(int? key, out IReadOnlyList<ExpeditionAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        if (byTextAudio is null)
        {
            byTextAudio = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextAudio;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTextAudio.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTextAudio.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTextAudio.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.byTextAudio"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionAreasDat>> GetManyToManyByTextAudio(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionAreasDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudio(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.CompletionAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCompletionAchievements(int? key, out ExpeditionAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCompletionAchievements(key, out var items))
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
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.CompletionAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCompletionAchievements(int? key, out IReadOnlyList<ExpeditionAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        if (byCompletionAchievements is null)
        {
            byCompletionAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.CompletionAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byCompletionAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCompletionAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCompletionAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExpeditionAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExpeditionAreasDat"/> with <see cref="ExpeditionAreasDat.byCompletionAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExpeditionAreasDat>> GetManyToManyByCompletionAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExpeditionAreasDat>>();
        }

        var items = new List<ResultItem<int, ExpeditionAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCompletionAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExpeditionAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ExpeditionAreasDat[] Load()
    {
        const string filePath = "Data/ExpeditionAreas.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionAreasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Area
            (var areaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PosX
            (var posxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PosY
            (var posyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tags
            (var temptagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagsLoading = temptagsLoading.AsReadOnly();

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading CompletionAchievements
            (var tempcompletionachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var completionachievementsLoading = tempcompletionachievementsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionAreasDat()
            {
                Area = areaLoading,
                PosX = posxLoading,
                PosY = posyLoading,
                Tags = tagsLoading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                TextAudio = textaudioLoading,
                CompletionAchievements = completionachievementsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
