using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="EffectivenessCostConstantsDat"/> related data and helper methods.
/// </summary>
public sealed class EffectivenessCostConstantsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<EffectivenessCostConstantsDat> Items { get; }

    private Dictionary<string, List<EffectivenessCostConstantsDat>>? byId;
    private Dictionary<float, List<EffectivenessCostConstantsDat>>? byMultiplier;

    /// <summary>
    /// Initializes a new instance of the <see cref="EffectivenessCostConstantsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal EffectivenessCostConstantsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="EffectivenessCostConstantsDat"/> with <see cref="EffectivenessCostConstantsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out EffectivenessCostConstantsDat? item)
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
    /// Tries to get <see cref="EffectivenessCostConstantsDat"/> with <see cref="EffectivenessCostConstantsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<EffectivenessCostConstantsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectivenessCostConstantsDat>();
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
            items = Array.Empty<EffectivenessCostConstantsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectivenessCostConstantsDat"/> with <see cref="EffectivenessCostConstantsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EffectivenessCostConstantsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EffectivenessCostConstantsDat>>();
        }

        var items = new List<ResultItem<string, EffectivenessCostConstantsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EffectivenessCostConstantsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EffectivenessCostConstantsDat"/> with <see cref="EffectivenessCostConstantsDat.Multiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMultiplier(float? key, out EffectivenessCostConstantsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMultiplier(key, out var items))
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
    /// Tries to get <see cref="EffectivenessCostConstantsDat"/> with <see cref="EffectivenessCostConstantsDat.Multiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMultiplier(float? key, out IReadOnlyList<EffectivenessCostConstantsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EffectivenessCostConstantsDat>();
            return false;
        }

        if (byMultiplier is null)
        {
            byMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.Multiplier;

                if (!byMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EffectivenessCostConstantsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EffectivenessCostConstantsDat"/> with <see cref="EffectivenessCostConstantsDat.byMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, EffectivenessCostConstantsDat>> GetManyToManyByMultiplier(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, EffectivenessCostConstantsDat>>();
        }

        var items = new List<ResultItem<float, EffectivenessCostConstantsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, EffectivenessCostConstantsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private EffectivenessCostConstantsDat[] Load()
    {
        const string filePath = "Data/EffectivenessCostConstants.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EffectivenessCostConstantsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Multiplier
            (var multiplierLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EffectivenessCostConstantsDat()
            {
                Id = idLoading,
                Multiplier = multiplierLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
