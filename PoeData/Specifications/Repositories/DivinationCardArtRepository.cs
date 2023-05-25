using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DivinationCardArtDat"/> related data and helper methods.
/// </summary>
public sealed class DivinationCardArtRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DivinationCardArtDat> Items { get; }

    private Dictionary<int, List<DivinationCardArtDat>>? byBaseItemTypesKey;
    private Dictionary<string, List<DivinationCardArtDat>>? byVirtualFile;
    private Dictionary<int, List<DivinationCardArtDat>>? byUnknown24;

    /// <summary>
    /// Initializes a new instance of the <see cref="DivinationCardArtRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DivinationCardArtRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DivinationCardArtDat"/> with <see cref="DivinationCardArtDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out DivinationCardArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="DivinationCardArtDat"/> with <see cref="DivinationCardArtDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<DivinationCardArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DivinationCardArtDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DivinationCardArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DivinationCardArtDat"/> with <see cref="DivinationCardArtDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DivinationCardArtDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DivinationCardArtDat>>();
        }

        var items = new List<ResultItem<int, DivinationCardArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DivinationCardArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DivinationCardArtDat"/> with <see cref="DivinationCardArtDat.VirtualFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVirtualFile(string? key, out DivinationCardArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVirtualFile(key, out var items))
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
    /// Tries to get <see cref="DivinationCardArtDat"/> with <see cref="DivinationCardArtDat.VirtualFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVirtualFile(string? key, out IReadOnlyList<DivinationCardArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DivinationCardArtDat>();
            return false;
        }

        if (byVirtualFile is null)
        {
            byVirtualFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.VirtualFile;

                if (!byVirtualFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byVirtualFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byVirtualFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<DivinationCardArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DivinationCardArtDat"/> with <see cref="DivinationCardArtDat.byVirtualFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DivinationCardArtDat>> GetManyToManyByVirtualFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DivinationCardArtDat>>();
        }

        var items = new List<ResultItem<string, DivinationCardArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVirtualFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DivinationCardArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DivinationCardArtDat"/> with <see cref="DivinationCardArtDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out DivinationCardArtDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown24(key, out var items))
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
    /// Tries to get <see cref="DivinationCardArtDat"/> with <see cref="DivinationCardArtDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<DivinationCardArtDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DivinationCardArtDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown24.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown24.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DivinationCardArtDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DivinationCardArtDat"/> with <see cref="DivinationCardArtDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DivinationCardArtDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DivinationCardArtDat>>();
        }

        var items = new List<ResultItem<int, DivinationCardArtDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DivinationCardArtDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DivinationCardArtDat[] Load()
    {
        const string filePath = "Data/DivinationCardArt.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DivinationCardArtDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading VirtualFile
            (var virtualfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DivinationCardArtDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                VirtualFile = virtualfileLoading,
                Unknown24 = unknown24Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
