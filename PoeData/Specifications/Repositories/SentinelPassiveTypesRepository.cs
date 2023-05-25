using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SentinelPassiveTypesDat"/> related data and helper methods.
/// </summary>
public sealed class SentinelPassiveTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SentinelPassiveTypesDat> Items { get; }

    private Dictionary<string, List<SentinelPassiveTypesDat>>? byId;
    private Dictionary<string, List<SentinelPassiveTypesDat>>? byDefaultIcon;
    private Dictionary<string, List<SentinelPassiveTypesDat>>? byActiveIcon;
    private Dictionary<int, List<SentinelPassiveTypesDat>>? byDroneType;
    private Dictionary<int, List<SentinelPassiveTypesDat>>? byUnknown40;

    /// <summary>
    /// Initializes a new instance of the <see cref="SentinelPassiveTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SentinelPassiveTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SentinelPassiveTypesDat? item)
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
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SentinelPassiveTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelPassiveTypesDat>();
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
            items = Array.Empty<SentinelPassiveTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SentinelPassiveTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SentinelPassiveTypesDat>>();
        }

        var items = new List<ResultItem<string, SentinelPassiveTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SentinelPassiveTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.DefaultIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDefaultIcon(string? key, out SentinelPassiveTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDefaultIcon(key, out var items))
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
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.DefaultIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDefaultIcon(string? key, out IReadOnlyList<SentinelPassiveTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelPassiveTypesDat>();
            return false;
        }

        if (byDefaultIcon is null)
        {
            byDefaultIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.DefaultIcon;

                if (!byDefaultIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDefaultIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDefaultIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SentinelPassiveTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.byDefaultIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SentinelPassiveTypesDat>> GetManyToManyByDefaultIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SentinelPassiveTypesDat>>();
        }

        var items = new List<ResultItem<string, SentinelPassiveTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDefaultIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SentinelPassiveTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.ActiveIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByActiveIcon(string? key, out SentinelPassiveTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByActiveIcon(key, out var items))
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
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.ActiveIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByActiveIcon(string? key, out IReadOnlyList<SentinelPassiveTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelPassiveTypesDat>();
            return false;
        }

        if (byActiveIcon is null)
        {
            byActiveIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.ActiveIcon;

                if (!byActiveIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byActiveIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byActiveIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SentinelPassiveTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.byActiveIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SentinelPassiveTypesDat>> GetManyToManyByActiveIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SentinelPassiveTypesDat>>();
        }

        var items = new List<ResultItem<string, SentinelPassiveTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByActiveIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SentinelPassiveTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.DroneType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDroneType(int? key, out SentinelPassiveTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDroneType(key, out var items))
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
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.DroneType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDroneType(int? key, out IReadOnlyList<SentinelPassiveTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelPassiveTypesDat>();
            return false;
        }

        if (byDroneType is null)
        {
            byDroneType = new();
            foreach (var item in Items)
            {
                var itemKey = item.DroneType;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDroneType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDroneType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDroneType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelPassiveTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.byDroneType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelPassiveTypesDat>> GetManyToManyByDroneType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelPassiveTypesDat>>();
        }

        var items = new List<ResultItem<int, SentinelPassiveTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDroneType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelPassiveTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out SentinelPassiveTypesDat? item)
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
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<SentinelPassiveTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelPassiveTypesDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;

                if (!byUnknown40.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelPassiveTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelPassiveTypesDat"/> with <see cref="SentinelPassiveTypesDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelPassiveTypesDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelPassiveTypesDat>>();
        }

        var items = new List<ResultItem<int, SentinelPassiveTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelPassiveTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SentinelPassiveTypesDat[] Load()
    {
        const string filePath = "Data/SentinelPassiveTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SentinelPassiveTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DefaultIcon
            (var defaulticonLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ActiveIcon
            (var activeiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DroneType
            (var dronetypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SentinelPassiveTypesDat()
            {
                Id = idLoading,
                DefaultIcon = defaulticonLoading,
                ActiveIcon = activeiconLoading,
                DroneType = dronetypeLoading,
                Unknown40 = unknown40Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
