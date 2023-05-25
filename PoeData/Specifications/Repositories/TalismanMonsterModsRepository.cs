using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="TalismanMonsterModsDat"/> related data and helper methods.
/// </summary>
public sealed class TalismanMonsterModsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<TalismanMonsterModsDat> Items { get; }

    private Dictionary<int, List<TalismanMonsterModsDat>>? byModTypeKey;
    private Dictionary<int, List<TalismanMonsterModsDat>>? byModsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="TalismanMonsterModsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal TalismanMonsterModsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="TalismanMonsterModsDat"/> with <see cref="TalismanMonsterModsDat.ModTypeKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModTypeKey(int? key, out TalismanMonsterModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModTypeKey(key, out var items))
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
    /// Tries to get <see cref="TalismanMonsterModsDat"/> with <see cref="TalismanMonsterModsDat.ModTypeKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModTypeKey(int? key, out IReadOnlyList<TalismanMonsterModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TalismanMonsterModsDat>();
            return false;
        }

        if (byModTypeKey is null)
        {
            byModTypeKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModTypeKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byModTypeKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byModTypeKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byModTypeKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TalismanMonsterModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TalismanMonsterModsDat"/> with <see cref="TalismanMonsterModsDat.byModTypeKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TalismanMonsterModsDat>> GetManyToManyByModTypeKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TalismanMonsterModsDat>>();
        }

        var items = new List<ResultItem<int, TalismanMonsterModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModTypeKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TalismanMonsterModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TalismanMonsterModsDat"/> with <see cref="TalismanMonsterModsDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKey(int? key, out TalismanMonsterModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKey(key, out var items))
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
    /// Tries to get <see cref="TalismanMonsterModsDat"/> with <see cref="TalismanMonsterModsDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKey(int? key, out IReadOnlyList<TalismanMonsterModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TalismanMonsterModsDat>();
            return false;
        }

        if (byModsKey is null)
        {
            byModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TalismanMonsterModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TalismanMonsterModsDat"/> with <see cref="TalismanMonsterModsDat.byModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TalismanMonsterModsDat>> GetManyToManyByModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TalismanMonsterModsDat>>();
        }

        var items = new List<ResultItem<int, TalismanMonsterModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TalismanMonsterModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private TalismanMonsterModsDat[] Load()
    {
        const string filePath = "Data/TalismanMonsterMods.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TalismanMonsterModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ModTypeKey
            (var modtypekeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TalismanMonsterModsDat()
            {
                ModTypeKey = modtypekeyLoading,
                ModsKey = modskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
