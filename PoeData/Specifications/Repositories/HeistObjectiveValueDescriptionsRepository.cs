using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistObjectiveValueDescriptionsDat"/> related data and helper methods.
/// </summary>
public sealed class HeistObjectiveValueDescriptionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistObjectiveValueDescriptionsDat> Items { get; }

    private Dictionary<int, List<HeistObjectiveValueDescriptionsDat>>? byTier;
    private Dictionary<float, List<HeistObjectiveValueDescriptionsDat>>? byMarkersMultiply;
    private Dictionary<string, List<HeistObjectiveValueDescriptionsDat>>? byDescription;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistObjectiveValueDescriptionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistObjectiveValueDescriptionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistObjectiveValueDescriptionsDat"/> with <see cref="HeistObjectiveValueDescriptionsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out HeistObjectiveValueDescriptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier(key, out var items))
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
    /// Tries to get <see cref="HeistObjectiveValueDescriptionsDat"/> with <see cref="HeistObjectiveValueDescriptionsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<HeistObjectiveValueDescriptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistObjectiveValueDescriptionsDat>();
            return false;
        }

        if (byTier is null)
        {
            byTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier;

                if (!byTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistObjectiveValueDescriptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistObjectiveValueDescriptionsDat"/> with <see cref="HeistObjectiveValueDescriptionsDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistObjectiveValueDescriptionsDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistObjectiveValueDescriptionsDat>>();
        }

        var items = new List<ResultItem<int, HeistObjectiveValueDescriptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistObjectiveValueDescriptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistObjectiveValueDescriptionsDat"/> with <see cref="HeistObjectiveValueDescriptionsDat.MarkersMultiply"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMarkersMultiply(float? key, out HeistObjectiveValueDescriptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMarkersMultiply(key, out var items))
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
    /// Tries to get <see cref="HeistObjectiveValueDescriptionsDat"/> with <see cref="HeistObjectiveValueDescriptionsDat.MarkersMultiply"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMarkersMultiply(float? key, out IReadOnlyList<HeistObjectiveValueDescriptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistObjectiveValueDescriptionsDat>();
            return false;
        }

        if (byMarkersMultiply is null)
        {
            byMarkersMultiply = new();
            foreach (var item in Items)
            {
                var itemKey = item.MarkersMultiply;

                if (!byMarkersMultiply.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMarkersMultiply.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMarkersMultiply.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistObjectiveValueDescriptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistObjectiveValueDescriptionsDat"/> with <see cref="HeistObjectiveValueDescriptionsDat.byMarkersMultiply"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistObjectiveValueDescriptionsDat>> GetManyToManyByMarkersMultiply(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistObjectiveValueDescriptionsDat>>();
        }

        var items = new List<ResultItem<float, HeistObjectiveValueDescriptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMarkersMultiply(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistObjectiveValueDescriptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistObjectiveValueDescriptionsDat"/> with <see cref="HeistObjectiveValueDescriptionsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out HeistObjectiveValueDescriptionsDat? item)
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
    /// Tries to get <see cref="HeistObjectiveValueDescriptionsDat"/> with <see cref="HeistObjectiveValueDescriptionsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<HeistObjectiveValueDescriptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistObjectiveValueDescriptionsDat>();
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
            items = Array.Empty<HeistObjectiveValueDescriptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistObjectiveValueDescriptionsDat"/> with <see cref="HeistObjectiveValueDescriptionsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistObjectiveValueDescriptionsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistObjectiveValueDescriptionsDat>>();
        }

        var items = new List<ResultItem<string, HeistObjectiveValueDescriptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistObjectiveValueDescriptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistObjectiveValueDescriptionsDat[] Load()
    {
        const string filePath = "Data/HeistObjectiveValueDescriptions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistObjectiveValueDescriptionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MarkersMultiply
            (var markersmultiplyLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistObjectiveValueDescriptionsDat()
            {
                Tier = tierLoading,
                MarkersMultiply = markersmultiplyLoading,
                Description = descriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
