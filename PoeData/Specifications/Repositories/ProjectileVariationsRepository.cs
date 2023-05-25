﻿using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ProjectileVariationsDat"/> related data and helper methods.
/// </summary>
public sealed class ProjectileVariationsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ProjectileVariationsDat> Items { get; }

    private Dictionary<string, List<ProjectileVariationsDat>>? byId;
    private Dictionary<int, List<ProjectileVariationsDat>>? byProjectileKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectileVariationsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ProjectileVariationsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ProjectileVariationsDat"/> with <see cref="ProjectileVariationsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ProjectileVariationsDat? item)
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
    /// Tries to get <see cref="ProjectileVariationsDat"/> with <see cref="ProjectileVariationsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ProjectileVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectileVariationsDat>();
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
            items = Array.Empty<ProjectileVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectileVariationsDat"/> with <see cref="ProjectileVariationsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ProjectileVariationsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ProjectileVariationsDat>>();
        }

        var items = new List<ResultItem<string, ProjectileVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ProjectileVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectileVariationsDat"/> with <see cref="ProjectileVariationsDat.ProjectileKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProjectileKey(int? key, out ProjectileVariationsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProjectileKey(key, out var items))
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
    /// Tries to get <see cref="ProjectileVariationsDat"/> with <see cref="ProjectileVariationsDat.ProjectileKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProjectileKey(int? key, out IReadOnlyList<ProjectileVariationsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ProjectileVariationsDat>();
            return false;
        }

        if (byProjectileKey is null)
        {
            byProjectileKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ProjectileKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byProjectileKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byProjectileKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byProjectileKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ProjectileVariationsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ProjectileVariationsDat"/> with <see cref="ProjectileVariationsDat.byProjectileKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ProjectileVariationsDat>> GetManyToManyByProjectileKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ProjectileVariationsDat>>();
        }

        var items = new List<ResultItem<int, ProjectileVariationsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProjectileKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ProjectileVariationsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ProjectileVariationsDat[] Load()
    {
        const string filePath = "Data/ProjectileVariations.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ProjectileVariationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ProjectileKey
            (var projectilekeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ProjectileVariationsDat()
            {
                Id = idLoading,
                ProjectileKey = projectilekeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
