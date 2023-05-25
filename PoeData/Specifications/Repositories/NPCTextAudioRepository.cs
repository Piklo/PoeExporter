using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="NPCTextAudioDat"/> related data and helper methods.
/// </summary>
public sealed class NPCTextAudioRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<NPCTextAudioDat> Items { get; }

    private Dictionary<string, List<NPCTextAudioDat>>? byId;
    private Dictionary<int, List<NPCTextAudioDat>>? byCharactersKey;
    private Dictionary<string, List<NPCTextAudioDat>>? byText;
    private Dictionary<string, List<NPCTextAudioDat>>? byMono_AudioFile;
    private Dictionary<string, List<NPCTextAudioDat>>? byStereo_AudioFile;
    private Dictionary<bool, List<NPCTextAudioDat>>? byHasStereo;
    private Dictionary<bool, List<NPCTextAudioDat>>? byUnknown49;
    private Dictionary<string, List<NPCTextAudioDat>>? byVideo;
    private Dictionary<int, List<NPCTextAudioDat>>? byUnknown58;
    private Dictionary<int, List<NPCTextAudioDat>>? byUnknown62;
    private Dictionary<int, List<NPCTextAudioDat>>? byUnknown66;
    private Dictionary<int, List<NPCTextAudioDat>>? byUnknown70;
    private Dictionary<int, List<NPCTextAudioDat>>? byUnknown86;

    /// <summary>
    /// Initializes a new instance of the <see cref="NPCTextAudioRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal NPCTextAudioRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out NPCTextAudioDat? item)
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
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
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCTextAudioDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<string, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharactersKey(int? key, out NPCTextAudioDat? item)
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharactersKey(int? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
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
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byCharactersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTextAudioDat>> GetManyToManyByCharactersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<int, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharactersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out NPCTextAudioDat? item)
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
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
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCTextAudioDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<string, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Mono_AudioFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMono_AudioFile(string? key, out NPCTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMono_AudioFile(key, out var items))
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Mono_AudioFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMono_AudioFile(string? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        if (byMono_AudioFile is null)
        {
            byMono_AudioFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mono_AudioFile;

                if (!byMono_AudioFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMono_AudioFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMono_AudioFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byMono_AudioFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCTextAudioDat>> GetManyToManyByMono_AudioFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<string, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMono_AudioFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Stereo_AudioFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStereo_AudioFile(string? key, out NPCTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStereo_AudioFile(key, out var items))
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Stereo_AudioFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStereo_AudioFile(string? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        if (byStereo_AudioFile is null)
        {
            byStereo_AudioFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stereo_AudioFile;

                if (!byStereo_AudioFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStereo_AudioFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStereo_AudioFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byStereo_AudioFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCTextAudioDat>> GetManyToManyByStereo_AudioFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<string, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStereo_AudioFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.HasStereo"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHasStereo(bool? key, out NPCTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHasStereo(key, out var items))
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.HasStereo"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHasStereo(bool? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        if (byHasStereo is null)
        {
            byHasStereo = new();
            foreach (var item in Items)
            {
                var itemKey = item.HasStereo;

                if (!byHasStereo.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHasStereo.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHasStereo.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byHasStereo"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCTextAudioDat>> GetManyToManyByHasStereo(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<bool, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHasStereo(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown49(bool? key, out NPCTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown49(key, out var items))
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown49(bool? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        if (byUnknown49 is null)
        {
            byUnknown49 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown49;

                if (!byUnknown49.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown49.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown49.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byUnknown49"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCTextAudioDat>> GetManyToManyByUnknown49(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<bool, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown49(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Video"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVideo(string? key, out NPCTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVideo(key, out var items))
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Video"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVideo(string? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        if (byVideo is null)
        {
            byVideo = new();
            foreach (var item in Items)
            {
                var itemKey = item.Video;

                if (!byVideo.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byVideo.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byVideo.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byVideo"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCTextAudioDat>> GetManyToManyByVideo(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<string, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVideo(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown58"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown58(int? key, out NPCTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown58(key, out var items))
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown58"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown58(int? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        if (byUnknown58 is null)
        {
            byUnknown58 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown58;

                if (!byUnknown58.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown58.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown58.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byUnknown58"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTextAudioDat>> GetManyToManyByUnknown58(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<int, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown58(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown62(int? key, out NPCTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown62(key, out var items))
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown62(int? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        if (byUnknown62 is null)
        {
            byUnknown62 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown62;

                if (!byUnknown62.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown62.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown62.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byUnknown62"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTextAudioDat>> GetManyToManyByUnknown62(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<int, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown62(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown66(int? key, out NPCTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown66(key, out var items))
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown66(int? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        if (byUnknown66 is null)
        {
            byUnknown66 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown66;

                if (!byUnknown66.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown66.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown66.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byUnknown66"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTextAudioDat>> GetManyToManyByUnknown66(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<int, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown66(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown70(int? key, out NPCTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown70(key, out var items))
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown70(int? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        if (byUnknown70 is null)
        {
            byUnknown70 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown70;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown70.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown70.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown70.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byUnknown70"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTextAudioDat>> GetManyToManyByUnknown70(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<int, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown70(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown86"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown86(int? key, out NPCTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown86(key, out var items))
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
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.Unknown86"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown86(int? key, out IReadOnlyList<NPCTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        if (byUnknown86 is null)
        {
            byUnknown86 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown86;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown86.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown86.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown86.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCTextAudioDat"/> with <see cref="NPCTextAudioDat.byUnknown86"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCTextAudioDat>> GetManyToManyByUnknown86(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCTextAudioDat>>();
        }

        var items = new List<ResultItem<int, NPCTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown86(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private NPCTextAudioDat[] Load()
    {
        const string filePath = "Data/NPCTextAudio.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCTextAudioDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CharactersKey
            (var characterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Mono_AudioFile
            (var mono_audiofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Stereo_AudioFile
            (var stereo_audiofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HasStereo
            (var hasstereoLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Video
            (var videoLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown58
            (var unknown58Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown70
            (var tempunknown70Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown70Loading = tempunknown70Loading.AsReadOnly();

            // loading Unknown86
            (var unknown86Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCTextAudioDat()
            {
                Id = idLoading,
                CharactersKey = characterskeyLoading,
                Text = textLoading,
                Mono_AudioFile = mono_audiofileLoading,
                Stereo_AudioFile = stereo_audiofileLoading,
                HasStereo = hasstereoLoading,
                Unknown49 = unknown49Loading,
                Video = videoLoading,
                Unknown58 = unknown58Loading,
                Unknown62 = unknown62Loading,
                Unknown66 = unknown66Loading,
                Unknown70 = unknown70Loading,
                Unknown86 = unknown86Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
