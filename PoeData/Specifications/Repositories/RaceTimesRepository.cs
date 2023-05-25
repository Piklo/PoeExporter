using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="RaceTimesDat"/> related data and helper methods.
/// </summary>
public sealed class RaceTimesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<RaceTimesDat> Items { get; }

    private Dictionary<int, List<RaceTimesDat>>? byRacesKey;
    private Dictionary<int, List<RaceTimesDat>>? byIndex;
    private Dictionary<int, List<RaceTimesDat>>? byStartUNIXTime;
    private Dictionary<int, List<RaceTimesDat>>? byEndUNIXTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="RaceTimesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal RaceTimesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.RacesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRacesKey(int? key, out RaceTimesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRacesKey(key, out var items))
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
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.RacesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRacesKey(int? key, out IReadOnlyList<RaceTimesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RaceTimesDat>();
            return false;
        }

        if (byRacesKey is null)
        {
            byRacesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.RacesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRacesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRacesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRacesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RaceTimesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.byRacesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RaceTimesDat>> GetManyToManyByRacesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RaceTimesDat>>();
        }

        var items = new List<ResultItem<int, RaceTimesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRacesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RaceTimesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.Index"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIndex(int? key, out RaceTimesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIndex(key, out var items))
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
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.Index"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIndex(int? key, out IReadOnlyList<RaceTimesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RaceTimesDat>();
            return false;
        }

        if (byIndex is null)
        {
            byIndex = new();
            foreach (var item in Items)
            {
                var itemKey = item.Index;

                if (!byIndex.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIndex.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIndex.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RaceTimesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.byIndex"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RaceTimesDat>> GetManyToManyByIndex(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RaceTimesDat>>();
        }

        var items = new List<ResultItem<int, RaceTimesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIndex(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RaceTimesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.StartUNIXTime"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStartUNIXTime(int? key, out RaceTimesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStartUNIXTime(key, out var items))
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
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.StartUNIXTime"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStartUNIXTime(int? key, out IReadOnlyList<RaceTimesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RaceTimesDat>();
            return false;
        }

        if (byStartUNIXTime is null)
        {
            byStartUNIXTime = new();
            foreach (var item in Items)
            {
                var itemKey = item.StartUNIXTime;

                if (!byStartUNIXTime.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStartUNIXTime.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStartUNIXTime.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RaceTimesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.byStartUNIXTime"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RaceTimesDat>> GetManyToManyByStartUNIXTime(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RaceTimesDat>>();
        }

        var items = new List<ResultItem<int, RaceTimesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStartUNIXTime(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RaceTimesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.EndUNIXTime"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEndUNIXTime(int? key, out RaceTimesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEndUNIXTime(key, out var items))
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
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.EndUNIXTime"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEndUNIXTime(int? key, out IReadOnlyList<RaceTimesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RaceTimesDat>();
            return false;
        }

        if (byEndUNIXTime is null)
        {
            byEndUNIXTime = new();
            foreach (var item in Items)
            {
                var itemKey = item.EndUNIXTime;

                if (!byEndUNIXTime.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEndUNIXTime.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEndUNIXTime.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RaceTimesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RaceTimesDat"/> with <see cref="RaceTimesDat.byEndUNIXTime"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RaceTimesDat>> GetManyToManyByEndUNIXTime(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RaceTimesDat>>();
        }

        var items = new List<ResultItem<int, RaceTimesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEndUNIXTime(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RaceTimesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private RaceTimesDat[] Load()
    {
        const string filePath = "Data/RaceTimes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RaceTimesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading RacesKey
            (var raceskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Index
            (var indexLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StartUNIXTime
            (var startunixtimeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EndUNIXTime
            (var endunixtimeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RaceTimesDat()
            {
                RacesKey = raceskeyLoading,
                Index = indexLoading,
                StartUNIXTime = startunixtimeLoading,
                EndUNIXTime = endunixtimeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
