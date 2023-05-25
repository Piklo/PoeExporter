using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MeleeTrailsDat"/> related data and helper methods.
/// </summary>
public sealed class MeleeTrailsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MeleeTrailsDat> Items { get; }

    private Dictionary<string, List<MeleeTrailsDat>>? byEPKFile1;
    private Dictionary<string, List<MeleeTrailsDat>>? byEPKFile2;
    private Dictionary<int, List<MeleeTrailsDat>>? byUnknown16;
    private Dictionary<int, List<MeleeTrailsDat>>? byUnknown20;
    private Dictionary<int, List<MeleeTrailsDat>>? byUnknown24;
    private Dictionary<bool, List<MeleeTrailsDat>>? byUnknown28;
    private Dictionary<string, List<MeleeTrailsDat>>? byAOFile;
    private Dictionary<bool, List<MeleeTrailsDat>>? byUnknown37;

    /// <summary>
    /// Initializes a new instance of the <see cref="MeleeTrailsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MeleeTrailsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.EPKFile1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEPKFile1(string? key, out MeleeTrailsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEPKFile1(key, out var items))
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
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.EPKFile1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEPKFile1(string? key, out IReadOnlyList<MeleeTrailsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        if (byEPKFile1 is null)
        {
            byEPKFile1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.EPKFile1;

                if (!byEPKFile1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEPKFile1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEPKFile1.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.byEPKFile1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MeleeTrailsDat>> GetManyToManyByEPKFile1(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MeleeTrailsDat>>();
        }

        var items = new List<ResultItem<string, MeleeTrailsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEPKFile1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MeleeTrailsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.EPKFile2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEPKFile2(string? key, out MeleeTrailsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEPKFile2(key, out var items))
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
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.EPKFile2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEPKFile2(string? key, out IReadOnlyList<MeleeTrailsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        if (byEPKFile2 is null)
        {
            byEPKFile2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.EPKFile2;

                if (!byEPKFile2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEPKFile2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEPKFile2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.byEPKFile2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MeleeTrailsDat>> GetManyToManyByEPKFile2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MeleeTrailsDat>>();
        }

        var items = new List<ResultItem<string, MeleeTrailsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEPKFile2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MeleeTrailsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out MeleeTrailsDat? item)
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
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<MeleeTrailsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeTrailsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeTrailsDat>>();
        }

        var items = new List<ResultItem<int, MeleeTrailsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeTrailsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out MeleeTrailsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown20(key, out var items))
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
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<MeleeTrailsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;

                if (!byUnknown20.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeTrailsDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeTrailsDat>>();
        }

        var items = new List<ResultItem<int, MeleeTrailsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeTrailsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out MeleeTrailsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown24(key, out var items))
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
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<MeleeTrailsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;

                if (!byUnknown24.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MeleeTrailsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MeleeTrailsDat>>();
        }

        var items = new List<ResultItem<int, MeleeTrailsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MeleeTrailsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(bool? key, out MeleeTrailsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(bool? key, out IReadOnlyList<MeleeTrailsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MeleeTrailsDat>> GetManyToManyByUnknown28(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MeleeTrailsDat>>();
        }

        var items = new List<ResultItem<bool, MeleeTrailsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MeleeTrailsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFile(string? key, out MeleeTrailsDat? item)
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
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFile(string? key, out IReadOnlyList<MeleeTrailsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeTrailsDat>();
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
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.byAOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MeleeTrailsDat>> GetManyToManyByAOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MeleeTrailsDat>>();
        }

        var items = new List<ResultItem<string, MeleeTrailsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MeleeTrailsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown37(bool? key, out MeleeTrailsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown37(key, out var items))
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
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown37(bool? key, out IReadOnlyList<MeleeTrailsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        if (byUnknown37 is null)
        {
            byUnknown37 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown37;

                if (!byUnknown37.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown37.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown37.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MeleeTrailsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MeleeTrailsDat"/> with <see cref="MeleeTrailsDat.byUnknown37"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MeleeTrailsDat>> GetManyToManyByUnknown37(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MeleeTrailsDat>>();
        }

        var items = new List<ResultItem<bool, MeleeTrailsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown37(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MeleeTrailsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MeleeTrailsDat[] Load()
    {
        const string filePath = "Data/MeleeTrails.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MeleeTrailsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading EPKFile1
            (var epkfile1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EPKFile2
            (var epkfile2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MeleeTrailsDat()
            {
                EPKFile1 = epkfile1Loading,
                EPKFile2 = epkfile2Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                AOFile = aofileLoading,
                Unknown37 = unknown37Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
