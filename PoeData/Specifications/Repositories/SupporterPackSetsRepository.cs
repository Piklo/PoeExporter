using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SupporterPackSetsDat"/> related data and helper methods.
/// </summary>
public sealed class SupporterPackSetsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SupporterPackSetsDat> Items { get; }

    private Dictionary<string, List<SupporterPackSetsDat>>? byId;
    private Dictionary<string, List<SupporterPackSetsDat>>? byFormatTitle;
    private Dictionary<string, List<SupporterPackSetsDat>>? byBackground;
    private Dictionary<string, List<SupporterPackSetsDat>>? byTime0;
    private Dictionary<string, List<SupporterPackSetsDat>>? byTime1;
    private Dictionary<int, List<SupporterPackSetsDat>>? byShopPackagePlatform;
    private Dictionary<string, List<SupporterPackSetsDat>>? byUnknown56;

    /// <summary>
    /// Initializes a new instance of the <see cref="SupporterPackSetsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SupporterPackSetsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SupporterPackSetsDat? item)
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
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SupporterPackSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SupporterPackSetsDat>();
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
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SupporterPackSetsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SupporterPackSetsDat>>();
        }

        var items = new List<ResultItem<string, SupporterPackSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SupporterPackSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.FormatTitle"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFormatTitle(string? key, out SupporterPackSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFormatTitle(key, out var items))
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
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.FormatTitle"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFormatTitle(string? key, out IReadOnlyList<SupporterPackSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        if (byFormatTitle is null)
        {
            byFormatTitle = new();
            foreach (var item in Items)
            {
                var itemKey = item.FormatTitle;

                if (!byFormatTitle.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFormatTitle.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFormatTitle.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.byFormatTitle"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SupporterPackSetsDat>> GetManyToManyByFormatTitle(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SupporterPackSetsDat>>();
        }

        var items = new List<ResultItem<string, SupporterPackSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFormatTitle(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SupporterPackSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.Background"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBackground(string? key, out SupporterPackSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBackground(key, out var items))
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
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.Background"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBackground(string? key, out IReadOnlyList<SupporterPackSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        if (byBackground is null)
        {
            byBackground = new();
            foreach (var item in Items)
            {
                var itemKey = item.Background;

                if (!byBackground.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBackground.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBackground.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.byBackground"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SupporterPackSetsDat>> GetManyToManyByBackground(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SupporterPackSetsDat>>();
        }

        var items = new List<ResultItem<string, SupporterPackSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBackground(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SupporterPackSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.Time0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTime0(string? key, out SupporterPackSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTime0(key, out var items))
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
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.Time0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTime0(string? key, out IReadOnlyList<SupporterPackSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        if (byTime0 is null)
        {
            byTime0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Time0;

                if (!byTime0.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTime0.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTime0.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.byTime0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SupporterPackSetsDat>> GetManyToManyByTime0(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SupporterPackSetsDat>>();
        }

        var items = new List<ResultItem<string, SupporterPackSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTime0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SupporterPackSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.Time1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTime1(string? key, out SupporterPackSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTime1(key, out var items))
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
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.Time1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTime1(string? key, out IReadOnlyList<SupporterPackSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        if (byTime1 is null)
        {
            byTime1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Time1;

                if (!byTime1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTime1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTime1.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.byTime1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SupporterPackSetsDat>> GetManyToManyByTime1(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SupporterPackSetsDat>>();
        }

        var items = new List<ResultItem<string, SupporterPackSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTime1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SupporterPackSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.ShopPackagePlatform"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShopPackagePlatform(int? key, out SupporterPackSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShopPackagePlatform(key, out var items))
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
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.ShopPackagePlatform"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShopPackagePlatform(int? key, out IReadOnlyList<SupporterPackSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        if (byShopPackagePlatform is null)
        {
            byShopPackagePlatform = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShopPackagePlatform;
                foreach (var listKey in itemKey)
                {
                    if (!byShopPackagePlatform.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byShopPackagePlatform.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byShopPackagePlatform.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.byShopPackagePlatform"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SupporterPackSetsDat>> GetManyToManyByShopPackagePlatform(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SupporterPackSetsDat>>();
        }

        var items = new List<ResultItem<int, SupporterPackSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShopPackagePlatform(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SupporterPackSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(string? key, out SupporterPackSetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(string? key, out IReadOnlyList<SupporterPackSetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;

                if (!byUnknown56.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SupporterPackSetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SupporterPackSetsDat"/> with <see cref="SupporterPackSetsDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SupporterPackSetsDat>> GetManyToManyByUnknown56(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SupporterPackSetsDat>>();
        }

        var items = new List<ResultItem<string, SupporterPackSetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SupporterPackSetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SupporterPackSetsDat[] Load()
    {
        const string filePath = "Data/SupporterPackSets.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SupporterPackSetsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FormatTitle
            (var formattitleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Background
            (var backgroundLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Time0
            (var time0Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Time1
            (var time1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShopPackagePlatform
            (var tempshoppackageplatformLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var shoppackageplatformLoading = tempshoppackageplatformLoading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SupporterPackSetsDat()
            {
                Id = idLoading,
                FormatTitle = formattitleLoading,
                Background = backgroundLoading,
                Time0 = time0Loading,
                Time1 = time1Loading,
                ShopPackagePlatform = shoppackageplatformLoading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
