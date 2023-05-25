using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="NPCDialogueStylesDat"/> related data and helper methods.
/// </summary>
public sealed class NPCDialogueStylesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<NPCDialogueStylesDat> Items { get; }

    private Dictionary<string, List<NPCDialogueStylesDat>>? byId;
    private Dictionary<string, List<NPCDialogueStylesDat>>? byHeaderBaseFile;
    private Dictionary<string, List<NPCDialogueStylesDat>>? byButtomFile;
    private Dictionary<string, List<NPCDialogueStylesDat>>? byBannerFiles;
    private Dictionary<string, List<NPCDialogueStylesDat>>? byHeaderFiles;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown56;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown60;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown64;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown68;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown72;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown76;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown92;
    private Dictionary<string, List<NPCDialogueStylesDat>>? byUnknown100;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown108;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown124;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown140;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown144;
    private Dictionary<float, List<NPCDialogueStylesDat>>? byUnknown148;
    private Dictionary<string, List<NPCDialogueStylesDat>>? byUnknown152;
    private Dictionary<string, List<NPCDialogueStylesDat>>? byUnknown160;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown168;
    private Dictionary<int, List<NPCDialogueStylesDat>>? byUnknown172;

    /// <summary>
    /// Initializes a new instance of the <see cref="NPCDialogueStylesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal NPCDialogueStylesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out NPCDialogueStylesDat? item)
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
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
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCDialogueStylesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<string, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.HeaderBaseFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeaderBaseFile(string? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeaderBaseFile(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.HeaderBaseFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeaderBaseFile(string? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byHeaderBaseFile is null)
        {
            byHeaderBaseFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeaderBaseFile;

                if (!byHeaderBaseFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeaderBaseFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeaderBaseFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byHeaderBaseFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCDialogueStylesDat>> GetManyToManyByHeaderBaseFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<string, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeaderBaseFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.ButtomFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByButtomFile(string? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByButtomFile(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.ButtomFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByButtomFile(string? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byButtomFile is null)
        {
            byButtomFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.ButtomFile;

                if (!byButtomFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byButtomFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byButtomFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byButtomFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCDialogueStylesDat>> GetManyToManyByButtomFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<string, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByButtomFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.BannerFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBannerFiles(string? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBannerFiles(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.BannerFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBannerFiles(string? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byBannerFiles is null)
        {
            byBannerFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.BannerFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byBannerFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBannerFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBannerFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byBannerFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCDialogueStylesDat>> GetManyToManyByBannerFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<string, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBannerFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.HeaderFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeaderFiles(string? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeaderFiles(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.HeaderFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeaderFiles(string? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byHeaderFiles is null)
        {
            byHeaderFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeaderFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byHeaderFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byHeaderFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byHeaderFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byHeaderFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCDialogueStylesDat>> GetManyToManyByHeaderFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<string, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeaderFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out NPCDialogueStylesDat? item)
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
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
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(int? key, out NPCDialogueStylesDat? item)
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
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
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown60(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out NPCDialogueStylesDat? item)
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
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
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(int? key, out NPCDialogueStylesDat? item)
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;

                if (!byUnknown68.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown68(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(int? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown76(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown76 is null)
        {
            byUnknown76 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown76;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown76.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown76.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown76.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown76(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(int? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown92(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown92 is null)
        {
            byUnknown92 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown92;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown92.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown92.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown92.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown92(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(string? key, out NPCDialogueStylesDat? item)
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(string? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
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

        if (!byUnknown100.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCDialogueStylesDat>> GetManyToManyByUnknown100(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<string, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(int? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown108(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown108 is null)
        {
            byUnknown108 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown108;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown108.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown108.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown108.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown108(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown124(int? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown124(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown124(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown124 is null)
        {
            byUnknown124 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown124;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown124.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown124.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown124.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown124"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown124(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown124(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown140"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown140(int? key, out NPCDialogueStylesDat? item)
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown140"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown140(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
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
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown140"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown140(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown140(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown144(int? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown144(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown144(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown144 is null)
        {
            byUnknown144 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown144;

                if (!byUnknown144.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown144.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown144.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown144"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown144(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown144(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown148(float? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown148(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown148(float? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown148 is null)
        {
            byUnknown148 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown148;

                if (!byUnknown148.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown148.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown148.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown148"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, NPCDialogueStylesDat>> GetManyToManyByUnknown148(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<float, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown148(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown152"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown152(string? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown152(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown152"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown152(string? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown152 is null)
        {
            byUnknown152 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown152;

                if (!byUnknown152.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown152.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown152.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown152"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCDialogueStylesDat>> GetManyToManyByUnknown152(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<string, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown152(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown160(string? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown160(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown160(string? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown160 is null)
        {
            byUnknown160 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown160;

                if (!byUnknown160.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown160.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown160.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown160"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCDialogueStylesDat>> GetManyToManyByUnknown160(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<string, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown160(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown168"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown168(int? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown168(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown168"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown168(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown168 is null)
        {
            byUnknown168 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown168;

                if (!byUnknown168.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown168.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown168.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown168"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown168(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown168(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown172(int? key, out NPCDialogueStylesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown172(key, out var items))
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
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown172(int? key, out IReadOnlyList<NPCDialogueStylesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        if (byUnknown172 is null)
        {
            byUnknown172 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown172;

                if (!byUnknown172.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown172.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown172.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCDialogueStylesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCDialogueStylesDat"/> with <see cref="NPCDialogueStylesDat.byUnknown172"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCDialogueStylesDat>> GetManyToManyByUnknown172(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCDialogueStylesDat>>();
        }

        var items = new List<ResultItem<int, NPCDialogueStylesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown172(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCDialogueStylesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private NPCDialogueStylesDat[] Load()
    {
        const string filePath = "Data/NPCDialogueStyles.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCDialogueStylesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeaderBaseFile
            (var headerbasefileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ButtomFile
            (var buttomfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BannerFiles
            (var tempbannerfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var bannerfilesLoading = tempbannerfilesLoading.AsReadOnly();

            // loading HeaderFiles
            (var tempheaderfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var headerfilesLoading = tempheaderfilesLoading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var tempunknown76Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown76Loading = tempunknown76Loading.AsReadOnly();

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var tempunknown108Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown108Loading = tempunknown108Loading.AsReadOnly();

            // loading Unknown124
            (var tempunknown124Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown124Loading = tempunknown124Loading.AsReadOnly();

            // loading Unknown140
            (var unknown140Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown152
            (var unknown152Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown160
            (var unknown160Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown168
            (var unknown168Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown172
            (var unknown172Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCDialogueStylesDat()
            {
                Id = idLoading,
                HeaderBaseFile = headerbasefileLoading,
                ButtomFile = buttomfileLoading,
                BannerFiles = bannerfilesLoading,
                HeaderFiles = headerfilesLoading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                Unknown92 = unknown92Loading,
                Unknown100 = unknown100Loading,
                Unknown108 = unknown108Loading,
                Unknown124 = unknown124Loading,
                Unknown140 = unknown140Loading,
                Unknown144 = unknown144Loading,
                Unknown148 = unknown148Loading,
                Unknown152 = unknown152Loading,
                Unknown160 = unknown160Loading,
                Unknown168 = unknown168Loading,
                Unknown172 = unknown172Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
