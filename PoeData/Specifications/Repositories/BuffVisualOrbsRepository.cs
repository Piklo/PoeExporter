using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BuffVisualOrbsDat"/> related data and helper methods.
/// </summary>
public sealed class BuffVisualOrbsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BuffVisualOrbsDat> Items { get; }

    private Dictionary<string, List<BuffVisualOrbsDat>>? byId;
    private Dictionary<int, List<BuffVisualOrbsDat>>? byBuffVisualOrbTypesKey;
    private Dictionary<int, List<BuffVisualOrbsDat>>? byBuffVisualOrbArtKeys;
    private Dictionary<int, List<BuffVisualOrbsDat>>? byPlayer_BuffVisualOrbArtKeys;
    private Dictionary<int, List<BuffVisualOrbsDat>>? byBuffVisualOrbArtKeys2;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuffVisualOrbsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BuffVisualOrbsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BuffVisualOrbsDat? item)
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
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BuffVisualOrbsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbsDat>();
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
            items = Array.Empty<BuffVisualOrbsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffVisualOrbsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffVisualOrbsDat>>();
        }

        var items = new List<ResultItem<string, BuffVisualOrbsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffVisualOrbsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.BuffVisualOrbTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisualOrbTypesKey(int? key, out BuffVisualOrbsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffVisualOrbTypesKey(key, out var items))
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
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.BuffVisualOrbTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisualOrbTypesKey(int? key, out IReadOnlyList<BuffVisualOrbsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbsDat>();
            return false;
        }

        if (byBuffVisualOrbTypesKey is null)
        {
            byBuffVisualOrbTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffVisualOrbTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffVisualOrbTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffVisualOrbTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffVisualOrbTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.byBuffVisualOrbTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualOrbsDat>> GetManyToManyByBuffVisualOrbTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualOrbsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualOrbsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisualOrbTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualOrbsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.BuffVisualOrbArtKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisualOrbArtKeys(int? key, out BuffVisualOrbsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffVisualOrbArtKeys(key, out var items))
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
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.BuffVisualOrbArtKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisualOrbArtKeys(int? key, out IReadOnlyList<BuffVisualOrbsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbsDat>();
            return false;
        }

        if (byBuffVisualOrbArtKeys is null)
        {
            byBuffVisualOrbArtKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffVisualOrbArtKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byBuffVisualOrbArtKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBuffVisualOrbArtKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBuffVisualOrbArtKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.byBuffVisualOrbArtKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualOrbsDat>> GetManyToManyByBuffVisualOrbArtKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualOrbsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualOrbsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisualOrbArtKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualOrbsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.Player_BuffVisualOrbArtKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPlayer_BuffVisualOrbArtKeys(int? key, out BuffVisualOrbsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPlayer_BuffVisualOrbArtKeys(key, out var items))
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
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.Player_BuffVisualOrbArtKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPlayer_BuffVisualOrbArtKeys(int? key, out IReadOnlyList<BuffVisualOrbsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbsDat>();
            return false;
        }

        if (byPlayer_BuffVisualOrbArtKeys is null)
        {
            byPlayer_BuffVisualOrbArtKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Player_BuffVisualOrbArtKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byPlayer_BuffVisualOrbArtKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPlayer_BuffVisualOrbArtKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPlayer_BuffVisualOrbArtKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.byPlayer_BuffVisualOrbArtKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualOrbsDat>> GetManyToManyByPlayer_BuffVisualOrbArtKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualOrbsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualOrbsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPlayer_BuffVisualOrbArtKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualOrbsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.BuffVisualOrbArtKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisualOrbArtKeys2(int? key, out BuffVisualOrbsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffVisualOrbArtKeys2(key, out var items))
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
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.BuffVisualOrbArtKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisualOrbArtKeys2(int? key, out IReadOnlyList<BuffVisualOrbsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffVisualOrbsDat>();
            return false;
        }

        if (byBuffVisualOrbArtKeys2 is null)
        {
            byBuffVisualOrbArtKeys2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffVisualOrbArtKeys2;
                foreach (var listKey in itemKey)
                {
                    if (!byBuffVisualOrbArtKeys2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBuffVisualOrbArtKeys2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBuffVisualOrbArtKeys2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffVisualOrbsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffVisualOrbsDat"/> with <see cref="BuffVisualOrbsDat.byBuffVisualOrbArtKeys2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffVisualOrbsDat>> GetManyToManyByBuffVisualOrbArtKeys2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffVisualOrbsDat>>();
        }

        var items = new List<ResultItem<int, BuffVisualOrbsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisualOrbArtKeys2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffVisualOrbsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BuffVisualOrbsDat[] Load()
    {
        const string filePath = "Data/BuffVisualOrbs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BuffVisualOrbsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffVisualOrbTypesKey
            (var buffvisualorbtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BuffVisualOrbArtKeys
            (var tempbuffvisualorbartkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var buffvisualorbartkeysLoading = tempbuffvisualorbartkeysLoading.AsReadOnly();

            // loading Player_BuffVisualOrbArtKeys
            (var tempplayer_buffvisualorbartkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var player_buffvisualorbartkeysLoading = tempplayer_buffvisualorbartkeysLoading.AsReadOnly();

            // loading BuffVisualOrbArtKeys2
            (var tempbuffvisualorbartkeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var buffvisualorbartkeys2Loading = tempbuffvisualorbartkeys2Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BuffVisualOrbsDat()
            {
                Id = idLoading,
                BuffVisualOrbTypesKey = buffvisualorbtypeskeyLoading,
                BuffVisualOrbArtKeys = buffvisualorbartkeysLoading,
                Player_BuffVisualOrbArtKeys = player_buffvisualorbartkeysLoading,
                BuffVisualOrbArtKeys2 = buffvisualorbartkeys2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
