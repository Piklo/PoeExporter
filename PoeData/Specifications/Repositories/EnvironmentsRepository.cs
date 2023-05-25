using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="EnvironmentsDat"/> related data and helper methods.
/// </summary>
public sealed class EnvironmentsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<EnvironmentsDat> Items { get; }

    private Dictionary<string, List<EnvironmentsDat>>? byId;
    private Dictionary<string, List<EnvironmentsDat>>? byBase_ENVFile;
    private Dictionary<string, List<EnvironmentsDat>>? byCorrupted_ENVFile;
    private Dictionary<int, List<EnvironmentsDat>>? byUnknown24;
    private Dictionary<int, List<EnvironmentsDat>>? byUnknown40;
    private Dictionary<int, List<EnvironmentsDat>>? byUnknown56;
    private Dictionary<int, List<EnvironmentsDat>>? byEnvironmentTransitionsKey;
    private Dictionary<int, List<EnvironmentsDat>>? byPreloadGroup;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal EnvironmentsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out EnvironmentsDat? item)
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
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<EnvironmentsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentsDat>();
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
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EnvironmentsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EnvironmentsDat>>();
        }

        var items = new List<ResultItem<string, EnvironmentsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EnvironmentsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Base_ENVFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBase_ENVFile(string? key, out EnvironmentsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBase_ENVFile(key, out var items))
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
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Base_ENVFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBase_ENVFile(string? key, out IReadOnlyList<EnvironmentsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        if (byBase_ENVFile is null)
        {
            byBase_ENVFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Base_ENVFile;

                if (!byBase_ENVFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBase_ENVFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBase_ENVFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.byBase_ENVFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EnvironmentsDat>> GetManyToManyByBase_ENVFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EnvironmentsDat>>();
        }

        var items = new List<ResultItem<string, EnvironmentsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBase_ENVFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EnvironmentsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Corrupted_ENVFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCorrupted_ENVFile(string? key, out EnvironmentsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCorrupted_ENVFile(key, out var items))
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
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Corrupted_ENVFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCorrupted_ENVFile(string? key, out IReadOnlyList<EnvironmentsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        if (byCorrupted_ENVFile is null)
        {
            byCorrupted_ENVFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Corrupted_ENVFile;

                if (!byCorrupted_ENVFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCorrupted_ENVFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCorrupted_ENVFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.byCorrupted_ENVFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EnvironmentsDat>> GetManyToManyByCorrupted_ENVFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EnvironmentsDat>>();
        }

        var items = new List<ResultItem<string, EnvironmentsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCorrupted_ENVFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EnvironmentsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out EnvironmentsDat? item)
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
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<EnvironmentsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentsDat>();
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
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EnvironmentsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EnvironmentsDat>>();
        }

        var items = new List<ResultItem<int, EnvironmentsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EnvironmentsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out EnvironmentsDat? item)
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
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<EnvironmentsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown40.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EnvironmentsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EnvironmentsDat>>();
        }

        var items = new List<ResultItem<int, EnvironmentsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EnvironmentsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out EnvironmentsDat? item)
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
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<EnvironmentsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown56.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown56.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EnvironmentsDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EnvironmentsDat>>();
        }

        var items = new List<ResultItem<int, EnvironmentsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EnvironmentsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.EnvironmentTransitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEnvironmentTransitionsKey(int? key, out EnvironmentsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEnvironmentTransitionsKey(key, out var items))
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
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.EnvironmentTransitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEnvironmentTransitionsKey(int? key, out IReadOnlyList<EnvironmentsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        if (byEnvironmentTransitionsKey is null)
        {
            byEnvironmentTransitionsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.EnvironmentTransitionsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEnvironmentTransitionsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEnvironmentTransitionsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEnvironmentTransitionsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.byEnvironmentTransitionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EnvironmentsDat>> GetManyToManyByEnvironmentTransitionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EnvironmentsDat>>();
        }

        var items = new List<ResultItem<int, EnvironmentsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEnvironmentTransitionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EnvironmentsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.PreloadGroup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPreloadGroup(int? key, out EnvironmentsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPreloadGroup(key, out var items))
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
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.PreloadGroup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPreloadGroup(int? key, out IReadOnlyList<EnvironmentsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        if (byPreloadGroup is null)
        {
            byPreloadGroup = new();
            foreach (var item in Items)
            {
                var itemKey = item.PreloadGroup;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPreloadGroup.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPreloadGroup.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPreloadGroup.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EnvironmentsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentsDat"/> with <see cref="EnvironmentsDat.byPreloadGroup"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EnvironmentsDat>> GetManyToManyByPreloadGroup(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EnvironmentsDat>>();
        }

        var items = new List<ResultItem<int, EnvironmentsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPreloadGroup(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EnvironmentsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private EnvironmentsDat[] Load()
    {
        const string filePath = "Data/Environments.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EnvironmentsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Base_ENVFile
            (var base_envfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Corrupted_ENVFile
            (var corrupted_envfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading EnvironmentTransitionsKey
            (var environmenttransitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PreloadGroup
            (var preloadgroupLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EnvironmentsDat()
            {
                Id = idLoading,
                Base_ENVFile = base_envfileLoading,
                Corrupted_ENVFile = corrupted_envfileLoading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                EnvironmentTransitionsKey = environmenttransitionskeyLoading,
                PreloadGroup = preloadgroupLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
