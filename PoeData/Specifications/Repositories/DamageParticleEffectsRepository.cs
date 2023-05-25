using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DamageParticleEffectsDat"/> related data and helper methods.
/// </summary>
public sealed class DamageParticleEffectsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DamageParticleEffectsDat> Items { get; }

    private Dictionary<int, List<DamageParticleEffectsDat>>? byDamageParticleEffectTypes;
    private Dictionary<int, List<DamageParticleEffectsDat>>? byVariation;
    private Dictionary<string, List<DamageParticleEffectsDat>>? byPETFile;
    private Dictionary<int, List<DamageParticleEffectsDat>>? byImpactSoundData1;
    private Dictionary<int, List<DamageParticleEffectsDat>>? byImpactSoundData2;
    private Dictionary<int, List<DamageParticleEffectsDat>>? byUnknown48;

    /// <summary>
    /// Initializes a new instance of the <see cref="DamageParticleEffectsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DamageParticleEffectsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.DamageParticleEffectTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDamageParticleEffectTypes(int? key, out DamageParticleEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDamageParticleEffectTypes(key, out var items))
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
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.DamageParticleEffectTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDamageParticleEffectTypes(int? key, out IReadOnlyList<DamageParticleEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        if (byDamageParticleEffectTypes is null)
        {
            byDamageParticleEffectTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.DamageParticleEffectTypes;

                if (!byDamageParticleEffectTypes.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDamageParticleEffectTypes.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDamageParticleEffectTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.byDamageParticleEffectTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DamageParticleEffectsDat>> GetManyToManyByDamageParticleEffectTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DamageParticleEffectsDat>>();
        }

        var items = new List<ResultItem<int, DamageParticleEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDamageParticleEffectTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DamageParticleEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.Variation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVariation(int? key, out DamageParticleEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVariation(key, out var items))
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
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.Variation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVariation(int? key, out IReadOnlyList<DamageParticleEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        if (byVariation is null)
        {
            byVariation = new();
            foreach (var item in Items)
            {
                var itemKey = item.Variation;

                if (!byVariation.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byVariation.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byVariation.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.byVariation"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DamageParticleEffectsDat>> GetManyToManyByVariation(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DamageParticleEffectsDat>>();
        }

        var items = new List<ResultItem<int, DamageParticleEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVariation(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DamageParticleEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.PETFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPETFile(string? key, out DamageParticleEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPETFile(key, out var items))
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
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.PETFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPETFile(string? key, out IReadOnlyList<DamageParticleEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        if (byPETFile is null)
        {
            byPETFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.PETFile;

                if (!byPETFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPETFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPETFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.byPETFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DamageParticleEffectsDat>> GetManyToManyByPETFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DamageParticleEffectsDat>>();
        }

        var items = new List<ResultItem<string, DamageParticleEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPETFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DamageParticleEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.ImpactSoundData1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImpactSoundData1(int? key, out DamageParticleEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByImpactSoundData1(key, out var items))
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
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.ImpactSoundData1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImpactSoundData1(int? key, out IReadOnlyList<DamageParticleEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        if (byImpactSoundData1 is null)
        {
            byImpactSoundData1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ImpactSoundData1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byImpactSoundData1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byImpactSoundData1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byImpactSoundData1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.byImpactSoundData1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DamageParticleEffectsDat>> GetManyToManyByImpactSoundData1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DamageParticleEffectsDat>>();
        }

        var items = new List<ResultItem<int, DamageParticleEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImpactSoundData1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DamageParticleEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.ImpactSoundData2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImpactSoundData2(int? key, out DamageParticleEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByImpactSoundData2(key, out var items))
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
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.ImpactSoundData2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImpactSoundData2(int? key, out IReadOnlyList<DamageParticleEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        if (byImpactSoundData2 is null)
        {
            byImpactSoundData2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ImpactSoundData2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byImpactSoundData2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byImpactSoundData2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byImpactSoundData2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.byImpactSoundData2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DamageParticleEffectsDat>> GetManyToManyByImpactSoundData2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DamageParticleEffectsDat>>();
        }

        var items = new List<ResultItem<int, DamageParticleEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImpactSoundData2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DamageParticleEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out DamageParticleEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown48(key, out var items))
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
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<DamageParticleEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;

                if (!byUnknown48.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DamageParticleEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DamageParticleEffectsDat"/> with <see cref="DamageParticleEffectsDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DamageParticleEffectsDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DamageParticleEffectsDat>>();
        }

        var items = new List<ResultItem<int, DamageParticleEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DamageParticleEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DamageParticleEffectsDat[] Load()
    {
        const string filePath = "Data/DamageParticleEffects.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DamageParticleEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading DamageParticleEffectTypes
            (var damageparticleeffecttypesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Variation
            (var variationLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PETFile
            (var petfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ImpactSoundData1
            (var impactsounddata1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ImpactSoundData2
            (var impactsounddata2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DamageParticleEffectsDat()
            {
                DamageParticleEffectTypes = damageparticleeffecttypesLoading,
                Variation = variationLoading,
                PETFile = petfileLoading,
                ImpactSoundData1 = impactsounddata1Loading,
                ImpactSoundData2 = impactsounddata2Loading,
                Unknown48 = unknown48Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
