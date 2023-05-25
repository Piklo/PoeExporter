using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AlternateQualityTypesDat"/> related data and helper methods.
/// </summary>
public sealed class AlternateQualityTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AlternateQualityTypesDat> Items { get; }

    private Dictionary<int, List<AlternateQualityTypesDat>>? byStatsKey;
    private Dictionary<string, List<AlternateQualityTypesDat>>? byDescription;
    private Dictionary<int, List<AlternateQualityTypesDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<AlternateQualityTypesDat>>? byModsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlternateQualityTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AlternateQualityTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey(int? key, out AlternateQualityTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey(key, out var items))
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
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey(int? key, out IReadOnlyList<AlternateQualityTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateQualityTypesDat>();
            return false;
        }

        if (byStatsKey is null)
        {
            byStatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateQualityTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.byStatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateQualityTypesDat>> GetManyToManyByStatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateQualityTypesDat>>();
        }

        var items = new List<ResultItem<int, AlternateQualityTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateQualityTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out AlternateQualityTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<AlternateQualityTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateQualityTypesDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AlternateQualityTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AlternateQualityTypesDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AlternateQualityTypesDat>>();
        }

        var items = new List<ResultItem<string, AlternateQualityTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AlternateQualityTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out AlternateQualityTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<AlternateQualityTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateQualityTypesDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateQualityTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateQualityTypesDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateQualityTypesDat>>();
        }

        var items = new List<ResultItem<int, AlternateQualityTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateQualityTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKey(int? key, out AlternateQualityTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKey(key, out var items))
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
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKey(int? key, out IReadOnlyList<AlternateQualityTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateQualityTypesDat>();
            return false;
        }

        if (byModsKey is null)
        {
            byModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateQualityTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateQualityTypesDat"/> with <see cref="AlternateQualityTypesDat.byModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateQualityTypesDat>> GetManyToManyByModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateQualityTypesDat>>();
        }

        var items = new List<ResultItem<int, AlternateQualityTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateQualityTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AlternateQualityTypesDat[] Load()
    {
        const string filePath = "Data/AlternateQualityTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AlternateQualityTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AlternateQualityTypesDat()
            {
                StatsKey = statskeyLoading,
                Description = descriptionLoading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
                ModsKey = modskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
