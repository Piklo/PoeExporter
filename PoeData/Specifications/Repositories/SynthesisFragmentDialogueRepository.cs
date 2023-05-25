using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SynthesisFragmentDialogueDat"/> related data and helper methods.
/// </summary>
public sealed class SynthesisFragmentDialogueRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SynthesisFragmentDialogueDat> Items { get; }

    private Dictionary<int, List<SynthesisFragmentDialogueDat>>? byUnknown0;
    private Dictionary<int, List<SynthesisFragmentDialogueDat>>? byNPCTextAudioKey1;
    private Dictionary<int, List<SynthesisFragmentDialogueDat>>? byNPCTextAudioKey2;
    private Dictionary<int, List<SynthesisFragmentDialogueDat>>? byNPCTextAudioKey3;
    private Dictionary<int, List<SynthesisFragmentDialogueDat>>? byNPCTextAudioKey4;
    private Dictionary<int, List<SynthesisFragmentDialogueDat>>? byNPCTextAudioKey5;
    private Dictionary<int, List<SynthesisFragmentDialogueDat>>? byNPCTextAudioKey6;

    /// <summary>
    /// Initializes a new instance of the <see cref="SynthesisFragmentDialogueRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SynthesisFragmentDialogueRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out SynthesisFragmentDialogueDat? item)
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
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<SynthesisFragmentDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SynthesisFragmentDialogueDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SynthesisFragmentDialogueDat>>();
        }

        var items = new List<ResultItem<int, SynthesisFragmentDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SynthesisFragmentDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCTextAudioKey1(int? key, out SynthesisFragmentDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCTextAudioKey1(key, out var items))
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
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCTextAudioKey1(int? key, out IReadOnlyList<SynthesisFragmentDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        if (byNPCTextAudioKey1 is null)
        {
            byNPCTextAudioKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCTextAudioKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCTextAudioKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCTextAudioKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCTextAudioKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.byNPCTextAudioKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SynthesisFragmentDialogueDat>> GetManyToManyByNPCTextAudioKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SynthesisFragmentDialogueDat>>();
        }

        var items = new List<ResultItem<int, SynthesisFragmentDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCTextAudioKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SynthesisFragmentDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCTextAudioKey2(int? key, out SynthesisFragmentDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCTextAudioKey2(key, out var items))
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
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCTextAudioKey2(int? key, out IReadOnlyList<SynthesisFragmentDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        if (byNPCTextAudioKey2 is null)
        {
            byNPCTextAudioKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCTextAudioKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCTextAudioKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCTextAudioKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCTextAudioKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.byNPCTextAudioKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SynthesisFragmentDialogueDat>> GetManyToManyByNPCTextAudioKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SynthesisFragmentDialogueDat>>();
        }

        var items = new List<ResultItem<int, SynthesisFragmentDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCTextAudioKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SynthesisFragmentDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCTextAudioKey3(int? key, out SynthesisFragmentDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCTextAudioKey3(key, out var items))
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
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCTextAudioKey3(int? key, out IReadOnlyList<SynthesisFragmentDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        if (byNPCTextAudioKey3 is null)
        {
            byNPCTextAudioKey3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCTextAudioKey3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCTextAudioKey3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCTextAudioKey3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCTextAudioKey3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.byNPCTextAudioKey3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SynthesisFragmentDialogueDat>> GetManyToManyByNPCTextAudioKey3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SynthesisFragmentDialogueDat>>();
        }

        var items = new List<ResultItem<int, SynthesisFragmentDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCTextAudioKey3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SynthesisFragmentDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCTextAudioKey4(int? key, out SynthesisFragmentDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCTextAudioKey4(key, out var items))
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
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCTextAudioKey4(int? key, out IReadOnlyList<SynthesisFragmentDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        if (byNPCTextAudioKey4 is null)
        {
            byNPCTextAudioKey4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCTextAudioKey4;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCTextAudioKey4.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCTextAudioKey4.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCTextAudioKey4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.byNPCTextAudioKey4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SynthesisFragmentDialogueDat>> GetManyToManyByNPCTextAudioKey4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SynthesisFragmentDialogueDat>>();
        }

        var items = new List<ResultItem<int, SynthesisFragmentDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCTextAudioKey4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SynthesisFragmentDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCTextAudioKey5(int? key, out SynthesisFragmentDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCTextAudioKey5(key, out var items))
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
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCTextAudioKey5(int? key, out IReadOnlyList<SynthesisFragmentDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        if (byNPCTextAudioKey5 is null)
        {
            byNPCTextAudioKey5 = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCTextAudioKey5;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCTextAudioKey5.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCTextAudioKey5.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCTextAudioKey5.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.byNPCTextAudioKey5"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SynthesisFragmentDialogueDat>> GetManyToManyByNPCTextAudioKey5(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SynthesisFragmentDialogueDat>>();
        }

        var items = new List<ResultItem<int, SynthesisFragmentDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCTextAudioKey5(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SynthesisFragmentDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCTextAudioKey6(int? key, out SynthesisFragmentDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCTextAudioKey6(key, out var items))
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
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.NPCTextAudioKey6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCTextAudioKey6(int? key, out IReadOnlyList<SynthesisFragmentDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        if (byNPCTextAudioKey6 is null)
        {
            byNPCTextAudioKey6 = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCTextAudioKey6;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCTextAudioKey6.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCTextAudioKey6.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCTextAudioKey6.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SynthesisFragmentDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SynthesisFragmentDialogueDat"/> with <see cref="SynthesisFragmentDialogueDat.byNPCTextAudioKey6"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SynthesisFragmentDialogueDat>> GetManyToManyByNPCTextAudioKey6(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SynthesisFragmentDialogueDat>>();
        }

        var items = new List<ResultItem<int, SynthesisFragmentDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCTextAudioKey6(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SynthesisFragmentDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SynthesisFragmentDialogueDat[] Load()
    {
        const string filePath = "Data/SynthesisFragmentDialogue.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SynthesisFragmentDialogueDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading NPCTextAudioKey1
            (var npctextaudiokey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading NPCTextAudioKey2
            (var npctextaudiokey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading NPCTextAudioKey3
            (var npctextaudiokey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading NPCTextAudioKey4
            (var npctextaudiokey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading NPCTextAudioKey5
            (var npctextaudiokey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading NPCTextAudioKey6
            (var npctextaudiokey6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SynthesisFragmentDialogueDat()
            {
                Unknown0 = unknown0Loading,
                NPCTextAudioKey1 = npctextaudiokey1Loading,
                NPCTextAudioKey2 = npctextaudiokey2Loading,
                NPCTextAudioKey3 = npctextaudiokey3Loading,
                NPCTextAudioKey4 = npctextaudiokey4Loading,
                NPCTextAudioKey5 = npctextaudiokey5Loading,
                NPCTextAudioKey6 = npctextaudiokey6Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
