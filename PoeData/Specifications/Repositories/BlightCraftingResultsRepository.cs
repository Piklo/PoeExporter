using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BlightCraftingResultsDat"/> related data and helper methods.
/// </summary>
public sealed class BlightCraftingResultsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BlightCraftingResultsDat> Items { get; }

    private Dictionary<string, List<BlightCraftingResultsDat>>? byId;
    private Dictionary<int, List<BlightCraftingResultsDat>>? byModsKey;
    private Dictionary<int, List<BlightCraftingResultsDat>>? byPassiveSkillsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightCraftingResultsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BlightCraftingResultsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingResultsDat"/> with <see cref="BlightCraftingResultsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BlightCraftingResultsDat? item)
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
    /// Tries to get <see cref="BlightCraftingResultsDat"/> with <see cref="BlightCraftingResultsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BlightCraftingResultsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingResultsDat>();
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
            items = Array.Empty<BlightCraftingResultsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingResultsDat"/> with <see cref="BlightCraftingResultsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightCraftingResultsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightCraftingResultsDat>>();
        }

        var items = new List<ResultItem<string, BlightCraftingResultsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightCraftingResultsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingResultsDat"/> with <see cref="BlightCraftingResultsDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKey(int? key, out BlightCraftingResultsDat? item)
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
    /// Tries to get <see cref="BlightCraftingResultsDat"/> with <see cref="BlightCraftingResultsDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKey(int? key, out IReadOnlyList<BlightCraftingResultsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingResultsDat>();
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
            items = Array.Empty<BlightCraftingResultsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingResultsDat"/> with <see cref="BlightCraftingResultsDat.byModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightCraftingResultsDat>> GetManyToManyByModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightCraftingResultsDat>>();
        }

        var items = new List<ResultItem<int, BlightCraftingResultsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightCraftingResultsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingResultsDat"/> with <see cref="BlightCraftingResultsDat.PassiveSkillsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveSkillsKey(int? key, out BlightCraftingResultsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveSkillsKey(key, out var items))
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
    /// Tries to get <see cref="BlightCraftingResultsDat"/> with <see cref="BlightCraftingResultsDat.PassiveSkillsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveSkillsKey(int? key, out IReadOnlyList<BlightCraftingResultsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingResultsDat>();
            return false;
        }

        if (byPassiveSkillsKey is null)
        {
            byPassiveSkillsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveSkillsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPassiveSkillsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPassiveSkillsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveSkillsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightCraftingResultsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingResultsDat"/> with <see cref="BlightCraftingResultsDat.byPassiveSkillsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightCraftingResultsDat>> GetManyToManyByPassiveSkillsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightCraftingResultsDat>>();
        }

        var items = new List<ResultItem<int, BlightCraftingResultsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveSkillsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightCraftingResultsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BlightCraftingResultsDat[] Load()
    {
        const string filePath = "Data/BlightCraftingResults.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightCraftingResultsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKey
            (var modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PassiveSkillsKey
            (var passiveskillskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightCraftingResultsDat()
            {
                Id = idLoading,
                ModsKey = modskeyLoading,
                PassiveSkillsKey = passiveskillskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
