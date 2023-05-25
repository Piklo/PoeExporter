using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UniqueMapsDat"/> related data and helper methods.
/// </summary>
public sealed class UniqueMapsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UniqueMapsDat> Items { get; }

    private Dictionary<int, List<UniqueMapsDat>>? byItemVisualIdentityKey;
    private Dictionary<int, List<UniqueMapsDat>>? byWorldAreasKey;
    private Dictionary<int, List<UniqueMapsDat>>? byWordsKey;
    private Dictionary<int, List<UniqueMapsDat>>? byFlavourTextKey;
    private Dictionary<bool, List<UniqueMapsDat>>? byHasGuildCharacter;
    private Dictionary<string, List<UniqueMapsDat>>? byGuildCharacter;
    private Dictionary<string, List<UniqueMapsDat>>? byName;
    private Dictionary<bool, List<UniqueMapsDat>>? byIsDropDisabled;

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueMapsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UniqueMapsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentityKey(int? key, out UniqueMapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualIdentityKey(key, out var items))
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
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentityKey(int? key, out IReadOnlyList<UniqueMapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        if (byItemVisualIdentityKey is null)
        {
            byItemVisualIdentityKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualIdentityKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualIdentityKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualIdentityKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualIdentityKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.byItemVisualIdentityKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueMapsDat>> GetManyToManyByItemVisualIdentityKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueMapsDat>>();
        }

        var items = new List<ResultItem<int, UniqueMapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentityKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueMapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out UniqueMapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<UniqueMapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        if (byWorldAreasKey is null)
        {
            byWorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueMapsDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueMapsDat>>();
        }

        var items = new List<ResultItem<int, UniqueMapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueMapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.WordsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWordsKey(int? key, out UniqueMapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWordsKey(key, out var items))
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
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.WordsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWordsKey(int? key, out IReadOnlyList<UniqueMapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        if (byWordsKey is null)
        {
            byWordsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WordsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWordsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWordsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWordsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.byWordsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueMapsDat>> GetManyToManyByWordsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueMapsDat>>();
        }

        var items = new List<ResultItem<int, UniqueMapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWordsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueMapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.FlavourTextKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourTextKey(int? key, out UniqueMapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlavourTextKey(key, out var items))
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
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.FlavourTextKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourTextKey(int? key, out IReadOnlyList<UniqueMapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        if (byFlavourTextKey is null)
        {
            byFlavourTextKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlavourTextKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFlavourTextKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFlavourTextKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFlavourTextKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.byFlavourTextKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueMapsDat>> GetManyToManyByFlavourTextKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueMapsDat>>();
        }

        var items = new List<ResultItem<int, UniqueMapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourTextKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueMapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.HasGuildCharacter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHasGuildCharacter(bool? key, out UniqueMapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHasGuildCharacter(key, out var items))
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
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.HasGuildCharacter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHasGuildCharacter(bool? key, out IReadOnlyList<UniqueMapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        if (byHasGuildCharacter is null)
        {
            byHasGuildCharacter = new();
            foreach (var item in Items)
            {
                var itemKey = item.HasGuildCharacter;

                if (!byHasGuildCharacter.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHasGuildCharacter.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHasGuildCharacter.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.byHasGuildCharacter"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UniqueMapsDat>> GetManyToManyByHasGuildCharacter(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UniqueMapsDat>>();
        }

        var items = new List<ResultItem<bool, UniqueMapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHasGuildCharacter(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UniqueMapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.GuildCharacter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGuildCharacter(string? key, out UniqueMapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGuildCharacter(key, out var items))
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
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.GuildCharacter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGuildCharacter(string? key, out IReadOnlyList<UniqueMapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        if (byGuildCharacter is null)
        {
            byGuildCharacter = new();
            foreach (var item in Items)
            {
                var itemKey = item.GuildCharacter;

                if (!byGuildCharacter.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGuildCharacter.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGuildCharacter.TryGetValue(key, out var temp))
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.byGuildCharacter"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UniqueMapsDat>> GetManyToManyByGuildCharacter(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UniqueMapsDat>>();
        }

        var items = new List<ResultItem<string, UniqueMapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGuildCharacter(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UniqueMapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out UniqueMapsDat? item)
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
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<UniqueMapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapsDat>();
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
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UniqueMapsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UniqueMapsDat>>();
        }

        var items = new List<ResultItem<string, UniqueMapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UniqueMapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.IsDropDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsDropDisabled(bool? key, out UniqueMapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsDropDisabled(key, out var items))
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
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.IsDropDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsDropDisabled(bool? key, out IReadOnlyList<UniqueMapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        if (byIsDropDisabled is null)
        {
            byIsDropDisabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsDropDisabled;

                if (!byIsDropDisabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsDropDisabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsDropDisabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueMapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueMapsDat"/> with <see cref="UniqueMapsDat.byIsDropDisabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UniqueMapsDat>> GetManyToManyByIsDropDisabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UniqueMapsDat>>();
        }

        var items = new List<ResultItem<bool, UniqueMapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsDropDisabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UniqueMapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UniqueMapsDat[] Load()
    {
        const string filePath = "Data/UniqueMaps.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueMapsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ItemVisualIdentityKey
            (var itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading WordsKey
            (var wordskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading FlavourTextKey
            (var flavourtextkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HasGuildCharacter
            (var hasguildcharacterLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading GuildCharacter
            (var guildcharacterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsDropDisabled
            (var isdropdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueMapsDat()
            {
                ItemVisualIdentityKey = itemvisualidentitykeyLoading,
                WorldAreasKey = worldareaskeyLoading,
                WordsKey = wordskeyLoading,
                FlavourTextKey = flavourtextkeyLoading,
                HasGuildCharacter = hasguildcharacterLoading,
                GuildCharacter = guildcharacterLoading,
                Name = nameLoading,
                IsDropDisabled = isdropdisabledLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
