using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="RunicCirclesDat"/> related data and helper methods.
/// </summary>
public sealed class RunicCirclesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<RunicCirclesDat> Items { get; }

    private Dictionary<string, List<RunicCirclesDat>>? byUnknown0;
    private Dictionary<int, List<RunicCirclesDat>>? byUnknown8;
    private Dictionary<int, List<RunicCirclesDat>>? byUnknown12;
    private Dictionary<int, List<RunicCirclesDat>>? byUnknown28;

    /// <summary>
    /// Initializes a new instance of the <see cref="RunicCirclesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal RunicCirclesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(string? key, out RunicCirclesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown0(key, out var items))
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
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(string? key, out IReadOnlyList<RunicCirclesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RunicCirclesDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;

                if (!byUnknown0.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RunicCirclesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RunicCirclesDat>> GetManyToManyByUnknown0(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RunicCirclesDat>>();
        }

        var items = new List<ResultItem<string, RunicCirclesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RunicCirclesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out RunicCirclesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<RunicCirclesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RunicCirclesDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;

                if (!byUnknown8.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RunicCirclesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RunicCirclesDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RunicCirclesDat>>();
        }

        var items = new List<ResultItem<int, RunicCirclesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RunicCirclesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out RunicCirclesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown12(key, out var items))
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
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<RunicCirclesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RunicCirclesDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown12.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown12.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RunicCirclesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RunicCirclesDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RunicCirclesDat>>();
        }

        var items = new List<ResultItem<int, RunicCirclesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RunicCirclesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out RunicCirclesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<RunicCirclesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RunicCirclesDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RunicCirclesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RunicCirclesDat"/> with <see cref="RunicCirclesDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RunicCirclesDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RunicCirclesDat>>();
        }

        var items = new List<ResultItem<int, RunicCirclesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RunicCirclesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private RunicCirclesDat[] Load()
    {
        const string filePath = "Data/RunicCircles.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RunicCirclesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var tempunknown12Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown12Loading = tempunknown12Loading.AsReadOnly();

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RunicCirclesDat()
            {
                Unknown0 = unknown0Loading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown28 = unknown28Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
