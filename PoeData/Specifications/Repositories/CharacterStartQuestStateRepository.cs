using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CharacterStartQuestStateDat"/> related data and helper methods.
/// </summary>
public sealed class CharacterStartQuestStateRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CharacterStartQuestStateDat> Items { get; }

    private Dictionary<string, List<CharacterStartQuestStateDat>>? byId;
    private Dictionary<int, List<CharacterStartQuestStateDat>>? byQuestKeys;
    private Dictionary<int, List<CharacterStartQuestStateDat>>? byQuestStates;
    private Dictionary<int, List<CharacterStartQuestStateDat>>? byUnknown40;
    private Dictionary<int, List<CharacterStartQuestStateDat>>? byMapPinsKeys;
    private Dictionary<int, List<CharacterStartQuestStateDat>>? byUnknown72;
    private Dictionary<int, List<CharacterStartQuestStateDat>>? byUnknown88;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterStartQuestStateRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CharacterStartQuestStateRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CharacterStartQuestStateDat? item)
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
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CharacterStartQuestStateDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
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
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterStartQuestStateDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterStartQuestStateDat>>();
        }

        var items = new List<ResultItem<string, CharacterStartQuestStateDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterStartQuestStateDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.QuestKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestKeys(int? key, out CharacterStartQuestStateDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestKeys(key, out var items))
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
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.QuestKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestKeys(int? key, out IReadOnlyList<CharacterStartQuestStateDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        if (byQuestKeys is null)
        {
            byQuestKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byQuestKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byQuestKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byQuestKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.byQuestKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartQuestStateDat>> GetManyToManyByQuestKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartQuestStateDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartQuestStateDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartQuestStateDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.QuestStates"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestStates(int? key, out CharacterStartQuestStateDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestStates(key, out var items))
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
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.QuestStates"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestStates(int? key, out IReadOnlyList<CharacterStartQuestStateDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        if (byQuestStates is null)
        {
            byQuestStates = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestStates;
                foreach (var listKey in itemKey)
                {
                    if (!byQuestStates.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byQuestStates.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byQuestStates.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.byQuestStates"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartQuestStateDat>> GetManyToManyByQuestStates(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartQuestStateDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartQuestStateDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestStates(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartQuestStateDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out CharacterStartQuestStateDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<CharacterStartQuestStateDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown40.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown40.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartQuestStateDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartQuestStateDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartQuestStateDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartQuestStateDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.MapPinsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapPinsKeys(int? key, out CharacterStartQuestStateDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapPinsKeys(key, out var items))
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
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.MapPinsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapPinsKeys(int? key, out IReadOnlyList<CharacterStartQuestStateDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        if (byMapPinsKeys is null)
        {
            byMapPinsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapPinsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMapPinsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMapPinsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMapPinsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.byMapPinsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartQuestStateDat>> GetManyToManyByMapPinsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartQuestStateDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartQuestStateDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapPinsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartQuestStateDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out CharacterStartQuestStateDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<CharacterStartQuestStateDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown72.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown72.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartQuestStateDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartQuestStateDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartQuestStateDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartQuestStateDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out CharacterStartQuestStateDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown88(key, out var items))
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
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<CharacterStartQuestStateDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown88.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown88.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterStartQuestStateDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterStartQuestStateDat"/> with <see cref="CharacterStartQuestStateDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterStartQuestStateDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterStartQuestStateDat>>();
        }

        var items = new List<ResultItem<int, CharacterStartQuestStateDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterStartQuestStateDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CharacterStartQuestStateDat[] Load()
    {
        const string filePath = "Data/CharacterStartQuestState.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterStartQuestStateDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading QuestKeys
            (var tempquestkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var questkeysLoading = tempquestkeysLoading.AsReadOnly();

            // loading QuestStates
            (var tempqueststatesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var queststatesLoading = tempqueststatesLoading.AsReadOnly();

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading MapPinsKeys
            (var tempmappinskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var mappinskeysLoading = tempmappinskeysLoading.AsReadOnly();

            // loading Unknown72
            (var tempunknown72Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown72Loading = tempunknown72Loading.AsReadOnly();

            // loading Unknown88
            (var tempunknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown88Loading = tempunknown88Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterStartQuestStateDat()
            {
                Id = idLoading,
                QuestKeys = questkeysLoading,
                QuestStates = queststatesLoading,
                Unknown40 = unknown40Loading,
                MapPinsKeys = mappinskeysLoading,
                Unknown72 = unknown72Loading,
                Unknown88 = unknown88Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
