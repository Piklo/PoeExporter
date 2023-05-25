using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="IndexableSupportGemsDat"/> related data and helper methods.
/// </summary>
public sealed class IndexableSupportGemsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<IndexableSupportGemsDat> Items { get; }

    private Dictionary<int, List<IndexableSupportGemsDat>>? byIndex;
    private Dictionary<int, List<IndexableSupportGemsDat>>? bySupportGem;
    private Dictionary<string, List<IndexableSupportGemsDat>>? byName;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexableSupportGemsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal IndexableSupportGemsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="IndexableSupportGemsDat"/> with <see cref="IndexableSupportGemsDat.Index"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIndex(int? key, out IndexableSupportGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIndex(key, out var items))
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
    /// Tries to get <see cref="IndexableSupportGemsDat"/> with <see cref="IndexableSupportGemsDat.Index"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIndex(int? key, out IReadOnlyList<IndexableSupportGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IndexableSupportGemsDat>();
            return false;
        }

        if (byIndex is null)
        {
            byIndex = new();
            foreach (var item in Items)
            {
                var itemKey = item.Index;

                if (!byIndex.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIndex.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIndex.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IndexableSupportGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IndexableSupportGemsDat"/> with <see cref="IndexableSupportGemsDat.byIndex"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IndexableSupportGemsDat>> GetManyToManyByIndex(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IndexableSupportGemsDat>>();
        }

        var items = new List<ResultItem<int, IndexableSupportGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIndex(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IndexableSupportGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IndexableSupportGemsDat"/> with <see cref="IndexableSupportGemsDat.SupportGem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySupportGem(int? key, out IndexableSupportGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySupportGem(key, out var items))
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
    /// Tries to get <see cref="IndexableSupportGemsDat"/> with <see cref="IndexableSupportGemsDat.SupportGem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySupportGem(int? key, out IReadOnlyList<IndexableSupportGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IndexableSupportGemsDat>();
            return false;
        }

        if (bySupportGem is null)
        {
            bySupportGem = new();
            foreach (var item in Items)
            {
                var itemKey = item.SupportGem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySupportGem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySupportGem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySupportGem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IndexableSupportGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IndexableSupportGemsDat"/> with <see cref="IndexableSupportGemsDat.bySupportGem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IndexableSupportGemsDat>> GetManyToManyBySupportGem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IndexableSupportGemsDat>>();
        }

        var items = new List<ResultItem<int, IndexableSupportGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySupportGem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IndexableSupportGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IndexableSupportGemsDat"/> with <see cref="IndexableSupportGemsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out IndexableSupportGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByName(key, out var items))
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
    /// Tries to get <see cref="IndexableSupportGemsDat"/> with <see cref="IndexableSupportGemsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<IndexableSupportGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IndexableSupportGemsDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;

                if (!byName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<IndexableSupportGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IndexableSupportGemsDat"/> with <see cref="IndexableSupportGemsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IndexableSupportGemsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IndexableSupportGemsDat>>();
        }

        var items = new List<ResultItem<string, IndexableSupportGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IndexableSupportGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private IndexableSupportGemsDat[] Load()
    {
        const string filePath = "Data/IndexableSupportGems.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IndexableSupportGemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Index
            (var indexLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SupportGem
            (var supportgemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IndexableSupportGemsDat()
            {
                Index = indexLoading,
                SupportGem = supportgemLoading,
                Name = nameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
