using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AnimationDat"/> related data and helper methods.
/// </summary>
public sealed class AnimationRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AnimationDat> Items { get; }

    private Dictionary<string, List<AnimationDat>>? byId;
    private Dictionary<bool, List<AnimationDat>>? byUnknown8;
    private Dictionary<bool, List<AnimationDat>>? byUnknown9;
    private Dictionary<bool, List<AnimationDat>>? byUnknown10;
    private Dictionary<string, List<AnimationDat>>? byMainhand_AnimationKey;
    private Dictionary<string, List<AnimationDat>>? byOffhand_AnimationKey;
    private Dictionary<bool, List<AnimationDat>>? byUnknown27;
    private Dictionary<int, List<AnimationDat>>? byUnknown28;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnimationRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AnimationRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AnimationDat? item)
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
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AnimationDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AnimationDat>();
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
            items = Array.Empty<AnimationDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AnimationDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AnimationDat>>();
        }

        var items = new List<ResultItem<string, AnimationDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AnimationDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(bool? key, out AnimationDat? item)
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
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(bool? key, out IReadOnlyList<AnimationDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AnimationDat>();
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
            items = Array.Empty<AnimationDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AnimationDat>> GetManyToManyByUnknown8(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AnimationDat>>();
        }

        var items = new List<ResultItem<bool, AnimationDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AnimationDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Unknown9"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown9(bool? key, out AnimationDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown9(key, out var items))
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
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Unknown9"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown9(bool? key, out IReadOnlyList<AnimationDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        if (byUnknown9 is null)
        {
            byUnknown9 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown9;

                if (!byUnknown9.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown9.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown9.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.byUnknown9"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AnimationDat>> GetManyToManyByUnknown9(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AnimationDat>>();
        }

        var items = new List<ResultItem<bool, AnimationDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown9(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AnimationDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Unknown10"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown10(bool? key, out AnimationDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown10(key, out var items))
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
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Unknown10"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown10(bool? key, out IReadOnlyList<AnimationDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        if (byUnknown10 is null)
        {
            byUnknown10 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown10;

                if (!byUnknown10.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown10.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown10.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.byUnknown10"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AnimationDat>> GetManyToManyByUnknown10(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AnimationDat>>();
        }

        var items = new List<ResultItem<bool, AnimationDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown10(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AnimationDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Mainhand_AnimationKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMainhand_AnimationKey(string? key, out AnimationDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMainhand_AnimationKey(key, out var items))
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
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Mainhand_AnimationKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMainhand_AnimationKey(string? key, out IReadOnlyList<AnimationDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        if (byMainhand_AnimationKey is null)
        {
            byMainhand_AnimationKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mainhand_AnimationKey;

                if (!byMainhand_AnimationKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMainhand_AnimationKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMainhand_AnimationKey.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.byMainhand_AnimationKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AnimationDat>> GetManyToManyByMainhand_AnimationKey(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AnimationDat>>();
        }

        var items = new List<ResultItem<string, AnimationDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMainhand_AnimationKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AnimationDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Offhand_AnimationKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOffhand_AnimationKey(string? key, out AnimationDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOffhand_AnimationKey(key, out var items))
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
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Offhand_AnimationKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOffhand_AnimationKey(string? key, out IReadOnlyList<AnimationDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        if (byOffhand_AnimationKey is null)
        {
            byOffhand_AnimationKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Offhand_AnimationKey;

                if (!byOffhand_AnimationKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOffhand_AnimationKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOffhand_AnimationKey.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.byOffhand_AnimationKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AnimationDat>> GetManyToManyByOffhand_AnimationKey(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AnimationDat>>();
        }

        var items = new List<ResultItem<string, AnimationDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOffhand_AnimationKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AnimationDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Unknown27"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown27(bool? key, out AnimationDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown27(key, out var items))
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
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Unknown27"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown27(bool? key, out IReadOnlyList<AnimationDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        if (byUnknown27 is null)
        {
            byUnknown27 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown27;

                if (!byUnknown27.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown27.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown27.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.byUnknown27"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AnimationDat>> GetManyToManyByUnknown27(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AnimationDat>>();
        }

        var items = new List<ResultItem<bool, AnimationDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown27(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AnimationDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out AnimationDat? item)
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
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<AnimationDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown28.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AnimationDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AnimationDat"/> with <see cref="AnimationDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AnimationDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AnimationDat>>();
        }

        var items = new List<ResultItem<int, AnimationDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AnimationDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AnimationDat[] Load()
    {
        const string filePath = "Data/Animation.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AnimationDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown9
            (var unknown9Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown10
            (var unknown10Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Mainhand_AnimationKey
            (var mainhand_animationkeyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Offhand_AnimationKey
            (var offhand_animationkeyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown27
            (var unknown27Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AnimationDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown9 = unknown9Loading,
                Unknown10 = unknown10Loading,
                Mainhand_AnimationKey = mainhand_animationkeyLoading,
                Offhand_AnimationKey = offhand_animationkeyLoading,
                Unknown27 = unknown27Loading,
                Unknown28 = unknown28Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
