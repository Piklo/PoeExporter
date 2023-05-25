using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UltimatumTrialMasterAudioDat"/> related data and helper methods.
/// </summary>
public sealed class UltimatumTrialMasterAudioRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UltimatumTrialMasterAudioDat> Items { get; }

    private Dictionary<string, List<UltimatumTrialMasterAudioDat>>? byId;
    private Dictionary<int, List<UltimatumTrialMasterAudioDat>>? byVariant;
    private Dictionary<int, List<UltimatumTrialMasterAudioDat>>? byUnknown12;
    private Dictionary<int, List<UltimatumTrialMasterAudioDat>>? byUnknown16;
    private Dictionary<int, List<UltimatumTrialMasterAudioDat>>? byTextAudio;
    private Dictionary<int, List<UltimatumTrialMasterAudioDat>>? byRoundsMin;
    private Dictionary<int, List<UltimatumTrialMasterAudioDat>>? byRoundsMax;

    /// <summary>
    /// Initializes a new instance of the <see cref="UltimatumTrialMasterAudioRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UltimatumTrialMasterAudioRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out UltimatumTrialMasterAudioDat? item)
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
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<UltimatumTrialMasterAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
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
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumTrialMasterAudioDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumTrialMasterAudioDat>>();
        }

        var items = new List<ResultItem<string, UltimatumTrialMasterAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumTrialMasterAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.Variant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVariant(int? key, out UltimatumTrialMasterAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVariant(key, out var items))
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
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.Variant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVariant(int? key, out IReadOnlyList<UltimatumTrialMasterAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        if (byVariant is null)
        {
            byVariant = new();
            foreach (var item in Items)
            {
                var itemKey = item.Variant;

                if (!byVariant.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byVariant.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byVariant.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.byVariant"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumTrialMasterAudioDat>> GetManyToManyByVariant(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumTrialMasterAudioDat>>();
        }

        var items = new List<ResultItem<int, UltimatumTrialMasterAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVariant(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumTrialMasterAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out UltimatumTrialMasterAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown12(key, out var items))
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
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<UltimatumTrialMasterAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;

                if (!byUnknown12.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown12.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumTrialMasterAudioDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumTrialMasterAudioDat>>();
        }

        var items = new List<ResultItem<int, UltimatumTrialMasterAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumTrialMasterAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out UltimatumTrialMasterAudioDat? item)
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
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<UltimatumTrialMasterAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
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
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumTrialMasterAudioDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumTrialMasterAudioDat>>();
        }

        var items = new List<ResultItem<int, UltimatumTrialMasterAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumTrialMasterAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudio(int? key, out UltimatumTrialMasterAudioDat? item)
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
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudio(int? key, out IReadOnlyList<UltimatumTrialMasterAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
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
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.byTextAudio"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumTrialMasterAudioDat>> GetManyToManyByTextAudio(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumTrialMasterAudioDat>>();
        }

        var items = new List<ResultItem<int, UltimatumTrialMasterAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudio(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumTrialMasterAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.RoundsMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRoundsMin(int? key, out UltimatumTrialMasterAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRoundsMin(key, out var items))
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
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.RoundsMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRoundsMin(int? key, out IReadOnlyList<UltimatumTrialMasterAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        if (byRoundsMin is null)
        {
            byRoundsMin = new();
            foreach (var item in Items)
            {
                var itemKey = item.RoundsMin;

                if (!byRoundsMin.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRoundsMin.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRoundsMin.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.byRoundsMin"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumTrialMasterAudioDat>> GetManyToManyByRoundsMin(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumTrialMasterAudioDat>>();
        }

        var items = new List<ResultItem<int, UltimatumTrialMasterAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRoundsMin(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumTrialMasterAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.RoundsMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRoundsMax(int? key, out UltimatumTrialMasterAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRoundsMax(key, out var items))
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
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.RoundsMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRoundsMax(int? key, out IReadOnlyList<UltimatumTrialMasterAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        if (byRoundsMax is null)
        {
            byRoundsMax = new();
            foreach (var item in Items)
            {
                var itemKey = item.RoundsMax;

                if (!byRoundsMax.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRoundsMax.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRoundsMax.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumTrialMasterAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumTrialMasterAudioDat"/> with <see cref="UltimatumTrialMasterAudioDat.byRoundsMax"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumTrialMasterAudioDat>> GetManyToManyByRoundsMax(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumTrialMasterAudioDat>>();
        }

        var items = new List<ResultItem<int, UltimatumTrialMasterAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRoundsMax(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumTrialMasterAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UltimatumTrialMasterAudioDat[] Load()
    {
        const string filePath = "Data/UltimatumTrialMasterAudio.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UltimatumTrialMasterAudioDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Variant
            (var variantLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading RoundsMin
            (var roundsminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RoundsMax
            (var roundsmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UltimatumTrialMasterAudioDat()
            {
                Id = idLoading,
                Variant = variantLoading,
                Unknown12 = unknown12Loading,
                Unknown16 = unknown16Loading,
                TextAudio = textaudioLoading,
                RoundsMin = roundsminLoading,
                RoundsMax = roundsmaxLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
