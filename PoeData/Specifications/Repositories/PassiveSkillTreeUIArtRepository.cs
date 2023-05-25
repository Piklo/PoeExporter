using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PassiveSkillTreeUIArtDat"/> related data and helper methods.
/// </summary>
public sealed class PassiveSkillTreeUIArtRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PassiveSkillTreeUIArtDat> Items { get; }

    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byId;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byGroupBackgroundSmall;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byGroupBackgroundMedium;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byGroupBackgroundLarge;
    private Dictionary<bool, List<PassiveSkillTreeUIArtDat>>? byUnknown32;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byPassiveFrameNormal;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byNotableFrameNormal;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byKeystoneFrameNormal;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byPassiveFrameActive;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byNotableFrameActive;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byKeystoneFrameActive;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byPassiveFrameCanAllocate;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byNotableFrameCanAllocate;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byKeystoneCanAllocate;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byOrnament;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byGroupBackgroundSmallBlank;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byGroupBackgroundMediumBlank;
    private Dictionary<string, List<PassiveSkillTreeUIArtDat>>? byGroupBackgroundLargeBlank;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassiveSkillTreeUIArtRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PassiveSkillTreeUIArtRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out PassiveSkillTreeUIArtDat? item)
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
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
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundSmall"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGroupBackgroundSmall(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGroupBackgroundSmall(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundSmall"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGroupBackgroundSmall(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byGroupBackgroundSmall is null)
        {
            byGroupBackgroundSmall = new();
            foreach (var item in Items)
            {
                var itemKey = item.GroupBackgroundSmall;

                if (!byGroupBackgroundSmall.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGroupBackgroundSmall.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGroupBackgroundSmall.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byGroupBackgroundSmall"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByGroupBackgroundSmall(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGroupBackgroundSmall(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundMedium"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGroupBackgroundMedium(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGroupBackgroundMedium(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundMedium"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGroupBackgroundMedium(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byGroupBackgroundMedium is null)
        {
            byGroupBackgroundMedium = new();
            foreach (var item in Items)
            {
                var itemKey = item.GroupBackgroundMedium;

                if (!byGroupBackgroundMedium.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGroupBackgroundMedium.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGroupBackgroundMedium.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byGroupBackgroundMedium"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByGroupBackgroundMedium(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGroupBackgroundMedium(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundLarge"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGroupBackgroundLarge(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGroupBackgroundLarge(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundLarge"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGroupBackgroundLarge(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byGroupBackgroundLarge is null)
        {
            byGroupBackgroundLarge = new();
            foreach (var item in Items)
            {
                var itemKey = item.GroupBackgroundLarge;

                if (!byGroupBackgroundLarge.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGroupBackgroundLarge.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGroupBackgroundLarge.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byGroupBackgroundLarge"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByGroupBackgroundLarge(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGroupBackgroundLarge(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(bool? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(bool? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PassiveSkillTreeUIArtDat>> GetManyToManyByUnknown32(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<bool, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.PassiveFrameNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveFrameNormal(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveFrameNormal(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.PassiveFrameNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveFrameNormal(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byPassiveFrameNormal is null)
        {
            byPassiveFrameNormal = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveFrameNormal;

                if (!byPassiveFrameNormal.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPassiveFrameNormal.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveFrameNormal.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byPassiveFrameNormal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByPassiveFrameNormal(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveFrameNormal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.NotableFrameNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNotableFrameNormal(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNotableFrameNormal(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.NotableFrameNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNotableFrameNormal(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byNotableFrameNormal is null)
        {
            byNotableFrameNormal = new();
            foreach (var item in Items)
            {
                var itemKey = item.NotableFrameNormal;

                if (!byNotableFrameNormal.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNotableFrameNormal.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNotableFrameNormal.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byNotableFrameNormal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByNotableFrameNormal(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNotableFrameNormal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.KeystoneFrameNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKeystoneFrameNormal(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKeystoneFrameNormal(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.KeystoneFrameNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKeystoneFrameNormal(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byKeystoneFrameNormal is null)
        {
            byKeystoneFrameNormal = new();
            foreach (var item in Items)
            {
                var itemKey = item.KeystoneFrameNormal;

                if (!byKeystoneFrameNormal.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byKeystoneFrameNormal.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byKeystoneFrameNormal.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byKeystoneFrameNormal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByKeystoneFrameNormal(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKeystoneFrameNormal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.PassiveFrameActive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveFrameActive(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveFrameActive(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.PassiveFrameActive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveFrameActive(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byPassiveFrameActive is null)
        {
            byPassiveFrameActive = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveFrameActive;

                if (!byPassiveFrameActive.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPassiveFrameActive.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveFrameActive.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byPassiveFrameActive"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByPassiveFrameActive(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveFrameActive(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.NotableFrameActive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNotableFrameActive(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNotableFrameActive(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.NotableFrameActive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNotableFrameActive(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byNotableFrameActive is null)
        {
            byNotableFrameActive = new();
            foreach (var item in Items)
            {
                var itemKey = item.NotableFrameActive;

                if (!byNotableFrameActive.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNotableFrameActive.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNotableFrameActive.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byNotableFrameActive"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByNotableFrameActive(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNotableFrameActive(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.KeystoneFrameActive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKeystoneFrameActive(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKeystoneFrameActive(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.KeystoneFrameActive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKeystoneFrameActive(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byKeystoneFrameActive is null)
        {
            byKeystoneFrameActive = new();
            foreach (var item in Items)
            {
                var itemKey = item.KeystoneFrameActive;

                if (!byKeystoneFrameActive.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byKeystoneFrameActive.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byKeystoneFrameActive.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byKeystoneFrameActive"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByKeystoneFrameActive(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKeystoneFrameActive(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.PassiveFrameCanAllocate"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveFrameCanAllocate(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveFrameCanAllocate(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.PassiveFrameCanAllocate"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveFrameCanAllocate(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byPassiveFrameCanAllocate is null)
        {
            byPassiveFrameCanAllocate = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveFrameCanAllocate;

                if (!byPassiveFrameCanAllocate.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPassiveFrameCanAllocate.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveFrameCanAllocate.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byPassiveFrameCanAllocate"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByPassiveFrameCanAllocate(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveFrameCanAllocate(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.NotableFrameCanAllocate"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNotableFrameCanAllocate(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNotableFrameCanAllocate(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.NotableFrameCanAllocate"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNotableFrameCanAllocate(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byNotableFrameCanAllocate is null)
        {
            byNotableFrameCanAllocate = new();
            foreach (var item in Items)
            {
                var itemKey = item.NotableFrameCanAllocate;

                if (!byNotableFrameCanAllocate.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNotableFrameCanAllocate.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNotableFrameCanAllocate.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byNotableFrameCanAllocate"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByNotableFrameCanAllocate(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNotableFrameCanAllocate(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.KeystoneCanAllocate"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByKeystoneCanAllocate(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByKeystoneCanAllocate(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.KeystoneCanAllocate"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByKeystoneCanAllocate(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byKeystoneCanAllocate is null)
        {
            byKeystoneCanAllocate = new();
            foreach (var item in Items)
            {
                var itemKey = item.KeystoneCanAllocate;

                if (!byKeystoneCanAllocate.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byKeystoneCanAllocate.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byKeystoneCanAllocate.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byKeystoneCanAllocate"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByKeystoneCanAllocate(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByKeystoneCanAllocate(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.Ornament"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOrnament(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOrnament(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.Ornament"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOrnament(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byOrnament is null)
        {
            byOrnament = new();
            foreach (var item in Items)
            {
                var itemKey = item.Ornament;

                if (!byOrnament.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOrnament.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOrnament.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byOrnament"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByOrnament(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOrnament(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundSmallBlank"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGroupBackgroundSmallBlank(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGroupBackgroundSmallBlank(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundSmallBlank"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGroupBackgroundSmallBlank(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byGroupBackgroundSmallBlank is null)
        {
            byGroupBackgroundSmallBlank = new();
            foreach (var item in Items)
            {
                var itemKey = item.GroupBackgroundSmallBlank;

                if (!byGroupBackgroundSmallBlank.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGroupBackgroundSmallBlank.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGroupBackgroundSmallBlank.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byGroupBackgroundSmallBlank"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByGroupBackgroundSmallBlank(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGroupBackgroundSmallBlank(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundMediumBlank"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGroupBackgroundMediumBlank(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGroupBackgroundMediumBlank(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundMediumBlank"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGroupBackgroundMediumBlank(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byGroupBackgroundMediumBlank is null)
        {
            byGroupBackgroundMediumBlank = new();
            foreach (var item in Items)
            {
                var itemKey = item.GroupBackgroundMediumBlank;

                if (!byGroupBackgroundMediumBlank.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGroupBackgroundMediumBlank.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGroupBackgroundMediumBlank.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byGroupBackgroundMediumBlank"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByGroupBackgroundMediumBlank(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGroupBackgroundMediumBlank(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundLargeBlank"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGroupBackgroundLargeBlank(string? key, out PassiveSkillTreeUIArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGroupBackgroundLargeBlank(key, out var items))
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
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.GroupBackgroundLargeBlank"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGroupBackgroundLargeBlank(string? key, out IReadOnlyList<PassiveSkillTreeUIArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        if (byGroupBackgroundLargeBlank is null)
        {
            byGroupBackgroundLargeBlank = new();
            foreach (var item in Items)
            {
                var itemKey = item.GroupBackgroundLargeBlank;

                if (!byGroupBackgroundLargeBlank.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGroupBackgroundLargeBlank.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGroupBackgroundLargeBlank.TryGetValue(key, out var temp))
        {
            items = Array.Empty<PassiveSkillTreeUIArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PassiveSkillTreeUIArtDat"/> with <see cref="PassiveSkillTreeUIArtDat.byGroupBackgroundLargeBlank"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PassiveSkillTreeUIArtDat>> GetManyToManyByGroupBackgroundLargeBlank(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PassiveSkillTreeUIArtDat>>();
        }

        var items = new List<ResultItem<string, PassiveSkillTreeUIArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGroupBackgroundLargeBlank(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PassiveSkillTreeUIArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PassiveSkillTreeUIArtDat[] Load()
    {
        const string filePath = "Data/PassiveSkillTreeUIArt.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveSkillTreeUIArtDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundSmall
            (var groupbackgroundsmallLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundMedium
            (var groupbackgroundmediumLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundLarge
            (var groupbackgroundlargeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading PassiveFrameNormal
            (var passiveframenormalLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NotableFrameNormal
            (var notableframenormalLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading KeystoneFrameNormal
            (var keystoneframenormalLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveFrameActive
            (var passiveframeactiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NotableFrameActive
            (var notableframeactiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading KeystoneFrameActive
            (var keystoneframeactiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveFrameCanAllocate
            (var passiveframecanallocateLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NotableFrameCanAllocate
            (var notableframecanallocateLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading KeystoneCanAllocate
            (var keystonecanallocateLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Ornament
            (var ornamentLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundSmallBlank
            (var groupbackgroundsmallblankLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundMediumBlank
            (var groupbackgroundmediumblankLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GroupBackgroundLargeBlank
            (var groupbackgroundlargeblankLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveSkillTreeUIArtDat()
            {
                Id = idLoading,
                GroupBackgroundSmall = groupbackgroundsmallLoading,
                GroupBackgroundMedium = groupbackgroundmediumLoading,
                GroupBackgroundLarge = groupbackgroundlargeLoading,
                Unknown32 = unknown32Loading,
                PassiveFrameNormal = passiveframenormalLoading,
                NotableFrameNormal = notableframenormalLoading,
                KeystoneFrameNormal = keystoneframenormalLoading,
                PassiveFrameActive = passiveframeactiveLoading,
                NotableFrameActive = notableframeactiveLoading,
                KeystoneFrameActive = keystoneframeactiveLoading,
                PassiveFrameCanAllocate = passiveframecanallocateLoading,
                NotableFrameCanAllocate = notableframecanallocateLoading,
                KeystoneCanAllocate = keystonecanallocateLoading,
                Ornament = ornamentLoading,
                GroupBackgroundSmallBlank = groupbackgroundsmallblankLoading,
                GroupBackgroundMediumBlank = groupbackgroundmediumblankLoading,
                GroupBackgroundLargeBlank = groupbackgroundlargeblankLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
