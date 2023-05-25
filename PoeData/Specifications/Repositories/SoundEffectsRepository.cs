using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SoundEffectsDat"/> related data and helper methods.
/// </summary>
public sealed class SoundEffectsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SoundEffectsDat> Items { get; }

    private Dictionary<string, List<SoundEffectsDat>>? byId;
    private Dictionary<string, List<SoundEffectsDat>>? bySoundFile;
    private Dictionary<string, List<SoundEffectsDat>>? bySoundFile_2D;
    private Dictionary<bool, List<SoundEffectsDat>>? byUnknown24;
    private Dictionary<string, List<SoundEffectsDat>>? byUnknown25;

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundEffectsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SoundEffectsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out SoundEffectsDat? item)
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
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<SoundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SoundEffectsDat>();
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
            items = Array.Empty<SoundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SoundEffectsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SoundEffectsDat>>();
        }

        var items = new List<ResultItem<string, SoundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SoundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.SoundFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySoundFile(string? key, out SoundEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySoundFile(key, out var items))
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
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.SoundFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySoundFile(string? key, out IReadOnlyList<SoundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SoundEffectsDat>();
            return false;
        }

        if (bySoundFile is null)
        {
            bySoundFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.SoundFile;

                if (!bySoundFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySoundFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySoundFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SoundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.bySoundFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SoundEffectsDat>> GetManyToManyBySoundFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SoundEffectsDat>>();
        }

        var items = new List<ResultItem<string, SoundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySoundFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SoundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.SoundFile_2D"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySoundFile_2D(string? key, out SoundEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySoundFile_2D(key, out var items))
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
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.SoundFile_2D"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySoundFile_2D(string? key, out IReadOnlyList<SoundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SoundEffectsDat>();
            return false;
        }

        if (bySoundFile_2D is null)
        {
            bySoundFile_2D = new();
            foreach (var item in Items)
            {
                var itemKey = item.SoundFile_2D;

                if (!bySoundFile_2D.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySoundFile_2D.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySoundFile_2D.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SoundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.bySoundFile_2D"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SoundEffectsDat>> GetManyToManyBySoundFile_2D(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SoundEffectsDat>>();
        }

        var items = new List<ResultItem<string, SoundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySoundFile_2D(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SoundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(bool? key, out SoundEffectsDat? item)
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
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(bool? key, out IReadOnlyList<SoundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SoundEffectsDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;

                if (!byUnknown24.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SoundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SoundEffectsDat>> GetManyToManyByUnknown24(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SoundEffectsDat>>();
        }

        var items = new List<ResultItem<bool, SoundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SoundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown25(string? key, out SoundEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown25(key, out var items))
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
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown25(string? key, out IReadOnlyList<SoundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SoundEffectsDat>();
            return false;
        }

        if (byUnknown25 is null)
        {
            byUnknown25 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown25;

                if (!byUnknown25.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown25.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown25.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SoundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SoundEffectsDat"/> with <see cref="SoundEffectsDat.byUnknown25"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SoundEffectsDat>> GetManyToManyByUnknown25(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SoundEffectsDat>>();
        }

        var items = new List<ResultItem<string, SoundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown25(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SoundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SoundEffectsDat[] Load()
    {
        const string filePath = "Data/SoundEffects.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SoundEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SoundFile
            (var soundfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SoundFile_2D
            (var soundfile_2dLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SoundEffectsDat()
            {
                Id = idLoading,
                SoundFile = soundfileLoading,
                SoundFile_2D = soundfile_2dLoading,
                Unknown24 = unknown24Loading,
                Unknown25 = unknown25Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
