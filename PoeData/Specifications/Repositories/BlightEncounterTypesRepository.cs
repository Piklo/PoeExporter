using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BlightEncounterTypesDat"/> related data and helper methods.
/// </summary>
public sealed class BlightEncounterTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BlightEncounterTypesDat> Items { get; }

    private Dictionary<string, List<BlightEncounterTypesDat>>? byId;
    private Dictionary<string, List<BlightEncounterTypesDat>>? byIcon;
    private Dictionary<bool, List<BlightEncounterTypesDat>>? byIsGeneric;
    private Dictionary<int, List<BlightEncounterTypesDat>>? byWeight;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightEncounterTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BlightEncounterTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BlightEncounterTypesDat? item)
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
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BlightEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightEncounterTypesDat>();
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
            items = Array.Empty<BlightEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightEncounterTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightEncounterTypesDat>>();
        }

        var items = new List<ResultItem<string, BlightEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon(string? key, out BlightEncounterTypesDat? item)
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
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon(string? key, out IReadOnlyList<BlightEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightEncounterTypesDat>();
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
            items = Array.Empty<BlightEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.byIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightEncounterTypesDat>> GetManyToManyByIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightEncounterTypesDat>>();
        }

        var items = new List<ResultItem<string, BlightEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.IsGeneric"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsGeneric(bool? key, out BlightEncounterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsGeneric(key, out var items))
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
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.IsGeneric"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsGeneric(bool? key, out IReadOnlyList<BlightEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightEncounterTypesDat>();
            return false;
        }

        if (byIsGeneric is null)
        {
            byIsGeneric = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsGeneric;

                if (!byIsGeneric.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsGeneric.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsGeneric.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.byIsGeneric"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BlightEncounterTypesDat>> GetManyToManyByIsGeneric(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BlightEncounterTypesDat>>();
        }

        var items = new List<ResultItem<bool, BlightEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsGeneric(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BlightEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.Weight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeight(int? key, out BlightEncounterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeight(key, out var items))
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
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.Weight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeight(int? key, out IReadOnlyList<BlightEncounterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightEncounterTypesDat>();
            return false;
        }

        if (byWeight is null)
        {
            byWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.Weight;

                if (!byWeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightEncounterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterTypesDat"/> with <see cref="BlightEncounterTypesDat.byWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightEncounterTypesDat>> GetManyToManyByWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightEncounterTypesDat>>();
        }

        var items = new List<ResultItem<int, BlightEncounterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightEncounterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BlightEncounterTypesDat[] Load()
    {
        const string filePath = "Data/BlightEncounterTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightEncounterTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsGeneric
            (var isgenericLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightEncounterTypesDat()
            {
                Id = idLoading,
                Icon = iconLoading,
                IsGeneric = isgenericLoading,
                Weight = weightLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
