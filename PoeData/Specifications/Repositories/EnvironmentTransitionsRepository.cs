using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="EnvironmentTransitionsDat"/> related data and helper methods.
/// </summary>
public sealed class EnvironmentTransitionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<EnvironmentTransitionsDat> Items { get; }

    private Dictionary<string, List<EnvironmentTransitionsDat>>? byId;
    private Dictionary<string, List<EnvironmentTransitionsDat>>? byOTFiles;
    private Dictionary<string, List<EnvironmentTransitionsDat>>? byUnknown24;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentTransitionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal EnvironmentTransitionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentTransitionsDat"/> with <see cref="EnvironmentTransitionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out EnvironmentTransitionsDat? item)
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
    /// Tries to get <see cref="EnvironmentTransitionsDat"/> with <see cref="EnvironmentTransitionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<EnvironmentTransitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentTransitionsDat>();
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
            items = Array.Empty<EnvironmentTransitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentTransitionsDat"/> with <see cref="EnvironmentTransitionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EnvironmentTransitionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EnvironmentTransitionsDat>>();
        }

        var items = new List<ResultItem<string, EnvironmentTransitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EnvironmentTransitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentTransitionsDat"/> with <see cref="EnvironmentTransitionsDat.OTFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOTFiles(string? key, out EnvironmentTransitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOTFiles(key, out var items))
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
    /// Tries to get <see cref="EnvironmentTransitionsDat"/> with <see cref="EnvironmentTransitionsDat.OTFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOTFiles(string? key, out IReadOnlyList<EnvironmentTransitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentTransitionsDat>();
            return false;
        }

        if (byOTFiles is null)
        {
            byOTFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.OTFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byOTFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byOTFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byOTFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<EnvironmentTransitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentTransitionsDat"/> with <see cref="EnvironmentTransitionsDat.byOTFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EnvironmentTransitionsDat>> GetManyToManyByOTFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EnvironmentTransitionsDat>>();
        }

        var items = new List<ResultItem<string, EnvironmentTransitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOTFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EnvironmentTransitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentTransitionsDat"/> with <see cref="EnvironmentTransitionsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(string? key, out EnvironmentTransitionsDat? item)
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
    /// Tries to get <see cref="EnvironmentTransitionsDat"/> with <see cref="EnvironmentTransitionsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(string? key, out IReadOnlyList<EnvironmentTransitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EnvironmentTransitionsDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown24.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown24.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown24.TryGetValue(key, out var temp))
        {
            items = Array.Empty<EnvironmentTransitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EnvironmentTransitionsDat"/> with <see cref="EnvironmentTransitionsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EnvironmentTransitionsDat>> GetManyToManyByUnknown24(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EnvironmentTransitionsDat>>();
        }

        var items = new List<ResultItem<string, EnvironmentTransitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EnvironmentTransitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private EnvironmentTransitionsDat[] Load()
    {
        const string filePath = "Data/EnvironmentTransitions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EnvironmentTransitionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OTFiles
            (var tempotfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var otfilesLoading = tempotfilesLoading.AsReadOnly();

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EnvironmentTransitionsDat()
            {
                Id = idLoading,
                OTFiles = otfilesLoading,
                Unknown24 = unknown24Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
