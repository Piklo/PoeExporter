using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistNPCAurasDat"/> related data and helper methods.
/// </summary>
public sealed class HeistNPCAurasRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistNPCAurasDat> Items { get; }

    private Dictionary<int, List<HeistNPCAurasDat>>? byStat;
    private Dictionary<int, List<HeistNPCAurasDat>>? byGrantedEffect;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistNPCAurasRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistNPCAurasRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCAurasDat"/> with <see cref="HeistNPCAurasDat.Stat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat(int? key, out HeistNPCAurasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat(key, out var items))
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
    /// Tries to get <see cref="HeistNPCAurasDat"/> with <see cref="HeistNPCAurasDat.Stat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat(int? key, out IReadOnlyList<HeistNPCAurasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCAurasDat>();
            return false;
        }

        if (byStat is null)
        {
            byStat = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStat.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStat.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStat.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCAurasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCAurasDat"/> with <see cref="HeistNPCAurasDat.byStat"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCAurasDat>> GetManyToManyByStat(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCAurasDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCAurasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCAurasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCAurasDat"/> with <see cref="HeistNPCAurasDat.GrantedEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffect(int? key, out HeistNPCAurasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffect(key, out var items))
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
    /// Tries to get <see cref="HeistNPCAurasDat"/> with <see cref="HeistNPCAurasDat.GrantedEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffect(int? key, out IReadOnlyList<HeistNPCAurasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCAurasDat>();
            return false;
        }

        if (byGrantedEffect is null)
        {
            byGrantedEffect = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffect;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGrantedEffect.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGrantedEffect.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGrantedEffect.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCAurasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCAurasDat"/> with <see cref="HeistNPCAurasDat.byGrantedEffect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCAurasDat>> GetManyToManyByGrantedEffect(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCAurasDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCAurasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCAurasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistNPCAurasDat[] Load()
    {
        const string filePath = "Data/HeistNPCAuras.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistNPCAurasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Stat
            (var statLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading GrantedEffect
            (var grantedeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistNPCAurasDat()
            {
                Stat = statLoading,
                GrantedEffect = grantedeffectLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
