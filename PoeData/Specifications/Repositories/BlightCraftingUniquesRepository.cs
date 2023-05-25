using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BlightCraftingUniquesDat"/> related data and helper methods.
/// </summary>
public sealed class BlightCraftingUniquesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BlightCraftingUniquesDat> Items { get; }

    private Dictionary<int, List<BlightCraftingUniquesDat>>? byWordsKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightCraftingUniquesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BlightCraftingUniquesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingUniquesDat"/> with <see cref="BlightCraftingUniquesDat.WordsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWordsKey(int? key, out BlightCraftingUniquesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWordsKey(key, out var items))
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
    /// Tries to get <see cref="BlightCraftingUniquesDat"/> with <see cref="BlightCraftingUniquesDat.WordsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWordsKey(int? key, out IReadOnlyList<BlightCraftingUniquesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightCraftingUniquesDat>();
            return false;
        }

        if (byWordsKey is null)
        {
            byWordsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WordsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWordsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWordsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWordsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightCraftingUniquesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightCraftingUniquesDat"/> with <see cref="BlightCraftingUniquesDat.byWordsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightCraftingUniquesDat>> GetManyToManyByWordsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightCraftingUniquesDat>>();
        }

        var items = new List<ResultItem<int, BlightCraftingUniquesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWordsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightCraftingUniquesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BlightCraftingUniquesDat[] Load()
    {
        const string filePath = "Data/BlightCraftingUniques.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightCraftingUniquesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WordsKey
            (var wordskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightCraftingUniquesDat()
            {
                WordsKey = wordskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
