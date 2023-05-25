using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ChatIconsDat"/> related data and helper methods.
/// </summary>
public sealed class ChatIconsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ChatIconsDat> Items { get; }

    private Dictionary<string, List<ChatIconsDat>>? byIcon;
    private Dictionary<string, List<ChatIconsDat>>? byImage;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatIconsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ChatIconsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ChatIconsDat"/> with <see cref="ChatIconsDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon(string? key, out ChatIconsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIcon(key, out var items))
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
    /// Tries to get <see cref="ChatIconsDat"/> with <see cref="ChatIconsDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon(string? key, out IReadOnlyList<ChatIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChatIconsDat>();
            return false;
        }

        if (byIcon is null)
        {
            byIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.Icon;

                if (!byIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChatIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChatIconsDat"/> with <see cref="ChatIconsDat.byIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChatIconsDat>> GetManyToManyByIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChatIconsDat>>();
        }

        var items = new List<ResultItem<string, ChatIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChatIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ChatIconsDat"/> with <see cref="ChatIconsDat.Image"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImage(string? key, out ChatIconsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByImage(key, out var items))
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
    /// Tries to get <see cref="ChatIconsDat"/> with <see cref="ChatIconsDat.Image"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImage(string? key, out IReadOnlyList<ChatIconsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ChatIconsDat>();
            return false;
        }

        if (byImage is null)
        {
            byImage = new();
            foreach (var item in Items)
            {
                var itemKey = item.Image;

                if (!byImage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byImage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byImage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ChatIconsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ChatIconsDat"/> with <see cref="ChatIconsDat.byImage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ChatIconsDat>> GetManyToManyByImage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ChatIconsDat>>();
        }

        var items = new List<ResultItem<string, ChatIconsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ChatIconsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ChatIconsDat[] Load()
    {
        const string filePath = "Data/ChatIcons.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ChatIconsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Image
            (var imageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ChatIconsDat()
            {
                Icon = iconLoading,
                Image = imageLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
