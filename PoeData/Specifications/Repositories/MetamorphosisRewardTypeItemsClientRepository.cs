using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MetamorphosisRewardTypeItemsClientDat"/> related data and helper methods.
/// </summary>
public sealed class MetamorphosisRewardTypeItemsClientRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MetamorphosisRewardTypeItemsClientDat> Items { get; }

    private Dictionary<int, List<MetamorphosisRewardTypeItemsClientDat>>? byMetamorphosisRewardTypesKey;
    private Dictionary<int, List<MetamorphosisRewardTypeItemsClientDat>>? byUnknown16;
    private Dictionary<string, List<MetamorphosisRewardTypeItemsClientDat>>? byDescription;

    /// <summary>
    /// Initializes a new instance of the <see cref="MetamorphosisRewardTypeItemsClientRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MetamorphosisRewardTypeItemsClientRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypeItemsClientDat"/> with <see cref="MetamorphosisRewardTypeItemsClientDat.MetamorphosisRewardTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMetamorphosisRewardTypesKey(int? key, out MetamorphosisRewardTypeItemsClientDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMetamorphosisRewardTypesKey(key, out var items))
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
    /// Tries to get <see cref="MetamorphosisRewardTypeItemsClientDat"/> with <see cref="MetamorphosisRewardTypeItemsClientDat.MetamorphosisRewardTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMetamorphosisRewardTypesKey(int? key, out IReadOnlyList<MetamorphosisRewardTypeItemsClientDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisRewardTypeItemsClientDat>();
            return false;
        }

        if (byMetamorphosisRewardTypesKey is null)
        {
            byMetamorphosisRewardTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MetamorphosisRewardTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMetamorphosisRewardTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMetamorphosisRewardTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMetamorphosisRewardTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MetamorphosisRewardTypeItemsClientDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypeItemsClientDat"/> with <see cref="MetamorphosisRewardTypeItemsClientDat.byMetamorphosisRewardTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisRewardTypeItemsClientDat>> GetManyToManyByMetamorphosisRewardTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisRewardTypeItemsClientDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisRewardTypeItemsClientDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMetamorphosisRewardTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisRewardTypeItemsClientDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypeItemsClientDat"/> with <see cref="MetamorphosisRewardTypeItemsClientDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out MetamorphosisRewardTypeItemsClientDat? item)
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
    /// Tries to get <see cref="MetamorphosisRewardTypeItemsClientDat"/> with <see cref="MetamorphosisRewardTypeItemsClientDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<MetamorphosisRewardTypeItemsClientDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisRewardTypeItemsClientDat>();
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
            items = Array.Empty<MetamorphosisRewardTypeItemsClientDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypeItemsClientDat"/> with <see cref="MetamorphosisRewardTypeItemsClientDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MetamorphosisRewardTypeItemsClientDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MetamorphosisRewardTypeItemsClientDat>>();
        }

        var items = new List<ResultItem<int, MetamorphosisRewardTypeItemsClientDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MetamorphosisRewardTypeItemsClientDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypeItemsClientDat"/> with <see cref="MetamorphosisRewardTypeItemsClientDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out MetamorphosisRewardTypeItemsClientDat? item)
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
    /// Tries to get <see cref="MetamorphosisRewardTypeItemsClientDat"/> with <see cref="MetamorphosisRewardTypeItemsClientDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<MetamorphosisRewardTypeItemsClientDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MetamorphosisRewardTypeItemsClientDat>();
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
            items = Array.Empty<MetamorphosisRewardTypeItemsClientDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MetamorphosisRewardTypeItemsClientDat"/> with <see cref="MetamorphosisRewardTypeItemsClientDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MetamorphosisRewardTypeItemsClientDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MetamorphosisRewardTypeItemsClientDat>>();
        }

        var items = new List<ResultItem<string, MetamorphosisRewardTypeItemsClientDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MetamorphosisRewardTypeItemsClientDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MetamorphosisRewardTypeItemsClientDat[] Load()
    {
        const string filePath = "Data/MetamorphosisRewardTypeItemsClient.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MetamorphosisRewardTypeItemsClientDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MetamorphosisRewardTypesKey
            (var metamorphosisrewardtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MetamorphosisRewardTypeItemsClientDat()
            {
                MetamorphosisRewardTypesKey = metamorphosisrewardtypeskeyLoading,
                Unknown16 = unknown16Loading,
                Description = descriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
