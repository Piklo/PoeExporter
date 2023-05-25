using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="StatDescriptionFunctionsDat"/> related data and helper methods.
/// </summary>
public sealed class StatDescriptionFunctionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<StatDescriptionFunctionsDat> Items { get; }

    private Dictionary<string, List<StatDescriptionFunctionsDat>>? byId;
    private Dictionary<string, List<StatDescriptionFunctionsDat>>? byTranslationId;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatDescriptionFunctionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal StatDescriptionFunctionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="StatDescriptionFunctionsDat"/> with <see cref="StatDescriptionFunctionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out StatDescriptionFunctionsDat? item)
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
    /// Tries to get <see cref="StatDescriptionFunctionsDat"/> with <see cref="StatDescriptionFunctionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<StatDescriptionFunctionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatDescriptionFunctionsDat>();
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
            items = Array.Empty<StatDescriptionFunctionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatDescriptionFunctionsDat"/> with <see cref="StatDescriptionFunctionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, StatDescriptionFunctionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, StatDescriptionFunctionsDat>>();
        }

        var items = new List<ResultItem<string, StatDescriptionFunctionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, StatDescriptionFunctionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StatDescriptionFunctionsDat"/> with <see cref="StatDescriptionFunctionsDat.TranslationId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTranslationId(string? key, out StatDescriptionFunctionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTranslationId(key, out var items))
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
    /// Tries to get <see cref="StatDescriptionFunctionsDat"/> with <see cref="StatDescriptionFunctionsDat.TranslationId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTranslationId(string? key, out IReadOnlyList<StatDescriptionFunctionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StatDescriptionFunctionsDat>();
            return false;
        }

        if (byTranslationId is null)
        {
            byTranslationId = new();
            foreach (var item in Items)
            {
                var itemKey = item.TranslationId;

                if (!byTranslationId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTranslationId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTranslationId.TryGetValue(key, out var temp))
        {
            items = Array.Empty<StatDescriptionFunctionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StatDescriptionFunctionsDat"/> with <see cref="StatDescriptionFunctionsDat.byTranslationId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, StatDescriptionFunctionsDat>> GetManyToManyByTranslationId(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, StatDescriptionFunctionsDat>>();
        }

        var items = new List<ResultItem<string, StatDescriptionFunctionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTranslationId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, StatDescriptionFunctionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private StatDescriptionFunctionsDat[] Load()
    {
        const string filePath = "Data/StatDescriptionFunctions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StatDescriptionFunctionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TranslationId
            (var translationidLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StatDescriptionFunctionsDat()
            {
                Id = idLoading,
                TranslationId = translationidLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
