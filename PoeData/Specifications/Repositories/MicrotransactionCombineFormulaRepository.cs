using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MicrotransactionCombineFormulaDat"/> related data and helper methods.
/// </summary>
public sealed class MicrotransactionCombineFormulaRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MicrotransactionCombineFormulaDat> Items { get; }

    private Dictionary<string, List<MicrotransactionCombineFormulaDat>>? byId;
    private Dictionary<int, List<MicrotransactionCombineFormulaDat>>? byResult_BaseItemTypesKey;
    private Dictionary<int, List<MicrotransactionCombineFormulaDat>>? byIngredients_BaseItemTypesKeys;
    private Dictionary<string, List<MicrotransactionCombineFormulaDat>>? byBK2File;
    private Dictionary<int, List<MicrotransactionCombineFormulaDat>>? byUnknown48;
    private Dictionary<int, List<MicrotransactionCombineFormulaDat>>? byUnknown64;
    private Dictionary<bool, List<MicrotransactionCombineFormulaDat>>? byUnknown68;

    /// <summary>
    /// Initializes a new instance of the <see cref="MicrotransactionCombineFormulaRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MicrotransactionCombineFormulaRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MicrotransactionCombineFormulaDat? item)
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
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MicrotransactionCombineFormulaDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
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
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionCombineFormulaDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionCombineFormulaDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionCombineFormulaDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionCombineFormulaDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Result_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByResult_BaseItemTypesKey(int? key, out MicrotransactionCombineFormulaDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByResult_BaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Result_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByResult_BaseItemTypesKey(int? key, out IReadOnlyList<MicrotransactionCombineFormulaDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        if (byResult_BaseItemTypesKey is null)
        {
            byResult_BaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Result_BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byResult_BaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byResult_BaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byResult_BaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.byResult_BaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionCombineFormulaDat>> GetManyToManyByResult_BaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionCombineFormulaDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionCombineFormulaDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByResult_BaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionCombineFormulaDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Ingredients_BaseItemTypesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIngredients_BaseItemTypesKeys(int? key, out MicrotransactionCombineFormulaDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIngredients_BaseItemTypesKeys(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Ingredients_BaseItemTypesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIngredients_BaseItemTypesKeys(int? key, out IReadOnlyList<MicrotransactionCombineFormulaDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        if (byIngredients_BaseItemTypesKeys is null)
        {
            byIngredients_BaseItemTypesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Ingredients_BaseItemTypesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byIngredients_BaseItemTypesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byIngredients_BaseItemTypesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byIngredients_BaseItemTypesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.byIngredients_BaseItemTypesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionCombineFormulaDat>> GetManyToManyByIngredients_BaseItemTypesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionCombineFormulaDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionCombineFormulaDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIngredients_BaseItemTypesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionCombineFormulaDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.BK2File"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBK2File(string? key, out MicrotransactionCombineFormulaDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBK2File(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.BK2File"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBK2File(string? key, out IReadOnlyList<MicrotransactionCombineFormulaDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        if (byBK2File is null)
        {
            byBK2File = new();
            foreach (var item in Items)
            {
                var itemKey = item.BK2File;

                if (!byBK2File.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBK2File.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBK2File.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.byBK2File"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MicrotransactionCombineFormulaDat>> GetManyToManyByBK2File(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MicrotransactionCombineFormulaDat>>();
        }

        var items = new List<ResultItem<string, MicrotransactionCombineFormulaDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBK2File(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MicrotransactionCombineFormulaDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out MicrotransactionCombineFormulaDat? item)
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
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<MicrotransactionCombineFormulaDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown48.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown48.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionCombineFormulaDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionCombineFormulaDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionCombineFormulaDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionCombineFormulaDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out MicrotransactionCombineFormulaDat? item)
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
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<MicrotransactionCombineFormulaDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
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
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MicrotransactionCombineFormulaDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MicrotransactionCombineFormulaDat>>();
        }

        var items = new List<ResultItem<int, MicrotransactionCombineFormulaDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MicrotransactionCombineFormulaDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(bool? key, out MicrotransactionCombineFormulaDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown68(key, out var items))
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
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(bool? key, out IReadOnlyList<MicrotransactionCombineFormulaDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;

                if (!byUnknown68.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MicrotransactionCombineFormulaDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MicrotransactionCombineFormulaDat"/> with <see cref="MicrotransactionCombineFormulaDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MicrotransactionCombineFormulaDat>> GetManyToManyByUnknown68(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MicrotransactionCombineFormulaDat>>();
        }

        var items = new List<ResultItem<bool, MicrotransactionCombineFormulaDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MicrotransactionCombineFormulaDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MicrotransactionCombineFormulaDat[] Load()
    {
        const string filePath = "Data/MicrotransactionCombineFormula.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionCombineFormulaDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Result_BaseItemTypesKey
            (var result_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Ingredients_BaseItemTypesKeys
            (var tempingredients_baseitemtypeskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var ingredients_baseitemtypeskeysLoading = tempingredients_baseitemtypeskeysLoading.AsReadOnly();

            // loading BK2File
            (var bk2fileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var tempunknown48Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown48Loading = tempunknown48Loading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionCombineFormulaDat()
            {
                Id = idLoading,
                Result_BaseItemTypesKey = result_baseitemtypeskeyLoading,
                Ingredients_BaseItemTypesKeys = ingredients_baseitemtypeskeysLoading,
                BK2File = bk2fileLoading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
