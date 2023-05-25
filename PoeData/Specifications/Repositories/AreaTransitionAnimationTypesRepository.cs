﻿using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AreaTransitionAnimationTypesDat"/> related data and helper methods.
/// </summary>
public sealed class AreaTransitionAnimationTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AreaTransitionAnimationTypesDat> Items { get; }

    private Dictionary<string, List<AreaTransitionAnimationTypesDat>>? byId;

    /// <summary>
    /// Initializes a new instance of the <see cref="AreaTransitionAnimationTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AreaTransitionAnimationTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionAnimationTypesDat"/> with <see cref="AreaTransitionAnimationTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AreaTransitionAnimationTypesDat? item)
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
    /// Tries to get <see cref="AreaTransitionAnimationTypesDat"/> with <see cref="AreaTransitionAnimationTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AreaTransitionAnimationTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionAnimationTypesDat>();
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
            items = Array.Empty<AreaTransitionAnimationTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionAnimationTypesDat"/> with <see cref="AreaTransitionAnimationTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AreaTransitionAnimationTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AreaTransitionAnimationTypesDat>>();
        }

        var items = new List<ResultItem<string, AreaTransitionAnimationTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AreaTransitionAnimationTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AreaTransitionAnimationTypesDat[] Load()
    {
        const string filePath = "Data/AreaTransitionAnimationTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AreaTransitionAnimationTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AreaTransitionAnimationTypesDat()
            {
                Id = idLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
