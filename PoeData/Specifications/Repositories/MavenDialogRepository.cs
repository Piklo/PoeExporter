using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MavenDialogDat"/> related data and helper methods.
/// </summary>
public sealed class MavenDialogRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MavenDialogDat> Items { get; }

    private Dictionary<string, List<MavenDialogDat>>? byId;
    private Dictionary<int, List<MavenDialogDat>>? byTextAudioT1;
    private Dictionary<int, List<MavenDialogDat>>? byTextAudioT2;
    private Dictionary<int, List<MavenDialogDat>>? byTextAudioT3;
    private Dictionary<int, List<MavenDialogDat>>? byTextAudioT4;
    private Dictionary<int, List<MavenDialogDat>>? byTextAudioT5;
    private Dictionary<bool, List<MavenDialogDat>>? byUnknown88;
    private Dictionary<int, List<MavenDialogDat>>? byTextAudioT6;

    /// <summary>
    /// Initializes a new instance of the <see cref="MavenDialogRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MavenDialogRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MavenDialogDat? item)
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
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MavenDialogDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenDialogDat>();
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
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MavenDialogDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MavenDialogDat>>();
        }

        var items = new List<ResultItem<string, MavenDialogDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MavenDialogDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudioT1(int? key, out MavenDialogDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTextAudioT1(key, out var items))
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
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudioT1(int? key, out IReadOnlyList<MavenDialogDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        if (byTextAudioT1 is null)
        {
            byTextAudioT1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextAudioT1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTextAudioT1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTextAudioT1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTextAudioT1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.byTextAudioT1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenDialogDat>> GetManyToManyByTextAudioT1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenDialogDat>>();
        }

        var items = new List<ResultItem<int, MavenDialogDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudioT1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenDialogDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudioT2(int? key, out MavenDialogDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTextAudioT2(key, out var items))
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
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudioT2(int? key, out IReadOnlyList<MavenDialogDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        if (byTextAudioT2 is null)
        {
            byTextAudioT2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextAudioT2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTextAudioT2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTextAudioT2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTextAudioT2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.byTextAudioT2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenDialogDat>> GetManyToManyByTextAudioT2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenDialogDat>>();
        }

        var items = new List<ResultItem<int, MavenDialogDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudioT2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenDialogDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudioT3(int? key, out MavenDialogDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTextAudioT3(key, out var items))
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
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudioT3(int? key, out IReadOnlyList<MavenDialogDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        if (byTextAudioT3 is null)
        {
            byTextAudioT3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextAudioT3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTextAudioT3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTextAudioT3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTextAudioT3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.byTextAudioT3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenDialogDat>> GetManyToManyByTextAudioT3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenDialogDat>>();
        }

        var items = new List<ResultItem<int, MavenDialogDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudioT3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenDialogDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudioT4(int? key, out MavenDialogDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTextAudioT4(key, out var items))
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
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudioT4(int? key, out IReadOnlyList<MavenDialogDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        if (byTextAudioT4 is null)
        {
            byTextAudioT4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextAudioT4;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTextAudioT4.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTextAudioT4.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTextAudioT4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.byTextAudioT4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenDialogDat>> GetManyToManyByTextAudioT4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenDialogDat>>();
        }

        var items = new List<ResultItem<int, MavenDialogDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudioT4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenDialogDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudioT5(int? key, out MavenDialogDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTextAudioT5(key, out var items))
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
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudioT5(int? key, out IReadOnlyList<MavenDialogDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        if (byTextAudioT5 is null)
        {
            byTextAudioT5 = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextAudioT5;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTextAudioT5.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTextAudioT5.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTextAudioT5.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.byTextAudioT5"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenDialogDat>> GetManyToManyByTextAudioT5(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenDialogDat>>();
        }

        var items = new List<ResultItem<int, MavenDialogDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudioT5(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenDialogDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(bool? key, out MavenDialogDat? item)
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
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(bool? key, out IReadOnlyList<MavenDialogDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;

                if (!byUnknown88.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MavenDialogDat>> GetManyToManyByUnknown88(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MavenDialogDat>>();
        }

        var items = new List<ResultItem<bool, MavenDialogDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MavenDialogDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudioT6(int? key, out MavenDialogDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTextAudioT6(key, out var items))
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
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.TextAudioT6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudioT6(int? key, out IReadOnlyList<MavenDialogDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        if (byTextAudioT6 is null)
        {
            byTextAudioT6 = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextAudioT6;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTextAudioT6.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTextAudioT6.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTextAudioT6.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenDialogDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenDialogDat"/> with <see cref="MavenDialogDat.byTextAudioT6"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenDialogDat>> GetManyToManyByTextAudioT6(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenDialogDat>>();
        }

        var items = new List<ResultItem<int, MavenDialogDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudioT6(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenDialogDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MavenDialogDat[] Load()
    {
        const string filePath = "Data/MavenDialog.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MavenDialogDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TextAudioT1
            (var textaudiot1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TextAudioT2
            (var textaudiot2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TextAudioT3
            (var textaudiot3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TextAudioT4
            (var textaudiot4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TextAudioT5
            (var textaudiot5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TextAudioT6
            (var textaudiot6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MavenDialogDat()
            {
                Id = idLoading,
                TextAudioT1 = textaudiot1Loading,
                TextAudioT2 = textaudiot2Loading,
                TextAudioT3 = textaudiot3Loading,
                TextAudioT4 = textaudiot4Loading,
                TextAudioT5 = textaudiot5Loading,
                Unknown88 = unknown88Loading,
                TextAudioT6 = textaudiot6Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
