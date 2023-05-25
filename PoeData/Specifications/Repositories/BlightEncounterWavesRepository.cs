using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BlightEncounterWavesDat"/> related data and helper methods.
/// </summary>
public sealed class BlightEncounterWavesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BlightEncounterWavesDat> Items { get; }

    private Dictionary<string, List<BlightEncounterWavesDat>>? byMonsterSpawnerId;
    private Dictionary<int, List<BlightEncounterWavesDat>>? byEncounterType;
    private Dictionary<int, List<BlightEncounterWavesDat>>? byUnknown24;
    private Dictionary<int, List<BlightEncounterWavesDat>>? byUnknown28;
    private Dictionary<int, List<BlightEncounterWavesDat>>? byUnknown32;
    private Dictionary<int, List<BlightEncounterWavesDat>>? byWave;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightEncounterWavesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BlightEncounterWavesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.MonsterSpawnerId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterSpawnerId(string? key, out BlightEncounterWavesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterSpawnerId(key, out var items))
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
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.MonsterSpawnerId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterSpawnerId(string? key, out IReadOnlyList<BlightEncounterWavesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightEncounterWavesDat>();
            return false;
        }

        if (byMonsterSpawnerId is null)
        {
            byMonsterSpawnerId = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterSpawnerId;

                if (!byMonsterSpawnerId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMonsterSpawnerId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterSpawnerId.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BlightEncounterWavesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.byMonsterSpawnerId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BlightEncounterWavesDat>> GetManyToManyByMonsterSpawnerId(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BlightEncounterWavesDat>>();
        }

        var items = new List<ResultItem<string, BlightEncounterWavesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterSpawnerId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BlightEncounterWavesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.EncounterType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEncounterType(int? key, out BlightEncounterWavesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEncounterType(key, out var items))
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
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.EncounterType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEncounterType(int? key, out IReadOnlyList<BlightEncounterWavesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightEncounterWavesDat>();
            return false;
        }

        if (byEncounterType is null)
        {
            byEncounterType = new();
            foreach (var item in Items)
            {
                var itemKey = item.EncounterType;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEncounterType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEncounterType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEncounterType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightEncounterWavesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.byEncounterType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightEncounterWavesDat>> GetManyToManyByEncounterType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightEncounterWavesDat>>();
        }

        var items = new List<ResultItem<int, BlightEncounterWavesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEncounterType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightEncounterWavesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out BlightEncounterWavesDat? item)
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
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<BlightEncounterWavesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightEncounterWavesDat>();
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
            items = Array.Empty<BlightEncounterWavesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightEncounterWavesDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightEncounterWavesDat>>();
        }

        var items = new List<ResultItem<int, BlightEncounterWavesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightEncounterWavesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out BlightEncounterWavesDat? item)
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
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<BlightEncounterWavesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightEncounterWavesDat>();
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
            items = Array.Empty<BlightEncounterWavesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightEncounterWavesDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightEncounterWavesDat>>();
        }

        var items = new List<ResultItem<int, BlightEncounterWavesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightEncounterWavesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out BlightEncounterWavesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<BlightEncounterWavesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightEncounterWavesDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightEncounterWavesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightEncounterWavesDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightEncounterWavesDat>>();
        }

        var items = new List<ResultItem<int, BlightEncounterWavesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightEncounterWavesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.Wave"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWave(int? key, out BlightEncounterWavesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWave(key, out var items))
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
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.Wave"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWave(int? key, out IReadOnlyList<BlightEncounterWavesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BlightEncounterWavesDat>();
            return false;
        }

        if (byWave is null)
        {
            byWave = new();
            foreach (var item in Items)
            {
                var itemKey = item.Wave;

                if (!byWave.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWave.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWave.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BlightEncounterWavesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BlightEncounterWavesDat"/> with <see cref="BlightEncounterWavesDat.byWave"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BlightEncounterWavesDat>> GetManyToManyByWave(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BlightEncounterWavesDat>>();
        }

        var items = new List<ResultItem<int, BlightEncounterWavesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWave(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BlightEncounterWavesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BlightEncounterWavesDat[] Load()
    {
        const string filePath = "Data/BlightEncounterWaves.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightEncounterWavesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterSpawnerId
            (var monsterspawneridLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EncounterType
            (var encountertypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Wave
            (var waveLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightEncounterWavesDat()
            {
                MonsterSpawnerId = monsterspawneridLoading,
                EncounterType = encountertypeLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                Wave = waveLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
