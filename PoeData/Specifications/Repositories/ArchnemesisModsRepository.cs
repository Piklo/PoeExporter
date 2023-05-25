using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ArchnemesisModsDat"/> related data and helper methods.
/// </summary>
public sealed class ArchnemesisModsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ArchnemesisModsDat> Items { get; }

    private Dictionary<int, List<ArchnemesisModsDat>>? byMod;
    private Dictionary<string, List<ArchnemesisModsDat>>? byName;
    private Dictionary<int, List<ArchnemesisModsDat>>? byVisual;
    private Dictionary<string, List<ArchnemesisModsDat>>? byTextStyles;
    private Dictionary<bool, List<ArchnemesisModsDat>>? byUnknown56;
    private Dictionary<bool, List<ArchnemesisModsDat>>? byUnknown57;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchnemesisModsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ArchnemesisModsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.Mod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMod(int? key, out ArchnemesisModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMod(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.Mod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMod(int? key, out IReadOnlyList<ArchnemesisModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        if (byMod is null)
        {
            byMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.byMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArchnemesisModsDat>> GetManyToManyByMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArchnemesisModsDat>>();
        }

        var items = new List<ResultItem<int, ArchnemesisModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArchnemesisModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out ArchnemesisModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByName(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<ArchnemesisModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;

                if (!byName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchnemesisModsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchnemesisModsDat>>();
        }

        var items = new List<ResultItem<string, ArchnemesisModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchnemesisModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.Visual"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVisual(int? key, out ArchnemesisModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVisual(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.Visual"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVisual(int? key, out IReadOnlyList<ArchnemesisModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        if (byVisual is null)
        {
            byVisual = new();
            foreach (var item in Items)
            {
                var itemKey = item.Visual;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byVisual.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byVisual.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byVisual.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.byVisual"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArchnemesisModsDat>> GetManyToManyByVisual(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArchnemesisModsDat>>();
        }

        var items = new List<ResultItem<int, ArchnemesisModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVisual(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArchnemesisModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.TextStyles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTextStyles(string? key, out ArchnemesisModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTextStyles(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.TextStyles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTextStyles(string? key, out IReadOnlyList<ArchnemesisModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        if (byTextStyles is null)
        {
            byTextStyles = new();
            foreach (var item in Items)
            {
                var itemKey = item.TextStyles;
                foreach (var listKey in itemKey)
                {
                    if (!byTextStyles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTextStyles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTextStyles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.byTextStyles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ArchnemesisModsDat>> GetManyToManyByTextStyles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ArchnemesisModsDat>>();
        }

        var items = new List<ResultItem<string, ArchnemesisModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTextStyles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ArchnemesisModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(bool? key, out ArchnemesisModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(bool? key, out IReadOnlyList<ArchnemesisModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;

                if (!byUnknown56.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ArchnemesisModsDat>> GetManyToManyByUnknown56(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ArchnemesisModsDat>>();
        }

        var items = new List<ResultItem<bool, ArchnemesisModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ArchnemesisModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown57(bool? key, out ArchnemesisModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown57(key, out var items))
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
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown57(bool? key, out IReadOnlyList<ArchnemesisModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        if (byUnknown57 is null)
        {
            byUnknown57 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown57;

                if (!byUnknown57.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown57.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown57.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArchnemesisModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArchnemesisModsDat"/> with <see cref="ArchnemesisModsDat.byUnknown57"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ArchnemesisModsDat>> GetManyToManyByUnknown57(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ArchnemesisModsDat>>();
        }

        var items = new List<ResultItem<bool, ArchnemesisModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown57(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ArchnemesisModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ArchnemesisModsDat[] Load()
    {
        const string filePath = "Data/ArchnemesisMods.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArchnemesisModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Mod
            (var modLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Visual
            (var visualLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TextStyles
            (var temptextstylesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var textstylesLoading = temptextstylesLoading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown57
            (var unknown57Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArchnemesisModsDat()
            {
                Mod = modLoading,
                Name = nameLoading,
                Visual = visualLoading,
                TextStyles = textstylesLoading,
                Unknown56 = unknown56Loading,
                Unknown57 = unknown57Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
