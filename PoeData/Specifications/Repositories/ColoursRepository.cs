using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ColoursDat"/> related data and helper methods.
/// </summary>
public sealed class ColoursRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ColoursDat> Items { get; }

    private Dictionary<string, List<ColoursDat>>? byItem;
    private Dictionary<int, List<ColoursDat>>? byRed;
    private Dictionary<int, List<ColoursDat>>? byGreen;
    private Dictionary<int, List<ColoursDat>>? byBlue;
    private Dictionary<string, List<ColoursDat>>? byRgbCode;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColoursRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ColoursRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.Item"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItem(string? key, out ColoursDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItem(key, out var items))
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
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.Item"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItem(string? key, out IReadOnlyList<ColoursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ColoursDat>();
            return false;
        }

        if (byItem is null)
        {
            byItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.Item;

                if (!byItem.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byItem.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byItem.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ColoursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.byItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ColoursDat>> GetManyToManyByItem(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ColoursDat>>();
        }

        var items = new List<ResultItem<string, ColoursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ColoursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.Red"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRed(int? key, out ColoursDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRed(key, out var items))
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
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.Red"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRed(int? key, out IReadOnlyList<ColoursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ColoursDat>();
            return false;
        }

        if (byRed is null)
        {
            byRed = new();
            foreach (var item in Items)
            {
                var itemKey = item.Red;

                if (!byRed.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRed.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRed.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ColoursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.byRed"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ColoursDat>> GetManyToManyByRed(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ColoursDat>>();
        }

        var items = new List<ResultItem<int, ColoursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRed(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ColoursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.Green"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGreen(int? key, out ColoursDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGreen(key, out var items))
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
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.Green"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGreen(int? key, out IReadOnlyList<ColoursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ColoursDat>();
            return false;
        }

        if (byGreen is null)
        {
            byGreen = new();
            foreach (var item in Items)
            {
                var itemKey = item.Green;

                if (!byGreen.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGreen.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGreen.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ColoursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.byGreen"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ColoursDat>> GetManyToManyByGreen(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ColoursDat>>();
        }

        var items = new List<ResultItem<int, ColoursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGreen(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ColoursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.Blue"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBlue(int? key, out ColoursDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBlue(key, out var items))
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
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.Blue"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBlue(int? key, out IReadOnlyList<ColoursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ColoursDat>();
            return false;
        }

        if (byBlue is null)
        {
            byBlue = new();
            foreach (var item in Items)
            {
                var itemKey = item.Blue;

                if (!byBlue.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBlue.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBlue.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ColoursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.byBlue"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ColoursDat>> GetManyToManyByBlue(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ColoursDat>>();
        }

        var items = new List<ResultItem<int, ColoursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBlue(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ColoursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.RgbCode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRgbCode(string? key, out ColoursDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRgbCode(key, out var items))
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
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.RgbCode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRgbCode(string? key, out IReadOnlyList<ColoursDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ColoursDat>();
            return false;
        }

        if (byRgbCode is null)
        {
            byRgbCode = new();
            foreach (var item in Items)
            {
                var itemKey = item.RgbCode;

                if (!byRgbCode.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRgbCode.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRgbCode.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ColoursDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ColoursDat"/> with <see cref="ColoursDat.byRgbCode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ColoursDat>> GetManyToManyByRgbCode(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ColoursDat>>();
        }

        var items = new List<ResultItem<string, ColoursDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRgbCode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ColoursDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ColoursDat[] Load()
    {
        const string filePath = "Data/Colours.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ColoursDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Item
            (var itemLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Red
            (var redLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Green
            (var greenLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Blue
            (var blueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RgbCode
            (var rgbcodeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ColoursDat()
            {
                Item = itemLoading,
                Red = redLoading,
                Green = greenLoading,
                Blue = blueLoading,
                RgbCode = rgbcodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
