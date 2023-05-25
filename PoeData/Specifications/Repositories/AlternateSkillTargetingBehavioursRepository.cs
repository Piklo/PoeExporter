using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AlternateSkillTargetingBehavioursDat"/> related data and helper methods.
/// </summary>
public sealed class AlternateSkillTargetingBehavioursRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AlternateSkillTargetingBehavioursDat> Items { get; }

    private Dictionary<string, List<AlternateSkillTargetingBehavioursDat>>? byId;
    private Dictionary<int, List<AlternateSkillTargetingBehavioursDat>>? byUnknown8;
    private Dictionary<int, List<AlternateSkillTargetingBehavioursDat>>? byClientStrings;
    private Dictionary<int, List<AlternateSkillTargetingBehavioursDat>>? byUnknown28;
    private Dictionary<int, List<AlternateSkillTargetingBehavioursDat>>? byUnknown32;
    private Dictionary<int, List<AlternateSkillTargetingBehavioursDat>>? byUnknown36;
    private Dictionary<int, List<AlternateSkillTargetingBehavioursDat>>? byUnknown40;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlternateSkillTargetingBehavioursRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AlternateSkillTargetingBehavioursRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AlternateSkillTargetingBehavioursDat? item)
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
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AlternateSkillTargetingBehavioursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
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
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AlternateSkillTargetingBehavioursDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AlternateSkillTargetingBehavioursDat>>();
        }

        var items = new List<ResultItem<string, AlternateSkillTargetingBehavioursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AlternateSkillTargetingBehavioursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out AlternateSkillTargetingBehavioursDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<AlternateSkillTargetingBehavioursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;

                if (!byUnknown8.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateSkillTargetingBehavioursDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();
        }

        var items = new List<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateSkillTargetingBehavioursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.ClientStrings"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClientStrings(int? key, out AlternateSkillTargetingBehavioursDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClientStrings(key, out var items))
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
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.ClientStrings"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClientStrings(int? key, out IReadOnlyList<AlternateSkillTargetingBehavioursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
            return false;
        }

        if (byClientStrings is null)
        {
            byClientStrings = new();
            foreach (var item in Items)
            {
                var itemKey = item.ClientStrings;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byClientStrings.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byClientStrings.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byClientStrings.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.byClientStrings"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateSkillTargetingBehavioursDat>> GetManyToManyByClientStrings(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();
        }

        var items = new List<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClientStrings(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateSkillTargetingBehavioursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out AlternateSkillTargetingBehavioursDat? item)
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
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<AlternateSkillTargetingBehavioursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
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
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateSkillTargetingBehavioursDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();
        }

        var items = new List<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateSkillTargetingBehavioursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out AlternateSkillTargetingBehavioursDat? item)
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
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<AlternateSkillTargetingBehavioursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
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
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateSkillTargetingBehavioursDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();
        }

        var items = new List<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateSkillTargetingBehavioursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out AlternateSkillTargetingBehavioursDat? item)
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
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<AlternateSkillTargetingBehavioursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
            return false;
        }

        if (byUnknown36 is null)
        {
            byUnknown36 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown36;

                if (!byUnknown36.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown36.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown36.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateSkillTargetingBehavioursDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();
        }

        var items = new List<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateSkillTargetingBehavioursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out AlternateSkillTargetingBehavioursDat? item)
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
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<AlternateSkillTargetingBehavioursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
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
            items = Array.Empty<AlternateSkillTargetingBehavioursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateSkillTargetingBehavioursDat"/> with <see cref="AlternateSkillTargetingBehavioursDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateSkillTargetingBehavioursDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();
        }

        var items = new List<ResultItem<int, AlternateSkillTargetingBehavioursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateSkillTargetingBehavioursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AlternateSkillTargetingBehavioursDat[] Load()
    {
        const string filePath = "Data/AlternateSkillTargetingBehaviours.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AlternateSkillTargetingBehavioursDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ClientStrings
            (var clientstringsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AlternateSkillTargetingBehavioursDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                ClientStrings = clientstringsLoading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
