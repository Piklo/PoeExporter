using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasPrimordialBossOptionsDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasPrimordialBossOptionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasPrimordialBossOptionsDat> Items { get; }

    private Dictionary<int, List<AtlasPrimordialBossOptionsDat>>? byUnknown0;
    private Dictionary<int, List<AtlasPrimordialBossOptionsDat>>? byUnknown4;
    private Dictionary<string, List<AtlasPrimordialBossOptionsDat>>? byDefaultIcon;
    private Dictionary<string, List<AtlasPrimordialBossOptionsDat>>? byHoverIcon;
    private Dictionary<string, List<AtlasPrimordialBossOptionsDat>>? byHighlightIcon;
    private Dictionary<string, List<AtlasPrimordialBossOptionsDat>>? byEmptyIcon;
    private Dictionary<int, List<AtlasPrimordialBossOptionsDat>>? byDescription;
    private Dictionary<int, List<AtlasPrimordialBossOptionsDat>>? byDescriptionActive;
    private Dictionary<string, List<AtlasPrimordialBossOptionsDat>>? byProgressTracker;
    private Dictionary<string, List<AtlasPrimordialBossOptionsDat>>? byProgressTrackerFill;
    private Dictionary<string, List<AtlasPrimordialBossOptionsDat>>? byName;
    private Dictionary<string, List<AtlasPrimordialBossOptionsDat>>? byMapDeviceTrackerFill;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasPrimordialBossOptionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasPrimordialBossOptionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out AtlasPrimordialBossOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown0(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;

                if (!byUnknown0.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossOptionsDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown4(int? key, out AtlasPrimordialBossOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown4(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown4(int? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byUnknown4 is null)
        {
            byUnknown4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown4;

                if (!byUnknown4.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown4.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byUnknown4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossOptionsDat>> GetManyToManyByUnknown4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.DefaultIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDefaultIcon(string? key, out AtlasPrimordialBossOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDefaultIcon(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.DefaultIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDefaultIcon(string? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byDefaultIcon is null)
        {
            byDefaultIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.DefaultIcon;

                if (!byDefaultIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDefaultIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDefaultIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byDefaultIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialBossOptionsDat>> GetManyToManyByDefaultIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDefaultIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.HoverIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHoverIcon(string? key, out AtlasPrimordialBossOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHoverIcon(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.HoverIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHoverIcon(string? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byHoverIcon is null)
        {
            byHoverIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.HoverIcon;

                if (!byHoverIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHoverIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHoverIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byHoverIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialBossOptionsDat>> GetManyToManyByHoverIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHoverIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.HighlightIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHighlightIcon(string? key, out AtlasPrimordialBossOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHighlightIcon(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.HighlightIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHighlightIcon(string? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byHighlightIcon is null)
        {
            byHighlightIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.HighlightIcon;

                if (!byHighlightIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHighlightIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHighlightIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byHighlightIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialBossOptionsDat>> GetManyToManyByHighlightIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHighlightIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.EmptyIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEmptyIcon(string? key, out AtlasPrimordialBossOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEmptyIcon(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.EmptyIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEmptyIcon(string? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byEmptyIcon is null)
        {
            byEmptyIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.EmptyIcon;

                if (!byEmptyIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEmptyIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEmptyIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byEmptyIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialBossOptionsDat>> GetManyToManyByEmptyIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEmptyIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(int? key, out AtlasPrimordialBossOptionsDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(int? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDescription.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossOptionsDat>> GetManyToManyByDescription(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.DescriptionActive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescriptionActive(int? key, out AtlasPrimordialBossOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescriptionActive(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.DescriptionActive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescriptionActive(int? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byDescriptionActive is null)
        {
            byDescriptionActive = new();
            foreach (var item in Items)
            {
                var itemKey = item.DescriptionActive;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDescriptionActive.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDescriptionActive.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDescriptionActive.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byDescriptionActive"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasPrimordialBossOptionsDat>> GetManyToManyByDescriptionActive(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<int, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescriptionActive(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.ProgressTracker"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProgressTracker(string? key, out AtlasPrimordialBossOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProgressTracker(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.ProgressTracker"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProgressTracker(string? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byProgressTracker is null)
        {
            byProgressTracker = new();
            foreach (var item in Items)
            {
                var itemKey = item.ProgressTracker;

                if (!byProgressTracker.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byProgressTracker.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byProgressTracker.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byProgressTracker"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialBossOptionsDat>> GetManyToManyByProgressTracker(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProgressTracker(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.ProgressTrackerFill"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProgressTrackerFill(string? key, out AtlasPrimordialBossOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProgressTrackerFill(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.ProgressTrackerFill"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProgressTrackerFill(string? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byProgressTrackerFill is null)
        {
            byProgressTrackerFill = new();
            foreach (var item in Items)
            {
                var itemKey = item.ProgressTrackerFill;

                if (!byProgressTrackerFill.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byProgressTrackerFill.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byProgressTrackerFill.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byProgressTrackerFill"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialBossOptionsDat>> GetManyToManyByProgressTrackerFill(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProgressTrackerFill(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out AtlasPrimordialBossOptionsDat? item)
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
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
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialBossOptionsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.MapDeviceTrackerFill"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapDeviceTrackerFill(string? key, out AtlasPrimordialBossOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapDeviceTrackerFill(key, out var items))
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
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.MapDeviceTrackerFill"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapDeviceTrackerFill(string? key, out IReadOnlyList<AtlasPrimordialBossOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        if (byMapDeviceTrackerFill is null)
        {
            byMapDeviceTrackerFill = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapDeviceTrackerFill;

                if (!byMapDeviceTrackerFill.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMapDeviceTrackerFill.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMapDeviceTrackerFill.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasPrimordialBossOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasPrimordialBossOptionsDat"/> with <see cref="AtlasPrimordialBossOptionsDat.byMapDeviceTrackerFill"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasPrimordialBossOptionsDat>> GetManyToManyByMapDeviceTrackerFill(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasPrimordialBossOptionsDat>>();
        }

        var items = new List<ResultItem<string, AtlasPrimordialBossOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapDeviceTrackerFill(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasPrimordialBossOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasPrimordialBossOptionsDat[] Load()
    {
        const string filePath = "Data/AtlasPrimordialBossOptions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasPrimordialBossOptionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DefaultIcon
            (var defaulticonLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HoverIcon
            (var hovericonLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HighlightIcon
            (var highlighticonLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EmptyIcon
            (var emptyiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DescriptionActive
            (var descriptionactiveLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ProgressTracker
            (var progresstrackerLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ProgressTrackerFill
            (var progresstrackerfillLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MapDeviceTrackerFill
            (var mapdevicetrackerfillLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasPrimordialBossOptionsDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                DefaultIcon = defaulticonLoading,
                HoverIcon = hovericonLoading,
                HighlightIcon = highlighticonLoading,
                EmptyIcon = emptyiconLoading,
                Description = descriptionLoading,
                DescriptionActive = descriptionactiveLoading,
                ProgressTracker = progresstrackerLoading,
                ProgressTrackerFill = progresstrackerfillLoading,
                Name = nameLoading,
                MapDeviceTrackerFill = mapdevicetrackerfillLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
