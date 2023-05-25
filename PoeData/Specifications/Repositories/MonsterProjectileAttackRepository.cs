using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterProjectileAttackDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterProjectileAttackRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterProjectileAttackDat> Items { get; }

    private Dictionary<int, List<MonsterProjectileAttackDat>>? byId;
    private Dictionary<int, List<MonsterProjectileAttackDat>>? byProjectile;
    private Dictionary<bool, List<MonsterProjectileAttackDat>>? byUnknown20;
    private Dictionary<bool, List<MonsterProjectileAttackDat>>? byUnknown21;
    private Dictionary<bool, List<MonsterProjectileAttackDat>>? byUnknown22;
    private Dictionary<int, List<MonsterProjectileAttackDat>>? byUnknown23;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterProjectileAttackRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterProjectileAttackRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(int? key, out MonsterProjectileAttackDat? item)
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
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(int? key, out IReadOnlyList<MonsterProjectileAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
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

        if (!byId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterProjectileAttackDat>> GetManyToManyById(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterProjectileAttackDat>>();
        }

        var items = new List<ResultItem<int, MonsterProjectileAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterProjectileAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Projectile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByProjectile(int? key, out MonsterProjectileAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByProjectile(key, out var items))
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
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Projectile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByProjectile(int? key, out IReadOnlyList<MonsterProjectileAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
            return false;
        }

        if (byProjectile is null)
        {
            byProjectile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Projectile;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byProjectile.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byProjectile.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byProjectile.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.byProjectile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterProjectileAttackDat>> GetManyToManyByProjectile(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterProjectileAttackDat>>();
        }

        var items = new List<ResultItem<int, MonsterProjectileAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByProjectile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterProjectileAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(bool? key, out MonsterProjectileAttackDat? item)
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
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(bool? key, out IReadOnlyList<MonsterProjectileAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
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

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterProjectileAttackDat>> GetManyToManyByUnknown20(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterProjectileAttackDat>>();
        }

        var items = new List<ResultItem<bool, MonsterProjectileAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterProjectileAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Unknown21"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown21(bool? key, out MonsterProjectileAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown21(key, out var items))
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
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Unknown21"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown21(bool? key, out IReadOnlyList<MonsterProjectileAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
            return false;
        }

        if (byUnknown21 is null)
        {
            byUnknown21 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown21;

                if (!byUnknown21.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown21.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown21.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.byUnknown21"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterProjectileAttackDat>> GetManyToManyByUnknown21(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterProjectileAttackDat>>();
        }

        var items = new List<ResultItem<bool, MonsterProjectileAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown21(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterProjectileAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Unknown22"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown22(bool? key, out MonsterProjectileAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown22(key, out var items))
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
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Unknown22"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown22(bool? key, out IReadOnlyList<MonsterProjectileAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
            return false;
        }

        if (byUnknown22 is null)
        {
            byUnknown22 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown22;

                if (!byUnknown22.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown22.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown22.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.byUnknown22"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterProjectileAttackDat>> GetManyToManyByUnknown22(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterProjectileAttackDat>>();
        }

        var items = new List<ResultItem<bool, MonsterProjectileAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown22(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterProjectileAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Unknown23"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown23(int? key, out MonsterProjectileAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown23(key, out var items))
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
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.Unknown23"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown23(int? key, out IReadOnlyList<MonsterProjectileAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
            return false;
        }

        if (byUnknown23 is null)
        {
            byUnknown23 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown23;

                if (!byUnknown23.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown23.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown23.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterProjectileAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterProjectileAttackDat"/> with <see cref="MonsterProjectileAttackDat.byUnknown23"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterProjectileAttackDat>> GetManyToManyByUnknown23(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterProjectileAttackDat>>();
        }

        var items = new List<ResultItem<int, MonsterProjectileAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown23(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterProjectileAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterProjectileAttackDat[] Load()
    {
        const string filePath = "Data/MonsterProjectileAttack.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterProjectileAttackDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Projectile
            (var projectileLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown21
            (var unknown21Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown22
            (var unknown22Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown23
            (var unknown23Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterProjectileAttackDat()
            {
                Id = idLoading,
                Projectile = projectileLoading,
                Unknown20 = unknown20Loading,
                Unknown21 = unknown21Loading,
                Unknown22 = unknown22Loading,
                Unknown23 = unknown23Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
