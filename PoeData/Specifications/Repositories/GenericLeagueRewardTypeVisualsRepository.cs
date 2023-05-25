using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GenericLeagueRewardTypeVisualsDat"/> related data and helper methods.
/// </summary>
public sealed class GenericLeagueRewardTypeVisualsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GenericLeagueRewardTypeVisualsDat> Items { get; }

    private Dictionary<int, List<GenericLeagueRewardTypeVisualsDat>>? byType;
    private Dictionary<int, List<GenericLeagueRewardTypeVisualsDat>>? byUnknown16;
    private Dictionary<int, List<GenericLeagueRewardTypeVisualsDat>>? byUnknown32;
    private Dictionary<float, List<GenericLeagueRewardTypeVisualsDat>>? byUnknown48;
    private Dictionary<string, List<GenericLeagueRewardTypeVisualsDat>>? byIcon;
    private Dictionary<string, List<GenericLeagueRewardTypeVisualsDat>>? byName;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericLeagueRewardTypeVisualsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GenericLeagueRewardTypeVisualsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByType(int? key, out GenericLeagueRewardTypeVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByType(key, out var items))
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
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByType(int? key, out IReadOnlyList<GenericLeagueRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        if (byType is null)
        {
            byType = new();
            foreach (var item in Items)
            {
                var itemKey = item.Type;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.byType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GenericLeagueRewardTypeVisualsDat>> GetManyToManyByType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GenericLeagueRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<int, GenericLeagueRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GenericLeagueRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out GenericLeagueRewardTypeVisualsDat? item)
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
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<GenericLeagueRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown16.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GenericLeagueRewardTypeVisualsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GenericLeagueRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<int, GenericLeagueRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GenericLeagueRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out GenericLeagueRewardTypeVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<GenericLeagueRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown32.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GenericLeagueRewardTypeVisualsDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GenericLeagueRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<int, GenericLeagueRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GenericLeagueRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(float? key, out GenericLeagueRewardTypeVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown48(key, out var items))
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
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(float? key, out IReadOnlyList<GenericLeagueRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;

                if (!byUnknown48.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, GenericLeagueRewardTypeVisualsDat>> GetManyToManyByUnknown48(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, GenericLeagueRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<float, GenericLeagueRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, GenericLeagueRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon(string? key, out GenericLeagueRewardTypeVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIcon(key, out var items))
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
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon(string? key, out IReadOnlyList<GenericLeagueRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        if (byIcon is null)
        {
            byIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.Icon;

                if (!byIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.byIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GenericLeagueRewardTypeVisualsDat>> GetManyToManyByIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GenericLeagueRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<string, GenericLeagueRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GenericLeagueRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out GenericLeagueRewardTypeVisualsDat? item)
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
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<GenericLeagueRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
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
            items = Array.Empty<GenericLeagueRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GenericLeagueRewardTypeVisualsDat"/> with <see cref="GenericLeagueRewardTypeVisualsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GenericLeagueRewardTypeVisualsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GenericLeagueRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<string, GenericLeagueRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GenericLeagueRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GenericLeagueRewardTypeVisualsDat[] Load()
    {
        const string filePath = "Data/GenericLeagueRewardTypeVisuals.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GenericLeagueRewardTypeVisualsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GenericLeagueRewardTypeVisualsDat()
            {
                Type = typeLoading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Icon = iconLoading,
                Name = nameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
