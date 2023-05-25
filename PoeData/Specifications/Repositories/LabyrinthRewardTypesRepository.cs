using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LabyrinthRewardTypesDat"/> related data and helper methods.
/// </summary>
public sealed class LabyrinthRewardTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LabyrinthRewardTypesDat> Items { get; }

    private Dictionary<string, List<LabyrinthRewardTypesDat>>? byId;
    private Dictionary<string, List<LabyrinthRewardTypesDat>>? byObjectPath;

    /// <summary>
    /// Initializes a new instance of the <see cref="LabyrinthRewardTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LabyrinthRewardTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthRewardTypesDat"/> with <see cref="LabyrinthRewardTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out LabyrinthRewardTypesDat? item)
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
    /// Tries to get <see cref="LabyrinthRewardTypesDat"/> with <see cref="LabyrinthRewardTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<LabyrinthRewardTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthRewardTypesDat>();
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
            items = Array.Empty<LabyrinthRewardTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthRewardTypesDat"/> with <see cref="LabyrinthRewardTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthRewardTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthRewardTypesDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthRewardTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthRewardTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthRewardTypesDat"/> with <see cref="LabyrinthRewardTypesDat.ObjectPath"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByObjectPath(string? key, out LabyrinthRewardTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByObjectPath(key, out var items))
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
    /// Tries to get <see cref="LabyrinthRewardTypesDat"/> with <see cref="LabyrinthRewardTypesDat.ObjectPath"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByObjectPath(string? key, out IReadOnlyList<LabyrinthRewardTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthRewardTypesDat>();
            return false;
        }

        if (byObjectPath is null)
        {
            byObjectPath = new();
            foreach (var item in Items)
            {
                var itemKey = item.ObjectPath;

                if (!byObjectPath.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byObjectPath.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byObjectPath.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LabyrinthRewardTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthRewardTypesDat"/> with <see cref="LabyrinthRewardTypesDat.byObjectPath"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthRewardTypesDat>> GetManyToManyByObjectPath(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthRewardTypesDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthRewardTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByObjectPath(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthRewardTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LabyrinthRewardTypesDat[] Load()
    {
        const string filePath = "Data/LabyrinthRewardTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthRewardTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ObjectPath
            (var objectpathLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthRewardTypesDat()
            {
                Id = idLoading,
                ObjectPath = objectpathLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
