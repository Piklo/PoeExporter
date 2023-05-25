using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LeagueFlagDat"/> related data and helper methods.
/// </summary>
public sealed class LeagueFlagRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LeagueFlagDat> Items { get; }

    private Dictionary<string, List<LeagueFlagDat>>? byId;
    private Dictionary<string, List<LeagueFlagDat>>? byImage;
    private Dictionary<bool, List<LeagueFlagDat>>? byIsHC;
    private Dictionary<bool, List<LeagueFlagDat>>? byIsSSF;
    private Dictionary<string, List<LeagueFlagDat>>? byBanner;
    private Dictionary<bool, List<LeagueFlagDat>>? byIsRuthless;

    /// <summary>
    /// Initializes a new instance of the <see cref="LeagueFlagRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LeagueFlagRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out LeagueFlagDat? item)
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
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<LeagueFlagDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueFlagDat>();
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
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueFlagDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueFlagDat>>();
        }

        var items = new List<ResultItem<string, LeagueFlagDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueFlagDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.Image"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImage(string? key, out LeagueFlagDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByImage(key, out var items))
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
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.Image"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImage(string? key, out IReadOnlyList<LeagueFlagDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        if (byImage is null)
        {
            byImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.Image;

                if (!byImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.byImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueFlagDat>> GetManyToManyByImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueFlagDat>>();
        }

        var items = new List<ResultItem<string, LeagueFlagDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueFlagDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.IsHC"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsHC(bool? key, out LeagueFlagDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsHC(key, out var items))
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
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.IsHC"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsHC(bool? key, out IReadOnlyList<LeagueFlagDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        if (byIsHC is null)
        {
            byIsHC = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsHC;

                if (!byIsHC.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsHC.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsHC.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.byIsHC"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LeagueFlagDat>> GetManyToManyByIsHC(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LeagueFlagDat>>();
        }

        var items = new List<ResultItem<bool, LeagueFlagDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsHC(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LeagueFlagDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.IsSSF"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsSSF(bool? key, out LeagueFlagDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsSSF(key, out var items))
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
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.IsSSF"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsSSF(bool? key, out IReadOnlyList<LeagueFlagDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        if (byIsSSF is null)
        {
            byIsSSF = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsSSF;

                if (!byIsSSF.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsSSF.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsSSF.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.byIsSSF"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LeagueFlagDat>> GetManyToManyByIsSSF(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LeagueFlagDat>>();
        }

        var items = new List<ResultItem<bool, LeagueFlagDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsSSF(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LeagueFlagDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.Banner"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBanner(string? key, out LeagueFlagDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBanner(key, out var items))
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
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.Banner"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBanner(string? key, out IReadOnlyList<LeagueFlagDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        if (byBanner is null)
        {
            byBanner = new();
            foreach (var item in Items)
            {
                var itemKey = item.Banner;

                if (!byBanner.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBanner.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBanner.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.byBanner"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LeagueFlagDat>> GetManyToManyByBanner(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LeagueFlagDat>>();
        }

        var items = new List<ResultItem<string, LeagueFlagDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBanner(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LeagueFlagDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.IsRuthless"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsRuthless(bool? key, out LeagueFlagDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsRuthless(key, out var items))
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
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.IsRuthless"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsRuthless(bool? key, out IReadOnlyList<LeagueFlagDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        if (byIsRuthless is null)
        {
            byIsRuthless = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsRuthless;

                if (!byIsRuthless.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsRuthless.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsRuthless.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LeagueFlagDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LeagueFlagDat"/> with <see cref="LeagueFlagDat.byIsRuthless"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, LeagueFlagDat>> GetManyToManyByIsRuthless(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, LeagueFlagDat>>();
        }

        var items = new List<ResultItem<bool, LeagueFlagDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsRuthless(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, LeagueFlagDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LeagueFlagDat[] Load()
    {
        const string filePath = "Data/LeagueFlag.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LeagueFlagDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Image
            (var imageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsHC
            (var ishcLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsSSF
            (var isssfLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Banner
            (var bannerLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsRuthless
            (var isruthlessLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LeagueFlagDat()
            {
                Id = idLoading,
                Image = imageLoading,
                IsHC = ishcLoading,
                IsSSF = isssfLoading,
                Banner = bannerLoading,
                IsRuthless = isruthlessLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
