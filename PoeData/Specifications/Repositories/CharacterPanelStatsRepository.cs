using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CharacterPanelStatsDat"/> related data and helper methods.
/// </summary>
public sealed class CharacterPanelStatsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CharacterPanelStatsDat> Items { get; }

    private Dictionary<string, List<CharacterPanelStatsDat>>? byId;
    private Dictionary<string, List<CharacterPanelStatsDat>>? byText;
    private Dictionary<int, List<CharacterPanelStatsDat>>? byStatsKeys1;
    private Dictionary<int, List<CharacterPanelStatsDat>>? byCharacterPanelDescriptionModesKey;
    private Dictionary<int, List<CharacterPanelStatsDat>>? byStatsKeys2;
    private Dictionary<int, List<CharacterPanelStatsDat>>? byStatsKeys3;
    private Dictionary<int, List<CharacterPanelStatsDat>>? byCharacterPanelTabsKey;
    private Dictionary<bool, List<CharacterPanelStatsDat>>? byUnknown96;
    private Dictionary<int, List<CharacterPanelStatsDat>>? byUnknown97;
    private Dictionary<int, List<CharacterPanelStatsDat>>? byUnknown113;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterPanelStatsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CharacterPanelStatsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CharacterPanelStatsDat? item)
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
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CharacterPanelStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelStatsDat>();
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
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterPanelStatsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterPanelStatsDat>>();
        }

        var items = new List<ResultItem<string, CharacterPanelStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterPanelStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out CharacterPanelStatsDat? item)
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
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<CharacterPanelStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelStatsDat>();
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
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterPanelStatsDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterPanelStatsDat>>();
        }

        var items = new List<ResultItem<string, CharacterPanelStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterPanelStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.StatsKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys1(int? key, out CharacterPanelStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKeys1(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.StatsKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys1(int? key, out IReadOnlyList<CharacterPanelStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        if (byStatsKeys1 is null)
        {
            byStatsKeys1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKeys1;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsKeys1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsKeys1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsKeys1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.byStatsKeys1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterPanelStatsDat>> GetManyToManyByStatsKeys1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterPanelStatsDat>>();
        }

        var items = new List<ResultItem<int, CharacterPanelStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterPanelStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.CharacterPanelDescriptionModesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharacterPanelDescriptionModesKey(int? key, out CharacterPanelStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharacterPanelDescriptionModesKey(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.CharacterPanelDescriptionModesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharacterPanelDescriptionModesKey(int? key, out IReadOnlyList<CharacterPanelStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        if (byCharacterPanelDescriptionModesKey is null)
        {
            byCharacterPanelDescriptionModesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.CharacterPanelDescriptionModesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCharacterPanelDescriptionModesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCharacterPanelDescriptionModesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCharacterPanelDescriptionModesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.byCharacterPanelDescriptionModesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterPanelStatsDat>> GetManyToManyByCharacterPanelDescriptionModesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterPanelStatsDat>>();
        }

        var items = new List<ResultItem<int, CharacterPanelStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharacterPanelDescriptionModesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterPanelStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.StatsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys2(int? key, out CharacterPanelStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKeys2(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.StatsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys2(int? key, out IReadOnlyList<CharacterPanelStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        if (byStatsKeys2 is null)
        {
            byStatsKeys2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKeys2;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsKeys2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsKeys2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsKeys2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.byStatsKeys2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterPanelStatsDat>> GetManyToManyByStatsKeys2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterPanelStatsDat>>();
        }

        var items = new List<ResultItem<int, CharacterPanelStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterPanelStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.StatsKeys3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys3(int? key, out CharacterPanelStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKeys3(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.StatsKeys3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys3(int? key, out IReadOnlyList<CharacterPanelStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        if (byStatsKeys3 is null)
        {
            byStatsKeys3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKeys3;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsKeys3.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsKeys3.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsKeys3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.byStatsKeys3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterPanelStatsDat>> GetManyToManyByStatsKeys3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterPanelStatsDat>>();
        }

        var items = new List<ResultItem<int, CharacterPanelStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterPanelStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.CharacterPanelTabsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharacterPanelTabsKey(int? key, out CharacterPanelStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharacterPanelTabsKey(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.CharacterPanelTabsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharacterPanelTabsKey(int? key, out IReadOnlyList<CharacterPanelStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        if (byCharacterPanelTabsKey is null)
        {
            byCharacterPanelTabsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.CharacterPanelTabsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCharacterPanelTabsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCharacterPanelTabsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCharacterPanelTabsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.byCharacterPanelTabsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterPanelStatsDat>> GetManyToManyByCharacterPanelTabsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterPanelStatsDat>>();
        }

        var items = new List<ResultItem<int, CharacterPanelStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharacterPanelTabsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterPanelStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown96(bool? key, out CharacterPanelStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown96(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown96(bool? key, out IReadOnlyList<CharacterPanelStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        if (byUnknown96 is null)
        {
            byUnknown96 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown96;

                if (!byUnknown96.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown96.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown96.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.byUnknown96"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CharacterPanelStatsDat>> GetManyToManyByUnknown96(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CharacterPanelStatsDat>>();
        }

        var items = new List<ResultItem<bool, CharacterPanelStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown96(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CharacterPanelStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown97(int? key, out CharacterPanelStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown97(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown97(int? key, out IReadOnlyList<CharacterPanelStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        if (byUnknown97 is null)
        {
            byUnknown97 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown97;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown97.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown97.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown97.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.byUnknown97"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterPanelStatsDat>> GetManyToManyByUnknown97(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterPanelStatsDat>>();
        }

        var items = new List<ResultItem<int, CharacterPanelStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown97(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterPanelStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown113(int? key, out CharacterPanelStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown113(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown113(int? key, out IReadOnlyList<CharacterPanelStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        if (byUnknown113 is null)
        {
            byUnknown113 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown113;

                if (!byUnknown113.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown113.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown113.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterPanelStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelStatsDat"/> with <see cref="CharacterPanelStatsDat.byUnknown113"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterPanelStatsDat>> GetManyToManyByUnknown113(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterPanelStatsDat>>();
        }

        var items = new List<ResultItem<int, CharacterPanelStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown113(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterPanelStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CharacterPanelStatsDat[] Load()
    {
        const string filePath = "Data/CharacterPanelStats.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterPanelStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StatsKeys1
            (var tempstatskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeys1Loading = tempstatskeys1Loading.AsReadOnly();

            // loading CharacterPanelDescriptionModesKey
            (var characterpaneldescriptionmodeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKeys2
            (var tempstatskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeys2Loading = tempstatskeys2Loading.AsReadOnly();

            // loading StatsKeys3
            (var tempstatskeys3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeys3Loading = tempstatskeys3Loading.AsReadOnly();

            // loading CharacterPanelTabsKey
            (var characterpaneltabskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown97
            (var tempunknown97Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown97Loading = tempunknown97Loading.AsReadOnly();

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterPanelStatsDat()
            {
                Id = idLoading,
                Text = textLoading,
                StatsKeys1 = statskeys1Loading,
                CharacterPanelDescriptionModesKey = characterpaneldescriptionmodeskeyLoading,
                StatsKeys2 = statskeys2Loading,
                StatsKeys3 = statskeys3Loading,
                CharacterPanelTabsKey = characterpaneltabskeyLoading,
                Unknown96 = unknown96Loading,
                Unknown97 = unknown97Loading,
                Unknown113 = unknown113Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
