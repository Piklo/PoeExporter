﻿using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="IncursionArchitectDat"/> related data and helper methods.
/// </summary>
public sealed class IncursionArchitectRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<IncursionArchitectDat> Items { get; }

    private Dictionary<int, List<IncursionArchitectDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<IncursionArchitectDat>>? byMinLevel;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncursionArchitectRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal IncursionArchitectRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="IncursionArchitectDat"/> with <see cref="IncursionArchitectDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out IncursionArchitectDat? item)
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
    /// Tries to get <see cref="IncursionArchitectDat"/> with <see cref="IncursionArchitectDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<IncursionArchitectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionArchitectDat>();
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
            items = Array.Empty<IncursionArchitectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionArchitectDat"/> with <see cref="IncursionArchitectDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionArchitectDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionArchitectDat>>();
        }

        var items = new List<ResultItem<int, IncursionArchitectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionArchitectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionArchitectDat"/> with <see cref="IncursionArchitectDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out IncursionArchitectDat? item)
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
    /// Tries to get <see cref="IncursionArchitectDat"/> with <see cref="IncursionArchitectDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<IncursionArchitectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionArchitectDat>();
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
            items = Array.Empty<IncursionArchitectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionArchitectDat"/> with <see cref="IncursionArchitectDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionArchitectDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionArchitectDat>>();
        }

        var items = new List<ResultItem<int, IncursionArchitectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionArchitectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private IncursionArchitectDat[] Load()
    {
        const string filePath = "Data/IncursionArchitect.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionArchitectDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionArchitectDat()
            {
                MonsterVarietiesKey = monstervarietieskeyLoading,
                MinLevel = minlevelLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
