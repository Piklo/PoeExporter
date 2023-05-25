using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ShrineSoundsDat"/> related data and helper methods.
/// </summary>
public sealed class ShrineSoundsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ShrineSoundsDat> Items { get; }

    private Dictionary<string, List<ShrineSoundsDat>>? byId;
    private Dictionary<string, List<ShrineSoundsDat>>? byStereoSoundFile;
    private Dictionary<string, List<ShrineSoundsDat>>? byMonoSoundFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShrineSoundsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ShrineSoundsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ShrineSoundsDat"/> with <see cref="ShrineSoundsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ShrineSoundsDat? item)
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
    /// Tries to get <see cref="ShrineSoundsDat"/> with <see cref="ShrineSoundsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ShrineSoundsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrineSoundsDat>();
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
            items = Array.Empty<ShrineSoundsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineSoundsDat"/> with <see cref="ShrineSoundsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShrineSoundsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShrineSoundsDat>>();
        }

        var items = new List<ResultItem<string, ShrineSoundsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShrineSoundsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineSoundsDat"/> with <see cref="ShrineSoundsDat.StereoSoundFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStereoSoundFile(string? key, out ShrineSoundsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStereoSoundFile(key, out var items))
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
    /// Tries to get <see cref="ShrineSoundsDat"/> with <see cref="ShrineSoundsDat.StereoSoundFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStereoSoundFile(string? key, out IReadOnlyList<ShrineSoundsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrineSoundsDat>();
            return false;
        }

        if (byStereoSoundFile is null)
        {
            byStereoSoundFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.StereoSoundFile;

                if (!byStereoSoundFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStereoSoundFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStereoSoundFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShrineSoundsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineSoundsDat"/> with <see cref="ShrineSoundsDat.byStereoSoundFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShrineSoundsDat>> GetManyToManyByStereoSoundFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShrineSoundsDat>>();
        }

        var items = new List<ResultItem<string, ShrineSoundsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStereoSoundFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShrineSoundsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineSoundsDat"/> with <see cref="ShrineSoundsDat.MonoSoundFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonoSoundFile(string? key, out ShrineSoundsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonoSoundFile(key, out var items))
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
    /// Tries to get <see cref="ShrineSoundsDat"/> with <see cref="ShrineSoundsDat.MonoSoundFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonoSoundFile(string? key, out IReadOnlyList<ShrineSoundsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrineSoundsDat>();
            return false;
        }

        if (byMonoSoundFile is null)
        {
            byMonoSoundFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonoSoundFile;

                if (!byMonoSoundFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMonoSoundFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMonoSoundFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ShrineSoundsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrineSoundsDat"/> with <see cref="ShrineSoundsDat.byMonoSoundFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShrineSoundsDat>> GetManyToManyByMonoSoundFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShrineSoundsDat>>();
        }

        var items = new List<ResultItem<string, ShrineSoundsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonoSoundFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShrineSoundsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ShrineSoundsDat[] Load()
    {
        const string filePath = "Data/ShrineSounds.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShrineSoundsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StereoSoundFile
            (var stereosoundfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonoSoundFile
            (var monosoundfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShrineSoundsDat()
            {
                Id = idLoading,
                StereoSoundFile = stereosoundfileLoading,
                MonoSoundFile = monosoundfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
