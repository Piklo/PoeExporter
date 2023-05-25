using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BestiaryRecipeCategoriesDat"/> related data and helper methods.
/// </summary>
public sealed class BestiaryRecipeCategoriesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BestiaryRecipeCategoriesDat> Items { get; }

    private Dictionary<string, List<BestiaryRecipeCategoriesDat>>? byId;
    private Dictionary<string, List<BestiaryRecipeCategoriesDat>>? byText;

    /// <summary>
    /// Initializes a new instance of the <see cref="BestiaryRecipeCategoriesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BestiaryRecipeCategoriesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeCategoriesDat"/> with <see cref="BestiaryRecipeCategoriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BestiaryRecipeCategoriesDat? item)
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
    /// Tries to get <see cref="BestiaryRecipeCategoriesDat"/> with <see cref="BestiaryRecipeCategoriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BestiaryRecipeCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipeCategoriesDat>();
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
            items = Array.Empty<BestiaryRecipeCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeCategoriesDat"/> with <see cref="BestiaryRecipeCategoriesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryRecipeCategoriesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryRecipeCategoriesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryRecipeCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryRecipeCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeCategoriesDat"/> with <see cref="BestiaryRecipeCategoriesDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out BestiaryRecipeCategoriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByText(key, out var items))
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
    /// Tries to get <see cref="BestiaryRecipeCategoriesDat"/> with <see cref="BestiaryRecipeCategoriesDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<BestiaryRecipeCategoriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BestiaryRecipeCategoriesDat>();
            return false;
        }

        if (byText is null)
        {
            byText = new();
            foreach (var item in Items)
            {
                var itemKey = item.Text;

                if (!byText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BestiaryRecipeCategoriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BestiaryRecipeCategoriesDat"/> with <see cref="BestiaryRecipeCategoriesDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BestiaryRecipeCategoriesDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BestiaryRecipeCategoriesDat>>();
        }

        var items = new List<ResultItem<string, BestiaryRecipeCategoriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BestiaryRecipeCategoriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BestiaryRecipeCategoriesDat[] Load()
    {
        const string filePath = "Data/BestiaryRecipeCategories.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryRecipeCategoriesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryRecipeCategoriesDat()
            {
                Id = idLoading,
                Text = textLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
