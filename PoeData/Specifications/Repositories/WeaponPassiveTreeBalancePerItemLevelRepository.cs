using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> related data and helper methods.
/// </summary>
public sealed class WeaponPassiveTreeBalancePerItemLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<WeaponPassiveTreeBalancePerItemLevelDat> Items { get; }

    private Dictionary<int, List<WeaponPassiveTreeBalancePerItemLevelDat>>? byLevel;
    private Dictionary<int, List<WeaponPassiveTreeBalancePerItemLevelDat>>? byBar1;
    private Dictionary<int, List<WeaponPassiveTreeBalancePerItemLevelDat>>? byBar2;
    private Dictionary<int, List<WeaponPassiveTreeBalancePerItemLevelDat>>? byBar3;
    private Dictionary<int, List<WeaponPassiveTreeBalancePerItemLevelDat>>? byBar4;
    private Dictionary<int, List<WeaponPassiveTreeBalancePerItemLevelDat>>? byBar5;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeaponPassiveTreeBalancePerItemLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal WeaponPassiveTreeBalancePerItemLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out WeaponPassiveTreeBalancePerItemLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevel(key, out var items))
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
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<WeaponPassiveTreeBalancePerItemLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        if (byLevel is null)
        {
            byLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.Level;

                if (!byLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();
        }

        var items = new List<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Bar1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBar1(int? key, out WeaponPassiveTreeBalancePerItemLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBar1(key, out var items))
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
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Bar1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBar1(int? key, out IReadOnlyList<WeaponPassiveTreeBalancePerItemLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        if (byBar1 is null)
        {
            byBar1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Bar1;

                if (!byBar1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBar1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBar1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.byBar1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>> GetManyToManyByBar1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();
        }

        var items = new List<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBar1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Bar2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBar2(int? key, out WeaponPassiveTreeBalancePerItemLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBar2(key, out var items))
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
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Bar2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBar2(int? key, out IReadOnlyList<WeaponPassiveTreeBalancePerItemLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        if (byBar2 is null)
        {
            byBar2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Bar2;

                if (!byBar2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBar2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBar2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.byBar2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>> GetManyToManyByBar2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();
        }

        var items = new List<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBar2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Bar3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBar3(int? key, out WeaponPassiveTreeBalancePerItemLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBar3(key, out var items))
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
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Bar3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBar3(int? key, out IReadOnlyList<WeaponPassiveTreeBalancePerItemLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        if (byBar3 is null)
        {
            byBar3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Bar3;

                if (!byBar3.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBar3.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBar3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.byBar3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>> GetManyToManyByBar3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();
        }

        var items = new List<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBar3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Bar4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBar4(int? key, out WeaponPassiveTreeBalancePerItemLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBar4(key, out var items))
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
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Bar4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBar4(int? key, out IReadOnlyList<WeaponPassiveTreeBalancePerItemLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        if (byBar4 is null)
        {
            byBar4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Bar4;

                if (!byBar4.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBar4.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBar4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.byBar4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>> GetManyToManyByBar4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();
        }

        var items = new List<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBar4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Bar5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBar5(int? key, out WeaponPassiveTreeBalancePerItemLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBar5(key, out var items))
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
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.Bar5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBar5(int? key, out IReadOnlyList<WeaponPassiveTreeBalancePerItemLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        if (byBar5 is null)
        {
            byBar5 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Bar5;

                if (!byBar5.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBar5.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBar5.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WeaponPassiveTreeBalancePerItemLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WeaponPassiveTreeBalancePerItemLevelDat"/> with <see cref="WeaponPassiveTreeBalancePerItemLevelDat.byBar5"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>> GetManyToManyByBar5(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();
        }

        var items = new List<ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBar5(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WeaponPassiveTreeBalancePerItemLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private WeaponPassiveTreeBalancePerItemLevelDat[] Load()
    {
        const string filePath = "Data/WeaponPassiveTreeBalancePerItemLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WeaponPassiveTreeBalancePerItemLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Bar1
            (var bar1Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Bar2
            (var bar2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Bar3
            (var bar3Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Bar4
            (var bar4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Bar5
            (var bar5Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WeaponPassiveTreeBalancePerItemLevelDat()
            {
                Level = levelLoading,
                Bar1 = bar1Loading,
                Bar2 = bar2Loading,
                Bar3 = bar3Loading,
                Bar4 = bar4Loading,
                Bar5 = bar5Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
