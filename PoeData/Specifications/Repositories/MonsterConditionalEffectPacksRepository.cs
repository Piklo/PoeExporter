using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterConditionalEffectPacksDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterConditionalEffectPacksRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterConditionalEffectPacksDat> Items { get; }

    private Dictionary<string, List<MonsterConditionalEffectPacksDat>>? byId;
    private Dictionary<int, List<MonsterConditionalEffectPacksDat>>? byMiscEffectPack1;
    private Dictionary<int, List<MonsterConditionalEffectPacksDat>>? byMiscEffectPack2;
    private Dictionary<int, List<MonsterConditionalEffectPacksDat>>? byMiscEffectPack3;
    private Dictionary<int, List<MonsterConditionalEffectPacksDat>>? byMiscEffectPack4;
    private Dictionary<int, List<MonsterConditionalEffectPacksDat>>? byUnknown72;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterConditionalEffectPacksRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterConditionalEffectPacksRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MonsterConditionalEffectPacksDat? item)
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
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MonsterConditionalEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
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
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterConditionalEffectPacksDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterConditionalEffectPacksDat>>();
        }

        var items = new List<ResultItem<string, MonsterConditionalEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterConditionalEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.MiscEffectPack1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscEffectPack1(int? key, out MonsterConditionalEffectPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscEffectPack1(key, out var items))
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
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.MiscEffectPack1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscEffectPack1(int? key, out IReadOnlyList<MonsterConditionalEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        if (byMiscEffectPack1 is null)
        {
            byMiscEffectPack1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscEffectPack1;
                foreach (var listKey in itemKey)
                {
                    if (!byMiscEffectPack1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMiscEffectPack1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMiscEffectPack1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.byMiscEffectPack1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterConditionalEffectPacksDat>> GetManyToManyByMiscEffectPack1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterConditionalEffectPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterConditionalEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscEffectPack1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterConditionalEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.MiscEffectPack2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscEffectPack2(int? key, out MonsterConditionalEffectPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscEffectPack2(key, out var items))
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
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.MiscEffectPack2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscEffectPack2(int? key, out IReadOnlyList<MonsterConditionalEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        if (byMiscEffectPack2 is null)
        {
            byMiscEffectPack2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscEffectPack2;
                foreach (var listKey in itemKey)
                {
                    if (!byMiscEffectPack2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMiscEffectPack2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMiscEffectPack2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.byMiscEffectPack2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterConditionalEffectPacksDat>> GetManyToManyByMiscEffectPack2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterConditionalEffectPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterConditionalEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscEffectPack2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterConditionalEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.MiscEffectPack3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscEffectPack3(int? key, out MonsterConditionalEffectPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscEffectPack3(key, out var items))
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
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.MiscEffectPack3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscEffectPack3(int? key, out IReadOnlyList<MonsterConditionalEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        if (byMiscEffectPack3 is null)
        {
            byMiscEffectPack3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscEffectPack3;
                foreach (var listKey in itemKey)
                {
                    if (!byMiscEffectPack3.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMiscEffectPack3.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMiscEffectPack3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.byMiscEffectPack3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterConditionalEffectPacksDat>> GetManyToManyByMiscEffectPack3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterConditionalEffectPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterConditionalEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscEffectPack3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterConditionalEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.MiscEffectPack4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscEffectPack4(int? key, out MonsterConditionalEffectPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscEffectPack4(key, out var items))
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
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.MiscEffectPack4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscEffectPack4(int? key, out IReadOnlyList<MonsterConditionalEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        if (byMiscEffectPack4 is null)
        {
            byMiscEffectPack4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscEffectPack4;
                foreach (var listKey in itemKey)
                {
                    if (!byMiscEffectPack4.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMiscEffectPack4.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMiscEffectPack4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.byMiscEffectPack4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterConditionalEffectPacksDat>> GetManyToManyByMiscEffectPack4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterConditionalEffectPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterConditionalEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscEffectPack4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterConditionalEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out MonsterConditionalEffectPacksDat? item)
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
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<MonsterConditionalEffectPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterConditionalEffectPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterConditionalEffectPacksDat"/> with <see cref="MonsterConditionalEffectPacksDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterConditionalEffectPacksDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterConditionalEffectPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterConditionalEffectPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterConditionalEffectPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterConditionalEffectPacksDat[] Load()
    {
        const string filePath = "Data/MonsterConditionalEffectPacks.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterConditionalEffectPacksDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscEffectPack1
            (var tempmisceffectpack1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var misceffectpack1Loading = tempmisceffectpack1Loading.AsReadOnly();

            // loading MiscEffectPack2
            (var tempmisceffectpack2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var misceffectpack2Loading = tempmisceffectpack2Loading.AsReadOnly();

            // loading MiscEffectPack3
            (var tempmisceffectpack3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var misceffectpack3Loading = tempmisceffectpack3Loading.AsReadOnly();

            // loading MiscEffectPack4
            (var tempmisceffectpack4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var misceffectpack4Loading = tempmisceffectpack4Loading.AsReadOnly();

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterConditionalEffectPacksDat()
            {
                Id = idLoading,
                MiscEffectPack1 = misceffectpack1Loading,
                MiscEffectPack2 = misceffectpack2Loading,
                MiscEffectPack3 = misceffectpack3Loading,
                MiscEffectPack4 = misceffectpack4Loading,
                Unknown72 = unknown72Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
