using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ShaperGuardiansDat"/> related data and helper methods.
/// </summary>
public sealed class ShaperGuardiansRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ShaperGuardiansDat> Items { get; }

    private Dictionary<string, List<ShaperGuardiansDat>>? byId;
    private Dictionary<int, List<ShaperGuardiansDat>>? byWorldArea;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaperGuardiansRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ShaperGuardiansRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ShaperGuardiansDat"/> with <see cref="ShaperGuardiansDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ShaperGuardiansDat? item)
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
    /// Tries to get <see cref="ShaperGuardiansDat"/> with <see cref="ShaperGuardiansDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ShaperGuardiansDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShaperGuardiansDat>();
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
            items = Array.Empty<ShaperGuardiansDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShaperGuardiansDat"/> with <see cref="ShaperGuardiansDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShaperGuardiansDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShaperGuardiansDat>>();
        }

        var items = new List<ResultItem<string, ShaperGuardiansDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShaperGuardiansDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShaperGuardiansDat"/> with <see cref="ShaperGuardiansDat.WorldArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldArea(int? key, out ShaperGuardiansDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldArea(key, out var items))
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
    /// Tries to get <see cref="ShaperGuardiansDat"/> with <see cref="ShaperGuardiansDat.WorldArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldArea(int? key, out IReadOnlyList<ShaperGuardiansDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShaperGuardiansDat>();
            return false;
        }

        if (byWorldArea is null)
        {
            byWorldArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldArea;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldArea.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldArea.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShaperGuardiansDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShaperGuardiansDat"/> with <see cref="ShaperGuardiansDat.byWorldArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShaperGuardiansDat>> GetManyToManyByWorldArea(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShaperGuardiansDat>>();
        }

        var items = new List<ResultItem<int, ShaperGuardiansDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShaperGuardiansDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ShaperGuardiansDat[] Load()
    {
        const string filePath = "Data/ShaperGuardians.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShaperGuardiansDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WorldArea
            (var worldareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShaperGuardiansDat()
            {
                Id = idLoading,
                WorldArea = worldareaLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
