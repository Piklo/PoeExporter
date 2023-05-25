using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ShopPaymentPackageDat"/> related data and helper methods.
/// </summary>
public sealed class ShopPaymentPackageRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ShopPaymentPackageDat> Items { get; }

    private Dictionary<string, List<ShopPaymentPackageDat>>? byId;
    private Dictionary<string, List<ShopPaymentPackageDat>>? byText;
    private Dictionary<int, List<ShopPaymentPackageDat>>? byCoins;
    private Dictionary<bool, List<ShopPaymentPackageDat>>? byAvailableFlag;
    private Dictionary<int, List<ShopPaymentPackageDat>>? byUnknown21;
    private Dictionary<int, List<ShopPaymentPackageDat>>? byUnknown25;
    private Dictionary<bool, List<ShopPaymentPackageDat>>? byUnknown29;
    private Dictionary<bool, List<ShopPaymentPackageDat>>? byContainsBetaKey;
    private Dictionary<int, List<ShopPaymentPackageDat>>? byUnknown31;
    private Dictionary<int, List<ShopPaymentPackageDat>>? byUnknown47;
    private Dictionary<string, List<ShopPaymentPackageDat>>? byBackgroundImage;
    private Dictionary<string, List<ShopPaymentPackageDat>>? byUnknown71;
    private Dictionary<bool, List<ShopPaymentPackageDat>>? byUnknown79;
    private Dictionary<int, List<ShopPaymentPackageDat>>? byUpgrade_ShopPaymentPackageKey;
    private Dictionary<int, List<ShopPaymentPackageDat>>? byPhysicalItemPoints;
    private Dictionary<int, List<ShopPaymentPackageDat>>? byUnknown92;
    private Dictionary<int, List<ShopPaymentPackageDat>>? byShopPackagePlatform;
    private Dictionary<string, List<ShopPaymentPackageDat>>? byUnknown112;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShopPaymentPackageRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ShopPaymentPackageRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ShopPaymentPackageDat? item)
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
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
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopPaymentPackageDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<string, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out ShopPaymentPackageDat? item)
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
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
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopPaymentPackageDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<string, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Coins"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCoins(int? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCoins(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Coins"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCoins(int? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byCoins is null)
        {
            byCoins = new();
            foreach (var item in Items)
            {
                var itemKey = item.Coins;

                if (!byCoins.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCoins.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCoins.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byCoins"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackageDat>> GetManyToManyByCoins(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCoins(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.AvailableFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAvailableFlag(bool? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAvailableFlag(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.AvailableFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAvailableFlag(bool? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byAvailableFlag is null)
        {
            byAvailableFlag = new();
            foreach (var item in Items)
            {
                var itemKey = item.AvailableFlag;

                if (!byAvailableFlag.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAvailableFlag.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAvailableFlag.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byAvailableFlag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ShopPaymentPackageDat>> GetManyToManyByAvailableFlag(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<bool, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAvailableFlag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown21"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown21(int? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown21(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown21"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown21(int? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byUnknown21 is null)
        {
            byUnknown21 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown21;

                if (!byUnknown21.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown21.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown21.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byUnknown21"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackageDat>> GetManyToManyByUnknown21(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown21(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown25(int? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown25(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown25(int? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byUnknown25 is null)
        {
            byUnknown25 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown25;

                if (!byUnknown25.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown25.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown25.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byUnknown25"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackageDat>> GetManyToManyByUnknown25(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown25(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown29(bool? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown29(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown29(bool? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byUnknown29 is null)
        {
            byUnknown29 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown29;

                if (!byUnknown29.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown29.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown29.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byUnknown29"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ShopPaymentPackageDat>> GetManyToManyByUnknown29(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<bool, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown29(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.ContainsBetaKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByContainsBetaKey(bool? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByContainsBetaKey(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.ContainsBetaKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByContainsBetaKey(bool? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byContainsBetaKey is null)
        {
            byContainsBetaKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ContainsBetaKey;

                if (!byContainsBetaKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byContainsBetaKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byContainsBetaKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byContainsBetaKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ShopPaymentPackageDat>> GetManyToManyByContainsBetaKey(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<bool, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByContainsBetaKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown31"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown31(int? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown31(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown31"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown31(int? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byUnknown31 is null)
        {
            byUnknown31 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown31;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown31.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown31.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown31.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byUnknown31"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackageDat>> GetManyToManyByUnknown31(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown31(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown47(int? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown47(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown47(int? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byUnknown47 is null)
        {
            byUnknown47 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown47;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown47.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown47.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown47.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byUnknown47"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackageDat>> GetManyToManyByUnknown47(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown47(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.BackgroundImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBackgroundImage(string? key, out ShopPaymentPackageDat? item)
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.BackgroundImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBackgroundImage(string? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
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
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byBackgroundImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopPaymentPackageDat>> GetManyToManyByBackgroundImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<string, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBackgroundImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown71"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown71(string? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown71(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown71"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown71(string? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byUnknown71 is null)
        {
            byUnknown71 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown71;

                if (!byUnknown71.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown71.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown71.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byUnknown71"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopPaymentPackageDat>> GetManyToManyByUnknown71(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<string, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown71(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown79(bool? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown79(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown79(bool? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byUnknown79 is null)
        {
            byUnknown79 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown79;

                if (!byUnknown79.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown79.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown79.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byUnknown79"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ShopPaymentPackageDat>> GetManyToManyByUnknown79(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<bool, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown79(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Upgrade_ShopPaymentPackageKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUpgrade_ShopPaymentPackageKey(int? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUpgrade_ShopPaymentPackageKey(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Upgrade_ShopPaymentPackageKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUpgrade_ShopPaymentPackageKey(int? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byUpgrade_ShopPaymentPackageKey is null)
        {
            byUpgrade_ShopPaymentPackageKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Upgrade_ShopPaymentPackageKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUpgrade_ShopPaymentPackageKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUpgrade_ShopPaymentPackageKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUpgrade_ShopPaymentPackageKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byUpgrade_ShopPaymentPackageKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackageDat>> GetManyToManyByUpgrade_ShopPaymentPackageKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUpgrade_ShopPaymentPackageKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.PhysicalItemPoints"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPhysicalItemPoints(int? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPhysicalItemPoints(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.PhysicalItemPoints"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPhysicalItemPoints(int? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byPhysicalItemPoints is null)
        {
            byPhysicalItemPoints = new();
            foreach (var item in Items)
            {
                var itemKey = item.PhysicalItemPoints;

                if (!byPhysicalItemPoints.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPhysicalItemPoints.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPhysicalItemPoints.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byPhysicalItemPoints"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackageDat>> GetManyToManyByPhysicalItemPoints(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPhysicalItemPoints(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(int? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown92(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(int? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byUnknown92 is null)
        {
            byUnknown92 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown92;

                if (!byUnknown92.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown92.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown92.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackageDat>> GetManyToManyByUnknown92(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.ShopPackagePlatform"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShopPackagePlatform(int? key, out ShopPaymentPackageDat? item)
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.ShopPackagePlatform"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShopPackagePlatform(int? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
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
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byShopPackagePlatform"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShopPaymentPackageDat>> GetManyToManyByShopPackagePlatform(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<int, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShopPackagePlatform(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown112(string? key, out ShopPaymentPackageDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown112(key, out var items))
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
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown112(string? key, out IReadOnlyList<ShopPaymentPackageDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        if (byUnknown112 is null)
        {
            byUnknown112 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown112;

                if (!byUnknown112.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown112.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown112.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShopPaymentPackageDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShopPaymentPackageDat"/> with <see cref="ShopPaymentPackageDat.byUnknown112"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShopPaymentPackageDat>> GetManyToManyByUnknown112(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShopPaymentPackageDat>>();
        }

        var items = new List<ResultItem<string, ShopPaymentPackageDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown112(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShopPaymentPackageDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ShopPaymentPackageDat[] Load()
    {
        const string filePath = "Data/ShopPaymentPackage.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopPaymentPackageDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Coins
            (var coinsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AvailableFlag
            (var availableflagLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown21
            (var unknown21Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ContainsBetaKey
            (var containsbetakeyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown31
            (var tempunknown31Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown31Loading = tempunknown31Loading.AsReadOnly();

            // loading Unknown47
            (var unknown47Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BackgroundImage
            (var backgroundimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown71
            (var unknown71Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Upgrade_ShopPaymentPackageKey
            (var upgrade_shoppaymentpackagekeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading PhysicalItemPoints
            (var physicalitempointsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ShopPackagePlatform
            (var tempshoppackageplatformLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var shoppackageplatformLoading = tempshoppackageplatformLoading.AsReadOnly();

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopPaymentPackageDat()
            {
                Id = idLoading,
                Text = textLoading,
                Coins = coinsLoading,
                AvailableFlag = availableflagLoading,
                Unknown21 = unknown21Loading,
                Unknown25 = unknown25Loading,
                Unknown29 = unknown29Loading,
                ContainsBetaKey = containsbetakeyLoading,
                Unknown31 = unknown31Loading,
                Unknown47 = unknown47Loading,
                BackgroundImage = backgroundimageLoading,
                Unknown71 = unknown71Loading,
                Unknown79 = unknown79Loading,
                Upgrade_ShopPaymentPackageKey = upgrade_shoppaymentpackagekeyLoading,
                PhysicalItemPoints = physicalitempointsLoading,
                Unknown92 = unknown92Loading,
                ShopPackagePlatform = shoppackageplatformLoading,
                Unknown112 = unknown112Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
