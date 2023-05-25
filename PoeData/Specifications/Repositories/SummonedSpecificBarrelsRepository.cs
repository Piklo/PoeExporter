using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SummonedSpecificBarrelsDat"/> related data and helper methods.
/// </summary>
public sealed class SummonedSpecificBarrelsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SummonedSpecificBarrelsDat> Items { get; }

    private Dictionary<string, List<SummonedSpecificBarrelsDat>>? byId;
    private Dictionary<int, List<SummonedSpecificBarrelsDat>>? byChest;
    private Dictionary<int, List<SummonedSpecificBarrelsDat>>? byUnknown24;
    private Dictionary<int, List<SummonedSpecificBarrelsDat>>? byUnknown40;
    private Dictionary<int, List<SummonedSpecificBarrelsDat>>? byUnknown56;
    private Dictionary<string, List<SummonedSpecificBarrelsDat>>? byUnknown72;

    /// <summary>
    /// Initializes a new instance of the <see cref="SummonedSpecificBarrelsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SummonedSpecificBarrelsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SummonedSpecificBarrelsDat? item)
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
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SummonedSpecificBarrelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
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
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SummonedSpecificBarrelsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SummonedSpecificBarrelsDat>>();
        }

        var items = new List<ResultItem<string, SummonedSpecificBarrelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SummonedSpecificBarrelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Chest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChest(int? key, out SummonedSpecificBarrelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChest(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Chest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChest(int? key, out IReadOnlyList<SummonedSpecificBarrelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        if (byChest is null)
        {
            byChest = new();
            foreach (var item in Items)
            {
                var itemKey = item.Chest;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byChest.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byChest.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byChest.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.byChest"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificBarrelsDat>> GetManyToManyByChest(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificBarrelsDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificBarrelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChest(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificBarrelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out SummonedSpecificBarrelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown24(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<SummonedSpecificBarrelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown24.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificBarrelsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificBarrelsDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificBarrelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificBarrelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out SummonedSpecificBarrelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<SummonedSpecificBarrelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown40.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificBarrelsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificBarrelsDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificBarrelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificBarrelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out SummonedSpecificBarrelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<SummonedSpecificBarrelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown56.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificBarrelsDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificBarrelsDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificBarrelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificBarrelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(string? key, out SummonedSpecificBarrelsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(string? key, out IReadOnlyList<SummonedSpecificBarrelsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SummonedSpecificBarrelsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificBarrelsDat"/> with <see cref="SummonedSpecificBarrelsDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SummonedSpecificBarrelsDat>> GetManyToManyByUnknown72(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SummonedSpecificBarrelsDat>>();
        }

        var items = new List<ResultItem<string, SummonedSpecificBarrelsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SummonedSpecificBarrelsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SummonedSpecificBarrelsDat[] Load()
    {
        const string filePath = "Data/SummonedSpecificBarrels.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SummonedSpecificBarrelsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Chest
            (var chestLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SummonedSpecificBarrelsDat()
            {
                Id = idLoading,
                Chest = chestLoading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
