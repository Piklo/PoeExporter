using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ShopCategoryDat"/> related data and helper methods.
/// </summary>
public sealed class ShopCategoryRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ShopCategoryDat> Items { get; }

    private Dictionary<string, List<ShopCategoryDat>>? byId;
    private Dictionary<string, List<ShopCategoryDat>>? byClientText;
    private Dictionary<string, List<ShopCategoryDat>>? byClientJPGFile;
    private Dictionary<string, List<ShopCategoryDat>>? byWebsiteText;
    private Dictionary<string, List<ShopCategoryDat>>? byWebsiteJPGFile;
    private Dictionary<int, List<ShopCategoryDat>>? byUnknown40;
    private Dictionary<int, List<ShopCategoryDat>>? byAppliedTo_BaseItemTypesKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShopCategoryRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ShopCategoryRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ShopCategoryDat? item)
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
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ShopCategoryDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCategoryDat>();
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
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopCategoryDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopCategoryDat>>();
        }

        var items = new List<ResultItem<string, ShopCategoryDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopCategoryDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.ClientText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClientText(string? key, out ShopCategoryDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClientText(key, out var items))
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
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.ClientText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClientText(string? key, out IReadOnlyList<ShopCategoryDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        if (byClientText is null)
        {
            byClientText = new();
            foreach (var item in Items)
            {
                var itemKey = item.ClientText;

                if (!byClientText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byClientText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byClientText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.byClientText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopCategoryDat>> GetManyToManyByClientText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopCategoryDat>>();
        }

        var items = new List<ResultItem<string, ShopCategoryDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClientText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopCategoryDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.ClientJPGFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClientJPGFile(string? key, out ShopCategoryDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClientJPGFile(key, out var items))
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
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.ClientJPGFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClientJPGFile(string? key, out IReadOnlyList<ShopCategoryDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        if (byClientJPGFile is null)
        {
            byClientJPGFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.ClientJPGFile;

                if (!byClientJPGFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byClientJPGFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byClientJPGFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.byClientJPGFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopCategoryDat>> GetManyToManyByClientJPGFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopCategoryDat>>();
        }

        var items = new List<ResultItem<string, ShopCategoryDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClientJPGFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopCategoryDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.WebsiteText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWebsiteText(string? key, out ShopCategoryDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWebsiteText(key, out var items))
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
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.WebsiteText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWebsiteText(string? key, out IReadOnlyList<ShopCategoryDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        if (byWebsiteText is null)
        {
            byWebsiteText = new();
            foreach (var item in Items)
            {
                var itemKey = item.WebsiteText;

                if (!byWebsiteText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWebsiteText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWebsiteText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.byWebsiteText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopCategoryDat>> GetManyToManyByWebsiteText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopCategoryDat>>();
        }

        var items = new List<ResultItem<string, ShopCategoryDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWebsiteText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopCategoryDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.WebsiteJPGFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWebsiteJPGFile(string? key, out ShopCategoryDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWebsiteJPGFile(key, out var items))
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
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.WebsiteJPGFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWebsiteJPGFile(string? key, out IReadOnlyList<ShopCategoryDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        if (byWebsiteJPGFile is null)
        {
            byWebsiteJPGFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.WebsiteJPGFile;

                if (!byWebsiteJPGFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWebsiteJPGFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWebsiteJPGFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.byWebsiteJPGFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopCategoryDat>> GetManyToManyByWebsiteJPGFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopCategoryDat>>();
        }

        var items = new List<ResultItem<string, ShopCategoryDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWebsiteJPGFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopCategoryDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out ShopCategoryDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<ShopCategoryDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;

                if (!byUnknown40.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopCategoryDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopCategoryDat>>();
        }

        var items = new List<ResultItem<int, ShopCategoryDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopCategoryDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.AppliedTo_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAppliedTo_BaseItemTypesKey(int? key, out ShopCategoryDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAppliedTo_BaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.AppliedTo_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAppliedTo_BaseItemTypesKey(int? key, out IReadOnlyList<ShopCategoryDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        if (byAppliedTo_BaseItemTypesKey is null)
        {
            byAppliedTo_BaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AppliedTo_BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAppliedTo_BaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAppliedTo_BaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAppliedTo_BaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopCategoryDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopCategoryDat"/> with <see cref="ShopCategoryDat.byAppliedTo_BaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopCategoryDat>> GetManyToManyByAppliedTo_BaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopCategoryDat>>();
        }

        var items = new List<ResultItem<int, ShopCategoryDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAppliedTo_BaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopCategoryDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ShopCategoryDat[] Load()
    {
        const string filePath = "Data/ShopCategory.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopCategoryDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ClientText
            (var clienttextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ClientJPGFile
            (var clientjpgfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WebsiteText
            (var websitetextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WebsiteJPGFile
            (var websitejpgfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AppliedTo_BaseItemTypesKey
            (var appliedto_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopCategoryDat()
            {
                Id = idLoading,
                ClientText = clienttextLoading,
                ClientJPGFile = clientjpgfileLoading,
                WebsiteText = websitetextLoading,
                WebsiteJPGFile = websitejpgfileLoading,
                Unknown40 = unknown40Loading,
                AppliedTo_BaseItemTypesKey = appliedto_baseitemtypeskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
