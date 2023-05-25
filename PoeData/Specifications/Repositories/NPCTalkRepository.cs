using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="NPCTalkDat"/> related data and helper methods.
/// </summary>
public sealed class NPCTalkRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<NPCTalkDat> Items { get; }

    private Dictionary<int, List<NPCTalkDat>>? byNPCKey;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown16;
    private Dictionary<string, List<NPCTalkDat>>? byDialogueOption;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown28;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown44;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown60;
    private Dictionary<string, List<NPCTalkDat>>? byScript;
    private Dictionary<int, List<NPCTalkDat>>? byTextAudio;
    private Dictionary<int, List<NPCTalkDat>>? byCategory;
    private Dictionary<int, List<NPCTalkDat>>? byQuestRewardOffersKey;
    private Dictionary<int, List<NPCTalkDat>>? byQuestFlag;
    private Dictionary<int, List<NPCTalkDat>>? byNPCTextAudioKeys;
    private Dictionary<string, List<NPCTalkDat>>? byScript2;
    private Dictionary<bool, List<NPCTalkDat>>? byUnknown172;
    private Dictionary<bool, List<NPCTalkDat>>? byUnknown173;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown174;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown190;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown206;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown210;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown226;
    private Dictionary<bool, List<NPCTalkDat>>? byUnknown230;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown231;
    private Dictionary<bool, List<NPCTalkDat>>? byUnknown247;
    private Dictionary<bool, List<NPCTalkDat>>? byUnknown248;
    private Dictionary<string, List<NPCTalkDat>>? byDialogueOption2;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown257;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown273;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown289;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown293;
    private Dictionary<int, List<NPCTalkDat>>? byUnknown309;

    /// <summary>
    /// Initializes a new instance of the <see cref="NPCTalkRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal NPCTalkRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.NPCKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCKey(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCKey(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.NPCKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCKey(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byNPCKey is null)
        {
            byNPCKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byNPCKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByNPCKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out NPCTalkDat? item)
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
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
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.DialogueOption"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDialogueOption(string? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDialogueOption(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.DialogueOption"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDialogueOption(string? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byDialogueOption is null)
        {
            byDialogueOption = new();
            foreach (var item in Items)
            {
                var itemKey = item.DialogueOption;

                if (!byDialogueOption.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDialogueOption.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDialogueOption.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byDialogueOption"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCTalkDat>> GetManyToManyByDialogueOption(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCTalkDat>>();
        }

        var items = new List<ResultItem<string, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDialogueOption(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out NPCTalkDat? item)
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown28.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown28.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown44(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown44 is null)
        {
            byUnknown44 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown44;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown44.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown44.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown44.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown60(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown60 is null)
        {
            byUnknown60 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown60;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown60.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown60.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown60.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown60(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Script"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScript(string? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScript(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Script"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScript(string? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byScript is null)
        {
            byScript = new();
            foreach (var item in Items)
            {
                var itemKey = item.Script;

                if (!byScript.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScript.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScript.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byScript"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCTalkDat>> GetManyToManyByScript(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCTalkDat>>();
        }

        var items = new List<ResultItem<string, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScript(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudio(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTextAudio(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudio(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byTextAudio is null)
        {
            byTextAudio = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextAudio;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTextAudio.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTextAudio.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTextAudio.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byTextAudio"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByTextAudio(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudio(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Category"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCategory(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCategory(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Category"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCategory(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byCategory is null)
        {
            byCategory = new();
            foreach (var item in Items)
            {
                var itemKey = item.Category;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCategory.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCategory.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCategory.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byCategory"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByCategory(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCategory(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.QuestRewardOffersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestRewardOffersKey(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestRewardOffersKey(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.QuestRewardOffersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestRewardOffersKey(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byQuestRewardOffersKey is null)
        {
            byQuestRewardOffersKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestRewardOffersKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestRewardOffersKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestRewardOffersKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestRewardOffersKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byQuestRewardOffersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByQuestRewardOffersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestRewardOffersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestFlag(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestFlag(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestFlag(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byQuestFlag is null)
        {
            byQuestFlag = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestFlag;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestFlag.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestFlag.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestFlag.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byQuestFlag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByQuestFlag(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestFlag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.NPCTextAudioKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCTextAudioKeys(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCTextAudioKeys(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.NPCTextAudioKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCTextAudioKeys(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byNPCTextAudioKeys is null)
        {
            byNPCTextAudioKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCTextAudioKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byNPCTextAudioKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNPCTextAudioKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNPCTextAudioKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byNPCTextAudioKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByNPCTextAudioKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCTextAudioKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Script2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScript2(string? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScript2(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Script2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScript2(string? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byScript2 is null)
        {
            byScript2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Script2;

                if (!byScript2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScript2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScript2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byScript2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCTalkDat>> GetManyToManyByScript2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCTalkDat>>();
        }

        var items = new List<ResultItem<string, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScript2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown172(bool? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown172(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown172(bool? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown172 is null)
        {
            byUnknown172 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown172;

                if (!byUnknown172.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown172.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown172.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown172"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCTalkDat>> GetManyToManyByUnknown172(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCTalkDat>>();
        }

        var items = new List<ResultItem<bool, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown172(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown173"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown173(bool? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown173(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown173"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown173(bool? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown173 is null)
        {
            byUnknown173 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown173;

                if (!byUnknown173.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown173.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown173.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown173"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCTalkDat>> GetManyToManyByUnknown173(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCTalkDat>>();
        }

        var items = new List<ResultItem<bool, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown173(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown174"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown174(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown174(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown174"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown174(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown174 is null)
        {
            byUnknown174 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown174;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown174.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown174.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown174.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown174"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown174(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown174(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown190"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown190(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown190(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown190"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown190(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown190 is null)
        {
            byUnknown190 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown190;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown190.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown190.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown190.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown190"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown190(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown190(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown206"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown206(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown206(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown206"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown206(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown206 is null)
        {
            byUnknown206 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown206;

                if (!byUnknown206.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown206.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown206.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown206"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown206(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown206(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown210"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown210(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown210(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown210"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown210(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown210 is null)
        {
            byUnknown210 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown210;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown210.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown210.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown210.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown210"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown210(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown210(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown226"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown226(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown226(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown226"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown226(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown226 is null)
        {
            byUnknown226 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown226;

                if (!byUnknown226.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown226.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown226.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown226"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown226(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown226(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown230"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown230(bool? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown230(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown230"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown230(bool? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown230 is null)
        {
            byUnknown230 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown230;

                if (!byUnknown230.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown230.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown230.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown230"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCTalkDat>> GetManyToManyByUnknown230(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCTalkDat>>();
        }

        var items = new List<ResultItem<bool, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown230(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown231"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown231(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown231(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown231"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown231(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown231 is null)
        {
            byUnknown231 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown231;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown231.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown231.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown231.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown231"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown231(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown231(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown247"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown247(bool? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown247(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown247"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown247(bool? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown247 is null)
        {
            byUnknown247 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown247;

                if (!byUnknown247.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown247.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown247.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown247"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCTalkDat>> GetManyToManyByUnknown247(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCTalkDat>>();
        }

        var items = new List<ResultItem<bool, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown247(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown248"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown248(bool? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown248(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown248"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown248(bool? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown248 is null)
        {
            byUnknown248 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown248;

                if (!byUnknown248.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown248.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown248.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown248"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCTalkDat>> GetManyToManyByUnknown248(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCTalkDat>>();
        }

        var items = new List<ResultItem<bool, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown248(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.DialogueOption2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDialogueOption2(string? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDialogueOption2(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.DialogueOption2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDialogueOption2(string? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byDialogueOption2 is null)
        {
            byDialogueOption2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.DialogueOption2;

                if (!byDialogueOption2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDialogueOption2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDialogueOption2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byDialogueOption2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCTalkDat>> GetManyToManyByDialogueOption2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCTalkDat>>();
        }

        var items = new List<ResultItem<string, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDialogueOption2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown257"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown257(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown257(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown257"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown257(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown257 is null)
        {
            byUnknown257 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown257;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown257.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown257.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown257.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown257"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown257(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown257(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown273"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown273(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown273(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown273"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown273(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown273 is null)
        {
            byUnknown273 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown273;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown273.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown273.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown273.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown273"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown273(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown273(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown289"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown289(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown289(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown289"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown289(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown289 is null)
        {
            byUnknown289 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown289;

                if (!byUnknown289.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown289.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown289.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown289"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown289(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown289(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown293"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown293(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown293(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown293"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown293(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown293 is null)
        {
            byUnknown293 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown293;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown293.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown293.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown293.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown293"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown293(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown293(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown309"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown309(int? key, out NPCTalkDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown309(key, out var items))
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
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.Unknown309"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown309(int? key, out IReadOnlyList<NPCTalkDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        if (byUnknown309 is null)
        {
            byUnknown309 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown309;

                if (!byUnknown309.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown309.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown309.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTalkDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTalkDat"/> with <see cref="NPCTalkDat.byUnknown309"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTalkDat>> GetManyToManyByUnknown309(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTalkDat>>();
        }

        var items = new List<ResultItem<int, NPCTalkDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown309(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTalkDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private NPCTalkDat[] Load()
    {
        const string filePath = "Data/NPCTalk.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCTalkDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading NPCKey
            (var npckeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DialogueOption
            (var dialogueoptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown28
            (var tempunknown28Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown28Loading = tempunknown28Loading.AsReadOnly();

            // loading Unknown44
            (var tempunknown44Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown44Loading = tempunknown44Loading.AsReadOnly();

            // loading Unknown60
            (var tempunknown60Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown60Loading = tempunknown60Loading.AsReadOnly();

            // loading Script
            (var scriptLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Category
            (var categoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading QuestRewardOffersKey
            (var questrewardofferskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading NPCTextAudioKeys
            (var tempnpctextaudiokeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npctextaudiokeysLoading = tempnpctextaudiokeysLoading.AsReadOnly();

            // loading Script2
            (var script2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown172
            (var unknown172Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown173
            (var unknown173Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown174
            (var tempunknown174Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown174Loading = tempunknown174Loading.AsReadOnly();

            // loading Unknown190
            (var tempunknown190Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown190Loading = tempunknown190Loading.AsReadOnly();

            // loading Unknown206
            (var unknown206Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown210
            (var tempunknown210Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown210Loading = tempunknown210Loading.AsReadOnly();

            // loading Unknown226
            (var unknown226Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown230
            (var unknown230Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown231
            (var unknown231Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown247
            (var unknown247Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown248
            (var unknown248Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DialogueOption2
            (var dialogueoption2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown257
            (var unknown257Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown273
            (var unknown273Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown289
            (var unknown289Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown293
            (var tempunknown293Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown293Loading = tempunknown293Loading.AsReadOnly();

            // loading Unknown309
            (var unknown309Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCTalkDat()
            {
                NPCKey = npckeyLoading,
                Unknown16 = unknown16Loading,
                DialogueOption = dialogueoptionLoading,
                Unknown28 = unknown28Loading,
                Unknown44 = unknown44Loading,
                Unknown60 = unknown60Loading,
                Script = scriptLoading,
                TextAudio = textaudioLoading,
                Category = categoryLoading,
                QuestRewardOffersKey = questrewardofferskeyLoading,
                QuestFlag = questflagLoading,
                NPCTextAudioKeys = npctextaudiokeysLoading,
                Script2 = script2Loading,
                Unknown172 = unknown172Loading,
                Unknown173 = unknown173Loading,
                Unknown174 = unknown174Loading,
                Unknown190 = unknown190Loading,
                Unknown206 = unknown206Loading,
                Unknown210 = unknown210Loading,
                Unknown226 = unknown226Loading,
                Unknown230 = unknown230Loading,
                Unknown231 = unknown231Loading,
                Unknown247 = unknown247Loading,
                Unknown248 = unknown248Loading,
                DialogueOption2 = dialogueoption2Loading,
                Unknown257 = unknown257Loading,
                Unknown273 = unknown273Loading,
                Unknown289 = unknown289Loading,
                Unknown293 = unknown293Loading,
                Unknown309 = unknown309Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
