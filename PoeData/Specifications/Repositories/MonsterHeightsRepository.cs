using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterHeightsDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterHeightsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterHeightsDat> Items { get; }

    private Dictionary<int, List<MonsterHeightsDat>>? byMonsterVariety;
    private Dictionary<float, List<MonsterHeightsDat>>? byUnknown16;
    private Dictionary<int, List<MonsterHeightsDat>>? byMonsterHeightBracket;
    private Dictionary<int, List<MonsterHeightsDat>>? byUnknown36;
    private Dictionary<int, List<MonsterHeightsDat>>? byUnknown40;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterHeightsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterHeightsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.MonsterVariety"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVariety(int? key, out MonsterHeightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVariety(key, out var items))
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
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.MonsterVariety"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVariety(int? key, out IReadOnlyList<MonsterHeightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightsDat>();
            return false;
        }

        if (byMonsterVariety is null)
        {
            byMonsterVariety = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVariety;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterVariety.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterVariety.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterVariety.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterHeightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.byMonsterVariety"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterHeightsDat>> GetManyToManyByMonsterVariety(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterHeightsDat>>();
        }

        var items = new List<ResultItem<int, MonsterHeightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVariety(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterHeightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(float? key, out MonsterHeightsDat? item)
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
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(float? key, out IReadOnlyList<MonsterHeightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterHeightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, MonsterHeightsDat>> GetManyToManyByUnknown16(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, MonsterHeightsDat>>();
        }

        var items = new List<ResultItem<float, MonsterHeightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, MonsterHeightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.MonsterHeightBracket"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterHeightBracket(int? key, out MonsterHeightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterHeightBracket(key, out var items))
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
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.MonsterHeightBracket"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterHeightBracket(int? key, out IReadOnlyList<MonsterHeightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightsDat>();
            return false;
        }

        if (byMonsterHeightBracket is null)
        {
            byMonsterHeightBracket = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterHeightBracket;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterHeightBracket.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterHeightBracket.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterHeightBracket.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterHeightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.byMonsterHeightBracket"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterHeightsDat>> GetManyToManyByMonsterHeightBracket(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterHeightsDat>>();
        }

        var items = new List<ResultItem<int, MonsterHeightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterHeightBracket(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterHeightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out MonsterHeightsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown36(key, out var items))
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
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<MonsterHeightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightsDat>();
            return false;
        }

        if (byUnknown36 is null)
        {
            byUnknown36 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown36;

                if (!byUnknown36.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown36.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown36.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterHeightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterHeightsDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterHeightsDat>>();
        }

        var items = new List<ResultItem<int, MonsterHeightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterHeightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out MonsterHeightsDat? item)
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
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<MonsterHeightsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterHeightsDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;

                if (!byUnknown40.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterHeightsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterHeightsDat"/> with <see cref="MonsterHeightsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterHeightsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterHeightsDat>>();
        }

        var items = new List<ResultItem<int, MonsterHeightsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterHeightsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterHeightsDat[] Load()
    {
        const string filePath = "Data/MonsterHeights.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterHeightsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVariety
            (var monstervarietyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading MonsterHeightBracket
            (var monsterheightbracketLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterHeightsDat()
            {
                MonsterVariety = monstervarietyLoading,
                Unknown16 = unknown16Loading,
                MonsterHeightBracket = monsterheightbracketLoading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
