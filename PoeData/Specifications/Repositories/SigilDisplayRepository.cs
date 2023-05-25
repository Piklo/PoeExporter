using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SigilDisplayDat"/> related data and helper methods.
/// </summary>
public sealed class SigilDisplayRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SigilDisplayDat> Items { get; }

    private Dictionary<string, List<SigilDisplayDat>>? byId;
    private Dictionary<int, List<SigilDisplayDat>>? byActive_StatsKey;
    private Dictionary<int, List<SigilDisplayDat>>? byInactive_StatsKey;
    private Dictionary<string, List<SigilDisplayDat>>? byDDSFile;
    private Dictionary<string, List<SigilDisplayDat>>? byInactive_ArtFile;
    private Dictionary<string, List<SigilDisplayDat>>? byActive_ArtFile;
    private Dictionary<string, List<SigilDisplayDat>>? byFrame_ArtFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="SigilDisplayRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SigilDisplayRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SigilDisplayDat? item)
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
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SigilDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SigilDisplayDat>();
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
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SigilDisplayDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SigilDisplayDat>>();
        }

        var items = new List<ResultItem<string, SigilDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SigilDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Active_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByActive_StatsKey(int? key, out SigilDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByActive_StatsKey(key, out var items))
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
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Active_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByActive_StatsKey(int? key, out IReadOnlyList<SigilDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        if (byActive_StatsKey is null)
        {
            byActive_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Active_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byActive_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byActive_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byActive_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.byActive_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SigilDisplayDat>> GetManyToManyByActive_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SigilDisplayDat>>();
        }

        var items = new List<ResultItem<int, SigilDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByActive_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SigilDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Inactive_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInactive_StatsKey(int? key, out SigilDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInactive_StatsKey(key, out var items))
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
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Inactive_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInactive_StatsKey(int? key, out IReadOnlyList<SigilDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        if (byInactive_StatsKey is null)
        {
            byInactive_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Inactive_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byInactive_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byInactive_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byInactive_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.byInactive_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SigilDisplayDat>> GetManyToManyByInactive_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SigilDisplayDat>>();
        }

        var items = new List<ResultItem<int, SigilDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInactive_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SigilDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDDSFile(string? key, out SigilDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDDSFile(key, out var items))
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
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDDSFile(string? key, out IReadOnlyList<SigilDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        if (byDDSFile is null)
        {
            byDDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.DDSFile;

                if (!byDDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.byDDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SigilDisplayDat>> GetManyToManyByDDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SigilDisplayDat>>();
        }

        var items = new List<ResultItem<string, SigilDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SigilDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Inactive_ArtFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInactive_ArtFile(string? key, out SigilDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInactive_ArtFile(key, out var items))
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
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Inactive_ArtFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInactive_ArtFile(string? key, out IReadOnlyList<SigilDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        if (byInactive_ArtFile is null)
        {
            byInactive_ArtFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Inactive_ArtFile;

                if (!byInactive_ArtFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInactive_ArtFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInactive_ArtFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.byInactive_ArtFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SigilDisplayDat>> GetManyToManyByInactive_ArtFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SigilDisplayDat>>();
        }

        var items = new List<ResultItem<string, SigilDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInactive_ArtFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SigilDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Active_ArtFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByActive_ArtFile(string? key, out SigilDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByActive_ArtFile(key, out var items))
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
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Active_ArtFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByActive_ArtFile(string? key, out IReadOnlyList<SigilDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        if (byActive_ArtFile is null)
        {
            byActive_ArtFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Active_ArtFile;

                if (!byActive_ArtFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byActive_ArtFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byActive_ArtFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.byActive_ArtFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SigilDisplayDat>> GetManyToManyByActive_ArtFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SigilDisplayDat>>();
        }

        var items = new List<ResultItem<string, SigilDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByActive_ArtFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SigilDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Frame_ArtFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFrame_ArtFile(string? key, out SigilDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFrame_ArtFile(key, out var items))
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
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.Frame_ArtFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFrame_ArtFile(string? key, out IReadOnlyList<SigilDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        if (byFrame_ArtFile is null)
        {
            byFrame_ArtFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Frame_ArtFile;

                if (!byFrame_ArtFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFrame_ArtFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFrame_ArtFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SigilDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SigilDisplayDat"/> with <see cref="SigilDisplayDat.byFrame_ArtFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SigilDisplayDat>> GetManyToManyByFrame_ArtFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SigilDisplayDat>>();
        }

        var items = new List<ResultItem<string, SigilDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFrame_ArtFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SigilDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SigilDisplayDat[] Load()
    {
        const string filePath = "Data/SigilDisplay.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SigilDisplayDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Active_StatsKey
            (var active_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Inactive_StatsKey
            (var inactive_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DDSFile
            (var ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Inactive_ArtFile
            (var inactive_artfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Active_ArtFile
            (var active_artfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Frame_ArtFile
            (var frame_artfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SigilDisplayDat()
            {
                Id = idLoading,
                Active_StatsKey = active_statskeyLoading,
                Inactive_StatsKey = inactive_statskeyLoading,
                DDSFile = ddsfileLoading,
                Inactive_ArtFile = inactive_artfileLoading,
                Active_ArtFile = active_artfileLoading,
                Frame_ArtFile = frame_artfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
