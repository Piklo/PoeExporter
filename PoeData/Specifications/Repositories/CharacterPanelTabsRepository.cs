using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CharacterPanelTabsDat"/> related data and helper methods.
/// </summary>
public sealed class CharacterPanelTabsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CharacterPanelTabsDat> Items { get; }

    private Dictionary<string, List<CharacterPanelTabsDat>>? byId;
    private Dictionary<int, List<CharacterPanelTabsDat>>? byUnknown8;
    private Dictionary<string, List<CharacterPanelTabsDat>>? byText;
    private Dictionary<int, List<CharacterPanelTabsDat>>? byUnknown20;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterPanelTabsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CharacterPanelTabsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CharacterPanelTabsDat? item)
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
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CharacterPanelTabsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelTabsDat>();
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
            items = Array.Empty<CharacterPanelTabsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterPanelTabsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterPanelTabsDat>>();
        }

        var items = new List<ResultItem<string, CharacterPanelTabsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterPanelTabsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out CharacterPanelTabsDat? item)
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
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<CharacterPanelTabsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelTabsDat>();
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

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CharacterPanelTabsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterPanelTabsDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterPanelTabsDat>>();
        }

        var items = new List<ResultItem<int, CharacterPanelTabsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterPanelTabsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out CharacterPanelTabsDat? item)
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
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<CharacterPanelTabsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelTabsDat>();
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
            items = Array.Empty<CharacterPanelTabsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CharacterPanelTabsDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CharacterPanelTabsDat>>();
        }

        var items = new List<ResultItem<string, CharacterPanelTabsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CharacterPanelTabsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out CharacterPanelTabsDat? item)
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
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<CharacterPanelTabsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CharacterPanelTabsDat>();
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
            items = Array.Empty<CharacterPanelTabsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CharacterPanelTabsDat"/> with <see cref="CharacterPanelTabsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CharacterPanelTabsDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CharacterPanelTabsDat>>();
        }

        var items = new List<ResultItem<int, CharacterPanelTabsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CharacterPanelTabsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CharacterPanelTabsDat[] Load()
    {
        const string filePath = "Data/CharacterPanelTabs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharacterPanelTabsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharacterPanelTabsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Text = textLoading,
                Unknown20 = unknown20Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
