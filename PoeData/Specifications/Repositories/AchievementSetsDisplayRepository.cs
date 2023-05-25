using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AchievementSetsDisplayDat"/> related data and helper methods.
/// </summary>
public sealed class AchievementSetsDisplayRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AchievementSetsDisplayDat> Items { get; }

    private Dictionary<int, List<AchievementSetsDisplayDat>>? byId;
    private Dictionary<string, List<AchievementSetsDisplayDat>>? byTitle;

    /// <summary>
    /// Initializes a new instance of the <see cref="AchievementSetsDisplayRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AchievementSetsDisplayRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetsDisplayDat"/> with <see cref="AchievementSetsDisplayDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(int? key, out AchievementSetsDisplayDat? item)
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
    /// Tries to get <see cref="AchievementSetsDisplayDat"/> with <see cref="AchievementSetsDisplayDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(int? key, out IReadOnlyList<AchievementSetsDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementSetsDisplayDat>();
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

        if (!byId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementSetsDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetsDisplayDat"/> with <see cref="AchievementSetsDisplayDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementSetsDisplayDat>> GetManyToManyById(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementSetsDisplayDat>>();
        }

        var items = new List<ResultItem<int, AchievementSetsDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementSetsDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetsDisplayDat"/> with <see cref="AchievementSetsDisplayDat.Title"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTitle(string? key, out AchievementSetsDisplayDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTitle(key, out var items))
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
    /// Tries to get <see cref="AchievementSetsDisplayDat"/> with <see cref="AchievementSetsDisplayDat.Title"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTitle(string? key, out IReadOnlyList<AchievementSetsDisplayDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementSetsDisplayDat>();
            return false;
        }

        if (byTitle is null)
        {
            byTitle = new();
            foreach (var item in Items)
            {
                var itemKey = item.Title;

                if (!byTitle.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTitle.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTitle.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AchievementSetsDisplayDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetsDisplayDat"/> with <see cref="AchievementSetsDisplayDat.byTitle"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementSetsDisplayDat>> GetManyToManyByTitle(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementSetsDisplayDat>>();
        }

        var items = new List<ResultItem<string, AchievementSetsDisplayDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTitle(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementSetsDisplayDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AchievementSetsDisplayDat[] Load()
    {
        const string filePath = "Data/AchievementSetsDisplay.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AchievementSetsDisplayDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Title
            (var titleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AchievementSetsDisplayDat()
            {
                Id = idLoading,
                Title = titleLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
