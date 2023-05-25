using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HellscapeAOReplacementsDat"/> related data and helper methods.
/// </summary>
public sealed class HellscapeAOReplacementsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HellscapeAOReplacementsDat> Items { get; }

    private Dictionary<string, List<HellscapeAOReplacementsDat>>? byOriginal;
    private Dictionary<int, List<HellscapeAOReplacementsDat>>? byHASH32;
    private Dictionary<string, List<HellscapeAOReplacementsDat>>? byReplacement;

    /// <summary>
    /// Initializes a new instance of the <see cref="HellscapeAOReplacementsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HellscapeAOReplacementsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeAOReplacementsDat"/> with <see cref="HellscapeAOReplacementsDat.Original"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOriginal(string? key, out HellscapeAOReplacementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOriginal(key, out var items))
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
    /// Tries to get <see cref="HellscapeAOReplacementsDat"/> with <see cref="HellscapeAOReplacementsDat.Original"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOriginal(string? key, out IReadOnlyList<HellscapeAOReplacementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeAOReplacementsDat>();
            return false;
        }

        if (byOriginal is null)
        {
            byOriginal = new();
            foreach (var item in Items)
            {
                var itemKey = item.Original;

                if (!byOriginal.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOriginal.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOriginal.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HellscapeAOReplacementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeAOReplacementsDat"/> with <see cref="HellscapeAOReplacementsDat.byOriginal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HellscapeAOReplacementsDat>> GetManyToManyByOriginal(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HellscapeAOReplacementsDat>>();
        }

        var items = new List<ResultItem<string, HellscapeAOReplacementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOriginal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HellscapeAOReplacementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeAOReplacementsDat"/> with <see cref="HellscapeAOReplacementsDat.HASH32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH32(int? key, out HellscapeAOReplacementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHASH32(key, out var items))
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
    /// Tries to get <see cref="HellscapeAOReplacementsDat"/> with <see cref="HellscapeAOReplacementsDat.HASH32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH32(int? key, out IReadOnlyList<HellscapeAOReplacementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeAOReplacementsDat>();
            return false;
        }

        if (byHASH32 is null)
        {
            byHASH32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HASH32;

                if (!byHASH32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHASH32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHASH32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeAOReplacementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeAOReplacementsDat"/> with <see cref="HellscapeAOReplacementsDat.byHASH32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeAOReplacementsDat>> GetManyToManyByHASH32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeAOReplacementsDat>>();
        }

        var items = new List<ResultItem<int, HellscapeAOReplacementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeAOReplacementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeAOReplacementsDat"/> with <see cref="HellscapeAOReplacementsDat.Replacement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByReplacement(string? key, out HellscapeAOReplacementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByReplacement(key, out var items))
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
    /// Tries to get <see cref="HellscapeAOReplacementsDat"/> with <see cref="HellscapeAOReplacementsDat.Replacement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByReplacement(string? key, out IReadOnlyList<HellscapeAOReplacementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeAOReplacementsDat>();
            return false;
        }

        if (byReplacement is null)
        {
            byReplacement = new();
            foreach (var item in Items)
            {
                var itemKey = item.Replacement;

                if (!byReplacement.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byReplacement.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byReplacement.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HellscapeAOReplacementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeAOReplacementsDat"/> with <see cref="HellscapeAOReplacementsDat.byReplacement"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HellscapeAOReplacementsDat>> GetManyToManyByReplacement(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HellscapeAOReplacementsDat>>();
        }

        var items = new List<ResultItem<string, HellscapeAOReplacementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByReplacement(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HellscapeAOReplacementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HellscapeAOReplacementsDat[] Load()
    {
        const string filePath = "Data/HellscapeAOReplacements.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeAOReplacementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Original
            (var originalLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH32
            (var hash32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Replacement
            (var replacementLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeAOReplacementsDat()
            {
                Original = originalLoading,
                HASH32 = hash32Loading,
                Replacement = replacementLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
