using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CharacterEventTextAudioDat"/> related data and helper methods.
/// </summary>
public sealed class CharacterEventTextAudioRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CharacterEventTextAudioDat> Items { get; }

    private Dictionary<int, List<CharacterEventTextAudioDat>>? byEvent;
    private Dictionary<int, List<CharacterEventTextAudioDat>>? byCharacter;
    private Dictionary<int, List<CharacterEventTextAudioDat>>? byTextAudio;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterEventTextAudioRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CharacterEventTextAudioRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CharacterEventTextAudioDat"/> with <see cref="CharacterEventTextAudioDat.Event"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEvent(int? key, out CharacterEventTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEvent(key, out var items))
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
    /// Tries to get <see cref="CharacterEventTextAudioDat"/> with <see cref="CharacterEventTextAudioDat.Event"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEvent(int? key, out IReadOnlyList<CharacterEventTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterEventTextAudioDat>();
            return false;
        }

        if (byEvent is null)
        {
            byEvent = new();
            foreach (var item in Items)
            {
                var itemKey = item.Event;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEvent.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEvent.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEvent.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterEventTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterEventTextAudioDat"/> with <see cref="CharacterEventTextAudioDat.byEvent"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterEventTextAudioDat>> GetManyToManyByEvent(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterEventTextAudioDat>>();
        }

        var items = new List<ResultItem<int, CharacterEventTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEvent(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterEventTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterEventTextAudioDat"/> with <see cref="CharacterEventTextAudioDat.Character"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharacter(int? key, out CharacterEventTextAudioDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharacter(key, out var items))
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
    /// Tries to get <see cref="CharacterEventTextAudioDat"/> with <see cref="CharacterEventTextAudioDat.Character"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharacter(int? key, out IReadOnlyList<CharacterEventTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterEventTextAudioDat>();
            return false;
        }

        if (byCharacter is null)
        {
            byCharacter = new();
            foreach (var item in Items)
            {
                var itemKey = item.Character;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCharacter.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCharacter.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCharacter.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterEventTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterEventTextAudioDat"/> with <see cref="CharacterEventTextAudioDat.byCharacter"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterEventTextAudioDat>> GetManyToManyByCharacter(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterEventTextAudioDat>>();
        }

        var items = new List<ResultItem<int, CharacterEventTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharacter(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterEventTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterEventTextAudioDat"/> with <see cref="CharacterEventTextAudioDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextAudio(int? key, out CharacterEventTextAudioDat? item)
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
    /// Tries to get <see cref="CharacterEventTextAudioDat"/> with <see cref="CharacterEventTextAudioDat.TextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextAudio(int? key, out IReadOnlyList<CharacterEventTextAudioDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterEventTextAudioDat>();
            return false;
        }

        if (byTextAudio is null)
        {
            byTextAudio = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextAudio;
                foreach (var listKey in itemKey)
                {
                    if (!byTextAudio.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTextAudio.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTextAudio.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterEventTextAudioDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterEventTextAudioDat"/> with <see cref="CharacterEventTextAudioDat.byTextAudio"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterEventTextAudioDat>> GetManyToManyByTextAudio(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterEventTextAudioDat>>();
        }

        var items = new List<ResultItem<int, CharacterEventTextAudioDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextAudio(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterEventTextAudioDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CharacterEventTextAudioDat[] Load()
    {
        const string filePath = "Data/CharacterEventTextAudio.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterEventTextAudioDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Event
            (var eventLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Character
            (var characterLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TextAudio
            (var temptextaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var textaudioLoading = temptextaudioLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterEventTextAudioDat()
            {
                Event = eventLoading,
                Character = characterLoading,
                TextAudio = textaudioLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
