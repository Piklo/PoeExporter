using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SpectreOverridesDat"/> related data and helper methods.
/// </summary>
public sealed class SpectreOverridesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SpectreOverridesDat> Items { get; }

    private Dictionary<int, List<SpectreOverridesDat>>? byMonster;
    private Dictionary<int, List<SpectreOverridesDat>>? bySpectre;
    private Dictionary<int, List<SpectreOverridesDat>>? byUnknown32;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpectreOverridesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SpectreOverridesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SpectreOverridesDat"/> with <see cref="SpectreOverridesDat.Monster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonster(int? key, out SpectreOverridesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonster(key, out var items))
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
    /// Tries to get <see cref="SpectreOverridesDat"/> with <see cref="SpectreOverridesDat.Monster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonster(int? key, out IReadOnlyList<SpectreOverridesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SpectreOverridesDat>();
            return false;
        }

        if (byMonster is null)
        {
            byMonster = new();
            foreach (var item in Items)
            {
                var itemKey = item.Monster;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonster.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonster.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonster.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SpectreOverridesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SpectreOverridesDat"/> with <see cref="SpectreOverridesDat.byMonster"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SpectreOverridesDat>> GetManyToManyByMonster(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SpectreOverridesDat>>();
        }

        var items = new List<ResultItem<int, SpectreOverridesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonster(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SpectreOverridesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SpectreOverridesDat"/> with <see cref="SpectreOverridesDat.Spectre"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpectre(int? key, out SpectreOverridesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpectre(key, out var items))
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
    /// Tries to get <see cref="SpectreOverridesDat"/> with <see cref="SpectreOverridesDat.Spectre"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpectre(int? key, out IReadOnlyList<SpectreOverridesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SpectreOverridesDat>();
            return false;
        }

        if (bySpectre is null)
        {
            bySpectre = new();
            foreach (var item in Items)
            {
                var itemKey = item.Spectre;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySpectre.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySpectre.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySpectre.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SpectreOverridesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SpectreOverridesDat"/> with <see cref="SpectreOverridesDat.bySpectre"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SpectreOverridesDat>> GetManyToManyBySpectre(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SpectreOverridesDat>>();
        }

        var items = new List<ResultItem<int, SpectreOverridesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpectre(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SpectreOverridesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SpectreOverridesDat"/> with <see cref="SpectreOverridesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out SpectreOverridesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="SpectreOverridesDat"/> with <see cref="SpectreOverridesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<SpectreOverridesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SpectreOverridesDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown32.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown32.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SpectreOverridesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SpectreOverridesDat"/> with <see cref="SpectreOverridesDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SpectreOverridesDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SpectreOverridesDat>>();
        }

        var items = new List<ResultItem<int, SpectreOverridesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SpectreOverridesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SpectreOverridesDat[] Load()
    {
        const string filePath = "Data/SpectreOverrides.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SpectreOverridesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Monster
            (var monsterLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Spectre
            (var spectreLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var tempunknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown32Loading = tempunknown32Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SpectreOverridesDat()
            {
                Monster = monsterLoading,
                Spectre = spectreLoading,
                Unknown32 = unknown32Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
