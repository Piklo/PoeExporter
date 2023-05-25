using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PlayerTradeWhisperFormatsDat"/> related data and helper methods.
/// </summary>
public sealed class PlayerTradeWhisperFormatsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PlayerTradeWhisperFormatsDat> Items { get; }

    private Dictionary<string, List<PlayerTradeWhisperFormatsDat>>? byId;
    private Dictionary<string, List<PlayerTradeWhisperFormatsDat>>? byWhisper;
    private Dictionary<bool, List<PlayerTradeWhisperFormatsDat>>? byInStash;
    private Dictionary<bool, List<PlayerTradeWhisperFormatsDat>>? byIsPriced;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerTradeWhisperFormatsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PlayerTradeWhisperFormatsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out PlayerTradeWhisperFormatsDat? item)
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
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<PlayerTradeWhisperFormatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerTradeWhisperFormatsDat>();
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
            items = Array.Empty<PlayerTradeWhisperFormatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PlayerTradeWhisperFormatsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PlayerTradeWhisperFormatsDat>>();
        }

        var items = new List<ResultItem<string, PlayerTradeWhisperFormatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PlayerTradeWhisperFormatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.Whisper"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWhisper(string? key, out PlayerTradeWhisperFormatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWhisper(key, out var items))
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
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.Whisper"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWhisper(string? key, out IReadOnlyList<PlayerTradeWhisperFormatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerTradeWhisperFormatsDat>();
            return false;
        }

        if (byWhisper is null)
        {
            byWhisper = new();
            foreach (var item in Items)
            {
                var itemKey = item.Whisper;

                if (!byWhisper.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWhisper.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWhisper.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PlayerTradeWhisperFormatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.byWhisper"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PlayerTradeWhisperFormatsDat>> GetManyToManyByWhisper(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PlayerTradeWhisperFormatsDat>>();
        }

        var items = new List<ResultItem<string, PlayerTradeWhisperFormatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWhisper(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PlayerTradeWhisperFormatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.InStash"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInStash(bool? key, out PlayerTradeWhisperFormatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInStash(key, out var items))
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
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.InStash"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInStash(bool? key, out IReadOnlyList<PlayerTradeWhisperFormatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerTradeWhisperFormatsDat>();
            return false;
        }

        if (byInStash is null)
        {
            byInStash = new();
            foreach (var item in Items)
            {
                var itemKey = item.InStash;

                if (!byInStash.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInStash.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInStash.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PlayerTradeWhisperFormatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.byInStash"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PlayerTradeWhisperFormatsDat>> GetManyToManyByInStash(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PlayerTradeWhisperFormatsDat>>();
        }

        var items = new List<ResultItem<bool, PlayerTradeWhisperFormatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInStash(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PlayerTradeWhisperFormatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.IsPriced"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsPriced(bool? key, out PlayerTradeWhisperFormatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsPriced(key, out var items))
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
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.IsPriced"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsPriced(bool? key, out IReadOnlyList<PlayerTradeWhisperFormatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerTradeWhisperFormatsDat>();
            return false;
        }

        if (byIsPriced is null)
        {
            byIsPriced = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsPriced;

                if (!byIsPriced.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsPriced.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsPriced.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PlayerTradeWhisperFormatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerTradeWhisperFormatsDat"/> with <see cref="PlayerTradeWhisperFormatsDat.byIsPriced"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PlayerTradeWhisperFormatsDat>> GetManyToManyByIsPriced(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PlayerTradeWhisperFormatsDat>>();
        }

        var items = new List<ResultItem<bool, PlayerTradeWhisperFormatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsPriced(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PlayerTradeWhisperFormatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PlayerTradeWhisperFormatsDat[] Load()
    {
        const string filePath = "Data/PlayerTradeWhisperFormats.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PlayerTradeWhisperFormatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Whisper
            (var whisperLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading InStash
            (var instashLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsPriced
            (var ispricedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PlayerTradeWhisperFormatsDat()
            {
                Id = idLoading,
                Whisper = whisperLoading,
                InStash = instashLoading,
                IsPriced = ispricedLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
