using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CharacterAudioEventsDat"/> related data and helper methods.
/// </summary>
public sealed class CharacterAudioEventsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CharacterAudioEventsDat> Items { get; }

    private Dictionary<string, List<CharacterAudioEventsDat>>? byId;
    private Dictionary<int, List<CharacterAudioEventsDat>>? byEvent;
    private Dictionary<int, List<CharacterAudioEventsDat>>? byUnknown24;
    private Dictionary<int, List<CharacterAudioEventsDat>>? byUnknown28;
    private Dictionary<int, List<CharacterAudioEventsDat>>? byGoddess_CharacterTextAudioKeys;
    private Dictionary<int, List<CharacterAudioEventsDat>>? byJackTheAxe_CharacterTextAudioKeys;
    private Dictionary<bool, List<CharacterAudioEventsDat>>? byUnknown64;
    private Dictionary<bool, List<CharacterAudioEventsDat>>? byUnknown65;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterAudioEventsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CharacterAudioEventsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CharacterAudioEventsDat? item)
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
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CharacterAudioEventsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterAudioEventsDat>();
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
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterAudioEventsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterAudioEventsDat>>();
        }

        var items = new List<ResultItem<string, CharacterAudioEventsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterAudioEventsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Event"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEvent(int? key, out CharacterAudioEventsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEvent(key, out var items))
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
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Event"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEvent(int? key, out IReadOnlyList<CharacterAudioEventsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        if (byEvent is null)
        {
            byEvent = new();
            foreach (var item in Items)
            {
                var itemKey = item.Event;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEvent.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEvent.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEvent.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.byEvent"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterAudioEventsDat>> GetManyToManyByEvent(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterAudioEventsDat>>();
        }

        var items = new List<ResultItem<int, CharacterAudioEventsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEvent(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterAudioEventsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out CharacterAudioEventsDat? item)
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
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<CharacterAudioEventsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterAudioEventsDat>();
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
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterAudioEventsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterAudioEventsDat>>();
        }

        var items = new List<ResultItem<int, CharacterAudioEventsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterAudioEventsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out CharacterAudioEventsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<CharacterAudioEventsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterAudioEventsDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterAudioEventsDat>>();
        }

        var items = new List<ResultItem<int, CharacterAudioEventsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterAudioEventsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Goddess_CharacterTextAudioKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGoddess_CharacterTextAudioKeys(int? key, out CharacterAudioEventsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGoddess_CharacterTextAudioKeys(key, out var items))
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
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Goddess_CharacterTextAudioKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGoddess_CharacterTextAudioKeys(int? key, out IReadOnlyList<CharacterAudioEventsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        if (byGoddess_CharacterTextAudioKeys is null)
        {
            byGoddess_CharacterTextAudioKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Goddess_CharacterTextAudioKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byGoddess_CharacterTextAudioKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byGoddess_CharacterTextAudioKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byGoddess_CharacterTextAudioKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.byGoddess_CharacterTextAudioKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterAudioEventsDat>> GetManyToManyByGoddess_CharacterTextAudioKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterAudioEventsDat>>();
        }

        var items = new List<ResultItem<int, CharacterAudioEventsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGoddess_CharacterTextAudioKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterAudioEventsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.JackTheAxe_CharacterTextAudioKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByJackTheAxe_CharacterTextAudioKeys(int? key, out CharacterAudioEventsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByJackTheAxe_CharacterTextAudioKeys(key, out var items))
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
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.JackTheAxe_CharacterTextAudioKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByJackTheAxe_CharacterTextAudioKeys(int? key, out IReadOnlyList<CharacterAudioEventsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        if (byJackTheAxe_CharacterTextAudioKeys is null)
        {
            byJackTheAxe_CharacterTextAudioKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.JackTheAxe_CharacterTextAudioKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byJackTheAxe_CharacterTextAudioKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byJackTheAxe_CharacterTextAudioKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byJackTheAxe_CharacterTextAudioKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.byJackTheAxe_CharacterTextAudioKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterAudioEventsDat>> GetManyToManyByJackTheAxe_CharacterTextAudioKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterAudioEventsDat>>();
        }

        var items = new List<ResultItem<int, CharacterAudioEventsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByJackTheAxe_CharacterTextAudioKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterAudioEventsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(bool? key, out CharacterAudioEventsDat? item)
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
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(bool? key, out IReadOnlyList<CharacterAudioEventsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CharacterAudioEventsDat>> GetManyToManyByUnknown64(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CharacterAudioEventsDat>>();
        }

        var items = new List<ResultItem<bool, CharacterAudioEventsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CharacterAudioEventsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown65(bool? key, out CharacterAudioEventsDat? item)
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
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown65(bool? key, out IReadOnlyList<CharacterAudioEventsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterAudioEventsDat>();
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
            items = Array.Empty<CharacterAudioEventsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterAudioEventsDat"/> with <see cref="CharacterAudioEventsDat.byUnknown65"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CharacterAudioEventsDat>> GetManyToManyByUnknown65(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CharacterAudioEventsDat>>();
        }

        var items = new List<ResultItem<bool, CharacterAudioEventsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown65(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CharacterAudioEventsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CharacterAudioEventsDat[] Load()
    {
        const string filePath = "Data/CharacterAudioEvents.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterAudioEventsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Event
            (var eventLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Goddess_CharacterTextAudioKeys
            (var tempgoddess_charactertextaudiokeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var goddess_charactertextaudiokeysLoading = tempgoddess_charactertextaudiokeysLoading.AsReadOnly();

            // loading JackTheAxe_CharacterTextAudioKeys
            (var tempjacktheaxe_charactertextaudiokeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var jacktheaxe_charactertextaudiokeysLoading = tempjacktheaxe_charactertextaudiokeysLoading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterAudioEventsDat()
            {
                Id = idLoading,
                Event = eventLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Goddess_CharacterTextAudioKeys = goddess_charactertextaudiokeysLoading,
                JackTheAxe_CharacterTextAudioKeys = jacktheaxe_charactertextaudiokeysLoading,
                Unknown64 = unknown64Loading,
                Unknown65 = unknown65Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
