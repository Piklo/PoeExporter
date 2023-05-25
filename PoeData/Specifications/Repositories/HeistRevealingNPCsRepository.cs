using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistRevealingNPCsDat"/> related data and helper methods.
/// </summary>
public sealed class HeistRevealingNPCsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistRevealingNPCsDat> Items { get; }

    private Dictionary<int, List<HeistRevealingNPCsDat>>? byNPCsKey;
    private Dictionary<string, List<HeistRevealingNPCsDat>>? byPortraitFile;
    private Dictionary<int, List<HeistRevealingNPCsDat>>? byNPCAudioKey;
    private Dictionary<int, List<HeistRevealingNPCsDat>>? byUnknown40;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistRevealingNPCsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistRevealingNPCsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCsKey(int? key, out HeistRevealingNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCsKey(key, out var items))
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
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCsKey(int? key, out IReadOnlyList<HeistRevealingNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistRevealingNPCsDat>();
            return false;
        }

        if (byNPCsKey is null)
        {
            byNPCsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistRevealingNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.byNPCsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistRevealingNPCsDat>> GetManyToManyByNPCsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistRevealingNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistRevealingNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistRevealingNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.PortraitFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPortraitFile(string? key, out HeistRevealingNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPortraitFile(key, out var items))
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
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.PortraitFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPortraitFile(string? key, out IReadOnlyList<HeistRevealingNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistRevealingNPCsDat>();
            return false;
        }

        if (byPortraitFile is null)
        {
            byPortraitFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.PortraitFile;

                if (!byPortraitFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPortraitFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPortraitFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistRevealingNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.byPortraitFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistRevealingNPCsDat>> GetManyToManyByPortraitFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistRevealingNPCsDat>>();
        }

        var items = new List<ResultItem<string, HeistRevealingNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPortraitFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistRevealingNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.NPCAudioKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCAudioKey(int? key, out HeistRevealingNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCAudioKey(key, out var items))
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
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.NPCAudioKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCAudioKey(int? key, out IReadOnlyList<HeistRevealingNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistRevealingNPCsDat>();
            return false;
        }

        if (byNPCAudioKey is null)
        {
            byNPCAudioKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCAudioKey;
                foreach (var listKey in itemKey)
                {
                    if (!byNPCAudioKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNPCAudioKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNPCAudioKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistRevealingNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.byNPCAudioKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistRevealingNPCsDat>> GetManyToManyByNPCAudioKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistRevealingNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistRevealingNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCAudioKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistRevealingNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out HeistRevealingNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<HeistRevealingNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistRevealingNPCsDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;

                if (!byUnknown40.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistRevealingNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistRevealingNPCsDat"/> with <see cref="HeistRevealingNPCsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistRevealingNPCsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistRevealingNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistRevealingNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistRevealingNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistRevealingNPCsDat[] Load()
    {
        const string filePath = "Data/HeistRevealingNPCs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistRevealingNPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading NPCsKey
            (var npcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PortraitFile
            (var portraitfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NPCAudioKey
            (var tempnpcaudiokeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npcaudiokeyLoading = tempnpcaudiokeyLoading.AsReadOnly();

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistRevealingNPCsDat()
            {
                NPCsKey = npcskeyLoading,
                PortraitFile = portraitfileLoading,
                NPCAudioKey = npcaudiokeyLoading,
                Unknown40 = unknown40Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
