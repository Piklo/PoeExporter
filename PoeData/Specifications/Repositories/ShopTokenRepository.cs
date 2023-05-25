using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ShopTokenDat"/> related data and helper methods.
/// </summary>
public sealed class ShopTokenRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ShopTokenDat> Items { get; }

    private Dictionary<string, List<ShopTokenDat>>? byId;
    private Dictionary<string, List<ShopTokenDat>>? byTypeId;
    private Dictionary<string, List<ShopTokenDat>>? byDescription;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShopTokenRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ShopTokenRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ShopTokenDat"/> with <see cref="ShopTokenDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ShopTokenDat? item)
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
    /// Tries to get <see cref="ShopTokenDat"/> with <see cref="ShopTokenDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ShopTokenDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopTokenDat>();
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
            items = Array.Empty<ShopTokenDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopTokenDat"/> with <see cref="ShopTokenDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopTokenDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopTokenDat>>();
        }

        var items = new List<ResultItem<string, ShopTokenDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopTokenDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopTokenDat"/> with <see cref="ShopTokenDat.TypeId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTypeId(string? key, out ShopTokenDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTypeId(key, out var items))
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
    /// Tries to get <see cref="ShopTokenDat"/> with <see cref="ShopTokenDat.TypeId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTypeId(string? key, out IReadOnlyList<ShopTokenDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopTokenDat>();
            return false;
        }

        if (byTypeId is null)
        {
            byTypeId = new();
            foreach (var item in Items)
            {
                var itemKey = item.TypeId;

                if (!byTypeId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTypeId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTypeId.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopTokenDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopTokenDat"/> with <see cref="ShopTokenDat.byTypeId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopTokenDat>> GetManyToManyByTypeId(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopTokenDat>>();
        }

        var items = new List<ResultItem<string, ShopTokenDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTypeId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopTokenDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopTokenDat"/> with <see cref="ShopTokenDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out ShopTokenDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="ShopTokenDat"/> with <see cref="ShopTokenDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<ShopTokenDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopTokenDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopTokenDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopTokenDat"/> with <see cref="ShopTokenDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopTokenDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopTokenDat>>();
        }

        var items = new List<ResultItem<string, ShopTokenDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopTokenDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ShopTokenDat[] Load()
    {
        const string filePath = "Data/ShopToken.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopTokenDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TypeId
            (var typeidLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopTokenDat()
            {
                Id = idLoading,
                TypeId = typeidLoading,
                Description = descriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
