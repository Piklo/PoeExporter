using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="EssenceTypeDat"/> related data and helper methods.
/// </summary>
public sealed class EssenceTypeRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<EssenceTypeDat> Items { get; }

    private Dictionary<string, List<EssenceTypeDat>>? byId;
    private Dictionary<int, List<EssenceTypeDat>>? byEssenceType;
    private Dictionary<bool, List<EssenceTypeDat>>? byIsCorruptedEssence;
    private Dictionary<int, List<EssenceTypeDat>>? byWordsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="EssenceTypeRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal EssenceTypeRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out EssenceTypeDat? item)
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
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<EssenceTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceTypeDat>();
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
            items = Array.Empty<EssenceTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EssenceTypeDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EssenceTypeDat>>();
        }

        var items = new List<ResultItem<string, EssenceTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EssenceTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.EssenceType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEssenceType(int? key, out EssenceTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEssenceType(key, out var items))
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
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.EssenceType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEssenceType(int? key, out IReadOnlyList<EssenceTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceTypeDat>();
            return false;
        }

        if (byEssenceType is null)
        {
            byEssenceType = new();
            foreach (var item in Items)
            {
                var itemKey = item.EssenceType;

                if (!byEssenceType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEssenceType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEssenceType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssenceTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.byEssenceType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssenceTypeDat>> GetManyToManyByEssenceType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssenceTypeDat>>();
        }

        var items = new List<ResultItem<int, EssenceTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEssenceType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssenceTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.IsCorruptedEssence"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsCorruptedEssence(bool? key, out EssenceTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsCorruptedEssence(key, out var items))
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
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.IsCorruptedEssence"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsCorruptedEssence(bool? key, out IReadOnlyList<EssenceTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceTypeDat>();
            return false;
        }

        if (byIsCorruptedEssence is null)
        {
            byIsCorruptedEssence = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsCorruptedEssence;

                if (!byIsCorruptedEssence.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsCorruptedEssence.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsCorruptedEssence.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssenceTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.byIsCorruptedEssence"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EssenceTypeDat>> GetManyToManyByIsCorruptedEssence(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EssenceTypeDat>>();
        }

        var items = new List<ResultItem<bool, EssenceTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsCorruptedEssence(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EssenceTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.WordsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWordsKey(int? key, out EssenceTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWordsKey(key, out var items))
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
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.WordsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWordsKey(int? key, out IReadOnlyList<EssenceTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceTypeDat>();
            return false;
        }

        if (byWordsKey is null)
        {
            byWordsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WordsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWordsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWordsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWordsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssenceTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceTypeDat"/> with <see cref="EssenceTypeDat.byWordsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssenceTypeDat>> GetManyToManyByWordsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssenceTypeDat>>();
        }

        var items = new List<ResultItem<int, EssenceTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWordsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssenceTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private EssenceTypeDat[] Load()
    {
        const string filePath = "Data/EssenceType.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EssenceTypeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EssenceType
            (var essencetypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsCorruptedEssence
            (var iscorruptedessenceLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading WordsKey
            (var wordskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EssenceTypeDat()
            {
                Id = idLoading,
                EssenceType = essencetypeLoading,
                IsCorruptedEssence = iscorruptedessenceLoading,
                WordsKey = wordskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
