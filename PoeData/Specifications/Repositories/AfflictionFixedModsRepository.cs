using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AfflictionFixedModsDat"/> related data and helper methods.
/// </summary>
public sealed class AfflictionFixedModsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AfflictionFixedModsDat> Items { get; }

    private Dictionary<int, List<AfflictionFixedModsDat>>? byRarity;
    private Dictionary<int, List<AfflictionFixedModsDat>>? byMod;
    private Dictionary<int, List<AfflictionFixedModsDat>>? byUnknown20;

    /// <summary>
    /// Initializes a new instance of the <see cref="AfflictionFixedModsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AfflictionFixedModsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionFixedModsDat"/> with <see cref="AfflictionFixedModsDat.Rarity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRarity(int? key, out AfflictionFixedModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRarity(key, out var items))
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
    /// Tries to get <see cref="AfflictionFixedModsDat"/> with <see cref="AfflictionFixedModsDat.Rarity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRarity(int? key, out IReadOnlyList<AfflictionFixedModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AfflictionFixedModsDat>();
            return false;
        }

        if (byRarity is null)
        {
            byRarity = new();
            foreach (var item in Items)
            {
                var itemKey = item.Rarity;

                if (!byRarity.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRarity.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRarity.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AfflictionFixedModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionFixedModsDat"/> with <see cref="AfflictionFixedModsDat.byRarity"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AfflictionFixedModsDat>> GetManyToManyByRarity(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AfflictionFixedModsDat>>();
        }

        var items = new List<ResultItem<int, AfflictionFixedModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRarity(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AfflictionFixedModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionFixedModsDat"/> with <see cref="AfflictionFixedModsDat.Mod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMod(int? key, out AfflictionFixedModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMod(key, out var items))
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
    /// Tries to get <see cref="AfflictionFixedModsDat"/> with <see cref="AfflictionFixedModsDat.Mod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMod(int? key, out IReadOnlyList<AfflictionFixedModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AfflictionFixedModsDat>();
            return false;
        }

        if (byMod is null)
        {
            byMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AfflictionFixedModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionFixedModsDat"/> with <see cref="AfflictionFixedModsDat.byMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AfflictionFixedModsDat>> GetManyToManyByMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AfflictionFixedModsDat>>();
        }

        var items = new List<ResultItem<int, AfflictionFixedModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AfflictionFixedModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionFixedModsDat"/> with <see cref="AfflictionFixedModsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out AfflictionFixedModsDat? item)
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
    /// Tries to get <see cref="AfflictionFixedModsDat"/> with <see cref="AfflictionFixedModsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<AfflictionFixedModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AfflictionFixedModsDat>();
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
            items = Array.Empty<AfflictionFixedModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AfflictionFixedModsDat"/> with <see cref="AfflictionFixedModsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AfflictionFixedModsDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AfflictionFixedModsDat>>();
        }

        var items = new List<ResultItem<int, AfflictionFixedModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AfflictionFixedModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AfflictionFixedModsDat[] Load()
    {
        const string filePath = "Data/AfflictionFixedMods.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AfflictionFixedModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Rarity
            (var rarityLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Mod
            (var modLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AfflictionFixedModsDat()
            {
                Rarity = rarityLoading,
                Mod = modLoading,
                Unknown20 = unknown20Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
