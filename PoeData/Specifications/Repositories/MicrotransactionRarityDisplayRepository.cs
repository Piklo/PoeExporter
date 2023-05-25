using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MicrotransactionRarityDisplayDat"/> related data and helper methods.
/// </summary>
public sealed class MicrotransactionRarityDisplayRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MicrotransactionRarityDisplayDat> Items { get; }

    private Dictionary<string, List<MicrotransactionRarityDisplayDat>>? byRarity;
    private Dictionary<string, List<MicrotransactionRarityDisplayDat>>? byImageFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="MicrotransactionRarityDisplayRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MicrotransactionRarityDisplayRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionRarityDisplayDat"/> with <see cref="MicrotransactionRarityDisplayDat.Rarity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRarity(string? key, out MicrotransactionRarityDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRarity(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionRarityDisplayDat"/> with <see cref="MicrotransactionRarityDisplayDat.Rarity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRarity(string? key, out IReadOnlyList<MicrotransactionRarityDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionRarityDisplayDat>();
            return false;
        }

        if (byRarity is null)
        {
            byRarity = new();
            foreach (var item in Items)
            {
                var itemKey = item.Rarity;

                if (!byRarity.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRarity.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRarity.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionRarityDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionRarityDisplayDat"/> with <see cref="MicrotransactionRarityDisplayDat.byRarity"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionRarityDisplayDat>> GetManyToManyByRarity(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionRarityDisplayDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionRarityDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRarity(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionRarityDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionRarityDisplayDat"/> with <see cref="MicrotransactionRarityDisplayDat.ImageFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImageFile(string? key, out MicrotransactionRarityDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByImageFile(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionRarityDisplayDat"/> with <see cref="MicrotransactionRarityDisplayDat.ImageFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImageFile(string? key, out IReadOnlyList<MicrotransactionRarityDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionRarityDisplayDat>();
            return false;
        }

        if (byImageFile is null)
        {
            byImageFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.ImageFile;

                if (!byImageFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byImageFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byImageFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionRarityDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionRarityDisplayDat"/> with <see cref="MicrotransactionRarityDisplayDat.byImageFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionRarityDisplayDat>> GetManyToManyByImageFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionRarityDisplayDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionRarityDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImageFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionRarityDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MicrotransactionRarityDisplayDat[] Load()
    {
        const string filePath = "Data/MicrotransactionRarityDisplay.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionRarityDisplayDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Rarity
            (var rarityLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ImageFile
            (var imagefileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionRarityDisplayDat()
            {
                Rarity = rarityLoading,
                ImageFile = imagefileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
