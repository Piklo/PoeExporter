using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ArchetypesDat"/> related data and helper methods.
/// </summary>
public sealed class ArchetypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ArchetypesDat> Items { get; }

    private Dictionary<string, List<ArchetypesDat>>? byId;
    private Dictionary<int, List<ArchetypesDat>>? byCharactersKey;
    private Dictionary<string, List<ArchetypesDat>>? byPassiveSkillTreeURL;
    private Dictionary<string, List<ArchetypesDat>>? byAscendancyClassName;
    private Dictionary<string, List<ArchetypesDat>>? byDescription;
    private Dictionary<string, List<ArchetypesDat>>? byUIImageFile;
    private Dictionary<string, List<ArchetypesDat>>? byTutorialVideo_BKFile;
    private Dictionary<int, List<ArchetypesDat>>? byUnknown64;
    private Dictionary<float, List<ArchetypesDat>>? byUnknown68;
    private Dictionary<float, List<ArchetypesDat>>? byUnknown72;
    private Dictionary<string, List<ArchetypesDat>>? byBackgroundImageFile;
    private Dictionary<bool, List<ArchetypesDat>>? byIsTemporary;
    private Dictionary<bool, List<ArchetypesDat>>? byUnknown85;
    private Dictionary<string, List<ArchetypesDat>>? byArchetypeImage;
    private Dictionary<bool, List<ArchetypesDat>>? byUnknown94;
    private Dictionary<bool, List<ArchetypesDat>>? byUnknown95;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchetypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ArchetypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ArchetypesDat? item)
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
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
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchetypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchetypesDat>>();
        }

        var items = new List<ResultItem<string, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharactersKey(int? key, out ArchetypesDat? item)
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharactersKey(int? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byCharactersKey is null)
        {
            byCharactersKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.CharactersKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCharactersKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCharactersKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCharactersKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byCharactersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArchetypesDat>> GetManyToManyByCharactersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArchetypesDat>>();
        }

        var items = new List<ResultItem<int, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharactersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.PassiveSkillTreeURL"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveSkillTreeURL(string? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveSkillTreeURL(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.PassiveSkillTreeURL"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveSkillTreeURL(string? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byPassiveSkillTreeURL is null)
        {
            byPassiveSkillTreeURL = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveSkillTreeURL;

                if (!byPassiveSkillTreeURL.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPassiveSkillTreeURL.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPassiveSkillTreeURL.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byPassiveSkillTreeURL"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchetypesDat>> GetManyToManyByPassiveSkillTreeURL(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchetypesDat>>();
        }

        var items = new List<ResultItem<string, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveSkillTreeURL(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.AscendancyClassName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAscendancyClassName(string? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAscendancyClassName(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.AscendancyClassName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAscendancyClassName(string? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byAscendancyClassName is null)
        {
            byAscendancyClassName = new();
            foreach (var item in Items)
            {
                var itemKey = item.AscendancyClassName;

                if (!byAscendancyClassName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAscendancyClassName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAscendancyClassName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byAscendancyClassName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchetypesDat>> GetManyToManyByAscendancyClassName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchetypesDat>>();
        }

        var items = new List<ResultItem<string, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAscendancyClassName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out ArchetypesDat? item)
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
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
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchetypesDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchetypesDat>>();
        }

        var items = new List<ResultItem<string, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.UIImageFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUIImageFile(string? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUIImageFile(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.UIImageFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUIImageFile(string? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byUIImageFile is null)
        {
            byUIImageFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.UIImageFile;

                if (!byUIImageFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUIImageFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUIImageFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byUIImageFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchetypesDat>> GetManyToManyByUIImageFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchetypesDat>>();
        }

        var items = new List<ResultItem<string, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUIImageFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.TutorialVideo_BKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTutorialVideo_BKFile(string? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTutorialVideo_BKFile(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.TutorialVideo_BKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTutorialVideo_BKFile(string? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byTutorialVideo_BKFile is null)
        {
            byTutorialVideo_BKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.TutorialVideo_BKFile;

                if (!byTutorialVideo_BKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTutorialVideo_BKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTutorialVideo_BKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byTutorialVideo_BKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchetypesDat>> GetManyToManyByTutorialVideo_BKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchetypesDat>>();
        }

        var items = new List<ResultItem<string, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTutorialVideo_BKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out ArchetypesDat? item)
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
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
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArchetypesDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArchetypesDat>>();
        }

        var items = new List<ResultItem<int, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(float? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown68(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(float? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;

                if (!byUnknown68.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, ArchetypesDat>> GetManyToManyByUnknown68(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, ArchetypesDat>>();
        }

        var items = new List<ResultItem<float, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(float? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(float? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, ArchetypesDat>> GetManyToManyByUnknown72(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, ArchetypesDat>>();
        }

        var items = new List<ResultItem<float, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.BackgroundImageFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBackgroundImageFile(string? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBackgroundImageFile(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.BackgroundImageFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBackgroundImageFile(string? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byBackgroundImageFile is null)
        {
            byBackgroundImageFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.BackgroundImageFile;

                if (!byBackgroundImageFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBackgroundImageFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBackgroundImageFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byBackgroundImageFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchetypesDat>> GetManyToManyByBackgroundImageFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchetypesDat>>();
        }

        var items = new List<ResultItem<string, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBackgroundImageFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.IsTemporary"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsTemporary(bool? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsTemporary(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.IsTemporary"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsTemporary(bool? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byIsTemporary is null)
        {
            byIsTemporary = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsTemporary;

                if (!byIsTemporary.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsTemporary.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsTemporary.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byIsTemporary"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ArchetypesDat>> GetManyToManyByIsTemporary(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ArchetypesDat>>();
        }

        var items = new List<ResultItem<bool, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsTemporary(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown85"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown85(bool? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown85(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown85"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown85(bool? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byUnknown85 is null)
        {
            byUnknown85 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown85;

                if (!byUnknown85.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown85.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown85.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byUnknown85"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ArchetypesDat>> GetManyToManyByUnknown85(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ArchetypesDat>>();
        }

        var items = new List<ResultItem<bool, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown85(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.ArchetypeImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArchetypeImage(string? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArchetypeImage(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.ArchetypeImage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArchetypeImage(string? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byArchetypeImage is null)
        {
            byArchetypeImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.ArchetypeImage;

                if (!byArchetypeImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArchetypeImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArchetypeImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byArchetypeImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchetypesDat>> GetManyToManyByArchetypeImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchetypesDat>>();
        }

        var items = new List<ResultItem<string, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArchetypeImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown94"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown94(bool? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown94(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown94"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown94(bool? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byUnknown94 is null)
        {
            byUnknown94 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown94;

                if (!byUnknown94.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown94.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown94.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byUnknown94"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ArchetypesDat>> GetManyToManyByUnknown94(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ArchetypesDat>>();
        }

        var items = new List<ResultItem<bool, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown94(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown95"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown95(bool? key, out ArchetypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown95(key, out var items))
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
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.Unknown95"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown95(bool? key, out IReadOnlyList<ArchetypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        if (byUnknown95 is null)
        {
            byUnknown95 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown95;

                if (!byUnknown95.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown95.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown95.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchetypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchetypesDat"/> with <see cref="ArchetypesDat.byUnknown95"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ArchetypesDat>> GetManyToManyByUnknown95(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ArchetypesDat>>();
        }

        var items = new List<ResultItem<bool, ArchetypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown95(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ArchetypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ArchetypesDat[] Load()
    {
        const string filePath = "Data/Archetypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArchetypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKey
            (var characterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PassiveSkillTreeURL
            (var passiveskilltreeurlLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AscendancyClassName
            (var ascendancyclassnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading UIImageFile
            (var uiimagefileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TutorialVideo_BKFile
            (var tutorialvideo_bkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading BackgroundImageFile
            (var backgroundimagefileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsTemporary
            (var istemporaryLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown85
            (var unknown85Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ArchetypeImage
            (var archetypeimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown94
            (var unknown94Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown95
            (var unknown95Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArchetypesDat()
            {
                Id = idLoading,
                CharactersKey = characterskeyLoading,
                PassiveSkillTreeURL = passiveskilltreeurlLoading,
                AscendancyClassName = ascendancyclassnameLoading,
                Description = descriptionLoading,
                UIImageFile = uiimagefileLoading,
                TutorialVideo_BKFile = tutorialvideo_bkfileLoading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                BackgroundImageFile = backgroundimagefileLoading,
                IsTemporary = istemporaryLoading,
                Unknown85 = unknown85Loading,
                ArchetypeImage = archetypeimageLoading,
                Unknown94 = unknown94Loading,
                Unknown95 = unknown95Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
