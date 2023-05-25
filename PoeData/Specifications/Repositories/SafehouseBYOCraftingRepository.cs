using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SafehouseBYOCraftingDat"/> related data and helper methods.
/// </summary>
public sealed class SafehouseBYOCraftingRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SafehouseBYOCraftingDat> Items { get; }

    private Dictionary<int, List<SafehouseBYOCraftingDat>>? byBetrayalJobsKey;
    private Dictionary<int, List<SafehouseBYOCraftingDat>>? byBetrayalTargetsKey;
    private Dictionary<int, List<SafehouseBYOCraftingDat>>? byRank;
    private Dictionary<string, List<SafehouseBYOCraftingDat>>? byDescription;
    private Dictionary<string, List<SafehouseBYOCraftingDat>>? byServerCommand;
    private Dictionary<int, List<SafehouseBYOCraftingDat>>? byUnknown52;

    /// <summary>
    /// Initializes a new instance of the <see cref="SafehouseBYOCraftingRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SafehouseBYOCraftingRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.BetrayalJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalJobsKey(int? key, out SafehouseBYOCraftingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalJobsKey(key, out var items))
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
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.BetrayalJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalJobsKey(int? key, out IReadOnlyList<SafehouseBYOCraftingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        if (byBetrayalJobsKey is null)
        {
            byBetrayalJobsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalJobsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBetrayalJobsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBetrayalJobsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalJobsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.byBetrayalJobsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SafehouseBYOCraftingDat>> GetManyToManyByBetrayalJobsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SafehouseBYOCraftingDat>>();
        }

        var items = new List<ResultItem<int, SafehouseBYOCraftingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalJobsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SafehouseBYOCraftingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.BetrayalTargetsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalTargetsKey(int? key, out SafehouseBYOCraftingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalTargetsKey(key, out var items))
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
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.BetrayalTargetsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalTargetsKey(int? key, out IReadOnlyList<SafehouseBYOCraftingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        if (byBetrayalTargetsKey is null)
        {
            byBetrayalTargetsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalTargetsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBetrayalTargetsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBetrayalTargetsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalTargetsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.byBetrayalTargetsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SafehouseBYOCraftingDat>> GetManyToManyByBetrayalTargetsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SafehouseBYOCraftingDat>>();
        }

        var items = new List<ResultItem<int, SafehouseBYOCraftingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalTargetsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SafehouseBYOCraftingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.Rank"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRank(int? key, out SafehouseBYOCraftingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRank(key, out var items))
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
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.Rank"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRank(int? key, out IReadOnlyList<SafehouseBYOCraftingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        if (byRank is null)
        {
            byRank = new();
            foreach (var item in Items)
            {
                var itemKey = item.Rank;

                if (!byRank.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRank.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRank.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.byRank"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SafehouseBYOCraftingDat>> GetManyToManyByRank(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SafehouseBYOCraftingDat>>();
        }

        var items = new List<ResultItem<int, SafehouseBYOCraftingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRank(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SafehouseBYOCraftingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out SafehouseBYOCraftingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<SafehouseBYOCraftingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SafehouseBYOCraftingDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SafehouseBYOCraftingDat>>();
        }

        var items = new List<ResultItem<string, SafehouseBYOCraftingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SafehouseBYOCraftingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.ServerCommand"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByServerCommand(string? key, out SafehouseBYOCraftingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByServerCommand(key, out var items))
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
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.ServerCommand"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByServerCommand(string? key, out IReadOnlyList<SafehouseBYOCraftingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        if (byServerCommand is null)
        {
            byServerCommand = new();
            foreach (var item in Items)
            {
                var itemKey = item.ServerCommand;

                if (!byServerCommand.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byServerCommand.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byServerCommand.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.byServerCommand"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SafehouseBYOCraftingDat>> GetManyToManyByServerCommand(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SafehouseBYOCraftingDat>>();
        }

        var items = new List<ResultItem<string, SafehouseBYOCraftingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByServerCommand(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SafehouseBYOCraftingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out SafehouseBYOCraftingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown52(key, out var items))
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
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<SafehouseBYOCraftingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown52.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown52.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SafehouseBYOCraftingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SafehouseBYOCraftingDat"/> with <see cref="SafehouseBYOCraftingDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SafehouseBYOCraftingDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SafehouseBYOCraftingDat>>();
        }

        var items = new List<ResultItem<int, SafehouseBYOCraftingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SafehouseBYOCraftingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SafehouseBYOCraftingDat[] Load()
    {
        const string filePath = "Data/SafehouseBYOCrafting.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SafehouseBYOCraftingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BetrayalJobsKey
            (var betrayaljobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BetrayalTargetsKey
            (var betrayaltargetskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Rank
            (var rankLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ServerCommand
            (var servercommandLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown52
            (var tempunknown52Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown52Loading = tempunknown52Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SafehouseBYOCraftingDat()
            {
                BetrayalJobsKey = betrayaljobskeyLoading,
                BetrayalTargetsKey = betrayaltargetskeyLoading,
                Rank = rankLoading,
                Description = descriptionLoading,
                ServerCommand = servercommandLoading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
