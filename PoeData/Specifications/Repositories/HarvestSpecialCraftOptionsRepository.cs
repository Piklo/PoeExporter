﻿using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestSpecialCraftOptionsDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestSpecialCraftOptionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestSpecialCraftOptionsDat> Items { get; }

    private Dictionary<int, List<HarvestSpecialCraftOptionsDat>>? byUnknown0;
    private Dictionary<int, List<HarvestSpecialCraftOptionsDat>>? byUnknown16;
    private Dictionary<int, List<HarvestSpecialCraftOptionsDat>>? byUnknown20;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestSpecialCraftOptionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestSpecialCraftOptionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSpecialCraftOptionsDat"/> with <see cref="HarvestSpecialCraftOptionsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out HarvestSpecialCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown0(key, out var items))
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
    /// Tries to get <see cref="HarvestSpecialCraftOptionsDat"/> with <see cref="HarvestSpecialCraftOptionsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<HarvestSpecialCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSpecialCraftOptionsDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSpecialCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSpecialCraftOptionsDat"/> with <see cref="HarvestSpecialCraftOptionsDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSpecialCraftOptionsDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSpecialCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestSpecialCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSpecialCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSpecialCraftOptionsDat"/> with <see cref="HarvestSpecialCraftOptionsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out HarvestSpecialCraftOptionsDat? item)
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
    /// Tries to get <see cref="HarvestSpecialCraftOptionsDat"/> with <see cref="HarvestSpecialCraftOptionsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<HarvestSpecialCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSpecialCraftOptionsDat>();
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
            items = Array.Empty<HarvestSpecialCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSpecialCraftOptionsDat"/> with <see cref="HarvestSpecialCraftOptionsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSpecialCraftOptionsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSpecialCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestSpecialCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSpecialCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSpecialCraftOptionsDat"/> with <see cref="HarvestSpecialCraftOptionsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out HarvestSpecialCraftOptionsDat? item)
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
    /// Tries to get <see cref="HarvestSpecialCraftOptionsDat"/> with <see cref="HarvestSpecialCraftOptionsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<HarvestSpecialCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSpecialCraftOptionsDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown20.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSpecialCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSpecialCraftOptionsDat"/> with <see cref="HarvestSpecialCraftOptionsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSpecialCraftOptionsDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSpecialCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestSpecialCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSpecialCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestSpecialCraftOptionsDat[] Load()
    {
        const string filePath = "Data/HarvestSpecialCraftOptions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestSpecialCraftOptionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestSpecialCraftOptionsDat()
            {
                Unknown0 = unknown0Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
