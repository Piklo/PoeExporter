using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestCraftOptionsDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestCraftOptionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestCraftOptionsDat> Items { get; }

    private Dictionary<string, List<HarvestCraftOptionsDat>>? byId;
    private Dictionary<string, List<HarvestCraftOptionsDat>>? byText;
    private Dictionary<int, List<HarvestCraftOptionsDat>>? byUnknown16;
    private Dictionary<string, List<HarvestCraftOptionsDat>>? byCommand;
    private Dictionary<string, List<HarvestCraftOptionsDat>>? byParameters;
    private Dictionary<int, List<HarvestCraftOptionsDat>>? byUnknown48;
    private Dictionary<int, List<HarvestCraftOptionsDat>>? byUnknown64;
    private Dictionary<string, List<HarvestCraftOptionsDat>>? byDescription;
    private Dictionary<bool, List<HarvestCraftOptionsDat>>? byUnknown76;
    private Dictionary<int, List<HarvestCraftOptionsDat>>? byLifeforceType;
    private Dictionary<int, List<HarvestCraftOptionsDat>>? byLifeforceCost;
    private Dictionary<int, List<HarvestCraftOptionsDat>>? bySacredCost;
    private Dictionary<bool, List<HarvestCraftOptionsDat>>? byUnknown89;
    private Dictionary<int, List<HarvestCraftOptionsDat>>? byAchievements;
    private Dictionary<int, List<HarvestCraftOptionsDat>>? byUnknown106;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestCraftOptionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestCraftOptionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HarvestCraftOptionsDat? item)
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
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
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftOptionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out HarvestCraftOptionsDat? item)
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
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
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftOptionsDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown16.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestCraftOptionsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Command"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCommand(string? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCommand(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Command"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCommand(string? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (byCommand is null)
        {
            byCommand = new();
            foreach (var item in Items)
            {
                var itemKey = item.Command;

                if (!byCommand.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCommand.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCommand.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byCommand"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftOptionsDat>> GetManyToManyByCommand(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCommand(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Parameters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByParameters(string? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByParameters(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Parameters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByParameters(string? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (byParameters is null)
        {
            byParameters = new();
            foreach (var item in Items)
            {
                var itemKey = item.Parameters;

                if (!byParameters.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byParameters.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byParameters.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byParameters"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftOptionsDat>> GetManyToManyByParameters(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByParameters(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown48(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown48.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown48.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestCraftOptionsDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestCraftOptionsDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out HarvestCraftOptionsDat? item)
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
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
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestCraftOptionsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<string, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(bool? key, out HarvestCraftOptionsDat? item)
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(bool? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
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
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HarvestCraftOptionsDat>> GetManyToManyByUnknown76(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<bool, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.LifeforceType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLifeforceType(int? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLifeforceType(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.LifeforceType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLifeforceType(int? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (byLifeforceType is null)
        {
            byLifeforceType = new();
            foreach (var item in Items)
            {
                var itemKey = item.LifeforceType;

                if (!byLifeforceType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLifeforceType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLifeforceType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byLifeforceType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestCraftOptionsDat>> GetManyToManyByLifeforceType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLifeforceType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.LifeforceCost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLifeforceCost(int? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLifeforceCost(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.LifeforceCost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLifeforceCost(int? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (byLifeforceCost is null)
        {
            byLifeforceCost = new();
            foreach (var item in Items)
            {
                var itemKey = item.LifeforceCost;

                if (!byLifeforceCost.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLifeforceCost.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLifeforceCost.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byLifeforceCost"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestCraftOptionsDat>> GetManyToManyByLifeforceCost(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLifeforceCost(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.SacredCost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySacredCost(int? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySacredCost(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.SacredCost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySacredCost(int? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (bySacredCost is null)
        {
            bySacredCost = new();
            foreach (var item in Items)
            {
                var itemKey = item.SacredCost;

                if (!bySacredCost.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySacredCost.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySacredCost.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.bySacredCost"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestCraftOptionsDat>> GetManyToManyBySacredCost(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySacredCost(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown89"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown89(bool? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown89(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown89"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown89(bool? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (byUnknown89 is null)
        {
            byUnknown89 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown89;

                if (!byUnknown89.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown89.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown89.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byUnknown89"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HarvestCraftOptionsDat>> GetManyToManyByUnknown89(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<bool, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown89(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements(int? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievements(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements(int? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (byAchievements is null)
        {
            byAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievements;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestCraftOptionsDat>> GetManyToManyByAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown106(int? key, out HarvestCraftOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown106(key, out var items))
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
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown106(int? key, out IReadOnlyList<HarvestCraftOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        if (byUnknown106 is null)
        {
            byUnknown106 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown106;

                if (!byUnknown106.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown106.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown106.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestCraftOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestCraftOptionsDat"/> with <see cref="HarvestCraftOptionsDat.byUnknown106"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestCraftOptionsDat>> GetManyToManyByUnknown106(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestCraftOptionsDat>>();
        }

        var items = new List<ResultItem<int, HarvestCraftOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown106(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestCraftOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestCraftOptionsDat[] Load()
    {
        const string filePath = "Data/HarvestCraftOptions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestCraftOptionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Command
            (var commandLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Parameters
            (var parametersLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var tempunknown48Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown48Loading = tempunknown48Loading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading LifeforceType
            (var lifeforcetypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifeforceCost
            (var lifeforcecostLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SacredCost
            (var sacredcostLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown89
            (var unknown89Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestCraftOptionsDat()
            {
                Id = idLoading,
                Text = textLoading,
                Unknown16 = unknown16Loading,
                Command = commandLoading,
                Parameters = parametersLoading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Description = descriptionLoading,
                Unknown76 = unknown76Loading,
                LifeforceType = lifeforcetypeLoading,
                LifeforceCost = lifeforcecostLoading,
                SacredCost = sacredcostLoading,
                Unknown89 = unknown89Loading,
                Achievements = achievementsLoading,
                Unknown106 = unknown106Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
