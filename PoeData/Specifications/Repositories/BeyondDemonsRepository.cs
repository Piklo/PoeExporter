﻿using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BeyondDemonsDat"/> related data and helper methods.
/// </summary>
public sealed class BeyondDemonsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BeyondDemonsDat> Items { get; }

    private Dictionary<int, List<BeyondDemonsDat>>? byMonsterVarietiesKey;
    private Dictionary<bool, List<BeyondDemonsDat>>? byUnknown16;
    private Dictionary<bool, List<BeyondDemonsDat>>? byUnknown17;

    /// <summary>
    /// Initializes a new instance of the <see cref="BeyondDemonsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BeyondDemonsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BeyondDemonsDat"/> with <see cref="BeyondDemonsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out BeyondDemonsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="BeyondDemonsDat"/> with <see cref="BeyondDemonsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<BeyondDemonsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BeyondDemonsDat>();
            return false;
        }

        if (byMonsterVarietiesKey is null)
        {
            byMonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BeyondDemonsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BeyondDemonsDat"/> with <see cref="BeyondDemonsDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BeyondDemonsDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BeyondDemonsDat>>();
        }

        var items = new List<ResultItem<int, BeyondDemonsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BeyondDemonsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BeyondDemonsDat"/> with <see cref="BeyondDemonsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(bool? key, out BeyondDemonsDat? item)
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
    /// Tries to get <see cref="BeyondDemonsDat"/> with <see cref="BeyondDemonsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(bool? key, out IReadOnlyList<BeyondDemonsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BeyondDemonsDat>();
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
            items = Array.Empty<BeyondDemonsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BeyondDemonsDat"/> with <see cref="BeyondDemonsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BeyondDemonsDat>> GetManyToManyByUnknown16(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BeyondDemonsDat>>();
        }

        var items = new List<ResultItem<bool, BeyondDemonsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BeyondDemonsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BeyondDemonsDat"/> with <see cref="BeyondDemonsDat.Unknown17"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown17(bool? key, out BeyondDemonsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown17(key, out var items))
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
    /// Tries to get <see cref="BeyondDemonsDat"/> with <see cref="BeyondDemonsDat.Unknown17"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown17(bool? key, out IReadOnlyList<BeyondDemonsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BeyondDemonsDat>();
            return false;
        }

        if (byUnknown17 is null)
        {
            byUnknown17 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown17;

                if (!byUnknown17.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown17.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown17.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BeyondDemonsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BeyondDemonsDat"/> with <see cref="BeyondDemonsDat.byUnknown17"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BeyondDemonsDat>> GetManyToManyByUnknown17(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BeyondDemonsDat>>();
        }

        var items = new List<ResultItem<bool, BeyondDemonsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown17(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BeyondDemonsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BeyondDemonsDat[] Load()
    {
        const string filePath = "Data/BeyondDemons.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BeyondDemonsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown17
            (var unknown17Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BeyondDemonsDat()
            {
                MonsterVarietiesKey = monstervarietieskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown17 = unknown17Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
