using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestCraftOptionIconsDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestCraftOptionIconsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestCraftOptionIconsDat> Items { get; }

    private Dictionary<string, List<HarvestCraftOptionIconsDat>>? byId;
    private Dictionary<string, List<HarvestCraftOptionIconsDat>>? byDDSFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestCraftOptionIconsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestCraftOptionIconsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionIconsDat"/> with <see cref="HarvestCraftOptionIconsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HarvestCraftOptionIconsDat? item)
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
    /// Tries to get <see cref="HarvestCraftOptionIconsDat"/> with <see cref="HarvestCraftOptionIconsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HarvestCraftOptionIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionIconsDat>();
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
            items = Array.Empty<HarvestCraftOptionIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionIconsDat"/> with <see cref="HarvestCraftOptionIconsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftOptionIconsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftOptionIconsDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftOptionIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftOptionIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionIconsDat"/> with <see cref="HarvestCraftOptionIconsDat.DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDDSFile(string? key, out HarvestCraftOptionIconsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDDSFile(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionIconsDat"/> with <see cref="HarvestCraftOptionIconsDat.DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDDSFile(string? key, out IReadOnlyList<HarvestCraftOptionIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionIconsDat>();
            return false;
        }

        if (byDDSFile is null)
        {
            byDDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.DDSFile;

                if (!byDDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionIconsDat"/> with <see cref="HarvestCraftOptionIconsDat.byDDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftOptionIconsDat>> GetManyToManyByDDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftOptionIconsDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftOptionIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftOptionIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestCraftOptionIconsDat[] Load()
    {
        const string filePath = "Data/HarvestCraftOptionIcons.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestCraftOptionIconsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DDSFile
            (var ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestCraftOptionIconsDat()
            {
                Id = idLoading,
                DDSFile = ddsfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
