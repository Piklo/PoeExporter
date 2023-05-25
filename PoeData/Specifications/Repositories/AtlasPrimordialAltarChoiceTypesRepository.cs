using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasPrimordialAltarChoiceTypesDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasPrimordialAltarChoiceTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasPrimordialAltarChoiceTypesDat> Items { get; }

    private Dictionary<string, List<AtlasPrimordialAltarChoiceTypesDat>>? byId;
    private Dictionary<string, List<AtlasPrimordialAltarChoiceTypesDat>>? byTopIconEater;
    private Dictionary<string, List<AtlasPrimordialAltarChoiceTypesDat>>? byBottomIconEater;
    private Dictionary<string, List<AtlasPrimordialAltarChoiceTypesDat>>? byTopIconExarch;
    private Dictionary<string, List<AtlasPrimordialAltarChoiceTypesDat>>? byBottomIconExarch;
    private Dictionary<string, List<AtlasPrimordialAltarChoiceTypesDat>>? byText;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasPrimordialAltarChoiceTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasPrimordialAltarChoiceTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AtlasPrimordialAltarChoiceTypesDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AtlasPrimordialAltarChoiceTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
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
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.TopIconEater"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTopIconEater(string? key, out AtlasPrimordialAltarChoiceTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTopIconEater(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.TopIconEater"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTopIconEater(string? key, out IReadOnlyList<AtlasPrimordialAltarChoiceTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
            return false;
        }

        if (byTopIconEater is null)
        {
            byTopIconEater = new();
            foreach (var item in Items)
            {
                var itemKey = item.TopIconEater;

                if (!byTopIconEater.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTopIconEater.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTopIconEater.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.byTopIconEater"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>> GetManyToManyByTopIconEater(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTopIconEater(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.BottomIconEater"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBottomIconEater(string? key, out AtlasPrimordialAltarChoiceTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBottomIconEater(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.BottomIconEater"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBottomIconEater(string? key, out IReadOnlyList<AtlasPrimordialAltarChoiceTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
            return false;
        }

        if (byBottomIconEater is null)
        {
            byBottomIconEater = new();
            foreach (var item in Items)
            {
                var itemKey = item.BottomIconEater;

                if (!byBottomIconEater.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBottomIconEater.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBottomIconEater.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.byBottomIconEater"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>> GetManyToManyByBottomIconEater(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBottomIconEater(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.TopIconExarch"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTopIconExarch(string? key, out AtlasPrimordialAltarChoiceTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTopIconExarch(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.TopIconExarch"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTopIconExarch(string? key, out IReadOnlyList<AtlasPrimordialAltarChoiceTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
            return false;
        }

        if (byTopIconExarch is null)
        {
            byTopIconExarch = new();
            foreach (var item in Items)
            {
                var itemKey = item.TopIconExarch;

                if (!byTopIconExarch.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTopIconExarch.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTopIconExarch.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.byTopIconExarch"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>> GetManyToManyByTopIconExarch(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTopIconExarch(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.BottomIconExarch"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBottomIconExarch(string? key, out AtlasPrimordialAltarChoiceTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBottomIconExarch(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.BottomIconExarch"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBottomIconExarch(string? key, out IReadOnlyList<AtlasPrimordialAltarChoiceTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
            return false;
        }

        if (byBottomIconExarch is null)
        {
            byBottomIconExarch = new();
            foreach (var item in Items)
            {
                var itemKey = item.BottomIconExarch;

                if (!byBottomIconExarch.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBottomIconExarch.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBottomIconExarch.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.byBottomIconExarch"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>> GetManyToManyByBottomIconExarch(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBottomIconExarch(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out AtlasPrimordialAltarChoiceTypesDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<AtlasPrimordialAltarChoiceTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
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
            items = Array.Empty<AtlasPrimordialAltarChoiceTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialAltarChoiceTypesDat"/> with <see cref="AtlasPrimordialAltarChoiceTypesDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialAltarChoiceTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasPrimordialAltarChoiceTypesDat[] Load()
    {
        const string filePath = "Data/AtlasPrimordialAltarChoiceTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasPrimordialAltarChoiceTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TopIconEater
            (var topiconeaterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BottomIconEater
            (var bottomiconeaterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TopIconExarch
            (var topiconexarchLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BottomIconExarch
            (var bottomiconexarchLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasPrimordialAltarChoiceTypesDat()
            {
                Id = idLoading,
                TopIconEater = topiconeaterLoading,
                BottomIconEater = bottomiconeaterLoading,
                TopIconExarch = topiconexarchLoading,
                BottomIconExarch = bottomiconexarchLoading,
                Text = textLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
