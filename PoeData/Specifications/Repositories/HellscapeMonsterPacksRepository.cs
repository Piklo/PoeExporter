using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HellscapeMonsterPacksDat"/> related data and helper methods.
/// </summary>
public sealed class HellscapeMonsterPacksRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HellscapeMonsterPacksDat> Items { get; }

    private Dictionary<string, List<HellscapeMonsterPacksDat>>? byId;
    private Dictionary<int, List<HellscapeMonsterPacksDat>>? byMonsterPack;
    private Dictionary<int, List<HellscapeMonsterPacksDat>>? byFaction;
    private Dictionary<bool, List<HellscapeMonsterPacksDat>>? byIsDonutPack;
    private Dictionary<bool, List<HellscapeMonsterPacksDat>>? byIsElite;
    private Dictionary<int, List<HellscapeMonsterPacksDat>>? byMinLevel;
    private Dictionary<int, List<HellscapeMonsterPacksDat>>? byMaxLevel;

    /// <summary>
    /// Initializes a new instance of the <see cref="HellscapeMonsterPacksRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HellscapeMonsterPacksRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HellscapeMonsterPacksDat? item)
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
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HellscapeMonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
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
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HellscapeMonsterPacksDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HellscapeMonsterPacksDat>>();
        }

        var items = new List<ResultItem<string, HellscapeMonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HellscapeMonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.MonsterPack"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterPack(int? key, out HellscapeMonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterPack(key, out var items))
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
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.MonsterPack"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterPack(int? key, out IReadOnlyList<HellscapeMonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        if (byMonsterPack is null)
        {
            byMonsterPack = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterPack;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterPack.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterPack.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterPack.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.byMonsterPack"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeMonsterPacksDat>> GetManyToManyByMonsterPack(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeMonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, HellscapeMonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterPack(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeMonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.Faction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFaction(int? key, out HellscapeMonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFaction(key, out var items))
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
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.Faction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFaction(int? key, out IReadOnlyList<HellscapeMonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        if (byFaction is null)
        {
            byFaction = new();
            foreach (var item in Items)
            {
                var itemKey = item.Faction;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFaction.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFaction.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFaction.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.byFaction"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeMonsterPacksDat>> GetManyToManyByFaction(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeMonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, HellscapeMonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFaction(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeMonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.IsDonutPack"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsDonutPack(bool? key, out HellscapeMonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsDonutPack(key, out var items))
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
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.IsDonutPack"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsDonutPack(bool? key, out IReadOnlyList<HellscapeMonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        if (byIsDonutPack is null)
        {
            byIsDonutPack = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsDonutPack;

                if (!byIsDonutPack.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsDonutPack.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsDonutPack.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.byIsDonutPack"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HellscapeMonsterPacksDat>> GetManyToManyByIsDonutPack(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HellscapeMonsterPacksDat>>();
        }

        var items = new List<ResultItem<bool, HellscapeMonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsDonutPack(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HellscapeMonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.IsElite"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsElite(bool? key, out HellscapeMonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsElite(key, out var items))
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
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.IsElite"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsElite(bool? key, out IReadOnlyList<HellscapeMonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        if (byIsElite is null)
        {
            byIsElite = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsElite;

                if (!byIsElite.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsElite.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsElite.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.byIsElite"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HellscapeMonsterPacksDat>> GetManyToManyByIsElite(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HellscapeMonsterPacksDat>>();
        }

        var items = new List<ResultItem<bool, HellscapeMonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsElite(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HellscapeMonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out HellscapeMonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLevel(key, out var items))
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
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<HellscapeMonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        if (byMinLevel is null)
        {
            byMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLevel;

                if (!byMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeMonsterPacksDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeMonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, HellscapeMonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeMonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out HellscapeMonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxLevel(key, out var items))
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
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<HellscapeMonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        if (byMaxLevel is null)
        {
            byMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxLevel;

                if (!byMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeMonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeMonsterPacksDat"/> with <see cref="HellscapeMonsterPacksDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeMonsterPacksDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeMonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, HellscapeMonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeMonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HellscapeMonsterPacksDat[] Load()
    {
        const string filePath = "Data/HellscapeMonsterPacks.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeMonsterPacksDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterPack
            (var monsterpackLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Faction
            (var factionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsDonutPack
            (var isdonutpackLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsElite
            (var iseliteLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeMonsterPacksDat()
            {
                Id = idLoading,
                MonsterPack = monsterpackLoading,
                Faction = factionLoading,
                IsDonutPack = isdonutpackLoading,
                IsElite = iseliteLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
