using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ItemVisualIdentityDat"/> related data and helper methods.
/// </summary>
public sealed class ItemVisualIdentityRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ItemVisualIdentityDat> Items { get; }

    private Dictionary<string, List<ItemVisualIdentityDat>>? byId;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byDDSFile;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byAOFile;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byInventorySoundEffect;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byUnknown40;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byAOFile2;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byMarauderSMFiles;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byRangerSMFiles;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byWitchSMFiles;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byDuelistDexSMFiles;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byTemplarSMFiles;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byShadowSMFiles;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byScionSMFiles;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byMarauderShape;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byRangerShape;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byWitchShape;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byDuelistShape;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byTemplarShape;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byShadowShape;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byScionShape;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byUnknown220;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byUnknown224;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byPickup_AchievementItemsKeys;
    private Dictionary<string, List<ItemVisualIdentityDat>>? bySMFiles;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byIdentify_AchievementItemsKeys;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byEPKFile;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byCorrupt_AchievementItemsKeys;
    private Dictionary<bool, List<ItemVisualIdentityDat>>? byIsAlternateArt;
    private Dictionary<bool, List<ItemVisualIdentityDat>>? byUnknown301;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byCreateCorruptedJewelAchievementItemsKey;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byAnimationLocation;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown326;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown334;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown342;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown350;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown358;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown366;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown374;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown382;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown390;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown398;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown406;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown414;
    private Dictionary<bool, List<ItemVisualIdentityDat>>? byIsAtlasOfWorldsMapIcon;
    private Dictionary<bool, List<ItemVisualIdentityDat>>? byIsTier16Icon;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byUnknown424;
    private Dictionary<bool, List<ItemVisualIdentityDat>>? byUnknown440;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byUnknown441;
    private Dictionary<string, List<ItemVisualIdentityDat>>? byUnknown457;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byUnknown465;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byUnknown469;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byUnknown485;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byUnknown501;
    private Dictionary<int, List<ItemVisualIdentityDat>>? byUnknown517;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemVisualIdentityRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ItemVisualIdentityRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ItemVisualIdentityDat? item)
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
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
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDDSFile(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDDSFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDDSFile(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byDDSFile is null)
        {
            byDDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.DDSFile;

                if (!byDDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byDDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByDDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFile(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFile(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byAOFile is null)
        {
            byAOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFile;

                if (!byAOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byAOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByAOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.InventorySoundEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInventorySoundEffect(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInventorySoundEffect(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.InventorySoundEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInventorySoundEffect(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byInventorySoundEffect is null)
        {
            byInventorySoundEffect = new();
            foreach (var item in Items)
            {
                var itemKey = item.InventorySoundEffect;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byInventorySoundEffect.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byInventorySoundEffect.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byInventorySoundEffect.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byInventorySoundEffect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByInventorySoundEffect(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInventorySoundEffect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out ItemVisualIdentityDat? item)
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
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
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.AOFile2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFile2(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFile2(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.AOFile2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFile2(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byAOFile2 is null)
        {
            byAOFile2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFile2;

                if (!byAOFile2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAOFile2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAOFile2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byAOFile2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByAOFile2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFile2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.MarauderSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMarauderSMFiles(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMarauderSMFiles(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.MarauderSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMarauderSMFiles(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byMarauderSMFiles is null)
        {
            byMarauderSMFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.MarauderSMFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byMarauderSMFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMarauderSMFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMarauderSMFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byMarauderSMFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByMarauderSMFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMarauderSMFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.RangerSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRangerSMFiles(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRangerSMFiles(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.RangerSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRangerSMFiles(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byRangerSMFiles is null)
        {
            byRangerSMFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.RangerSMFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byRangerSMFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byRangerSMFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byRangerSMFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byRangerSMFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByRangerSMFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRangerSMFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.WitchSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWitchSMFiles(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWitchSMFiles(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.WitchSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWitchSMFiles(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byWitchSMFiles is null)
        {
            byWitchSMFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.WitchSMFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byWitchSMFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWitchSMFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWitchSMFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byWitchSMFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByWitchSMFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWitchSMFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.DuelistDexSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDuelistDexSMFiles(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDuelistDexSMFiles(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.DuelistDexSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDuelistDexSMFiles(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byDuelistDexSMFiles is null)
        {
            byDuelistDexSMFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.DuelistDexSMFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byDuelistDexSMFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byDuelistDexSMFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byDuelistDexSMFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byDuelistDexSMFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByDuelistDexSMFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDuelistDexSMFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.TemplarSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTemplarSMFiles(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTemplarSMFiles(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.TemplarSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTemplarSMFiles(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byTemplarSMFiles is null)
        {
            byTemplarSMFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.TemplarSMFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byTemplarSMFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTemplarSMFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTemplarSMFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byTemplarSMFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByTemplarSMFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTemplarSMFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.ShadowSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShadowSMFiles(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShadowSMFiles(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.ShadowSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShadowSMFiles(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byShadowSMFiles is null)
        {
            byShadowSMFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShadowSMFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byShadowSMFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byShadowSMFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byShadowSMFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byShadowSMFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByShadowSMFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShadowSMFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.ScionSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScionSMFiles(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScionSMFiles(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.ScionSMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScionSMFiles(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byScionSMFiles is null)
        {
            byScionSMFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScionSMFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byScionSMFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byScionSMFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byScionSMFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byScionSMFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByScionSMFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScionSMFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.MarauderShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMarauderShape(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMarauderShape(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.MarauderShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMarauderShape(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byMarauderShape is null)
        {
            byMarauderShape = new();
            foreach (var item in Items)
            {
                var itemKey = item.MarauderShape;

                if (!byMarauderShape.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMarauderShape.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMarauderShape.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byMarauderShape"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByMarauderShape(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMarauderShape(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.RangerShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRangerShape(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRangerShape(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.RangerShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRangerShape(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byRangerShape is null)
        {
            byRangerShape = new();
            foreach (var item in Items)
            {
                var itemKey = item.RangerShape;

                if (!byRangerShape.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRangerShape.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRangerShape.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byRangerShape"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByRangerShape(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRangerShape(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.WitchShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWitchShape(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWitchShape(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.WitchShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWitchShape(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byWitchShape is null)
        {
            byWitchShape = new();
            foreach (var item in Items)
            {
                var itemKey = item.WitchShape;

                if (!byWitchShape.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWitchShape.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWitchShape.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byWitchShape"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByWitchShape(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWitchShape(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.DuelistShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDuelistShape(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDuelistShape(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.DuelistShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDuelistShape(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byDuelistShape is null)
        {
            byDuelistShape = new();
            foreach (var item in Items)
            {
                var itemKey = item.DuelistShape;

                if (!byDuelistShape.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDuelistShape.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDuelistShape.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byDuelistShape"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByDuelistShape(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDuelistShape(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.TemplarShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTemplarShape(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTemplarShape(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.TemplarShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTemplarShape(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byTemplarShape is null)
        {
            byTemplarShape = new();
            foreach (var item in Items)
            {
                var itemKey = item.TemplarShape;

                if (!byTemplarShape.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTemplarShape.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTemplarShape.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byTemplarShape"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByTemplarShape(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTemplarShape(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.ShadowShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShadowShape(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShadowShape(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.ShadowShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShadowShape(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byShadowShape is null)
        {
            byShadowShape = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShadowShape;

                if (!byShadowShape.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShadowShape.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShadowShape.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byShadowShape"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByShadowShape(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShadowShape(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.ScionShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScionShape(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScionShape(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.ScionShape"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScionShape(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byScionShape is null)
        {
            byScionShape = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScionShape;

                if (!byScionShape.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScionShape.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScionShape.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byScionShape"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByScionShape(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScionShape(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown220"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown220(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown220(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown220"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown220(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown220 is null)
        {
            byUnknown220 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown220;

                if (!byUnknown220.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown220.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown220.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown220"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByUnknown220(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown220(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown224"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown224(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown224(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown224"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown224(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown224 is null)
        {
            byUnknown224 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown224;

                if (!byUnknown224.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown224.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown224.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown224"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByUnknown224(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown224(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Pickup_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPickup_AchievementItemsKeys(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPickup_AchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Pickup_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPickup_AchievementItemsKeys(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byPickup_AchievementItemsKeys is null)
        {
            byPickup_AchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Pickup_AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byPickup_AchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPickup_AchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPickup_AchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byPickup_AchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByPickup_AchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPickup_AchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.SMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySMFiles(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySMFiles(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.SMFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySMFiles(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (bySMFiles is null)
        {
            bySMFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.SMFiles;
                foreach (var listKey in itemKey)
                {
                    if (!bySMFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySMFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySMFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.bySMFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyBySMFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySMFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Identify_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIdentify_AchievementItemsKeys(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIdentify_AchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Identify_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIdentify_AchievementItemsKeys(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byIdentify_AchievementItemsKeys is null)
        {
            byIdentify_AchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Identify_AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byIdentify_AchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byIdentify_AchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byIdentify_AchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byIdentify_AchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByIdentify_AchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIdentify_AchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEPKFile(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.EPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEPKFile(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byEPKFile is null)
        {
            byEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.EPKFile;

                if (!byEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Corrupt_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCorrupt_AchievementItemsKeys(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCorrupt_AchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Corrupt_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCorrupt_AchievementItemsKeys(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byCorrupt_AchievementItemsKeys is null)
        {
            byCorrupt_AchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Corrupt_AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byCorrupt_AchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCorrupt_AchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCorrupt_AchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byCorrupt_AchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByCorrupt_AchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCorrupt_AchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.IsAlternateArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsAlternateArt(bool? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsAlternateArt(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.IsAlternateArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsAlternateArt(bool? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byIsAlternateArt is null)
        {
            byIsAlternateArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsAlternateArt;

                if (!byIsAlternateArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsAlternateArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsAlternateArt.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byIsAlternateArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemVisualIdentityDat>> GetManyToManyByIsAlternateArt(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<bool, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsAlternateArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown301"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown301(bool? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown301(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown301"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown301(bool? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown301 is null)
        {
            byUnknown301 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown301;

                if (!byUnknown301.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown301.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown301.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown301"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemVisualIdentityDat>> GetManyToManyByUnknown301(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<bool, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown301(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.CreateCorruptedJewelAchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCreateCorruptedJewelAchievementItemsKey(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCreateCorruptedJewelAchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.CreateCorruptedJewelAchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCreateCorruptedJewelAchievementItemsKey(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byCreateCorruptedJewelAchievementItemsKey is null)
        {
            byCreateCorruptedJewelAchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.CreateCorruptedJewelAchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCreateCorruptedJewelAchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCreateCorruptedJewelAchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCreateCorruptedJewelAchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byCreateCorruptedJewelAchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByCreateCorruptedJewelAchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCreateCorruptedJewelAchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.AnimationLocation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAnimationLocation(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAnimationLocation(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.AnimationLocation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAnimationLocation(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byAnimationLocation is null)
        {
            byAnimationLocation = new();
            foreach (var item in Items)
            {
                var itemKey = item.AnimationLocation;

                if (!byAnimationLocation.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAnimationLocation.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAnimationLocation.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byAnimationLocation"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByAnimationLocation(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAnimationLocation(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown326"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown326(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown326(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown326"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown326(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown326 is null)
        {
            byUnknown326 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown326;

                if (!byUnknown326.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown326.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown326.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown326"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown326(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown326(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown334"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown334(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown334(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown334"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown334(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown334 is null)
        {
            byUnknown334 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown334;

                if (!byUnknown334.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown334.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown334.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown334"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown334(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown334(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown342"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown342(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown342(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown342"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown342(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown342 is null)
        {
            byUnknown342 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown342;

                if (!byUnknown342.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown342.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown342.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown342"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown342(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown342(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown350"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown350(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown350(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown350"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown350(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown350 is null)
        {
            byUnknown350 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown350;

                if (!byUnknown350.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown350.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown350.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown350"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown350(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown350(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown358"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown358(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown358(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown358"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown358(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown358 is null)
        {
            byUnknown358 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown358;

                if (!byUnknown358.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown358.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown358.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown358"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown358(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown358(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown366"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown366(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown366(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown366"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown366(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown366 is null)
        {
            byUnknown366 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown366;

                if (!byUnknown366.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown366.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown366.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown366"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown366(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown366(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown374"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown374(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown374(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown374"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown374(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown374 is null)
        {
            byUnknown374 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown374;

                if (!byUnknown374.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown374.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown374.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown374"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown374(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown374(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown382"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown382(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown382(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown382"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown382(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown382 is null)
        {
            byUnknown382 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown382;

                if (!byUnknown382.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown382.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown382.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown382"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown382(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown382(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown390"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown390(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown390(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown390"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown390(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown390 is null)
        {
            byUnknown390 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown390;

                if (!byUnknown390.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown390.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown390.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown390"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown390(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown390(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown398"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown398(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown398(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown398"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown398(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown398 is null)
        {
            byUnknown398 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown398;

                if (!byUnknown398.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown398.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown398.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown398"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown398(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown398(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown406"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown406(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown406(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown406"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown406(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown406 is null)
        {
            byUnknown406 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown406;

                if (!byUnknown406.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown406.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown406.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown406"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown406(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown406(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown414"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown414(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown414(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown414"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown414(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown414 is null)
        {
            byUnknown414 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown414;

                if (!byUnknown414.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown414.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown414.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown414"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown414(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown414(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.IsAtlasOfWorldsMapIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsAtlasOfWorldsMapIcon(bool? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsAtlasOfWorldsMapIcon(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.IsAtlasOfWorldsMapIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsAtlasOfWorldsMapIcon(bool? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byIsAtlasOfWorldsMapIcon is null)
        {
            byIsAtlasOfWorldsMapIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsAtlasOfWorldsMapIcon;

                if (!byIsAtlasOfWorldsMapIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsAtlasOfWorldsMapIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsAtlasOfWorldsMapIcon.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byIsAtlasOfWorldsMapIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemVisualIdentityDat>> GetManyToManyByIsAtlasOfWorldsMapIcon(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<bool, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsAtlasOfWorldsMapIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.IsTier16Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsTier16Icon(bool? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsTier16Icon(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.IsTier16Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsTier16Icon(bool? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byIsTier16Icon is null)
        {
            byIsTier16Icon = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsTier16Icon;

                if (!byIsTier16Icon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsTier16Icon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsTier16Icon.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byIsTier16Icon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemVisualIdentityDat>> GetManyToManyByIsTier16Icon(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<bool, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsTier16Icon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown424"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown424(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown424(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown424"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown424(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown424 is null)
        {
            byUnknown424 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown424;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown424.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown424.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown424.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown424"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByUnknown424(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown424(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown440"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown440(bool? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown440(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown440"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown440(bool? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown440 is null)
        {
            byUnknown440 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown440;

                if (!byUnknown440.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown440.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown440.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown440"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemVisualIdentityDat>> GetManyToManyByUnknown440(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<bool, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown440(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown441"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown441(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown441(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown441"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown441(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown441 is null)
        {
            byUnknown441 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown441;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown441.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown441.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown441.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown441"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByUnknown441(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown441(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown457"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown457(string? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown457(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown457"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown457(string? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown457 is null)
        {
            byUnknown457 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown457;

                if (!byUnknown457.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown457.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown457.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown457"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualIdentityDat>> GetManyToManyByUnknown457(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown457(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown465"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown465(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown465(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown465"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown465(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown465 is null)
        {
            byUnknown465 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown465;

                if (!byUnknown465.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown465.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown465.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown465"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByUnknown465(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown465(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown469"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown469(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown469(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown469"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown469(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown469 is null)
        {
            byUnknown469 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown469;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown469.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown469.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown469.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown469"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByUnknown469(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown469(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown485"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown485(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown485(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown485"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown485(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown485 is null)
        {
            byUnknown485 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown485;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown485.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown485.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown485.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown485"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByUnknown485(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown485(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown501"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown501(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown501(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown501"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown501(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown501 is null)
        {
            byUnknown501 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown501;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown501.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown501.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown501.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown501"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByUnknown501(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown501(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown517"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown517(int? key, out ItemVisualIdentityDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown517(key, out var items))
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
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.Unknown517"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown517(int? key, out IReadOnlyList<ItemVisualIdentityDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        if (byUnknown517 is null)
        {
            byUnknown517 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown517;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown517.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown517.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown517.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualIdentityDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualIdentityDat"/> with <see cref="ItemVisualIdentityDat.byUnknown517"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualIdentityDat>> GetManyToManyByUnknown517(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualIdentityDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualIdentityDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown517(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualIdentityDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ItemVisualIdentityDat[] Load()
    {
        const string filePath = "Data/ItemVisualIdentity.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemVisualIdentityDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DDSFile
            (var ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading InventorySoundEffect
            (var inventorysoundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AOFile2
            (var aofile2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MarauderSMFiles
            (var tempmaraudersmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var maraudersmfilesLoading = tempmaraudersmfilesLoading.AsReadOnly();

            // loading RangerSMFiles
            (var temprangersmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var rangersmfilesLoading = temprangersmfilesLoading.AsReadOnly();

            // loading WitchSMFiles
            (var tempwitchsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var witchsmfilesLoading = tempwitchsmfilesLoading.AsReadOnly();

            // loading DuelistDexSMFiles
            (var tempduelistdexsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var duelistdexsmfilesLoading = tempduelistdexsmfilesLoading.AsReadOnly();

            // loading TemplarSMFiles
            (var temptemplarsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var templarsmfilesLoading = temptemplarsmfilesLoading.AsReadOnly();

            // loading ShadowSMFiles
            (var tempshadowsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var shadowsmfilesLoading = tempshadowsmfilesLoading.AsReadOnly();

            // loading ScionSMFiles
            (var tempscionsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var scionsmfilesLoading = tempscionsmfilesLoading.AsReadOnly();

            // loading MarauderShape
            (var maraudershapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RangerShape
            (var rangershapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WitchShape
            (var witchshapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DuelistShape
            (var duelistshapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TemplarShape
            (var templarshapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShadowShape
            (var shadowshapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ScionShape
            (var scionshapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown220
            (var unknown220Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown224
            (var unknown224Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Pickup_AchievementItemsKeys
            (var temppickup_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var pickup_achievementitemskeysLoading = temppickup_achievementitemskeysLoading.AsReadOnly();

            // loading SMFiles
            (var tempsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var smfilesLoading = tempsmfilesLoading.AsReadOnly();

            // loading Identify_AchievementItemsKeys
            (var tempidentify_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var identify_achievementitemskeysLoading = tempidentify_achievementitemskeysLoading.AsReadOnly();

            // loading EPKFile
            (var epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Corrupt_AchievementItemsKeys
            (var tempcorrupt_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var corrupt_achievementitemskeysLoading = tempcorrupt_achievementitemskeysLoading.AsReadOnly();

            // loading IsAlternateArt
            (var isalternateartLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown301
            (var unknown301Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CreateCorruptedJewelAchievementItemsKey
            (var createcorruptedjewelachievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AnimationLocation
            (var animationlocationLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown326
            (var unknown326Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown334
            (var unknown334Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown342
            (var unknown342Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown350
            (var unknown350Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown358
            (var unknown358Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown366
            (var unknown366Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown374
            (var unknown374Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown382
            (var unknown382Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown390
            (var unknown390Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown398
            (var unknown398Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown406
            (var unknown406Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown414
            (var unknown414Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsAtlasOfWorldsMapIcon
            (var isatlasofworldsmapiconLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsTier16Icon
            (var istier16iconLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown424
            (var tempunknown424Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown424Loading = tempunknown424Loading.AsReadOnly();

            // loading Unknown440
            (var unknown440Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown441
            (var tempunknown441Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown441Loading = tempunknown441Loading.AsReadOnly();

            // loading Unknown457
            (var unknown457Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown465
            (var unknown465Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown469
            (var unknown469Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown485
            (var unknown485Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown501
            (var unknown501Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown517
            (var unknown517Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemVisualIdentityDat()
            {
                Id = idLoading,
                DDSFile = ddsfileLoading,
                AOFile = aofileLoading,
                InventorySoundEffect = inventorysoundeffectLoading,
                Unknown40 = unknown40Loading,
                AOFile2 = aofile2Loading,
                MarauderSMFiles = maraudersmfilesLoading,
                RangerSMFiles = rangersmfilesLoading,
                WitchSMFiles = witchsmfilesLoading,
                DuelistDexSMFiles = duelistdexsmfilesLoading,
                TemplarSMFiles = templarsmfilesLoading,
                ShadowSMFiles = shadowsmfilesLoading,
                ScionSMFiles = scionsmfilesLoading,
                MarauderShape = maraudershapeLoading,
                RangerShape = rangershapeLoading,
                WitchShape = witchshapeLoading,
                DuelistShape = duelistshapeLoading,
                TemplarShape = templarshapeLoading,
                ShadowShape = shadowshapeLoading,
                ScionShape = scionshapeLoading,
                Unknown220 = unknown220Loading,
                Unknown224 = unknown224Loading,
                Pickup_AchievementItemsKeys = pickup_achievementitemskeysLoading,
                SMFiles = smfilesLoading,
                Identify_AchievementItemsKeys = identify_achievementitemskeysLoading,
                EPKFile = epkfileLoading,
                Corrupt_AchievementItemsKeys = corrupt_achievementitemskeysLoading,
                IsAlternateArt = isalternateartLoading,
                Unknown301 = unknown301Loading,
                CreateCorruptedJewelAchievementItemsKey = createcorruptedjewelachievementitemskeyLoading,
                AnimationLocation = animationlocationLoading,
                Unknown326 = unknown326Loading,
                Unknown334 = unknown334Loading,
                Unknown342 = unknown342Loading,
                Unknown350 = unknown350Loading,
                Unknown358 = unknown358Loading,
                Unknown366 = unknown366Loading,
                Unknown374 = unknown374Loading,
                Unknown382 = unknown382Loading,
                Unknown390 = unknown390Loading,
                Unknown398 = unknown398Loading,
                Unknown406 = unknown406Loading,
                Unknown414 = unknown414Loading,
                IsAtlasOfWorldsMapIcon = isatlasofworldsmapiconLoading,
                IsTier16Icon = istier16iconLoading,
                Unknown424 = unknown424Loading,
                Unknown440 = unknown440Loading,
                Unknown441 = unknown441Loading,
                Unknown457 = unknown457Loading,
                Unknown465 = unknown465Loading,
                Unknown469 = unknown469Loading,
                Unknown485 = unknown485Loading,
                Unknown501 = unknown501Loading,
                Unknown517 = unknown517Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
