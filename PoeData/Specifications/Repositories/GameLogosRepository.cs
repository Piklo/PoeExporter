using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GameLogosDat"/> related data and helper methods.
/// </summary>
public sealed class GameLogosRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GameLogosDat> Items { get; }

    private Dictionary<string, List<GameLogosDat>>? byId;
    private Dictionary<string, List<GameLogosDat>>? byLogoIntl;
    private Dictionary<string, List<GameLogosDat>>? byLogoTW;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameLogosRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GameLogosRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GameLogosDat"/> with <see cref="GameLogosDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out GameLogosDat? item)
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
    /// Tries to get <see cref="GameLogosDat"/> with <see cref="GameLogosDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<GameLogosDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GameLogosDat>();
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
            items = Array.Empty<GameLogosDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GameLogosDat"/> with <see cref="GameLogosDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GameLogosDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GameLogosDat>>();
        }

        var items = new List<ResultItem<string, GameLogosDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GameLogosDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GameLogosDat"/> with <see cref="GameLogosDat.LogoIntl"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLogoIntl(string? key, out GameLogosDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLogoIntl(key, out var items))
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
    /// Tries to get <see cref="GameLogosDat"/> with <see cref="GameLogosDat.LogoIntl"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLogoIntl(string? key, out IReadOnlyList<GameLogosDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GameLogosDat>();
            return false;
        }

        if (byLogoIntl is null)
        {
            byLogoIntl = new();
            foreach (var item in Items)
            {
                var itemKey = item.LogoIntl;

                if (!byLogoIntl.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLogoIntl.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLogoIntl.TryGetValue(key, out var temp))
        {
            items = Array.Empty<GameLogosDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GameLogosDat"/> with <see cref="GameLogosDat.byLogoIntl"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GameLogosDat>> GetManyToManyByLogoIntl(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GameLogosDat>>();
        }

        var items = new List<ResultItem<string, GameLogosDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLogoIntl(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GameLogosDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GameLogosDat"/> with <see cref="GameLogosDat.LogoTW"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLogoTW(string? key, out GameLogosDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLogoTW(key, out var items))
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
    /// Tries to get <see cref="GameLogosDat"/> with <see cref="GameLogosDat.LogoTW"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLogoTW(string? key, out IReadOnlyList<GameLogosDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GameLogosDat>();
            return false;
        }

        if (byLogoTW is null)
        {
            byLogoTW = new();
            foreach (var item in Items)
            {
                var itemKey = item.LogoTW;

                if (!byLogoTW.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLogoTW.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLogoTW.TryGetValue(key, out var temp))
        {
            items = Array.Empty<GameLogosDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GameLogosDat"/> with <see cref="GameLogosDat.byLogoTW"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GameLogosDat>> GetManyToManyByLogoTW(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GameLogosDat>>();
        }

        var items = new List<ResultItem<string, GameLogosDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLogoTW(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GameLogosDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GameLogosDat[] Load()
    {
        const string filePath = "Data/GameLogos.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GameLogosDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading LogoIntl
            (var logointlLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading LogoTW
            (var logotwLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GameLogosDat()
            {
                Id = idLoading,
                LogoIntl = logointlLoading,
                LogoTW = logotwLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
