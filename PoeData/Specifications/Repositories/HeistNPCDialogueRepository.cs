using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistNPCDialogueDat"/> related data and helper methods.
/// </summary>
public sealed class HeistNPCDialogueRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistNPCDialogueDat> Items { get; }

    private Dictionary<int, List<HeistNPCDialogueDat>>? byDialogueEventKey;
    private Dictionary<int, List<HeistNPCDialogueDat>>? byHeistNPCsKey;
    private Dictionary<int, List<HeistNPCDialogueDat>>? byAudioNormal;
    private Dictionary<int, List<HeistNPCDialogueDat>>? byAudioLoud;
    private Dictionary<int, List<HeistNPCDialogueDat>>? byUnknown64;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistNPCDialogueRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistNPCDialogueRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.DialogueEventKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDialogueEventKey(int? key, out HeistNPCDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDialogueEventKey(key, out var items))
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
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.DialogueEventKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDialogueEventKey(int? key, out IReadOnlyList<HeistNPCDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCDialogueDat>();
            return false;
        }

        if (byDialogueEventKey is null)
        {
            byDialogueEventKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.DialogueEventKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDialogueEventKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDialogueEventKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDialogueEventKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.byDialogueEventKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCDialogueDat>> GetManyToManyByDialogueEventKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCDialogueDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDialogueEventKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.HeistNPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistNPCsKey(int? key, out HeistNPCDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistNPCsKey(key, out var items))
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
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.HeistNPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistNPCsKey(int? key, out IReadOnlyList<HeistNPCDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCDialogueDat>();
            return false;
        }

        if (byHeistNPCsKey is null)
        {
            byHeistNPCsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistNPCsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistNPCsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistNPCsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistNPCsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.byHeistNPCsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCDialogueDat>> GetManyToManyByHeistNPCsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCDialogueDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistNPCsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.AudioNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAudioNormal(int? key, out HeistNPCDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAudioNormal(key, out var items))
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
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.AudioNormal"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAudioNormal(int? key, out IReadOnlyList<HeistNPCDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCDialogueDat>();
            return false;
        }

        if (byAudioNormal is null)
        {
            byAudioNormal = new();
            foreach (var item in Items)
            {
                var itemKey = item.AudioNormal;
                foreach (var listKey in itemKey)
                {
                    if (!byAudioNormal.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAudioNormal.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAudioNormal.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.byAudioNormal"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCDialogueDat>> GetManyToManyByAudioNormal(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCDialogueDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAudioNormal(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.AudioLoud"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAudioLoud(int? key, out HeistNPCDialogueDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAudioLoud(key, out var items))
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
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.AudioLoud"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAudioLoud(int? key, out IReadOnlyList<HeistNPCDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCDialogueDat>();
            return false;
        }

        if (byAudioLoud is null)
        {
            byAudioLoud = new();
            foreach (var item in Items)
            {
                var itemKey = item.AudioLoud;
                foreach (var listKey in itemKey)
                {
                    if (!byAudioLoud.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAudioLoud.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAudioLoud.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.byAudioLoud"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCDialogueDat>> GetManyToManyByAudioLoud(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCDialogueDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAudioLoud(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out HeistNPCDialogueDat? item)
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
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<HeistNPCDialogueDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCDialogueDat>();
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
            items = Array.Empty<HeistNPCDialogueDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCDialogueDat"/> with <see cref="HeistNPCDialogueDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCDialogueDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCDialogueDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCDialogueDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCDialogueDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistNPCDialogueDat[] Load()
    {
        const string filePath = "Data/HeistNPCDialogue.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistNPCDialogueDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading DialogueEventKey
            (var dialogueeventkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistNPCsKey
            (var heistnpcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AudioNormal
            (var tempaudionormalLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var audionormalLoading = tempaudionormalLoading.AsReadOnly();

            // loading AudioLoud
            (var tempaudioloudLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var audioloudLoading = tempaudioloudLoading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistNPCDialogueDat()
            {
                DialogueEventKey = dialogueeventkeyLoading,
                HeistNPCsKey = heistnpcskeyLoading,
                AudioNormal = audionormalLoading,
                AudioLoud = audioloudLoading,
                Unknown64 = unknown64Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
