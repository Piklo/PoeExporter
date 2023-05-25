using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UITalkTextDat"/> related data and helper methods.
/// </summary>
public sealed class UITalkTextRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UITalkTextDat> Items { get; }

    private Dictionary<string, List<UITalkTextDat>>? byId;
    private Dictionary<int, List<UITalkTextDat>>? byUITalkCategoriesKey;
    private Dictionary<string, List<UITalkTextDat>>? byOGGFile;
    private Dictionary<string, List<UITalkTextDat>>? byText;
    private Dictionary<bool, List<UITalkTextDat>>? byUnknown28;
    private Dictionary<int, List<UITalkTextDat>>? byNPCTextAudio;

    /// <summary>
    /// Initializes a new instance of the <see cref="UITalkTextRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UITalkTextRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out UITalkTextDat? item)
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
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<UITalkTextDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UITalkTextDat>();
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
            items = Array.Empty<UITalkTextDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UITalkTextDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UITalkTextDat>>();
        }

        var items = new List<ResultItem<string, UITalkTextDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UITalkTextDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.UITalkCategoriesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUITalkCategoriesKey(int? key, out UITalkTextDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUITalkCategoriesKey(key, out var items))
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
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.UITalkCategoriesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUITalkCategoriesKey(int? key, out IReadOnlyList<UITalkTextDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UITalkTextDat>();
            return false;
        }

        if (byUITalkCategoriesKey is null)
        {
            byUITalkCategoriesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.UITalkCategoriesKey;

                if (!byUITalkCategoriesKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUITalkCategoriesKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUITalkCategoriesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UITalkTextDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.byUITalkCategoriesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UITalkTextDat>> GetManyToManyByUITalkCategoriesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UITalkTextDat>>();
        }

        var items = new List<ResultItem<int, UITalkTextDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUITalkCategoriesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UITalkTextDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.OGGFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOGGFile(string? key, out UITalkTextDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOGGFile(key, out var items))
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
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.OGGFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOGGFile(string? key, out IReadOnlyList<UITalkTextDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UITalkTextDat>();
            return false;
        }

        if (byOGGFile is null)
        {
            byOGGFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.OGGFile;

                if (!byOGGFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOGGFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOGGFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<UITalkTextDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.byOGGFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UITalkTextDat>> GetManyToManyByOGGFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UITalkTextDat>>();
        }

        var items = new List<ResultItem<string, UITalkTextDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOGGFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UITalkTextDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out UITalkTextDat? item)
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
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<UITalkTextDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UITalkTextDat>();
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
            items = Array.Empty<UITalkTextDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UITalkTextDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UITalkTextDat>>();
        }

        var items = new List<ResultItem<string, UITalkTextDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UITalkTextDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(bool? key, out UITalkTextDat? item)
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
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(bool? key, out IReadOnlyList<UITalkTextDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UITalkTextDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UITalkTextDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UITalkTextDat>> GetManyToManyByUnknown28(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UITalkTextDat>>();
        }

        var items = new List<ResultItem<bool, UITalkTextDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UITalkTextDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.NPCTextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCTextAudio(int? key, out UITalkTextDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCTextAudio(key, out var items))
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
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.NPCTextAudio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCTextAudio(int? key, out IReadOnlyList<UITalkTextDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UITalkTextDat>();
            return false;
        }

        if (byNPCTextAudio is null)
        {
            byNPCTextAudio = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCTextAudio;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCTextAudio.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCTextAudio.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCTextAudio.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UITalkTextDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UITalkTextDat"/> with <see cref="UITalkTextDat.byNPCTextAudio"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UITalkTextDat>> GetManyToManyByNPCTextAudio(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UITalkTextDat>>();
        }

        var items = new List<ResultItem<int, UITalkTextDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCTextAudio(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UITalkTextDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UITalkTextDat[] Load()
    {
        const string filePath = "Data/UITalkText.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UITalkTextDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading UITalkCategoriesKey
            (var uitalkcategorieskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading OGGFile
            (var oggfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading NPCTextAudio
            (var npctextaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UITalkTextDat()
            {
                Id = idLoading,
                UITalkCategoriesKey = uitalkcategorieskeyLoading,
                OGGFile = oggfileLoading,
                Text = textLoading,
                Unknown28 = unknown28Loading,
                NPCTextAudio = npctextaudioLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
