using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SocketNotchesDat"/> related data and helper methods.
/// </summary>
public sealed class SocketNotchesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SocketNotchesDat> Items { get; }

    private Dictionary<string, List<SocketNotchesDat>>? byId;
    private Dictionary<string, List<SocketNotchesDat>>? byDescription;
    private Dictionary<string, List<SocketNotchesDat>>? byRedSocketImage;
    private Dictionary<string, List<SocketNotchesDat>>? byBlueSocketImage;
    private Dictionary<string, List<SocketNotchesDat>>? byGreenSocketImage;

    /// <summary>
    /// Initializes a new instance of the <see cref="SocketNotchesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SocketNotchesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SocketNotchesDat? item)
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
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SocketNotchesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SocketNotchesDat>();
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
            items = Array.Empty<SocketNotchesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SocketNotchesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SocketNotchesDat>>();
        }

        var items = new List<ResultItem<string, SocketNotchesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SocketNotchesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out SocketNotchesDat? item)
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
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<SocketNotchesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SocketNotchesDat>();
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
            items = Array.Empty<SocketNotchesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SocketNotchesDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SocketNotchesDat>>();
        }

        var items = new List<ResultItem<string, SocketNotchesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SocketNotchesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.RedSocketImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRedSocketImage(string? key, out SocketNotchesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRedSocketImage(key, out var items))
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
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.RedSocketImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRedSocketImage(string? key, out IReadOnlyList<SocketNotchesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SocketNotchesDat>();
            return false;
        }

        if (byRedSocketImage is null)
        {
            byRedSocketImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.RedSocketImage;

                if (!byRedSocketImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRedSocketImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRedSocketImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SocketNotchesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.byRedSocketImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SocketNotchesDat>> GetManyToManyByRedSocketImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SocketNotchesDat>>();
        }

        var items = new List<ResultItem<string, SocketNotchesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRedSocketImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SocketNotchesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.BlueSocketImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBlueSocketImage(string? key, out SocketNotchesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBlueSocketImage(key, out var items))
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
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.BlueSocketImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBlueSocketImage(string? key, out IReadOnlyList<SocketNotchesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SocketNotchesDat>();
            return false;
        }

        if (byBlueSocketImage is null)
        {
            byBlueSocketImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.BlueSocketImage;

                if (!byBlueSocketImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBlueSocketImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBlueSocketImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SocketNotchesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.byBlueSocketImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SocketNotchesDat>> GetManyToManyByBlueSocketImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SocketNotchesDat>>();
        }

        var items = new List<ResultItem<string, SocketNotchesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBlueSocketImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SocketNotchesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.GreenSocketImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGreenSocketImage(string? key, out SocketNotchesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGreenSocketImage(key, out var items))
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
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.GreenSocketImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGreenSocketImage(string? key, out IReadOnlyList<SocketNotchesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SocketNotchesDat>();
            return false;
        }

        if (byGreenSocketImage is null)
        {
            byGreenSocketImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.GreenSocketImage;

                if (!byGreenSocketImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGreenSocketImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGreenSocketImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SocketNotchesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SocketNotchesDat"/> with <see cref="SocketNotchesDat.byGreenSocketImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SocketNotchesDat>> GetManyToManyByGreenSocketImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SocketNotchesDat>>();
        }

        var items = new List<ResultItem<string, SocketNotchesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGreenSocketImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SocketNotchesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SocketNotchesDat[] Load()
    {
        const string filePath = "Data/SocketNotches.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SocketNotchesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RedSocketImage
            (var redsocketimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BlueSocketImage
            (var bluesocketimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GreenSocketImage
            (var greensocketimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SocketNotchesDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                RedSocketImage = redsocketimageLoading,
                BlueSocketImage = bluesocketimageLoading,
                GreenSocketImage = greensocketimageLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
