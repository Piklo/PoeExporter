using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="EndlessLedgeChestsDat"/> related data and helper methods.
/// </summary>
public sealed class EndlessLedgeChestsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<EndlessLedgeChestsDat> Items { get; }

    private Dictionary<string, List<EndlessLedgeChestsDat>>? byId;
    private Dictionary<int, List<EndlessLedgeChestsDat>>? byWorldAreasKey;
    private Dictionary<int, List<EndlessLedgeChestsDat>>? byBaseItemTypesKeys;
    private Dictionary<string, List<EndlessLedgeChestsDat>>? bySocketColour;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndlessLedgeChestsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal EndlessLedgeChestsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out EndlessLedgeChestsDat? item)
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
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<EndlessLedgeChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EndlessLedgeChestsDat>();
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
            items = Array.Empty<EndlessLedgeChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EndlessLedgeChestsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EndlessLedgeChestsDat>>();
        }

        var items = new List<ResultItem<string, EndlessLedgeChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EndlessLedgeChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out EndlessLedgeChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<EndlessLedgeChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EndlessLedgeChestsDat>();
            return false;
        }

        if (byWorldAreasKey is null)
        {
            byWorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EndlessLedgeChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EndlessLedgeChestsDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EndlessLedgeChestsDat>>();
        }

        var items = new List<ResultItem<int, EndlessLedgeChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EndlessLedgeChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.BaseItemTypesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKeys(int? key, out EndlessLedgeChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKeys(key, out var items))
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
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.BaseItemTypesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKeys(int? key, out IReadOnlyList<EndlessLedgeChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EndlessLedgeChestsDat>();
            return false;
        }

        if (byBaseItemTypesKeys is null)
        {
            byBaseItemTypesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseItemTypesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseItemTypesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseItemTypesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EndlessLedgeChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.byBaseItemTypesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EndlessLedgeChestsDat>> GetManyToManyByBaseItemTypesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EndlessLedgeChestsDat>>();
        }

        var items = new List<ResultItem<int, EndlessLedgeChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EndlessLedgeChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.SocketColour"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySocketColour(string? key, out EndlessLedgeChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySocketColour(key, out var items))
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
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.SocketColour"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySocketColour(string? key, out IReadOnlyList<EndlessLedgeChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EndlessLedgeChestsDat>();
            return false;
        }

        if (bySocketColour is null)
        {
            bySocketColour = new();
            foreach (var item in Items)
            {
                var itemKey = item.SocketColour;

                if (!bySocketColour.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySocketColour.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySocketColour.TryGetValue(key, out var temp))
        {
            items = Array.Empty<EndlessLedgeChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EndlessLedgeChestsDat"/> with <see cref="EndlessLedgeChestsDat.bySocketColour"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EndlessLedgeChestsDat>> GetManyToManyBySocketColour(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EndlessLedgeChestsDat>>();
        }

        var items = new List<ResultItem<string, EndlessLedgeChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySocketColour(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EndlessLedgeChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private EndlessLedgeChestsDat[] Load()
    {
        const string filePath = "Data/EndlessLedgeChests.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EndlessLedgeChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BaseItemTypesKeys
            (var tempbaseitemtypeskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeysLoading = tempbaseitemtypeskeysLoading.AsReadOnly();

            // loading SocketColour
            (var socketcolourLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EndlessLedgeChestsDat()
            {
                Id = idLoading,
                WorldAreasKey = worldareaskeyLoading,
                BaseItemTypesKeys = baseitemtypeskeysLoading,
                SocketColour = socketcolourLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
