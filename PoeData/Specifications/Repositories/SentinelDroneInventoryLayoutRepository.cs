﻿using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SentinelDroneInventoryLayoutDat"/> related data and helper methods.
/// </summary>
public sealed class SentinelDroneInventoryLayoutRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SentinelDroneInventoryLayoutDat> Items { get; }

    private Dictionary<int, List<SentinelDroneInventoryLayoutDat>>? byDroneType;
    private Dictionary<int, List<SentinelDroneInventoryLayoutDat>>? byUnknown16;
    private Dictionary<int, List<SentinelDroneInventoryLayoutDat>>? byUnknown20;
    private Dictionary<int, List<SentinelDroneInventoryLayoutDat>>? byUnknown24;
    private Dictionary<int, List<SentinelDroneInventoryLayoutDat>>? byUnknown28;

    /// <summary>
    /// Initializes a new instance of the <see cref="SentinelDroneInventoryLayoutRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SentinelDroneInventoryLayoutRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.DroneType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDroneType(int? key, out SentinelDroneInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDroneType(key, out var items))
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
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.DroneType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDroneType(int? key, out IReadOnlyList<SentinelDroneInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelDroneInventoryLayoutDat>();
            return false;
        }

        if (byDroneType is null)
        {
            byDroneType = new();
            foreach (var item in Items)
            {
                var itemKey = item.DroneType;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDroneType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDroneType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDroneType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelDroneInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.byDroneType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelDroneInventoryLayoutDat>> GetManyToManyByDroneType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelDroneInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, SentinelDroneInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDroneType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelDroneInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out SentinelDroneInventoryLayoutDat? item)
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
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<SentinelDroneInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelDroneInventoryLayoutDat>();
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
            items = Array.Empty<SentinelDroneInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelDroneInventoryLayoutDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelDroneInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, SentinelDroneInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelDroneInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out SentinelDroneInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown20(key, out var items))
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
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<SentinelDroneInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelDroneInventoryLayoutDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;

                if (!byUnknown20.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelDroneInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelDroneInventoryLayoutDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelDroneInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, SentinelDroneInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelDroneInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out SentinelDroneInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown24(key, out var items))
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
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<SentinelDroneInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelDroneInventoryLayoutDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;

                if (!byUnknown24.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelDroneInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelDroneInventoryLayoutDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelDroneInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, SentinelDroneInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelDroneInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out SentinelDroneInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<SentinelDroneInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SentinelDroneInventoryLayoutDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SentinelDroneInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SentinelDroneInventoryLayoutDat"/> with <see cref="SentinelDroneInventoryLayoutDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SentinelDroneInventoryLayoutDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SentinelDroneInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, SentinelDroneInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SentinelDroneInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SentinelDroneInventoryLayoutDat[] Load()
    {
        const string filePath = "Data/SentinelDroneInventoryLayout.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SentinelDroneInventoryLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading DroneType
            (var dronetypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SentinelDroneInventoryLayoutDat()
            {
                DroneType = dronetypeLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
