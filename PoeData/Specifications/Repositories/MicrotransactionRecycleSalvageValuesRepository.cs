using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MicrotransactionRecycleSalvageValuesDat"/> related data and helper methods.
/// </summary>
public sealed class MicrotransactionRecycleSalvageValuesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MicrotransactionRecycleSalvageValuesDat> Items { get; }

    private Dictionary<int, List<MicrotransactionRecycleSalvageValuesDat>>? byBaseItemType;
    private Dictionary<int, List<MicrotransactionRecycleSalvageValuesDat>>? byUnknown16;
    private Dictionary<int, List<MicrotransactionRecycleSalvageValuesDat>>? byUnknown20;

    /// <summary>
    /// Initializes a new instance of the <see cref="MicrotransactionRecycleSalvageValuesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MicrotransactionRecycleSalvageValuesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionRecycleSalvageValuesDat"/> with <see cref="MicrotransactionRecycleSalvageValuesDat.BaseItemType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemType(int? key, out MicrotransactionRecycleSalvageValuesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemType(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionRecycleSalvageValuesDat"/> with <see cref="MicrotransactionRecycleSalvageValuesDat.BaseItemType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemType(int? key, out IReadOnlyList<MicrotransactionRecycleSalvageValuesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionRecycleSalvageValuesDat>();
            return false;
        }

        if (byBaseItemType is null)
        {
            byBaseItemType = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemType;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionRecycleSalvageValuesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionRecycleSalvageValuesDat"/> with <see cref="MicrotransactionRecycleSalvageValuesDat.byBaseItemType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionRecycleSalvageValuesDat>> GetManyToManyByBaseItemType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionRecycleSalvageValuesDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionRecycleSalvageValuesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionRecycleSalvageValuesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionRecycleSalvageValuesDat"/> with <see cref="MicrotransactionRecycleSalvageValuesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out MicrotransactionRecycleSalvageValuesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionRecycleSalvageValuesDat"/> with <see cref="MicrotransactionRecycleSalvageValuesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<MicrotransactionRecycleSalvageValuesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionRecycleSalvageValuesDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionRecycleSalvageValuesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionRecycleSalvageValuesDat"/> with <see cref="MicrotransactionRecycleSalvageValuesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionRecycleSalvageValuesDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionRecycleSalvageValuesDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionRecycleSalvageValuesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionRecycleSalvageValuesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionRecycleSalvageValuesDat"/> with <see cref="MicrotransactionRecycleSalvageValuesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out MicrotransactionRecycleSalvageValuesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown20(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionRecycleSalvageValuesDat"/> with <see cref="MicrotransactionRecycleSalvageValuesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<MicrotransactionRecycleSalvageValuesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionRecycleSalvageValuesDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;

                if (!byUnknown20.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionRecycleSalvageValuesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionRecycleSalvageValuesDat"/> with <see cref="MicrotransactionRecycleSalvageValuesDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionRecycleSalvageValuesDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionRecycleSalvageValuesDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionRecycleSalvageValuesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionRecycleSalvageValuesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MicrotransactionRecycleSalvageValuesDat[] Load()
    {
        const string filePath = "Data/MicrotransactionRecycleSalvageValues.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionRecycleSalvageValuesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionRecycleSalvageValuesDat()
            {
                BaseItemType = baseitemtypeLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
