using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MouseCursorSizeSettingsDat"/> related data and helper methods.
/// </summary>
public sealed class MouseCursorSizeSettingsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MouseCursorSizeSettingsDat> Items { get; }

    private Dictionary<string, List<MouseCursorSizeSettingsDat>>? bySize;
    private Dictionary<string, List<MouseCursorSizeSettingsDat>>? byDescription;
    private Dictionary<float, List<MouseCursorSizeSettingsDat>>? byRatio;

    /// <summary>
    /// Initializes a new instance of the <see cref="MouseCursorSizeSettingsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MouseCursorSizeSettingsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MouseCursorSizeSettingsDat"/> with <see cref="MouseCursorSizeSettingsDat.Size"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySize(string? key, out MouseCursorSizeSettingsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySize(key, out var items))
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
    /// Tries to get <see cref="MouseCursorSizeSettingsDat"/> with <see cref="MouseCursorSizeSettingsDat.Size"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySize(string? key, out IReadOnlyList<MouseCursorSizeSettingsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MouseCursorSizeSettingsDat>();
            return false;
        }

        if (bySize is null)
        {
            bySize = new();
            foreach (var item in Items)
            {
                var itemKey = item.Size;

                if (!bySize.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySize.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySize.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MouseCursorSizeSettingsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MouseCursorSizeSettingsDat"/> with <see cref="MouseCursorSizeSettingsDat.bySize"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MouseCursorSizeSettingsDat>> GetManyToManyBySize(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MouseCursorSizeSettingsDat>>();
        }

        var items = new List<ResultItem<string, MouseCursorSizeSettingsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySize(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MouseCursorSizeSettingsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MouseCursorSizeSettingsDat"/> with <see cref="MouseCursorSizeSettingsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out MouseCursorSizeSettingsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="MouseCursorSizeSettingsDat"/> with <see cref="MouseCursorSizeSettingsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<MouseCursorSizeSettingsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MouseCursorSizeSettingsDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MouseCursorSizeSettingsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MouseCursorSizeSettingsDat"/> with <see cref="MouseCursorSizeSettingsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MouseCursorSizeSettingsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MouseCursorSizeSettingsDat>>();
        }

        var items = new List<ResultItem<string, MouseCursorSizeSettingsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MouseCursorSizeSettingsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MouseCursorSizeSettingsDat"/> with <see cref="MouseCursorSizeSettingsDat.Ratio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRatio(float? key, out MouseCursorSizeSettingsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRatio(key, out var items))
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
    /// Tries to get <see cref="MouseCursorSizeSettingsDat"/> with <see cref="MouseCursorSizeSettingsDat.Ratio"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRatio(float? key, out IReadOnlyList<MouseCursorSizeSettingsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MouseCursorSizeSettingsDat>();
            return false;
        }

        if (byRatio is null)
        {
            byRatio = new();
            foreach (var item in Items)
            {
                var itemKey = item.Ratio;

                if (!byRatio.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRatio.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRatio.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MouseCursorSizeSettingsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MouseCursorSizeSettingsDat"/> with <see cref="MouseCursorSizeSettingsDat.byRatio"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, MouseCursorSizeSettingsDat>> GetManyToManyByRatio(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, MouseCursorSizeSettingsDat>>();
        }

        var items = new List<ResultItem<float, MouseCursorSizeSettingsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRatio(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, MouseCursorSizeSettingsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MouseCursorSizeSettingsDat[] Load()
    {
        const string filePath = "Data/MouseCursorSizeSettings.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MouseCursorSizeSettingsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Size
            (var sizeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Ratio
            (var ratioLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MouseCursorSizeSettingsDat()
            {
                Size = sizeLoading,
                Description = descriptionLoading,
                Ratio = ratioLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
