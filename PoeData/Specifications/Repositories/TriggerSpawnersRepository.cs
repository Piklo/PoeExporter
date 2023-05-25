﻿using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="TriggerSpawnersDat"/> related data and helper methods.
/// </summary>
public sealed class TriggerSpawnersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<TriggerSpawnersDat> Items { get; }

    private Dictionary<string, List<TriggerSpawnersDat>>? byId;
    private Dictionary<int, List<TriggerSpawnersDat>>? byUnknown8;
    private Dictionary<int, List<TriggerSpawnersDat>>? byUnknown24;
    private Dictionary<int, List<TriggerSpawnersDat>>? byUnknown28;
    private Dictionary<bool, List<TriggerSpawnersDat>>? byUnknown44;

    /// <summary>
    /// Initializes a new instance of the <see cref="TriggerSpawnersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal TriggerSpawnersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out TriggerSpawnersDat? item)
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
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<TriggerSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TriggerSpawnersDat>();
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
            items = Array.Empty<TriggerSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, TriggerSpawnersDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, TriggerSpawnersDat>>();
        }

        var items = new List<ResultItem<string, TriggerSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, TriggerSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out TriggerSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<TriggerSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TriggerSpawnersDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown8.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown8.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TriggerSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TriggerSpawnersDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TriggerSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TriggerSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TriggerSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out TriggerSpawnersDat? item)
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
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<TriggerSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TriggerSpawnersDat>();
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
            items = Array.Empty<TriggerSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TriggerSpawnersDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TriggerSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TriggerSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TriggerSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out TriggerSpawnersDat? item)
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
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<TriggerSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TriggerSpawnersDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown28.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown28.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TriggerSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TriggerSpawnersDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TriggerSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TriggerSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TriggerSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(bool? key, out TriggerSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown44(key, out var items))
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
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(bool? key, out IReadOnlyList<TriggerSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TriggerSpawnersDat>();
            return false;
        }

        if (byUnknown44 is null)
        {
            byUnknown44 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown44;

                if (!byUnknown44.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown44.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown44.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TriggerSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TriggerSpawnersDat"/> with <see cref="TriggerSpawnersDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TriggerSpawnersDat>> GetManyToManyByUnknown44(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TriggerSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TriggerSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TriggerSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private TriggerSpawnersDat[] Load()
    {
        const string filePath = "Data/TriggerSpawners.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TriggerSpawnersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var tempunknown8Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown8Loading = tempunknown8Loading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var tempunknown28Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown28Loading = tempunknown28Loading.AsReadOnly();

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TriggerSpawnersDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown44 = unknown44Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
