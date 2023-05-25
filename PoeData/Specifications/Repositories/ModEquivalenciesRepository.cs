using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ModEquivalenciesDat"/> related data and helper methods.
/// </summary>
public sealed class ModEquivalenciesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ModEquivalenciesDat> Items { get; }

    private Dictionary<string, List<ModEquivalenciesDat>>? byId;
    private Dictionary<int, List<ModEquivalenciesDat>>? byModsKey0;
    private Dictionary<int, List<ModEquivalenciesDat>>? byModsKey1;
    private Dictionary<int, List<ModEquivalenciesDat>>? byModsKey2;
    private Dictionary<bool, List<ModEquivalenciesDat>>? byUnknown56;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModEquivalenciesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ModEquivalenciesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ModEquivalenciesDat? item)
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
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ModEquivalenciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModEquivalenciesDat>();
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
            items = Array.Empty<ModEquivalenciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ModEquivalenciesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ModEquivalenciesDat>>();
        }

        var items = new List<ResultItem<string, ModEquivalenciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ModEquivalenciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.ModsKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKey0(int? key, out ModEquivalenciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKey0(key, out var items))
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
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.ModsKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKey0(int? key, out IReadOnlyList<ModEquivalenciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModEquivalenciesDat>();
            return false;
        }

        if (byModsKey0 is null)
        {
            byModsKey0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKey0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byModsKey0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byModsKey0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byModsKey0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModEquivalenciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.byModsKey0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModEquivalenciesDat>> GetManyToManyByModsKey0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModEquivalenciesDat>>();
        }

        var items = new List<ResultItem<int, ModEquivalenciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKey0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModEquivalenciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.ModsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKey1(int? key, out ModEquivalenciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKey1(key, out var items))
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
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.ModsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKey1(int? key, out IReadOnlyList<ModEquivalenciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModEquivalenciesDat>();
            return false;
        }

        if (byModsKey1 is null)
        {
            byModsKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byModsKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byModsKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byModsKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModEquivalenciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.byModsKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModEquivalenciesDat>> GetManyToManyByModsKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModEquivalenciesDat>>();
        }

        var items = new List<ResultItem<int, ModEquivalenciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModEquivalenciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.ModsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKey2(int? key, out ModEquivalenciesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKey2(key, out var items))
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
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.ModsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKey2(int? key, out IReadOnlyList<ModEquivalenciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModEquivalenciesDat>();
            return false;
        }

        if (byModsKey2 is null)
        {
            byModsKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byModsKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byModsKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byModsKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModEquivalenciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.byModsKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModEquivalenciesDat>> GetManyToManyByModsKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModEquivalenciesDat>>();
        }

        var items = new List<ResultItem<int, ModEquivalenciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModEquivalenciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(bool? key, out ModEquivalenciesDat? item)
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
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(bool? key, out IReadOnlyList<ModEquivalenciesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModEquivalenciesDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;

                if (!byUnknown56.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModEquivalenciesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModEquivalenciesDat"/> with <see cref="ModEquivalenciesDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ModEquivalenciesDat>> GetManyToManyByUnknown56(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ModEquivalenciesDat>>();
        }

        var items = new List<ResultItem<bool, ModEquivalenciesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ModEquivalenciesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ModEquivalenciesDat[] Load()
    {
        const string filePath = "Data/ModEquivalencies.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ModEquivalenciesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKey0
            (var modskey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ModsKey1
            (var modskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ModsKey2
            (var modskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ModEquivalenciesDat()
            {
                Id = idLoading,
                ModsKey0 = modskey0Loading,
                ModsKey1 = modskey1Loading,
                ModsKey2 = modskey2Loading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
