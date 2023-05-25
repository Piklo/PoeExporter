using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="StashTabAffinitiesDat"/> related data and helper methods.
/// </summary>
public sealed class StashTabAffinitiesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<StashTabAffinitiesDat> Items { get; }

    private Dictionary<int, List<StashTabAffinitiesDat>>? bySpecializedStash;
    private Dictionary<string, List<StashTabAffinitiesDat>>? byName;
    private Dictionary<int, List<StashTabAffinitiesDat>>? byShowInStashes;

    /// <summary>
    /// Initializes a new instance of the <see cref="StashTabAffinitiesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal StashTabAffinitiesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="StashTabAffinitiesDat"/> with <see cref="StashTabAffinitiesDat.SpecializedStash"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpecializedStash(int? key, out StashTabAffinitiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpecializedStash(key, out var items))
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
    /// Tries to get <see cref="StashTabAffinitiesDat"/> with <see cref="StashTabAffinitiesDat.SpecializedStash"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpecializedStash(int? key, out IReadOnlyList<StashTabAffinitiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StashTabAffinitiesDat>();
            return false;
        }

        if (bySpecializedStash is null)
        {
            bySpecializedStash = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpecializedStash;

                if (!bySpecializedStash.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpecializedStash.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpecializedStash.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StashTabAffinitiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StashTabAffinitiesDat"/> with <see cref="StashTabAffinitiesDat.bySpecializedStash"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StashTabAffinitiesDat>> GetManyToManyBySpecializedStash(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StashTabAffinitiesDat>>();
        }

        var items = new List<ResultItem<int, StashTabAffinitiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpecializedStash(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StashTabAffinitiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StashTabAffinitiesDat"/> with <see cref="StashTabAffinitiesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out StashTabAffinitiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByName(key, out var items))
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
    /// Tries to get <see cref="StashTabAffinitiesDat"/> with <see cref="StashTabAffinitiesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<StashTabAffinitiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StashTabAffinitiesDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;

                if (!byName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<StashTabAffinitiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StashTabAffinitiesDat"/> with <see cref="StashTabAffinitiesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, StashTabAffinitiesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, StashTabAffinitiesDat>>();
        }

        var items = new List<ResultItem<string, StashTabAffinitiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, StashTabAffinitiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StashTabAffinitiesDat"/> with <see cref="StashTabAffinitiesDat.ShowInStashes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShowInStashes(int? key, out StashTabAffinitiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShowInStashes(key, out var items))
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
    /// Tries to get <see cref="StashTabAffinitiesDat"/> with <see cref="StashTabAffinitiesDat.ShowInStashes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShowInStashes(int? key, out IReadOnlyList<StashTabAffinitiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StashTabAffinitiesDat>();
            return false;
        }

        if (byShowInStashes is null)
        {
            byShowInStashes = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShowInStashes;
                foreach (var listKey in itemKey)
                {
                    if (!byShowInStashes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byShowInStashes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byShowInStashes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StashTabAffinitiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StashTabAffinitiesDat"/> with <see cref="StashTabAffinitiesDat.byShowInStashes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StashTabAffinitiesDat>> GetManyToManyByShowInStashes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StashTabAffinitiesDat>>();
        }

        var items = new List<ResultItem<int, StashTabAffinitiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShowInStashes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StashTabAffinitiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private StashTabAffinitiesDat[] Load()
    {
        const string filePath = "Data/StashTabAffinities.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StashTabAffinitiesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading SpecializedStash
            (var specializedstashLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShowInStashes
            (var tempshowinstashesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var showinstashesLoading = tempshowinstashesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StashTabAffinitiesDat()
            {
                SpecializedStash = specializedstashLoading,
                Name = nameLoading,
                ShowInStashes = showinstashesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
