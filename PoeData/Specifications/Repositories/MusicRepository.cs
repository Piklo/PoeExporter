using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MusicDat"/> related data and helper methods.
/// </summary>
public sealed class MusicRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MusicDat> Items { get; }

    private Dictionary<string, List<MusicDat>>? byId;
    private Dictionary<string, List<MusicDat>>? bySoundFile;
    private Dictionary<string, List<MusicDat>>? byBankFile;
    private Dictionary<int, List<MusicDat>>? byHASH16;
    private Dictionary<bool, List<MusicDat>>? byIsAvailableInHideout;
    private Dictionary<string, List<MusicDat>>? byName;
    private Dictionary<string, List<MusicDat>>? byUnknown37;
    private Dictionary<int, List<MusicDat>>? byMusicCategories;
    private Dictionary<bool, List<MusicDat>>? byUnknown61;
    private Dictionary<int, List<MusicDat>>? byUnknown62;

    /// <summary>
    /// Initializes a new instance of the <see cref="MusicRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MusicRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MusicDat? item)
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
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MusicDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MusicDat>();
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
            items = Array.Empty<MusicDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MusicDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MusicDat>>();
        }

        var items = new List<ResultItem<string, MusicDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MusicDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.SoundFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySoundFile(string? key, out MusicDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySoundFile(key, out var items))
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
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.SoundFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySoundFile(string? key, out IReadOnlyList<MusicDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        if (bySoundFile is null)
        {
            bySoundFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.SoundFile;

                if (!bySoundFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySoundFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySoundFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.bySoundFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MusicDat>> GetManyToManyBySoundFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MusicDat>>();
        }

        var items = new List<ResultItem<string, MusicDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySoundFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MusicDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.BankFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBankFile(string? key, out MusicDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBankFile(key, out var items))
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
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.BankFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBankFile(string? key, out IReadOnlyList<MusicDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        if (byBankFile is null)
        {
            byBankFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.BankFile;

                if (!byBankFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBankFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBankFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.byBankFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MusicDat>> GetManyToManyByBankFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MusicDat>>();
        }

        var items = new List<ResultItem<string, MusicDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBankFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MusicDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH16(int? key, out MusicDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHASH16(key, out var items))
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
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH16(int? key, out IReadOnlyList<MusicDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        if (byHASH16 is null)
        {
            byHASH16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HASH16;

                if (!byHASH16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHASH16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHASH16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.byHASH16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MusicDat>> GetManyToManyByHASH16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MusicDat>>();
        }

        var items = new List<ResultItem<int, MusicDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MusicDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.IsAvailableInHideout"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsAvailableInHideout(bool? key, out MusicDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsAvailableInHideout(key, out var items))
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
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.IsAvailableInHideout"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsAvailableInHideout(bool? key, out IReadOnlyList<MusicDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        if (byIsAvailableInHideout is null)
        {
            byIsAvailableInHideout = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsAvailableInHideout;

                if (!byIsAvailableInHideout.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsAvailableInHideout.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsAvailableInHideout.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.byIsAvailableInHideout"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MusicDat>> GetManyToManyByIsAvailableInHideout(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MusicDat>>();
        }

        var items = new List<ResultItem<bool, MusicDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsAvailableInHideout(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MusicDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out MusicDat? item)
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
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<MusicDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MusicDat>();
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
            items = Array.Empty<MusicDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MusicDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MusicDat>>();
        }

        var items = new List<ResultItem<string, MusicDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MusicDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown37(string? key, out MusicDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown37(key, out var items))
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
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown37(string? key, out IReadOnlyList<MusicDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        if (byUnknown37 is null)
        {
            byUnknown37 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown37;

                if (!byUnknown37.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown37.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown37.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.byUnknown37"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MusicDat>> GetManyToManyByUnknown37(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MusicDat>>();
        }

        var items = new List<ResultItem<string, MusicDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown37(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MusicDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.MusicCategories"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMusicCategories(int? key, out MusicDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMusicCategories(key, out var items))
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
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.MusicCategories"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMusicCategories(int? key, out IReadOnlyList<MusicDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        if (byMusicCategories is null)
        {
            byMusicCategories = new();
            foreach (var item in Items)
            {
                var itemKey = item.MusicCategories;
                foreach (var listKey in itemKey)
                {
                    if (!byMusicCategories.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMusicCategories.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMusicCategories.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.byMusicCategories"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MusicDat>> GetManyToManyByMusicCategories(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MusicDat>>();
        }

        var items = new List<ResultItem<int, MusicDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMusicCategories(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MusicDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(bool? key, out MusicDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown61(key, out var items))
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
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(bool? key, out IReadOnlyList<MusicDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        if (byUnknown61 is null)
        {
            byUnknown61 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown61;

                if (!byUnknown61.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown61.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown61.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MusicDat>> GetManyToManyByUnknown61(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MusicDat>>();
        }

        var items = new List<ResultItem<bool, MusicDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MusicDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown62(int? key, out MusicDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown62(key, out var items))
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
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown62(int? key, out IReadOnlyList<MusicDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        if (byUnknown62 is null)
        {
            byUnknown62 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown62;

                if (!byUnknown62.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown62.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown62.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MusicDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MusicDat"/> with <see cref="MusicDat.byUnknown62"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MusicDat>> GetManyToManyByUnknown62(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MusicDat>>();
        }

        var items = new List<ResultItem<int, MusicDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown62(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MusicDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MusicDat[] Load()
    {
        const string filePath = "Data/Music.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MusicDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SoundFile
            (var soundfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BankFile
            (var bankfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsAvailableInHideout
            (var isavailableinhideoutLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MusicCategories
            (var tempmusiccategoriesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var musiccategoriesLoading = tempmusiccategoriesLoading.AsReadOnly();

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MusicDat()
            {
                Id = idLoading,
                SoundFile = soundfileLoading,
                BankFile = bankfileLoading,
                HASH16 = hash16Loading,
                IsAvailableInHideout = isavailableinhideoutLoading,
                Name = nameLoading,
                Unknown37 = unknown37Loading,
                MusicCategories = musiccategoriesLoading,
                Unknown61 = unknown61Loading,
                Unknown62 = unknown62Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
