using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestCraftTiersDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestCraftTiersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestCraftTiersDat> Items { get; }

    private Dictionary<string, List<HarvestCraftTiersDat>>? byId;
    private Dictionary<string, List<HarvestCraftTiersDat>>? byFrameImage;
    private Dictionary<string, List<HarvestCraftTiersDat>>? byFrameHighlight;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestCraftTiersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestCraftTiersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftTiersDat"/> with <see cref="HarvestCraftTiersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HarvestCraftTiersDat? item)
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
    /// Tries to get <see cref="HarvestCraftTiersDat"/> with <see cref="HarvestCraftTiersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HarvestCraftTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftTiersDat>();
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
            items = Array.Empty<HarvestCraftTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftTiersDat"/> with <see cref="HarvestCraftTiersDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftTiersDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftTiersDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftTiersDat"/> with <see cref="HarvestCraftTiersDat.FrameImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFrameImage(string? key, out HarvestCraftTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFrameImage(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftTiersDat"/> with <see cref="HarvestCraftTiersDat.FrameImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFrameImage(string? key, out IReadOnlyList<HarvestCraftTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftTiersDat>();
            return false;
        }

        if (byFrameImage is null)
        {
            byFrameImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.FrameImage;

                if (!byFrameImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFrameImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFrameImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HarvestCraftTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftTiersDat"/> with <see cref="HarvestCraftTiersDat.byFrameImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftTiersDat>> GetManyToManyByFrameImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftTiersDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFrameImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftTiersDat"/> with <see cref="HarvestCraftTiersDat.FrameHighlight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFrameHighlight(string? key, out HarvestCraftTiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFrameHighlight(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftTiersDat"/> with <see cref="HarvestCraftTiersDat.FrameHighlight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFrameHighlight(string? key, out IReadOnlyList<HarvestCraftTiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftTiersDat>();
            return false;
        }

        if (byFrameHighlight is null)
        {
            byFrameHighlight = new();
            foreach (var item in Items)
            {
                var itemKey = item.FrameHighlight;

                if (!byFrameHighlight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFrameHighlight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFrameHighlight.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HarvestCraftTiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftTiersDat"/> with <see cref="HarvestCraftTiersDat.byFrameHighlight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftTiersDat>> GetManyToManyByFrameHighlight(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftTiersDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftTiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFrameHighlight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftTiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestCraftTiersDat[] Load()
    {
        const string filePath = "Data/HarvestCraftTiers.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestCraftTiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FrameImage
            (var frameimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FrameHighlight
            (var framehighlightLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestCraftTiersDat()
            {
                Id = idLoading,
                FrameImage = frameimageLoading,
                FrameHighlight = framehighlightLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
