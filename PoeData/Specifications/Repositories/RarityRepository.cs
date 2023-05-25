using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="RarityDat"/> related data and helper methods.
/// </summary>
public sealed class RarityRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<RarityDat> Items { get; }

    private Dictionary<string, List<RarityDat>>? byId;
    private Dictionary<int, List<RarityDat>>? byMinMods;
    private Dictionary<int, List<RarityDat>>? byMaxMods;
    private Dictionary<int, List<RarityDat>>? byUnknown16;
    private Dictionary<int, List<RarityDat>>? byMaxPrefix;
    private Dictionary<int, List<RarityDat>>? byUnknown24;
    private Dictionary<int, List<RarityDat>>? byMaxSuffix;
    private Dictionary<string, List<RarityDat>>? byColor;

    /// <summary>
    /// Initializes a new instance of the <see cref="RarityRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal RarityRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out RarityDat? item)
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
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<RarityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RarityDat>();
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
            items = Array.Empty<RarityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RarityDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RarityDat>>();
        }

        var items = new List<ResultItem<string, RarityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RarityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.MinMods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinMods(int? key, out RarityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinMods(key, out var items))
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
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.MinMods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinMods(int? key, out IReadOnlyList<RarityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RarityDat>();
            return false;
        }

        if (byMinMods is null)
        {
            byMinMods = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinMods;

                if (!byMinMods.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinMods.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinMods.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RarityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.byMinMods"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RarityDat>> GetManyToManyByMinMods(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RarityDat>>();
        }

        var items = new List<ResultItem<int, RarityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinMods(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RarityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.MaxMods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxMods(int? key, out RarityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxMods(key, out var items))
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
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.MaxMods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxMods(int? key, out IReadOnlyList<RarityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RarityDat>();
            return false;
        }

        if (byMaxMods is null)
        {
            byMaxMods = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxMods;

                if (!byMaxMods.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxMods.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxMods.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RarityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.byMaxMods"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RarityDat>> GetManyToManyByMaxMods(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RarityDat>>();
        }

        var items = new List<ResultItem<int, RarityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxMods(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RarityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out RarityDat? item)
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
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<RarityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RarityDat>();
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
            items = Array.Empty<RarityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RarityDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RarityDat>>();
        }

        var items = new List<ResultItem<int, RarityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RarityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.MaxPrefix"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxPrefix(int? key, out RarityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxPrefix(key, out var items))
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
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.MaxPrefix"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxPrefix(int? key, out IReadOnlyList<RarityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RarityDat>();
            return false;
        }

        if (byMaxPrefix is null)
        {
            byMaxPrefix = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxPrefix;

                if (!byMaxPrefix.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxPrefix.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxPrefix.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RarityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.byMaxPrefix"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RarityDat>> GetManyToManyByMaxPrefix(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RarityDat>>();
        }

        var items = new List<ResultItem<int, RarityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxPrefix(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RarityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out RarityDat? item)
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
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<RarityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RarityDat>();
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
            items = Array.Empty<RarityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RarityDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RarityDat>>();
        }

        var items = new List<ResultItem<int, RarityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RarityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.MaxSuffix"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxSuffix(int? key, out RarityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxSuffix(key, out var items))
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
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.MaxSuffix"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxSuffix(int? key, out IReadOnlyList<RarityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RarityDat>();
            return false;
        }

        if (byMaxSuffix is null)
        {
            byMaxSuffix = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxSuffix;

                if (!byMaxSuffix.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxSuffix.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxSuffix.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RarityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.byMaxSuffix"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RarityDat>> GetManyToManyByMaxSuffix(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RarityDat>>();
        }

        var items = new List<ResultItem<int, RarityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxSuffix(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RarityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.Color"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByColor(string? key, out RarityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByColor(key, out var items))
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
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.Color"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByColor(string? key, out IReadOnlyList<RarityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RarityDat>();
            return false;
        }

        if (byColor is null)
        {
            byColor = new();
            foreach (var item in Items)
            {
                var itemKey = item.Color;

                if (!byColor.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byColor.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byColor.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RarityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RarityDat"/> with <see cref="RarityDat.byColor"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RarityDat>> GetManyToManyByColor(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RarityDat>>();
        }

        var items = new List<ResultItem<string, RarityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByColor(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RarityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private RarityDat[] Load()
    {
        const string filePath = "Data/Rarity.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RarityDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinMods
            (var minmodsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxMods
            (var maxmodsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxPrefix
            (var maxprefixLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxSuffix
            (var maxsuffixLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Color
            (var colorLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RarityDat()
            {
                Id = idLoading,
                MinMods = minmodsLoading,
                MaxMods = maxmodsLoading,
                Unknown16 = unknown16Loading,
                MaxPrefix = maxprefixLoading,
                Unknown24 = unknown24Loading,
                MaxSuffix = maxsuffixLoading,
                Color = colorLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
