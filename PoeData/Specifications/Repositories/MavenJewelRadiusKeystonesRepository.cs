using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MavenJewelRadiusKeystonesDat"/> related data and helper methods.
/// </summary>
public sealed class MavenJewelRadiusKeystonesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MavenJewelRadiusKeystonesDat> Items { get; }

    private Dictionary<int, List<MavenJewelRadiusKeystonesDat>>? byKeystone;

    /// <summary>
    /// Initializes a new instance of the <see cref="MavenJewelRadiusKeystonesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MavenJewelRadiusKeystonesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MavenJewelRadiusKeystonesDat"/> with <see cref="MavenJewelRadiusKeystonesDat.Keystone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKeystone(int? key, out MavenJewelRadiusKeystonesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKeystone(key, out var items))
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
    /// Tries to get <see cref="MavenJewelRadiusKeystonesDat"/> with <see cref="MavenJewelRadiusKeystonesDat.Keystone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKeystone(int? key, out IReadOnlyList<MavenJewelRadiusKeystonesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MavenJewelRadiusKeystonesDat>();
            return false;
        }

        if (byKeystone is null)
        {
            byKeystone = new();
            foreach (var item in Items)
            {
                var itemKey = item.Keystone;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byKeystone.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byKeystone.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byKeystone.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MavenJewelRadiusKeystonesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MavenJewelRadiusKeystonesDat"/> with <see cref="MavenJewelRadiusKeystonesDat.byKeystone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MavenJewelRadiusKeystonesDat>> GetManyToManyByKeystone(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MavenJewelRadiusKeystonesDat>>();
        }

        var items = new List<ResultItem<int, MavenJewelRadiusKeystonesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKeystone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MavenJewelRadiusKeystonesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MavenJewelRadiusKeystonesDat[] Load()
    {
        const string filePath = "Data/MavenJewelRadiusKeystones.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MavenJewelRadiusKeystonesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Keystone
            (var keystoneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MavenJewelRadiusKeystonesDat()
            {
                Keystone = keystoneLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
