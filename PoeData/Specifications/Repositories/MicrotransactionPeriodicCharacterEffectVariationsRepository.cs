using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat"/> related data and helper methods.
/// </summary>
public sealed class MicrotransactionPeriodicCharacterEffectVariationsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MicrotransactionPeriodicCharacterEffectVariationsDat> Items { get; }

    private Dictionary<string, List<MicrotransactionPeriodicCharacterEffectVariationsDat>>? byId;
    private Dictionary<string, List<MicrotransactionPeriodicCharacterEffectVariationsDat>>? byAOFile;
    private Dictionary<int, List<MicrotransactionPeriodicCharacterEffectVariationsDat>>? byMiscObject;

    /// <summary>
    /// Initializes a new instance of the <see cref="MicrotransactionPeriodicCharacterEffectVariationsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MicrotransactionPeriodicCharacterEffectVariationsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat"/> with <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MicrotransactionPeriodicCharacterEffectVariationsDat? item)
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
    /// Tries to get <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat"/> with <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MicrotransactionPeriodicCharacterEffectVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPeriodicCharacterEffectVariationsDat>();
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
            items = Array.Empty<MicrotransactionPeriodicCharacterEffectVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat"/> with <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionPeriodicCharacterEffectVariationsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionPeriodicCharacterEffectVariationsDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionPeriodicCharacterEffectVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionPeriodicCharacterEffectVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat"/> with <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFile(string? key, out MicrotransactionPeriodicCharacterEffectVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFile(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat"/> with <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFile(string? key, out IReadOnlyList<MicrotransactionPeriodicCharacterEffectVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPeriodicCharacterEffectVariationsDat>();
            return false;
        }

        if (byAOFile is null)
        {
            byAOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFile;

                if (!byAOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionPeriodicCharacterEffectVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat"/> with <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat.byAOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionPeriodicCharacterEffectVariationsDat>> GetManyToManyByAOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionPeriodicCharacterEffectVariationsDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionPeriodicCharacterEffectVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionPeriodicCharacterEffectVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat"/> with <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat.MiscObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscObject(int? key, out MicrotransactionPeriodicCharacterEffectVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscObject(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat"/> with <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat.MiscObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscObject(int? key, out IReadOnlyList<MicrotransactionPeriodicCharacterEffectVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionPeriodicCharacterEffectVariationsDat>();
            return false;
        }

        if (byMiscObject is null)
        {
            byMiscObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscObject;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscObject.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscObject.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscObject.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionPeriodicCharacterEffectVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat"/> with <see cref="MicrotransactionPeriodicCharacterEffectVariationsDat.byMiscObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionPeriodicCharacterEffectVariationsDat>> GetManyToManyByMiscObject(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionPeriodicCharacterEffectVariationsDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionPeriodicCharacterEffectVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionPeriodicCharacterEffectVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MicrotransactionPeriodicCharacterEffectVariationsDat[] Load()
    {
        const string filePath = "Data/MicrotransactionPeriodicCharacterEffectVariations.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionPeriodicCharacterEffectVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscObject
            (var miscobjectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionPeriodicCharacterEffectVariationsDat()
            {
                Id = idLoading,
                AOFile = aofileLoading,
                MiscObject = miscobjectLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
