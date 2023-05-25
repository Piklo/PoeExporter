using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UniqueMapInfoDat"/> related data and helper methods.
/// </summary>
public sealed class UniqueMapInfoRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UniqueMapInfoDat> Items { get; }

    private Dictionary<int, List<UniqueMapInfoDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<UniqueMapInfoDat>>? byWord;
    private Dictionary<int, List<UniqueMapInfoDat>>? byFlavourTextKey;
    private Dictionary<int, List<UniqueMapInfoDat>>? byItemVisualIdentityKey;
    private Dictionary<bool, List<UniqueMapInfoDat>>? byUnknown64;

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueMapInfoRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UniqueMapInfoRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out UniqueMapInfoDat? item)
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
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<UniqueMapInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapInfoDat>();
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
            items = Array.Empty<UniqueMapInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueMapInfoDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueMapInfoDat>>();
        }

        var items = new List<ResultItem<int, UniqueMapInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueMapInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.Word"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWord(int? key, out UniqueMapInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWord(key, out var items))
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
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.Word"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWord(int? key, out IReadOnlyList<UniqueMapInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapInfoDat>();
            return false;
        }

        if (byWord is null)
        {
            byWord = new();
            foreach (var item in Items)
            {
                var itemKey = item.Word;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWord.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWord.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWord.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueMapInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.byWord"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueMapInfoDat>> GetManyToManyByWord(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueMapInfoDat>>();
        }

        var items = new List<ResultItem<int, UniqueMapInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWord(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueMapInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.FlavourTextKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourTextKey(int? key, out UniqueMapInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlavourTextKey(key, out var items))
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
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.FlavourTextKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourTextKey(int? key, out IReadOnlyList<UniqueMapInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapInfoDat>();
            return false;
        }

        if (byFlavourTextKey is null)
        {
            byFlavourTextKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlavourTextKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFlavourTextKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFlavourTextKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFlavourTextKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueMapInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.byFlavourTextKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueMapInfoDat>> GetManyToManyByFlavourTextKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueMapInfoDat>>();
        }

        var items = new List<ResultItem<int, UniqueMapInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourTextKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueMapInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentityKey(int? key, out UniqueMapInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualIdentityKey(key, out var items))
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
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentityKey(int? key, out IReadOnlyList<UniqueMapInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapInfoDat>();
            return false;
        }

        if (byItemVisualIdentityKey is null)
        {
            byItemVisualIdentityKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualIdentityKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualIdentityKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualIdentityKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualIdentityKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueMapInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.byItemVisualIdentityKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueMapInfoDat>> GetManyToManyByItemVisualIdentityKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueMapInfoDat>>();
        }

        var items = new List<ResultItem<int, UniqueMapInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentityKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueMapInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(bool? key, out UniqueMapInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(bool? key, out IReadOnlyList<UniqueMapInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapInfoDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueMapInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapInfoDat"/> with <see cref="UniqueMapInfoDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UniqueMapInfoDat>> GetManyToManyByUnknown64(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UniqueMapInfoDat>>();
        }

        var items = new List<ResultItem<bool, UniqueMapInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UniqueMapInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UniqueMapInfoDat[] Load()
    {
        const string filePath = "Data/UniqueMapInfo.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueMapInfoDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Word
            (var wordLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading FlavourTextKey
            (var flavourtextkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemVisualIdentityKey
            (var itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueMapInfoDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Word = wordLoading,
                FlavourTextKey = flavourtextkeyLoading,
                ItemVisualIdentityKey = itemvisualidentitykeyLoading,
                Unknown64 = unknown64Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
