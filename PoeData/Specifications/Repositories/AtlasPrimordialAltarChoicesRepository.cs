using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasPrimordialAltarChoicesDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasPrimordialAltarChoicesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasPrimordialAltarChoicesDat> Items { get; }

    private Dictionary<int, List<AtlasPrimordialAltarChoicesDat>>? byMod;
    private Dictionary<int, List<AtlasPrimordialAltarChoicesDat>>? byType;
    private Dictionary<bool, List<AtlasPrimordialAltarChoicesDat>>? byUnknown32;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasPrimordialAltarChoicesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasPrimordialAltarChoicesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoicesDat"/> with <see cref="AtlasPrimordialAltarChoicesDat.Mod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMod(int? key, out AtlasPrimordialAltarChoicesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMod(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialAltarChoicesDat"/> with <see cref="AtlasPrimordialAltarChoicesDat.Mod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMod(int? key, out IReadOnlyList<AtlasPrimordialAltarChoicesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialAltarChoicesDat>();
            return false;
        }

        if (byMod is null)
        {
            byMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialAltarChoicesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoicesDat"/> with <see cref="AtlasPrimordialAltarChoicesDat.byMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialAltarChoicesDat>> GetManyToManyByMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialAltarChoicesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialAltarChoicesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialAltarChoicesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoicesDat"/> with <see cref="AtlasPrimordialAltarChoicesDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByType(int? key, out AtlasPrimordialAltarChoicesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByType(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialAltarChoicesDat"/> with <see cref="AtlasPrimordialAltarChoicesDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByType(int? key, out IReadOnlyList<AtlasPrimordialAltarChoicesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialAltarChoicesDat>();
            return false;
        }

        if (byType is null)
        {
            byType = new();
            foreach (var item in Items)
            {
                var itemKey = item.Type;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialAltarChoicesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoicesDat"/> with <see cref="AtlasPrimordialAltarChoicesDat.byType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialAltarChoicesDat>> GetManyToManyByType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialAltarChoicesDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialAltarChoicesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialAltarChoicesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoicesDat"/> with <see cref="AtlasPrimordialAltarChoicesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(bool? key, out AtlasPrimordialAltarChoicesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialAltarChoicesDat"/> with <see cref="AtlasPrimordialAltarChoicesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(bool? key, out IReadOnlyList<AtlasPrimordialAltarChoicesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialAltarChoicesDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialAltarChoicesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoicesDat"/> with <see cref="AtlasPrimordialAltarChoicesDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AtlasPrimordialAltarChoicesDat>> GetManyToManyByUnknown32(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AtlasPrimordialAltarChoicesDat>>();
        }

        var items = new List<ResultItem<bool, AtlasPrimordialAltarChoicesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AtlasPrimordialAltarChoicesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasPrimordialAltarChoicesDat[] Load()
    {
        const string filePath = "Data/AtlasPrimordialAltarChoices.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasPrimordialAltarChoicesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Mod
            (var modLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasPrimordialAltarChoicesDat()
            {
                Mod = modLoading,
                Type = typeLoading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
