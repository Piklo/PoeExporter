using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UniqueStashLayoutDat"/> related data and helper methods.
/// </summary>
public sealed class UniqueStashLayoutRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UniqueStashLayoutDat> Items { get; }

    private Dictionary<int, List<UniqueStashLayoutDat>>? byWordsKey;
    private Dictionary<int, List<UniqueStashLayoutDat>>? byItemVisualIdentityKey;
    private Dictionary<int, List<UniqueStashLayoutDat>>? byUniqueStashTypesKey;
    private Dictionary<int, List<UniqueStashLayoutDat>>? byUnknown48;
    private Dictionary<int, List<UniqueStashLayoutDat>>? byUnknown52;
    private Dictionary<int, List<UniqueStashLayoutDat>>? byOverrideWidth;
    private Dictionary<int, List<UniqueStashLayoutDat>>? byOverrideHeight;
    private Dictionary<bool, List<UniqueStashLayoutDat>>? byShowIfEmptyChallengeLeague;
    private Dictionary<bool, List<UniqueStashLayoutDat>>? byShowIfEmptyStandard;
    private Dictionary<int, List<UniqueStashLayoutDat>>? byRenamedVersion;
    private Dictionary<int, List<UniqueStashLayoutDat>>? byBaseVersion;
    private Dictionary<bool, List<UniqueStashLayoutDat>>? byIsAlternateArt;

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueStashLayoutRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UniqueStashLayoutRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.WordsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWordsKey(int? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWordsKey(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.WordsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWordsKey(int? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byWordsKey is null)
        {
            byWordsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WordsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWordsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWordsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWordsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byWordsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashLayoutDat>> GetManyToManyByWordsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWordsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentityKey(int? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualIdentityKey(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentityKey(int? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byItemVisualIdentityKey is null)
        {
            byItemVisualIdentityKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualIdentityKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualIdentityKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualIdentityKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualIdentityKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byItemVisualIdentityKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashLayoutDat>> GetManyToManyByItemVisualIdentityKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentityKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.UniqueStashTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUniqueStashTypesKey(int? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUniqueStashTypesKey(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.UniqueStashTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUniqueStashTypesKey(int? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byUniqueStashTypesKey is null)
        {
            byUniqueStashTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.UniqueStashTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUniqueStashTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUniqueStashTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUniqueStashTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byUniqueStashTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashLayoutDat>> GetManyToManyByUniqueStashTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUniqueStashTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out UniqueStashLayoutDat? item)
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
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
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashLayoutDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown52(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;

                if (!byUnknown52.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown52.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashLayoutDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.OverrideWidth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOverrideWidth(int? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOverrideWidth(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.OverrideWidth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOverrideWidth(int? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byOverrideWidth is null)
        {
            byOverrideWidth = new();
            foreach (var item in Items)
            {
                var itemKey = item.OverrideWidth;

                if (!byOverrideWidth.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOverrideWidth.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOverrideWidth.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byOverrideWidth"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashLayoutDat>> GetManyToManyByOverrideWidth(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOverrideWidth(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.OverrideHeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOverrideHeight(int? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOverrideHeight(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.OverrideHeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOverrideHeight(int? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byOverrideHeight is null)
        {
            byOverrideHeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.OverrideHeight;

                if (!byOverrideHeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOverrideHeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOverrideHeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byOverrideHeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashLayoutDat>> GetManyToManyByOverrideHeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOverrideHeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.ShowIfEmptyChallengeLeague"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShowIfEmptyChallengeLeague(bool? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShowIfEmptyChallengeLeague(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.ShowIfEmptyChallengeLeague"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShowIfEmptyChallengeLeague(bool? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byShowIfEmptyChallengeLeague is null)
        {
            byShowIfEmptyChallengeLeague = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShowIfEmptyChallengeLeague;

                if (!byShowIfEmptyChallengeLeague.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShowIfEmptyChallengeLeague.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShowIfEmptyChallengeLeague.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byShowIfEmptyChallengeLeague"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UniqueStashLayoutDat>> GetManyToManyByShowIfEmptyChallengeLeague(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<bool, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShowIfEmptyChallengeLeague(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.ShowIfEmptyStandard"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShowIfEmptyStandard(bool? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShowIfEmptyStandard(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.ShowIfEmptyStandard"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShowIfEmptyStandard(bool? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byShowIfEmptyStandard is null)
        {
            byShowIfEmptyStandard = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShowIfEmptyStandard;

                if (!byShowIfEmptyStandard.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShowIfEmptyStandard.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShowIfEmptyStandard.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byShowIfEmptyStandard"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UniqueStashLayoutDat>> GetManyToManyByShowIfEmptyStandard(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<bool, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShowIfEmptyStandard(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.RenamedVersion"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRenamedVersion(int? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRenamedVersion(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.RenamedVersion"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRenamedVersion(int? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byRenamedVersion is null)
        {
            byRenamedVersion = new();
            foreach (var item in Items)
            {
                var itemKey = item.RenamedVersion;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRenamedVersion.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRenamedVersion.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRenamedVersion.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byRenamedVersion"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashLayoutDat>> GetManyToManyByRenamedVersion(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRenamedVersion(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.BaseVersion"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseVersion(int? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseVersion(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.BaseVersion"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseVersion(int? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byBaseVersion is null)
        {
            byBaseVersion = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseVersion;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseVersion.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseVersion.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseVersion.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byBaseVersion"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueStashLayoutDat>> GetManyToManyByBaseVersion(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<int, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseVersion(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.IsAlternateArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsAlternateArt(bool? key, out UniqueStashLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsAlternateArt(key, out var items))
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
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.IsAlternateArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsAlternateArt(bool? key, out IReadOnlyList<UniqueStashLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        if (byIsAlternateArt is null)
        {
            byIsAlternateArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsAlternateArt;

                if (!byIsAlternateArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsAlternateArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsAlternateArt.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueStashLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueStashLayoutDat"/> with <see cref="UniqueStashLayoutDat.byIsAlternateArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UniqueStashLayoutDat>> GetManyToManyByIsAlternateArt(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UniqueStashLayoutDat>>();
        }

        var items = new List<ResultItem<bool, UniqueStashLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsAlternateArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UniqueStashLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UniqueStashLayoutDat[] Load()
    {
        const string filePath = "Data/UniqueStashLayout.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueStashLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WordsKey
            (var wordskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemVisualIdentityKey
            (var itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading UniqueStashTypesKey
            (var uniquestashtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading OverrideWidth
            (var overridewidthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading OverrideHeight
            (var overrideheightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ShowIfEmptyChallengeLeague
            (var showifemptychallengeleagueLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ShowIfEmptyStandard
            (var showifemptystandardLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading RenamedVersion
            (var renamedversionLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading BaseVersion
            (var baseversionLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading IsAlternateArt
            (var isalternateartLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueStashLayoutDat()
            {
                WordsKey = wordskeyLoading,
                ItemVisualIdentityKey = itemvisualidentitykeyLoading,
                UniqueStashTypesKey = uniquestashtypeskeyLoading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                OverrideWidth = overridewidthLoading,
                OverrideHeight = overrideheightLoading,
                ShowIfEmptyChallengeLeague = showifemptychallengeleagueLoading,
                ShowIfEmptyStandard = showifemptystandardLoading,
                RenamedVersion = renamedversionLoading,
                BaseVersion = baseversionLoading,
                IsAlternateArt = isalternateartLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
