using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BetrayalChoiceActionsDat"/> related data and helper methods.
/// </summary>
public sealed class BetrayalChoiceActionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BetrayalChoiceActionsDat> Items { get; }

    private Dictionary<string, List<BetrayalChoiceActionsDat>>? byId;
    private Dictionary<int, List<BetrayalChoiceActionsDat>>? byBetrayalChoicesKey;
    private Dictionary<int, List<BetrayalChoiceActionsDat>>? byClientStringsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="BetrayalChoiceActionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BetrayalChoiceActionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalChoiceActionsDat"/> with <see cref="BetrayalChoiceActionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BetrayalChoiceActionsDat? item)
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
    /// Tries to get <see cref="BetrayalChoiceActionsDat"/> with <see cref="BetrayalChoiceActionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BetrayalChoiceActionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalChoiceActionsDat>();
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
            items = Array.Empty<BetrayalChoiceActionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalChoiceActionsDat"/> with <see cref="BetrayalChoiceActionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalChoiceActionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalChoiceActionsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalChoiceActionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalChoiceActionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalChoiceActionsDat"/> with <see cref="BetrayalChoiceActionsDat.BetrayalChoicesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalChoicesKey(int? key, out BetrayalChoiceActionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalChoicesKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalChoiceActionsDat"/> with <see cref="BetrayalChoiceActionsDat.BetrayalChoicesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalChoicesKey(int? key, out IReadOnlyList<BetrayalChoiceActionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalChoiceActionsDat>();
            return false;
        }

        if (byBetrayalChoicesKey is null)
        {
            byBetrayalChoicesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalChoicesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBetrayalChoicesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBetrayalChoicesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalChoicesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalChoiceActionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalChoiceActionsDat"/> with <see cref="BetrayalChoiceActionsDat.byBetrayalChoicesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalChoiceActionsDat>> GetManyToManyByBetrayalChoicesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalChoiceActionsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalChoiceActionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalChoicesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalChoiceActionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalChoiceActionsDat"/> with <see cref="BetrayalChoiceActionsDat.ClientStringsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClientStringsKey(int? key, out BetrayalChoiceActionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClientStringsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalChoiceActionsDat"/> with <see cref="BetrayalChoiceActionsDat.ClientStringsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClientStringsKey(int? key, out IReadOnlyList<BetrayalChoiceActionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalChoiceActionsDat>();
            return false;
        }

        if (byClientStringsKey is null)
        {
            byClientStringsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ClientStringsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byClientStringsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byClientStringsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byClientStringsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalChoiceActionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalChoiceActionsDat"/> with <see cref="BetrayalChoiceActionsDat.byClientStringsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalChoiceActionsDat>> GetManyToManyByClientStringsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalChoiceActionsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalChoiceActionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClientStringsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalChoiceActionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BetrayalChoiceActionsDat[] Load()
    {
        const string filePath = "Data/BetrayalChoiceActions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalChoiceActionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BetrayalChoicesKey
            (var betrayalchoiceskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ClientStringsKey
            (var clientstringskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalChoiceActionsDat()
            {
                Id = idLoading,
                BetrayalChoicesKey = betrayalchoiceskeyLoading,
                ClientStringsKey = clientstringskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
