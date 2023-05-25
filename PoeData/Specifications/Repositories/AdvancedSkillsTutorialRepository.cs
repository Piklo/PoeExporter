using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AdvancedSkillsTutorialDat"/> related data and helper methods.
/// </summary>
public sealed class AdvancedSkillsTutorialRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AdvancedSkillsTutorialDat> Items { get; }

    private Dictionary<string, List<AdvancedSkillsTutorialDat>>? byId;
    private Dictionary<int, List<AdvancedSkillsTutorialDat>>? bySkillGemInfoKey1;
    private Dictionary<int, List<AdvancedSkillsTutorialDat>>? bySkillGemInfoKey2;
    private Dictionary<string, List<AdvancedSkillsTutorialDat>>? byDescription;
    private Dictionary<string, List<AdvancedSkillsTutorialDat>>? byInternational_BK2File;
    private Dictionary<int, List<AdvancedSkillsTutorialDat>>? bySkillGemsKey;
    private Dictionary<string, List<AdvancedSkillsTutorialDat>>? byChina_BK2File;
    private Dictionary<int, List<AdvancedSkillsTutorialDat>>? byCharactersKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdvancedSkillsTutorialRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AdvancedSkillsTutorialRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AdvancedSkillsTutorialDat? item)
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
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AdvancedSkillsTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
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
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AdvancedSkillsTutorialDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AdvancedSkillsTutorialDat>>();
        }

        var items = new List<ResultItem<string, AdvancedSkillsTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AdvancedSkillsTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.SkillGemInfoKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillGemInfoKey1(int? key, out AdvancedSkillsTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillGemInfoKey1(key, out var items))
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
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.SkillGemInfoKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillGemInfoKey1(int? key, out IReadOnlyList<AdvancedSkillsTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        if (bySkillGemInfoKey1 is null)
        {
            bySkillGemInfoKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillGemInfoKey1;
                foreach (var listKey in itemKey)
                {
                    if (!bySkillGemInfoKey1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySkillGemInfoKey1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySkillGemInfoKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.bySkillGemInfoKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdvancedSkillsTutorialDat>> GetManyToManyBySkillGemInfoKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdvancedSkillsTutorialDat>>();
        }

        var items = new List<ResultItem<int, AdvancedSkillsTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillGemInfoKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdvancedSkillsTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.SkillGemInfoKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillGemInfoKey2(int? key, out AdvancedSkillsTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillGemInfoKey2(key, out var items))
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
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.SkillGemInfoKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillGemInfoKey2(int? key, out IReadOnlyList<AdvancedSkillsTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        if (bySkillGemInfoKey2 is null)
        {
            bySkillGemInfoKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillGemInfoKey2;
                foreach (var listKey in itemKey)
                {
                    if (!bySkillGemInfoKey2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySkillGemInfoKey2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySkillGemInfoKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.bySkillGemInfoKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdvancedSkillsTutorialDat>> GetManyToManyBySkillGemInfoKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdvancedSkillsTutorialDat>>();
        }

        var items = new List<ResultItem<int, AdvancedSkillsTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillGemInfoKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdvancedSkillsTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out AdvancedSkillsTutorialDat? item)
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
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<AdvancedSkillsTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
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
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AdvancedSkillsTutorialDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AdvancedSkillsTutorialDat>>();
        }

        var items = new List<ResultItem<string, AdvancedSkillsTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AdvancedSkillsTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.International_BK2File"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInternational_BK2File(string? key, out AdvancedSkillsTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInternational_BK2File(key, out var items))
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
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.International_BK2File"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInternational_BK2File(string? key, out IReadOnlyList<AdvancedSkillsTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        if (byInternational_BK2File is null)
        {
            byInternational_BK2File = new();
            foreach (var item in Items)
            {
                var itemKey = item.International_BK2File;

                if (!byInternational_BK2File.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInternational_BK2File.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInternational_BK2File.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.byInternational_BK2File"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AdvancedSkillsTutorialDat>> GetManyToManyByInternational_BK2File(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AdvancedSkillsTutorialDat>>();
        }

        var items = new List<ResultItem<string, AdvancedSkillsTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInternational_BK2File(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AdvancedSkillsTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.SkillGemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillGemsKey(int? key, out AdvancedSkillsTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillGemsKey(key, out var items))
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
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.SkillGemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillGemsKey(int? key, out IReadOnlyList<AdvancedSkillsTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        if (bySkillGemsKey is null)
        {
            bySkillGemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillGemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySkillGemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySkillGemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySkillGemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.bySkillGemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdvancedSkillsTutorialDat>> GetManyToManyBySkillGemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdvancedSkillsTutorialDat>>();
        }

        var items = new List<ResultItem<int, AdvancedSkillsTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillGemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdvancedSkillsTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.China_BK2File"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChina_BK2File(string? key, out AdvancedSkillsTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChina_BK2File(key, out var items))
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
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.China_BK2File"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChina_BK2File(string? key, out IReadOnlyList<AdvancedSkillsTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        if (byChina_BK2File is null)
        {
            byChina_BK2File = new();
            foreach (var item in Items)
            {
                var itemKey = item.China_BK2File;

                if (!byChina_BK2File.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChina_BK2File.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChina_BK2File.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.byChina_BK2File"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AdvancedSkillsTutorialDat>> GetManyToManyByChina_BK2File(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AdvancedSkillsTutorialDat>>();
        }

        var items = new List<ResultItem<string, AdvancedSkillsTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChina_BK2File(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AdvancedSkillsTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharactersKey(int? key, out AdvancedSkillsTutorialDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharactersKey(key, out var items))
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
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharactersKey(int? key, out IReadOnlyList<AdvancedSkillsTutorialDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        if (byCharactersKey is null)
        {
            byCharactersKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.CharactersKey;
                foreach (var listKey in itemKey)
                {
                    if (!byCharactersKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCharactersKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCharactersKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdvancedSkillsTutorialDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdvancedSkillsTutorialDat"/> with <see cref="AdvancedSkillsTutorialDat.byCharactersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdvancedSkillsTutorialDat>> GetManyToManyByCharactersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdvancedSkillsTutorialDat>>();
        }

        var items = new List<ResultItem<int, AdvancedSkillsTutorialDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharactersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdvancedSkillsTutorialDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AdvancedSkillsTutorialDat[] Load()
    {
        const string filePath = "Data/AdvancedSkillsTutorial.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AdvancedSkillsTutorialDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SkillGemInfoKey1
            (var tempskillgeminfokey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var skillgeminfokey1Loading = tempskillgeminfokey1Loading.AsReadOnly();

            // loading SkillGemInfoKey2
            (var tempskillgeminfokey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var skillgeminfokey2Loading = tempskillgeminfokey2Loading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading International_BK2File
            (var international_bk2fileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SkillGemsKey
            (var skillgemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading China_BK2File
            (var china_bk2fileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKey
            (var tempcharacterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var characterskeyLoading = tempcharacterskeyLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AdvancedSkillsTutorialDat()
            {
                Id = idLoading,
                SkillGemInfoKey1 = skillgeminfokey1Loading,
                SkillGemInfoKey2 = skillgeminfokey2Loading,
                Description = descriptionLoading,
                International_BK2File = international_bk2fileLoading,
                SkillGemsKey = skillgemskeyLoading,
                China_BK2File = china_bk2fileLoading,
                CharactersKey = characterskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
