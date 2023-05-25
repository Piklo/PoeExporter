using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CharacterPanelDescriptionModesDat"/> related data and helper methods.
/// </summary>
public sealed class CharacterPanelDescriptionModesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CharacterPanelDescriptionModesDat> Items { get; }

    private Dictionary<string, List<CharacterPanelDescriptionModesDat>>? byId;
    private Dictionary<string, List<CharacterPanelDescriptionModesDat>>? byUnknown8;
    private Dictionary<string, List<CharacterPanelDescriptionModesDat>>? byFormatString_Positive;
    private Dictionary<string, List<CharacterPanelDescriptionModesDat>>? byFormatString_Negative;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterPanelDescriptionModesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CharacterPanelDescriptionModesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CharacterPanelDescriptionModesDat? item)
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
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CharacterPanelDescriptionModesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelDescriptionModesDat>();
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
            items = Array.Empty<CharacterPanelDescriptionModesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterPanelDescriptionModesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterPanelDescriptionModesDat>>();
        }

        var items = new List<ResultItem<string, CharacterPanelDescriptionModesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterPanelDescriptionModesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(string? key, out CharacterPanelDescriptionModesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(string? key, out IReadOnlyList<CharacterPanelDescriptionModesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelDescriptionModesDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;

                if (!byUnknown8.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharacterPanelDescriptionModesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterPanelDescriptionModesDat>> GetManyToManyByUnknown8(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterPanelDescriptionModesDat>>();
        }

        var items = new List<ResultItem<string, CharacterPanelDescriptionModesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterPanelDescriptionModesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.FormatString_Positive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFormatString_Positive(string? key, out CharacterPanelDescriptionModesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFormatString_Positive(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.FormatString_Positive"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFormatString_Positive(string? key, out IReadOnlyList<CharacterPanelDescriptionModesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelDescriptionModesDat>();
            return false;
        }

        if (byFormatString_Positive is null)
        {
            byFormatString_Positive = new();
            foreach (var item in Items)
            {
                var itemKey = item.FormatString_Positive;

                if (!byFormatString_Positive.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFormatString_Positive.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFormatString_Positive.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharacterPanelDescriptionModesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.byFormatString_Positive"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterPanelDescriptionModesDat>> GetManyToManyByFormatString_Positive(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterPanelDescriptionModesDat>>();
        }

        var items = new List<ResultItem<string, CharacterPanelDescriptionModesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFormatString_Positive(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterPanelDescriptionModesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.FormatString_Negative"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFormatString_Negative(string? key, out CharacterPanelDescriptionModesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFormatString_Negative(key, out var items))
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
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.FormatString_Negative"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFormatString_Negative(string? key, out IReadOnlyList<CharacterPanelDescriptionModesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelDescriptionModesDat>();
            return false;
        }

        if (byFormatString_Negative is null)
        {
            byFormatString_Negative = new();
            foreach (var item in Items)
            {
                var itemKey = item.FormatString_Negative;

                if (!byFormatString_Negative.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFormatString_Negative.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFormatString_Negative.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CharacterPanelDescriptionModesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelDescriptionModesDat"/> with <see cref="CharacterPanelDescriptionModesDat.byFormatString_Negative"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterPanelDescriptionModesDat>> GetManyToManyByFormatString_Negative(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterPanelDescriptionModesDat>>();
        }

        var items = new List<ResultItem<string, CharacterPanelDescriptionModesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFormatString_Negative(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterPanelDescriptionModesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CharacterPanelDescriptionModesDat[] Load()
    {
        const string filePath = "Data/CharacterPanelDescriptionModes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterPanelDescriptionModesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FormatString_Positive
            (var formatstring_positiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FormatString_Negative
            (var formatstring_negativeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterPanelDescriptionModesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                FormatString_Positive = formatstring_positiveLoading,
                FormatString_Negative = formatstring_negativeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
