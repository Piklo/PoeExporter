using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LegionRewardTypeVisualsDat"/> related data and helper methods.
/// </summary>
public sealed class LegionRewardTypeVisualsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LegionRewardTypeVisualsDat> Items { get; }

    private Dictionary<int, List<LegionRewardTypeVisualsDat>>? byIntId;
    private Dictionary<int, List<LegionRewardTypeVisualsDat>>? byMinimapIconsKey;
    private Dictionary<string, List<LegionRewardTypeVisualsDat>>? byUnknown20;
    private Dictionary<int, List<LegionRewardTypeVisualsDat>>? byMiscAnimatedKey;
    private Dictionary<float, List<LegionRewardTypeVisualsDat>>? byUnknown44;
    private Dictionary<string, List<LegionRewardTypeVisualsDat>>? byId;

    /// <summary>
    /// Initializes a new instance of the <see cref="LegionRewardTypeVisualsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LegionRewardTypeVisualsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.IntId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIntId(int? key, out LegionRewardTypeVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIntId(key, out var items))
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
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.IntId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIntId(int? key, out IReadOnlyList<LegionRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
            return false;
        }

        if (byIntId is null)
        {
            byIntId = new();
            foreach (var item in Items)
            {
                var itemKey = item.IntId;

                if (!byIntId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIntId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIntId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.byIntId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionRewardTypeVisualsDat>> GetManyToManyByIntId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<int, LegionRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIntId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.MinimapIconsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinimapIconsKey(int? key, out LegionRewardTypeVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinimapIconsKey(key, out var items))
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
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.MinimapIconsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinimapIconsKey(int? key, out IReadOnlyList<LegionRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
            return false;
        }

        if (byMinimapIconsKey is null)
        {
            byMinimapIconsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinimapIconsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMinimapIconsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMinimapIconsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMinimapIconsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.byMinimapIconsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionRewardTypeVisualsDat>> GetManyToManyByMinimapIconsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<int, LegionRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinimapIconsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(string? key, out LegionRewardTypeVisualsDat? item)
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
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(string? key, out IReadOnlyList<LegionRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
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

        if (!byUnknown20.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LegionRewardTypeVisualsDat>> GetManyToManyByUnknown20(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LegionRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<string, LegionRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LegionRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.MiscAnimatedKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey(int? key, out LegionRewardTypeVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey(key, out var items))
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
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.MiscAnimatedKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey(int? key, out IReadOnlyList<LegionRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
            return false;
        }

        if (byMiscAnimatedKey is null)
        {
            byMiscAnimatedKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.byMiscAnimatedKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionRewardTypeVisualsDat>> GetManyToManyByMiscAnimatedKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<int, LegionRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(float? key, out LegionRewardTypeVisualsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown44(key, out var items))
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
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(float? key, out IReadOnlyList<LegionRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
            return false;
        }

        if (byUnknown44 is null)
        {
            byUnknown44 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown44;

                if (!byUnknown44.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown44.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown44.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, LegionRewardTypeVisualsDat>> GetManyToManyByUnknown44(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, LegionRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<float, LegionRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, LegionRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out LegionRewardTypeVisualsDat? item)
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
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<LegionRewardTypeVisualsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionRewardTypeVisualsDat>();
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
            items = Array.Empty<LegionRewardTypeVisualsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionRewardTypeVisualsDat"/> with <see cref="LegionRewardTypeVisualsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LegionRewardTypeVisualsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LegionRewardTypeVisualsDat>>();
        }

        var items = new List<ResultItem<string, LegionRewardTypeVisualsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LegionRewardTypeVisualsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LegionRewardTypeVisualsDat[] Load()
    {
        const string filePath = "Data/LegionRewardTypeVisuals.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LegionRewardTypeVisualsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinimapIconsKey
            (var minimapiconskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscAnimatedKey
            (var miscanimatedkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LegionRewardTypeVisualsDat()
            {
                IntId = intidLoading,
                MinimapIconsKey = minimapiconskeyLoading,
                Unknown20 = unknown20Loading,
                MiscAnimatedKey = miscanimatedkeyLoading,
                Unknown44 = unknown44Loading,
                Id = idLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
