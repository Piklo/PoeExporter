using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AscendancyDat"/> related data and helper methods.
/// </summary>
public sealed class AscendancyRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AscendancyDat> Items { get; }

    private Dictionary<string, List<AscendancyDat>>? byId;
    private Dictionary<int, List<AscendancyDat>>? byClassNo;
    private Dictionary<int, List<AscendancyDat>>? byCharacters;
    private Dictionary<string, List<AscendancyDat>>? byCoordinateRect;
    private Dictionary<string, List<AscendancyDat>>? byRGBFlavourTextColour;
    private Dictionary<string, List<AscendancyDat>>? byName;
    private Dictionary<string, List<AscendancyDat>>? byFlavourText;
    private Dictionary<string, List<AscendancyDat>>? byOGGFile;
    private Dictionary<string, List<AscendancyDat>>? byPassiveTreeImage;
    private Dictionary<int, List<AscendancyDat>>? byUnknown76;
    private Dictionary<int, List<AscendancyDat>>? byUnknown80;
    private Dictionary<string, List<AscendancyDat>>? byBackgroundImage;

    /// <summary>
    /// Initializes a new instance of the <see cref="AscendancyRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AscendancyRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AscendancyDat? item)
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
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
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AscendancyDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AscendancyDat>>();
        }

        var items = new List<ResultItem<string, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.ClassNo"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClassNo(int? key, out AscendancyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClassNo(key, out var items))
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.ClassNo"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClassNo(int? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        if (byClassNo is null)
        {
            byClassNo = new();
            foreach (var item in Items)
            {
                var itemKey = item.ClassNo;

                if (!byClassNo.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byClassNo.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byClassNo.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byClassNo"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AscendancyDat>> GetManyToManyByClassNo(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AscendancyDat>>();
        }

        var items = new List<ResultItem<int, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClassNo(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.Characters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharacters(int? key, out AscendancyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharacters(key, out var items))
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.Characters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharacters(int? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        if (byCharacters is null)
        {
            byCharacters = new();
            foreach (var item in Items)
            {
                var itemKey = item.Characters;
                foreach (var listKey in itemKey)
                {
                    if (!byCharacters.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCharacters.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCharacters.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byCharacters"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AscendancyDat>> GetManyToManyByCharacters(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AscendancyDat>>();
        }

        var items = new List<ResultItem<int, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharacters(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.CoordinateRect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCoordinateRect(string? key, out AscendancyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCoordinateRect(key, out var items))
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.CoordinateRect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCoordinateRect(string? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        if (byCoordinateRect is null)
        {
            byCoordinateRect = new();
            foreach (var item in Items)
            {
                var itemKey = item.CoordinateRect;

                if (!byCoordinateRect.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCoordinateRect.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCoordinateRect.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byCoordinateRect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AscendancyDat>> GetManyToManyByCoordinateRect(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AscendancyDat>>();
        }

        var items = new List<ResultItem<string, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCoordinateRect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.RGBFlavourTextColour"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRGBFlavourTextColour(string? key, out AscendancyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRGBFlavourTextColour(key, out var items))
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.RGBFlavourTextColour"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRGBFlavourTextColour(string? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        if (byRGBFlavourTextColour is null)
        {
            byRGBFlavourTextColour = new();
            foreach (var item in Items)
            {
                var itemKey = item.RGBFlavourTextColour;

                if (!byRGBFlavourTextColour.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRGBFlavourTextColour.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRGBFlavourTextColour.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byRGBFlavourTextColour"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AscendancyDat>> GetManyToManyByRGBFlavourTextColour(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AscendancyDat>>();
        }

        var items = new List<ResultItem<string, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRGBFlavourTextColour(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out AscendancyDat? item)
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
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
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AscendancyDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AscendancyDat>>();
        }

        var items = new List<ResultItem<string, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourText(string? key, out AscendancyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlavourText(key, out var items))
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourText(string? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        if (byFlavourText is null)
        {
            byFlavourText = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlavourText;

                if (!byFlavourText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFlavourText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFlavourText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byFlavourText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AscendancyDat>> GetManyToManyByFlavourText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AscendancyDat>>();
        }

        var items = new List<ResultItem<string, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.OGGFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOGGFile(string? key, out AscendancyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOGGFile(key, out var items))
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.OGGFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOGGFile(string? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        if (byOGGFile is null)
        {
            byOGGFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.OGGFile;

                if (!byOGGFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOGGFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOGGFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byOGGFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AscendancyDat>> GetManyToManyByOGGFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AscendancyDat>>();
        }

        var items = new List<ResultItem<string, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOGGFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.PassiveTreeImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveTreeImage(string? key, out AscendancyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveTreeImage(key, out var items))
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.PassiveTreeImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveTreeImage(string? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        if (byPassiveTreeImage is null)
        {
            byPassiveTreeImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveTreeImage;

                if (!byPassiveTreeImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPassiveTreeImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveTreeImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byPassiveTreeImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AscendancyDat>> GetManyToManyByPassiveTreeImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AscendancyDat>>();
        }

        var items = new List<ResultItem<string, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveTreeImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(int? key, out AscendancyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown76(key, out var items))
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(int? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        if (byUnknown76 is null)
        {
            byUnknown76 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown76;

                if (!byUnknown76.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown76.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown76.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AscendancyDat>> GetManyToManyByUnknown76(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AscendancyDat>>();
        }

        var items = new List<ResultItem<int, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(int? key, out AscendancyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown80(key, out var items))
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(int? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        if (byUnknown80 is null)
        {
            byUnknown80 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown80;

                if (!byUnknown80.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown80.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown80.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AscendancyDat>> GetManyToManyByUnknown80(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AscendancyDat>>();
        }

        var items = new List<ResultItem<int, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.BackgroundImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBackgroundImage(string? key, out AscendancyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBackgroundImage(key, out var items))
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
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.BackgroundImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBackgroundImage(string? key, out IReadOnlyList<AscendancyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        if (byBackgroundImage is null)
        {
            byBackgroundImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.BackgroundImage;

                if (!byBackgroundImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBackgroundImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBackgroundImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AscendancyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AscendancyDat"/> with <see cref="AscendancyDat.byBackgroundImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AscendancyDat>> GetManyToManyByBackgroundImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AscendancyDat>>();
        }

        var items = new List<ResultItem<string, AscendancyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBackgroundImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AscendancyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AscendancyDat[] Load()
    {
        const string filePath = "Data/Ascendancy.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AscendancyDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ClassNo
            (var classnoLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Characters
            (var tempcharactersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var charactersLoading = tempcharactersLoading.AsReadOnly();

            // loading CoordinateRect
            (var coordinaterectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RGBFlavourTextColour
            (var rgbflavourtextcolourLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OGGFile
            (var oggfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveTreeImage
            (var passivetreeimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BackgroundImage
            (var backgroundimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AscendancyDat()
            {
                Id = idLoading,
                ClassNo = classnoLoading,
                Characters = charactersLoading,
                CoordinateRect = coordinaterectLoading,
                RGBFlavourTextColour = rgbflavourtextcolourLoading,
                Name = nameLoading,
                FlavourText = flavourtextLoading,
                OGGFile = oggfileLoading,
                PassiveTreeImage = passivetreeimageLoading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
                BackgroundImage = backgroundimageLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
